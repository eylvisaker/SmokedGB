//    This file is part of SmokedGB.
//
//    SmokedGB is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    SmokedGB is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with SmokedGB.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AgateLib.AudioLib;

namespace SmokedGB
{
	public class GameboyAudio : IGameboyAudio
	{
		Gameboy theGameboy;
		GameboyCpu cpu;

		GameboyAudioChannel[] gac;
		int[] highMemoryChanged;
		bool mAudioRunning = false;
		bool mEmulating = false;
		int bytesWritten = 0;
		short[] audioBuffer = new short[0];

		AudioStream bufferStream;
		StreamingSoundBuffer AgateSoundBuffer { get; set; }

		class AudioStream : Stream 
		{
			GameboyAudio aud;

			public AudioStream(GameboyAudio aud)
			{
				this.aud = aud;
			}

			public override bool CanRead
			{
				get { return true; }
			}
			public override bool CanSeek
			{
				get { return false; }
			}
			public override bool CanWrite
			{
				get { return false; }
			}

			public override void Flush()
			{
			}

			public override long Length
			{
				get { return 100000; }
			}
			public override long Position
			{
				get
				{
					return 0;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				aud.EmulateAudio(buffer, offset, count);

				return count;
			}
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotImplementedException();
			}
			public override void SetLength(long value)
			{
				throw new NotImplementedException();
			}
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotImplementedException();
			}
		}

		public GameboyAudio()
		{
			bufferStream = new AudioStream(this);

			AgateSoundBuffer = new StreamingSoundBuffer(bufferStream, 
				SoundFormat.Pcm16(SamplingFrequency, 2), 500);

			AgateSoundBuffer.Play();
		}

		public void Initialize(Gameboy theGameboy)
		{
			this.theGameboy = theGameboy;
			this.cpu = theGameboy.Cpu;

			CloseStreams();

			f = File.Open("buffer.pcm", FileMode.Create);
			w = new BinaryWriter(f);

			gac = new GameboyAudioChannel[4];
			highMemoryChanged = new int[0x100];

			for (int i = 0; i < 4; i++)
			{
				gac[i] = new GameboyAudioChannel();
				gac[i].outputFile = new BinaryWriter(
					File.Open(string.Format("gac{0}.pcm", i), FileMode.Create));
			}

			for (int i = 0; i < 4; i++)
			{
				gac[i].chanIndex = i;
			}

		}

		private void CloseStreams()
		{
			if (w == null)
				return;
			w.BaseStream.Dispose();


			for (int i = 0; i < 4; i++)
			{
				gac[i].outputFile.BaseStream.Dispose();
			}
		}


		public void Dispose()
		{
			AgateSoundBuffer.Dispose();
		}

		int SamplesPerSec
		{
			get { return 44010; }
		}
		
		double TimePerSample { get { return 1.0 / SamplingFrequency; } }

		public void PassTime(double us)
		{
			return;
		}

		void EmulateAudio(byte[] buffer, int offset, int count)
		{
			if (mAudioRunning == false)
				return;

			try
			{
				mEmulating = true;

				if (audioBuffer.Length < count / 2)
				{
					audioBuffer = new short[count / 2];
				}

				double time_s = count * TimePerSample;
				Array.Clear(audioBuffer, 0, audioBuffer.Length);

				LoadSoundData();

				for (int i = 0; i < 4; i++)
				{
					try
					{
						EmulateAudioChannel(i, time_s * 1000, audioBuffer, count / 2);
					}
					catch
					{ }
				}

				Buffer.BlockCopy(audioBuffer, 0, buffer, offset, count);

				bytesWritten += count;

				SoundDataToGbMemory();
			}
			finally
			{
				mEmulating = false;
			}
		}
		void EmulateAudioChannel(int index, double time_ms, short[] buffer, int count)
		{
			double time = 0;
			GameboyAudioChannel chan = gac[index];

			const int baseGain = 75;

			if (chan.sweepTime > 0 || chan.envelopeStep > 0)
			{
				chan.IsChanged = true;
			}

			if (chan.IsChanged || chan.length() > 0)
			{
				chan.samplesPerPeriod = SamplingFrequency / chan.frequency;
				chan.modSamplesPerPeriod = SamplingFrequency % chan.frequency;

				if (chan.samplesPerPeriod == 0 || chan.length() < 0)
				{
					chan.frequency = SamplingFrequency;
					chan.samplesPerPeriod = 1;
					chan.Amplitude = 0;
				}

				chan.IsPlaying = chan.Amplitude != 0;

				int si = 0;
				while (si < count) 
				{
					if (index == 0)
					{
						// do frequency shift for first voice
						chan.TestShift(time);
					}
					else if (index == 3)
					{
						// check frequency ratio and stuff
						chan.NoiseScaler();
					}

					chan.TestEnvelope(time);

					chan.samplesPerPeriod = (double)(SamplingFrequency / (double)chan.frequency);

					chan.percentExtraSamples = SamplingFrequency /
						(double)chan.frequency - chan.samplesPerPeriod;

					chan.modSamplesPerPeriod = (int)(chan.percentExtraSamples *
						chan.samplesPerPeriod);

					if (chan.samplesPerPeriod == 0)
					{
						chan.samplesPerPeriod = 1;
						chan.Amplitude = 0;
					}

					int wd = chan.waveDuty;

					int lengthFrequencyPositive = (int)(wd * (float)(chan.samplesPerPeriod)
						/ 1000.0f);

					if (lengthFrequencyPositive == 0)
						lengthFrequencyPositive = 1;

					int sampleCount = (int)chan.samplesPerPeriod;

					time += TimePerSample * sampleCount;
						
					for (; chan.SampleIndex < sampleCount; chan.SampleIndex++)
					{
						chan.timeLasted += TimePerSample;
						chan.timeCounter += TimePerSample;

						// write buffer
						if ((theGameboy.Cpu.Memory[0xff26] & 128) != 0)
						{
							// square wave pattern:
							int sign =
								(chan.SampleIndex < lengthFrequencyPositive)
								? 1 : -1;

							short value = (short)(sign * chan.ActualAmplitude(baseGain));

							if (chan.leftChannel)
							{
								buffer[si] += value;
								chan.outputFile.Write(value);
							}
							else
								chan.outputFile.Write(0);

							if (chan.rightChannel)
							{
								buffer[si+1] += value;
								chan.outputFile.Write(value);
							}
							else
								chan.outputFile.Write(0);
						}
						else
						{
							chan.outputFile.Write((short)0);
							chan.outputFile.Write((short)0);
						}

						si += 2;

						if (si >= count)
							break;
					}

					if (chan.SampleIndex >= sampleCount)
						chan.SampleIndex = 0;

					if (sampleCount == 0)
						break;
				}
			}
			else if (chan.length() <= 0)
			{
				chan.IsPlaying = false;

				for (int i = 0; i < count; i++)
				{
					chan.outputFile.Write((short)0);
				}

			}
		}


