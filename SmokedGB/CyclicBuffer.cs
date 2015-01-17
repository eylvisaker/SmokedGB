using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmokedGBSharp
{
	public class CyclicBuffer : Stream
	{
		short[] data;
		int sampleIndex;

		public CyclicBuffer(int size)
		{
			data = new short[size];
		}

		public int SampleIndex
		{
			get { return sampleIndex; }
		}

		public short this[int index]
		{
			get
			{
				index %= data.Length;

				return data[index];
			}
			set
			{
				index %= data.Length;

				data[index] = value;
			}
		}

		public int DataLength
		{
			get { return data.Length; }
		}

		[Obsolete]
		public short[] Buffer { get { return data; } }

		public bool wrapped { get; private set; }

		int writePtr { get; set; }
		int lastWritePtr { get; set; }

		public int ReadPtr { get; private set; }

		public void SaveWritePtr()
		{
			lastWritePtr = writePtr;
			wrapped = false;
		}
		public void WriteSample(short sample)
		{
			data[writePtr] = sample;

			writePtr++;

			if (writePtr == DataLength)
			{
				writePtr = 0;
				wrapped = true;
			}

			sampleIndex++;
		}
		public void WriteSampleNoAdvance(int index, short sample)
		{
			index += writePtr;
			index %= DataLength;

			data[index] = sample;
		}
		public void ResetSampleIndex()
		{
			sampleIndex = 0;
		}

		/// <summary>
		/// Zeroes out the specified number of samples, starting from the
		/// current write pointer.  Does not advance the write pointer.
		/// </summary>
		/// <param name="samples"></param>
		internal void ClearFromWritePtr(int samples)
		{
			if (samples >= DataLength)
			{
				Array.Clear(data, 0, DataLength);
			}
			else if (samples + writePtr < DataLength)
			{
				Array.Clear(data, writePtr, samples);
			}
			else
			{
				Array.Clear(data, writePtr, data.Length - writePtr);

				samples -= data.Length - writePtr;

				Array.Clear(data, 0, samples);
			}
		}

		public int LastWriteLength
		{
			get
			{
				if (writePtr > lastWritePtr)
					return writePtr - lastWritePtr;
				else
					return data.Length - lastWritePtr + writePtr;
			}
		}

		internal void CopyLastDataWritenTo(byte[] array, int index)
		{
			//byte[] array = new byte[LastWriteLength];

			if (array.Length * 2 - index < LastWriteLength)
				throw new ArgumentException();

			if (writePtr > lastWritePtr)
			{
				System.Buffer.BlockCopy(data, lastWritePtr * 2,
					array, index, (writePtr - lastWritePtr) * 2);
			}
			else
			{
				System.Buffer.BlockCopy(data, lastWritePtr * 2,
					array, index, (data.Length - lastWritePtr) * 2);

				index += (data.Length - lastWritePtr) * 2;

				System.Buffer.BlockCopy(data, 0,
					array, index, writePtr * 2);
			}
		}

		internal void AdvanceWritePtr(int sampleCount)
		{
			writePtr += sampleCount;
			writePtr %= DataLength;
		}
		internal void AdvanceReadPtr(int sampleCount)
		{
			ReadPtr += sampleCount;
			ReadPtr %= DataLength;
		}

		internal void AddTo(CyclicBuffer dest, int sampleCount)
		{
			for (int i = 0; i < sampleCount; i++)
			{
				int destIndex = dest.writePtr + i;
				int srcIndex = ReadPtr + i;

				short sample = dest[destIndex];
				sample += this[srcIndex];

				dest[destIndex] = sample;
			}
		}

		internal void MoveWritePtrIfBehind(int p)
		{
			int r_ptr = streamBytePtr / 2 + p;
			int w_ptr = writePtr;

			if (r_ptr > DataLength)
				r_ptr -= DataLength;

			if (w_ptr < r_ptr)
			{
				writePtr = r_ptr;
			}
		}

		#region --- Stream Overrides ---

		int streamBytePtr;

		public override int ReadByte()
		{
			int value = this[streamBytePtr / 2];
			int val = (streamBytePtr % 2 == 0) ? value & 0xff : value >> 8;

			streamBytePtr++;

			if (streamBytePtr == Length)
				streamBytePtr = 0;

			return val;
		}
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (count + streamBytePtr < Length * 2)
			{
				System.Buffer.BlockCopy(data, streamBytePtr, buffer, offset, count);
			}
			else
			{
				int copyCount = (DataLength * 2 - streamBytePtr);
				System.Buffer.BlockCopy(
					data, streamBytePtr, buffer, offset, copyCount);

				count -= copyCount;
				offset += copyCount;

				System.Buffer.BlockCopy(data, 0, buffer, offset, count);
			}

			streamBytePtr += count;
			streamBytePtr %= DataLength * 2;

			return count;
		}

		public override bool CanSeek
		{
			get { return false; }
		}
		public override bool CanRead
		{
			get { return true; }
		}
		public override bool CanWrite
		{
			get { return false; }
		}
		public override long Position
		{
			get
			{
				return streamBytePtr;
			}
			set
			{
				streamBytePtr = (int)value;
			}
		}
		
		public override void Flush()
		{
			throw new InvalidOperationException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException();
		}

		public override void SetLength(long value)
		{
			throw new InvalidOperationException();
		}

		public override long Length
		{
			get { return data.Length*2; }
		}

		#endregion


	}
}
