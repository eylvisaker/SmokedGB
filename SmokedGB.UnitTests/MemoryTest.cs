using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmokedGB.UnitTests
{
	[TestClass]
	public class MemoryTest
	{
		[TestMethod]
		public void WriteShortTest()
		{
			byte[] buffer = new byte[50000];
			Rom rom = new Rom(buffer, null, "test.gb");
			rom.MemoryController.WriteShort(0x8000, 0x1234);

			Assert.AreEqual(0x34, rom.MemoryController[0x8000]);
			Assert.AreEqual(0x12, rom.MemoryController[0x8001]);
		}
	}
}
