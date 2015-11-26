using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokedGB
{
    public abstract class MemoryController : IMemoryController
    {
        private IGameboy theGameboy;
        public IGameboyAudio aud { get; set; }
        public IGameboyCpu Cpu { get; set; }

        protected byte[] mem;
        byte[] vRam;
        int vRamBankSelect;
        protected Rom mRom { get; private set; }
        int romBankSelect;
        public bool DoGbcHdma { get; set; }
        public byte[] objPaletteData { get; set; }
        public byte[] bgPaletteData { get; set; }

        public bool ramBankEnable;
        public ushort ramBankSelect;
        public int MBC1Mode;

        public byte[] sysRam;
        public int sysRamBank;

        public MemoryController(Rom rom)
        {
            mRom = rom;

            objPaletteData = new byte[128];
            bgPaletteData = new byte[128];

            mem = new byte[0x10000];
            vRam = new byte[0x4000];
            vRamBankSelect = 0;

            Array.Copy(mRom.RomData, mem, 0x8000);

            if (mRom.ActualSaveRamSize > 0)
            {
                Array.Copy(mRom.SaveRam, 0, mem, 0xa000, 0x2000);
            }
            else if (mRom.ActualSaveRamSize < 0)
            {
                throw new Exception("Actual save rame size smaller than zero?");
            }

            ramBankEnable = false;
            ramBankSelect = 0;

            MBC1Mode = 0;

            sysRam = new byte[0x8000];
            sysRamBank = 0;



            mem[0xFF05] = 0x00; // tima:	timer counter
            mem[0xFF06] = 0x00; // tma:		timer modulo
            mem[0xFF07] = 0x00; // tac:		timer control
            mem[0xFF10] = 0x80; // nr10		sound 1 sweep
            mem[0xFF11] = 0xBF; // nr11		sound 1 length / wave pattern duty
            mem[0xFF12] = 0xF3; // nr12		sound 1 envelope
            mem[0xFF13] = 0x00; // nr13		sound 1 frequency lo
            mem[0xFF14] = 0xBC; // nr14		sound 1 frequency hi	// was 0xbf
            mem[0xFF16] = 0x3F; // nr21		sound 2 length / wave pattern duty
            mem[0xFF17] = 0x00; // nr22		sound 2 envelope
            mem[0xFF18] = 0x00; // nr23		sound 2 frequency lo
            mem[0xFF19] = 0xBC; // nr24		sound 2 frequency hi	// was 0xbf
            mem[0xFF1A] = 0x7F; // nr30		sound 3 on / off (high bit)
            mem[0xFF1B] = 0xFC; // nr31		sound 3 length
            mem[0xFF1C] = 0x9F; // nr32		sound 3 output volume
            mem[0xFF1d] = 0x00; // nr33		sound 3 frequency lo
            mem[0xFF1E] = 0xBC; // nr34		sound 3 frequency hi    // was 0xbf
            mem[0xFF20] = 0xFF; // nr41		sound 4 length
            mem[0xFF21] = 0x00; // nr42		sound 4 envelope
            mem[0xFF22] = 0x00; // nr43		sound 4 polynomial counter
            mem[0xFF23] = 0xBF; // nr44		sound 4 counter/consecutive / initial
            mem[0xFF24] = 0x77; // nr50		channel control / ON-OFF / Volume
            mem[0xFF25] = 0xF3; // nr51		Selection of Sound output terminal (R/W)
            mem[0xFF26] = 0xF1; // nr52		enable / disable all sound;  0xF0 for SGB
            mem[0xFF40] = 0x91; // lcdc
            mem[0xFF41] = 0x00; // stat
            mem[0xFF42] = 0x00; // scy
            mem[0xFF43] = 0x00; // scx
            mem[0xFF44] = 0x00; // ly
            mem[0xFF45] = 0x00; // lyc
            mem[0xFF47] = 0xFC; // bgp
            mem[0xFF48] = 0xFF; // obp0
            mem[0xFF49] = 0xFF; // obp1
            mem[0xFF4A] = 0x00; // wy
            mem[0xFF4B] = 0x00; // wx
            mem[0xFF4D] = 0x7F;
            mem[0xFFFF] = 0x00; // IEF


            for (int i = 0xFF50; i < 0xFFFF; i++)
            {
                mem[i] = 0xc9;
            }


            for (int i = 0; i < 64; i += 2)
            {
                int r = 8 - (i % 8);
                int g = 8 - (i % 8);
                int b = 8 - (i % 8);

                r <<= 5;
                g <<= 5;
                b <<= 5;

                short val = (short)(((r >> 3) << 10) | ((g >> 3) << 5) | (b >> 3));

                new pair(val).WriteTo(bgPaletteData, i);
            }

        }

        bool IsGbc { get { return TheGameboy.IsGbc; } }

        public void WriteShort(int address, ushort value)
        {
            this[address] = (byte)(value & 0xff);
            this[address + 1] = (byte)(value >> 8);
        }


        public void MemWrite(int address, byte value)
        {
            mem[address] = value;
        }

        public byte this[int index]
        {
            get { return mem[index]; }
            set { Write(index, value); }
        }
        protected virtual void Write(int address, byte value)
        {
            if (address >= 0x8000)      // ram
            {
                mem[address] = value;

                if (address >= 0xc000 && address < 0xde00)
                {
                    mem[address + 0x2000] = value;
                }
                else if (address >= 0xe000 && address < 0xfe00)
                {
                    mem[address - 0x2000] = value;
                }
                else if (address >= 0xff00)
                {
                    WriteToHighMem(address, value);
                }
            }
            else
            {
                // This is a write to the ROM, so we need to switch banks somewhere
                WriteToRom(address, value);
            }
        }

        protected abstract void WriteToRom(int address, byte value);

        private void WriteToHighMem(int address, byte value)
        {
            switch (address)
            {
                case 0xff00:    // joystick register
                    TheGameboy.CheckJoysticks();
                    break;

                case 0xff01:    // Serial port I/O
                case 0xff02:
                case 0xff03:
                    break;

                case 0xff04:    // divider register
                    mem[address] = 0;
                    break;

                case 0xff07:
                    TheGameboy.UpdateTimerFrequency();
                    break;

                case 0xff10:
                case 0xff11:
                case 0xff12:
                case 0xff13:
                case 0xff14:
                    aud.SoundWrite(address);
                    break;

                case 0xff16:
                case 0xff17:
                case 0xff18:
                case 0xff19:
                    aud.SoundWrite(address);
                    break;

                case 0xff1a:
                case 0xff1b:
                case 0xff1c:
                case 0xff1d:
                case 0xff1e:
                    aud.SoundWrite(address);
                    break;

                case 0xff30:
                case 0xff31:
                case 0xff32:
                case 0xff33:
                case 0xff34:
                case 0xff35:
                case 0xff36:
                case 0xff37:
                case 0xff38:
                case 0xff39:
                case 0xff3a:
                case 0xff3b:
                case 0xff3c:
                case 0xff3d:
                case 0xff3e:
                case 0xff3f:
                    aud.SoundWrite(0xff30);

                    break;

                case 0xff20:
                case 0xff21:
                case 0xff22:
                case 0xff23:
                    aud.SoundWrite(address);
                    break;

                case 0xff24:
                case 0xff25:
                case 0xff26:
                    aud.SoundWrite(address);

                    break;

                case 0xff44:
                    mem[address] = 0;
                    break;

                case 0xff46:    // DMA transfer initiate

                    int start = value << 8;
                    Copy(start, 0xfe00, 0xa0);

                    break;

                case 0xff4d:    // CPU speed

                    if (TheGameboy.IsGbc)
                    {
                        if ((value & 0x01) != 0)
                        {
                            Cpu.CpuSpeed = 2;
                            mem[address] = 0x80;
                        }
                        else
                        {
                            Cpu.CpuSpeed = 1;
                            mem[address] = 0x7f;
                        }
                    }

                    break;

                case 0xff4f:    // VRAM Bank
                    if (TheGameboy.IsGbc)
                    {
                        value &= 0x01;

                        mem[address] = (byte)(0xfe | value);

                        VRamBankSelect = value;
                    }

                    break;

                case 0xff51:    // gbc dma src high
                case 0xff52:    // gbc dma src low
                case 0xff53:    // gbc dma dest high
                case 0xff54:    // gbc dma dest low

                    return;

                case 0xff55:    // gbc dma type & size

                    if (TheGameboy.IsGbc)
                    {
                        if (value >= 0x80)
                        {
                            DoGbcHdma = true;
                            mem[0xff55] &= 0x7f;
                        }
                        else if (DoGbcHdma)
                        {
                            DoGbcHdma = false;
                        }
                        else
                        {

                            pair src = new pair(), dest = new pair();

                            src.h = mem[0xff51];
                            src.l = mem[0xff52];
                            src.W &= 0xfff0;

                            dest.h = mem[0xff53];
                            dest.l = mem[0xff54];

                            dest.W &= 0x1ff0;
                            dest.W |= 0x8000;

                            do
                            {

                                for (int i = 0; i < 16; i++)
                                {
                                    this[dest.W] = this[src.W];

                                    dest.W++;
                                    src.W++;
                                }

                                mem[0xff51] = src.h;
                                mem[0xff52] = src.l;
                                mem[0xff53] = dest.h;
                                mem[0xff54] = dest.l;

                                mem[0xff55]--;

                            } while (mem[0xff55] != 0xff);
                        }
                    }
                    break;

                case 0xff68:        // CGB bgp Palette
                    if (IsGbc)
                    {
                        mem[0xff68] &= 0x7f;
                        mem[0xff69] = bgPaletteData[value & 0x7f];
                    }

                    break;

                case 0xff69:
                    if (IsGbc)
                    {
                        // TODO: Fix this code here
                        bgPaletteData[mem[0xff68]++] = value;
                    }

                    break;

                case 0xff6a:        // CGB obj Palette
                    if (IsGbc)
                    {
                        mem[0xff6a] &= 0x7f;
                        mem[0xff6b] = objPaletteData[value & 0x7f];
                    }

                    break;

                case 0xff6b:
                    if (IsGbc)
                    {
                        // TODO: Fix this code here
                        // Sometimes the value is out of the paletteData memory range,
                        // like for zelda oracle of seasons.
                        objPaletteData[mem[0xff6a]] = value;
                        mem[0xff6a]++;
                    }

                    break;

                case 0xff70:        // CGB System Ram Bank

                    if (IsGbc && value != sysRamBank)
                    {
                        value &= 0x03;

                        Array.Copy(mem, 0xc000, sysRam, sysRamBank * 0x2000, 0x2000);

                        sysRamBank = value;

                        Array.Copy(sysRam, sysRamBank * 0x2000, mem, 0xc000, 0x2000);
                        Array.Copy(sysRam, sysRamBank * 0x2000, mem, 0xe000, 0x1eff);
                    }

                    break;

            }

            return;
        }


        public void CheckGbcHdma()
        {
            if (IsGbc == false)
                return;
            if (DoGbcHdma == false)
                return;

            pair src = new pair(), dest = new pair();

            mem[0xff55] &= 0x7f;

            src.h = mem[0xff51];
            src.l = mem[0xff52];
            src.W &= 0xfff0;

            dest.h = mem[0xff53];
            dest.l = mem[0xff54];
            dest.W &= 0x1ff0;
            dest.W |= 0x8000;

            for (int i = 0; i < 16; i++)
            {
                this[dest.W] = this[src.W];

                dest.W++;
                src.W++;
            }

            mem[0xff51] = src.h;
            mem[0xff52] = src.l;
            mem[0xff53] = dest.h;
            mem[0xff54] = dest.l;

            mem[0xff55]--;

            if (mem[0xff55] == 0xff)
            {
                DoGbcHdma = false;
            }
        }

        public byte[] VideoMemory { get { return vRam; } }

        public void Copy(int srcAddr, int destAddr, int count)
        {
            Array.Copy(mem, srcAddr, mem, destAddr, count);
        }

        public int Length { get { return mem.Length; } }

        public int RomBank
        {
            get { return romBankSelect; }
            set
            {
                if (value == 0)
                    value = 1;
                if (romBankSelect == value)
                    return;
                if (value >= mRom.RomBanks)
                    return;

                romBankSelect = value;

                Array.Copy(mRom.RomData, value * 0x4000, mem, 0x4000, 0x4000);
            }
        }

        public int VRamBankSelect
        {
            get { return vRamBankSelect; }
            set
            {
                if (value == vRamBankSelect)
                    return;

                CopyMemoryToVram();
                vRamBankSelect = value;
                CopyVramToMemory();
            }
        }

        public IGameboy TheGameboy
        {
            get
            {
                return theGameboy;
            }
            set
            {
                theGameboy = value;
            }
        }

        private void CopyVramToMemory()
        {
            Array.Copy(vRam, 0x2000 * vRamBankSelect, mem, 0x8000, 0x2000);
        }

        public void CopyMemoryToVram()
        {
            Array.Copy(mem, 0x8000, vRam, 0x2000 * vRamBankSelect, 0x2000);
        }

        public void CopyMemoryToSaveRam()
        {
            if (mRom.SaveRam == null)
                return;

            Array.Copy(mem, 0xa000, mRom.SaveRam, ramBankSelect * 0x2000, 0x2000);
        }

        public void CopyTo(byte[] dest, int address, int length)
        {
            Array.Copy(mem, address, dest, 0, length);
        }
    }
}
