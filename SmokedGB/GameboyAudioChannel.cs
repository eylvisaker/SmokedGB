using System;
using System.Collections.Generic;
using System.IO;
using AgateLib.AudioLib;


namespace SmokedGB
{
	class GameboyAudioChannel
	{
		public int frequency;
		public int Amplitude;
		public int sampleAmplitude;

		public int chanIndex;

		public int waveDuty;
		public bool repeat;

		public double samplesPerPeriod;
		public double modSamplesPerPeriod;
		public double percentExtraSamples;

		public int sweepTime;
		public int sweepSign;
		public int sweepShifts;

		public int envelopeSign;
		public int envelopeStep;

		public int sampIndexOffset;
		public int SampleIndex { get; set; }
		public int freqIndex;

		// channel settings
		public bool leftChannel;
		public bool rightChannel;
		public int lGain;
		public int rGain;

		// sound mode 3
		public bool sndEnable;
		public int outputLevel;
		public byte[] voluntaryData = new byte[16];

		// sound mode 4
		public int frequencyRatio;
		public int shiftRegisterWidth;
		public int shiftClockFrequency;
		public int shiftClock;
		public int prngFrequency;
		public double timeCounter;

		public int totalShifts;
		public int totalEnvelopes;
		public ushort shiftRegister = 0xff;
		int output;

		public double totalLength;
		public double timeLasted;
		public double startedShift;
		public double startedEnvelope;


		public BinaryWriter outputFile;
		public bool IsChanged;
		public bool IsPlaying;
		public bool bReseting;

		const int BufferSize = 441000;
		public const int nSamplesPerSec = 44100;

		readonly double[] ratioTable = new double[] { .5, 1, 2, 3, 4, 5, 6, 7 };

		public GameboyAudioChannel()
		{
			frequency = nSamplesPerSec;

			repeat = true;
		}

		// /-------------------------------\
		// < PseudoRandom Number Generator >
		// \-------------------------------/
		// The noise channel has a 1-bit pseudo-random number generator, or PRNG. This
		// particular type of PRNG is called an LFSR, a Linear Feedback Shift Register.
		// It's based on a 15 or 7-bit shift register, and an exclusive OR gate (XOR). 
		// The
		// mode is selectable by the Shift register width selector.

		// Channel 4 - $FF22(NR43)[noise]
		// ---------------------------------------------
		// 3	Shift Register width (0: 15 bits; 1: 7 bits)

		// The following diagram shows where the XOR taps are taken off the shift
		// register to produce the 1-bit pseudo-random number sequences for each mode.

		// mode        <-----
		// ----   FEDCBA9876543210
		// 32K    %*.............R
		// 512    XXXXXXXX%*.....R

		// X- N/A
		// %- output and XOR tap
		// *- XOR tap
		// R- result of XOR, to be shifted in

		// The current result of the XOR will be transferred into bit position 0 of the
		// shift register upon the next shift cycle. The 1-bit 'random' output is taken
		// from the output, is inverted, then is sent to the volume/envelope hardware 
		// for
		// the noise channel.

		// VERIFIED: Upon the channel INIT trigger bit being set for this channel (4),
		// the PRNG will be reloaded with a value of 0xFF. (Kirby 2)
		// NOTE: There will be a 16-clock delay before the PRNG outputs any non-0 data
		// after an INIT. This means the value loaded on channel INIT isn't 0x01 as
		// previously written here, but is 0xFF. this will cause the channel to have a
		// value of 0x01 16 clocks later. Again, thanks to beware for pointing this 
		// out.

		// On system reset, this shift register is loaded with a value of 1.

		// Channel 4 - $FF22(NR43)[noise]
		// ---------------------------------------------
		// 7-4	Shift clock frequency (s)
		// 3	Shift Register width (0: 15 bits; 1: 7 bits)
		// 2-0	Dividing Ratio of frequency (r)
		// Frequency = 524288 Hz / r / 2^(s+1)     ;For r=0 use r=0.5 instead

		public void NoiseScaler()
		{
		}
		void SetNoiseAmplitude()
		{
			if (timeCounter > 1.0 / prngFrequency)
			{
				int mask = 1 << shiftRegisterWidth;
				int xormask = mask >> 1;

				output = ((shiftRegister & mask) != 0) ? 1 : 0;
				int xorvalue = ((shiftRegister & xormask) != 0) ? 1 : 0;

				ushort xorResult = (ushort)(output ^ xorvalue);

				shiftRegister <<= 1;
				shiftRegister |= xorResult;

				timeCounter -= 1.0 / prngFrequency;
			}


			sampleAmplitude = output * 15;
		}

		// Figure out what this is for.
		// Looks like amount of time since sound started, but that seems useless.
		public double length()
		{
			if (repeat)
				return totalLength - timeLasted;
			else
				return 1;
		}

		//Frequency shifts for voice one?
		public bool TestShift(double time)
		{
			if (!(sweepTime / 128.0 * (totalShifts + 1) < timeLasted - startedShift
				&& sweepTime > 0))
			{
				return false;
			}

			totalShifts++;

			frequency = (int)(2048 -
				(131072 / frequency));

			frequency += (int)(sweepSign *
				frequency / (1 << sweepShifts));


			if (frequency < 2048 && frequency >= 0)
			{
				frequency = 131072 / (2048 - frequency);
			}
			else
			{
				Amplitude = 0;
				frequency = nSamplesPerSec;
			}

			return true;
		}
		// This is for a volume envelop.  Understand what it does and if it can be simplified.
		public bool TestEnvelope(double time)
		{
			if (!(envelopeStep / 64.0 * (totalEnvelopes + 1) < timeLasted - startedEnvelope &&
				envelopeStep > 0))
			{
				return false;
			}

			totalEnvelopes++;

			if (envelopeSign > 0)
			{
				Amplitude++;

				if (Amplitude > 0x0f)
				{
					Amplitude = 0x0f;
				}

			}
			else
			{
				Amplitude--;

				if (Amplitude < 0)
				{
					Amplitude = 0;
				}
			}

			return true;
		}

		public short ActualAmplitude(int baseGain)
		{
			if (chanIndex == 2)
			{
				SetVoluntaryAmplitude();
			}
			else if (chanIndex == 3)
			{
				SetNoiseAmplitude();
			}

			if (chanIndex <= 3)
			{
				return AmplitudeFormula(rGain, baseGain);
			}

			return 0;
		}

		short AmplitudeFormula(int channelGain, int baseGain)
		{
			if (channelGain != 0 && Amplitude != 0)
				return (short)(Amplitude * sampleAmplitude / 0x0f * channelGain * baseGain);
			else
				return 0;

		}

		void SetVoluntaryAmplitude()
		{
			int voluntaryIndex = (int)(
				(SampleIndex * 2 / (double)(samplesPerPeriod)) * 32);

			// Why?
			if (voluntaryIndex >= 32)
				voluntaryIndex -= 32;

			// Voluntary amplitude is stored in 4-bit nybbles.
			// So we pull out the 4 bits needed and shift them to 0..0xf range.
			int mask = (voluntaryIndex % 2 != 0) ? 0x0f : 0xf0;
			int vData = voluntaryData[voluntaryIndex >> 1] & mask;

			if (vData > 0x0f)
			{
				vData >>= 4;
			}

			// Output Level is a volume?  Check.
			if (sndEnable && outputLevel != 0)
			{
				vData >>= (outputLevel - 1);
			}
			else
			{
				vData = 0;
			}

			Amplitude = vData;
		}

	}
}