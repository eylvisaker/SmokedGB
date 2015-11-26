using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokedGB.UnitTests.Fakes
{
    public class FakeMemoryController : IMemoryController
    {
        byte[] ram = new byte[65536];

        public byte this[int index]
        {
            get
            {
                return ram[index];
            }
            set
            {
                ram[index] = value;
            }
        }

        public int Length
        {
            get
            {
                return ram.Length;
            }
        }

        public void CopyMemoryToSaveRam()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(byte[] voluntaryData, int v1, int v2)
        {
            throw new NotImplementedException();
        }

        public void MemWrite(int v1, byte v2)
        {
            throw new NotImplementedException();
        }
    }
}