		Stream f;
		BinaryWriter w;

		public void SoundWrite(int index)
		{
			highMemoryChanged[index - 0xff00] = 1;
		}

		bool IsChanged(int address)
		{
			return highMemoryChanged[address - 0xff00] != 0;
		}

		readonly int[] waveDutyTable = new int[] { 125, 250, 500, 750 };

		void LoadChannelData(int index, int baseAddr, bool resetInitial)
		{
			var mem = theGameboy.Cpu.Memory;
			GameboyAudioChannel chan = gac[index];

			gac[index].sampleAmplitude = 0x0f;

			if ((mem[baseAddr + 4] & 0x80) != 0)	// restart sound
			{
				SoundWrite(baseAddr);
				SoundWrite(baseAddr + 1);
				SoundWrite(baseAddr + 2);
				SoundWrite(baseAddr + 3);
				SoundWrite(baseAddr + 4);

				chan.totalShifts = 0;
				chan.totalEnvelopes = 0;
				chan.shiftClock = 0;
				chan.startedEnvelope = 0;
				chan.startedShift = 0;
				chan.shiftRegister = 0xff;

				chan.timeLasted = 0;

				chan.sampIndexOffset = 0;
				chan.bReseting = true;

				mem[baseAddr + 4] &= 0x7f;

			}
			else
			{
				chan.bReseting = false;
			}

			chan.repeat = (mem[baseAddr + 4] & 64) != 0;		// 0 means repeat

			if (IsChanged(baseAddr + 4) || IsChanged(baseAddr + 3))
			{
				chan.frequency = (mem[baseAddr + 4] & 0x07) << 8;
				chan.frequency |= mem[baseAddr + 3];
				chan.frequency = 131072 / (2048 - chan.frequency);
			}

			if (IsChanged(baseAddr) && index == 0)
			{
				// Channel 1 - $FF10(NR10)[sq1]
				// --------------------------------------------
				// 7	Unused
				// 6-4	Sweep time(update rate) (if 0, sweeping is off)
				// 3	Sweep Direction (1: decrease, 0: increase)
				// 2-0	Sweep RtShift amount (if 0, sweeping is off)

				chan.sweepTime = ((mem[baseAddr] & 0x70) >> 4);
				chan.sweepSign = ((mem[baseAddr] & 0x08) != 0) ? -1 : 1;
				chan.sweepShifts = mem[baseAddr] & 0x07;

				chan.startedShift = chan.timeLasted;
				chan.IsChanged = true;
			}

			if (IsChanged(baseAddr + 2))
			{
				// Channels 1,2,4 - $FF12(NR12)[sq1], $FF17(NR22)[sq2], $FF21(NR42)[noise]
				// ---------------------------------------------
				// 7-4	(Initial) Channel Volume
				// 3	Volume sweep direction (0: down; 1: up)
				// 2-0	Length of each step in sweep (if 0, sweeping is off)
				// NOTE: each step is n/64 seconds long, where n is 1-7

				// Channel 3 - $FF1C(NR32)[wave]
				// ---------------------------------------------
				// 7	Unused
				// 6-5	Volume Level (0: Mute; 1: 100%; 2: 50% [rtshift 1]; 3: 25% [rtshift 2])
				// 4-0	Unuse

				chan.Amplitude = (mem[baseAddr + 2] & 0xf0) >> 4;
				chan.envelopeSign = ((mem[baseAddr + 2] & 8)!= 0) ? 1 : -1;
				chan.envelopeStep = mem[baseAddr + 2] & 0x07;

				// the timing here needs some fixing probably.
				chan.startedEnvelope = chan.timeLasted;
				chan.IsChanged = true;
			}

			chan.waveDuty = waveDutyTable[(mem[baseAddr + 1] & 0xc0) >> 6];

			chan.totalLength = (64.0 - (mem[baseAddr + 1] & 0x3f)) / 256.0;

			if (index == 2)
			{
				chan.sweepTime = 0;
				chan.envelopeStep = 0;
				chan.waveDuty = 500;
				chan.Amplitude = 15;

				if (IsChanged(0xff30))
				{
					mem.CopyTo(chan.voluntaryData, 0xff30, 16);
					mem.CopyTo(chan.voluntaryData, 0xff30, 16);

					chan.IsChanged = true;
				}

				chan.sndEnable = (mem[baseAddr] & 0x80) != 0;
				chan.totalLength = (256 - mem[baseAddr + 1]) / 256.0;
				chan.outputLevel = (mem[baseAddr + 2] & 96) >> 5;
			}
			else if (index == 3)
			{
				if (IsChanged(baseAddr + 3))
				{
					// Channel 4 - $FF22(NR43)[noise]
					// ---------------------------------------------
					// 7-4	Shift clock frequency (s)
					// 3	Shift Register width (0: 15 bits; 1: 7 bits)
					// 2-0	Dividing Ratio of frequency (r)
					// Frequency = 524288 Hz / r / 2^(s+1)     ;For r=0 use r=0.5 instead
					chan.waveDuty = 500;

					chan.frequencyRatio = mem[baseAddr + 3] & 0x07;
					chan.shiftRegisterWidth = ((mem[baseAddr + 3] & 8) != 0) ? 7 : 15;
					chan.shiftClockFrequency = (mem[baseAddr + 3] & 0xf0) >> 4;

					chan.prngFrequency = (1048576 / (chan.frequencyRatio + 1)) >> (chan.shiftClockFrequency + 1);
					chan.NoiseScaler();
				}
			}

			for (int i = 0; i < 5; i++)
			{
				if (IsChanged(baseAddr + i))
				{
					chan.IsChanged = true;
					break;
				}
			}

		}

		void SoundDataToGbMemory()
		{
			int i;
			var mem = theGameboy.Cpu.Memory;

			int val = mem[0xff26] & 0xF0;

			for (i = 0; i < 4; i++)
			{
				if (gac[i].IsPlaying)
					val |= 1 << i;
			}

			mem.MemWrite(0xff26, (byte)val);
		}

		void LoadSoundData()
		{
			int lGain, rGain;
			var mem = theGameboy.Cpu.Memory;

			lGain = (mem[0xff24] & 0x07);
			rGain = (mem[0xff24] & 0x70) >> 4;

			LoadChannelData(0, 0xff10, false);
			LoadChannelData(1, 0xff15, false);
			LoadChannelData(2, 0xff1a, false);
			LoadChannelData(3, 0xff1f, false);

			if (IsChanged(0xff25))
			{
				gac[0].leftChannel = (mem[0xff25] & 0x01) != 0;
				gac[0].rightChannel = (mem[0xff25] & 0x10) != 0;

				gac[1].leftChannel = (mem[0xff25] & 0x02) != 0;
				gac[1].rightChannel = (mem[0xff25] & 0x20) != 0;

				gac[2].leftChannel = (mem[0xff25] & 0x04) != 0;
				gac[2].rightChannel = (mem[0xff25] & 0x40) != 0;

				gac[3].leftChannel = (mem[0xff25] & 0x08) != 0;
				gac[3].rightChannel = (mem[0xff25] & 0x80) != 0;

				gac[0].IsChanged = true;
				gac[1].IsChanged = true;
				gac[2].IsChanged = true;
				gac[3].IsChanged = true;
			}

			for (int i = 0; i < 4; i++)
			{
				if (gac[i].frequency > SamplingFrequency / 2)
				{
					gac[i].frequency = SamplingFrequency;
					gac[i].Amplitude = 0;
				}

				gac[i].lGain = lGain;
				gac[i].rGain = rGain;
			}

			ClearChanged();
		}

		int SamplingFrequency { get { return 44100; } }

		void ClearChanged()
		{
			Array.Clear(highMemoryChanged, 0, 0x100);
		}



		public void StartAudio()
		{
			mAudioRunning = true;
		}

		public void StopAudio()
		{
			mAudioRunning = false;

			while (mEmulating)
				System.Threading.Thread.Sleep(1);
		}
	}
}
