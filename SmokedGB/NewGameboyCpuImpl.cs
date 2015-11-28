// This file was generated automatically by a code-generating tool.
// Do not edit manually, or your changes will probably be overwritten
// when the tool is rerun.
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SmokedGB
{
	public partial class GameboyCpu
	{
		public Registers registers;

		#region --- OpCode ---

		public  enum OpCode
		{
			HALT = 0x76,
			RET = 0xc9,
			RET_NZ = 0xc0,
			RET_Z = 0xc8,
			RET_NC = 0xd0,
			RET_C = 0xd8,
			RETI = 0xd9,
			EI = 0xfb,
			DI = 0xf3,
			RR_A = 0x1f,
			RRC_A = 0x0f,
			RL_A = 0x17,
			RLC_A = 0x07,
			INC_BC = 0x03,
			INC_DE = 0x13,
			INC_HL = 0x23,
			INC_SP = 0x33,
			DEC_BC = 0x0b,
			DEC_DE = 0x1b,
			DEC_HL = 0x2b,
			DEC_SP = 0x3b,
			ADD_HL_BC = 0x09,
			ADD_HL_DE = 0x19,
			ADD_HL_HL = 0x29,
			ADD_HL_SP = 0x39,
			ADD_SP_n = 0xe8,
			SCF = 0x37,
			CCF = 0x3f,
			CPL_A = 0x2f,
			CALL_nn = 0xcd,
			CALL_NZ_nn = 0xc4,
			CALL_Z_nn = 0xcc,
			CALL_NC_nn = 0xd4,
			CALL_C_nn = 0xdc,
			RST_00 = 0xc7,
			RST_08 = 0xcf,
			RST_10 = 0xd7,
			RST_18 = 0xdf,
			RST_20 = 0xe7,
			RST_28 = 0xef,
			RST_30 = 0xf7,
			RST_38 = 0xff,
			JR_s = 0x18,
			JR_NZ_s = 0x20,
			JR_Z_s = 0x28,
			JR_NC_s = 0x30,
			JR_C_s = 0x38,
			JP_nn = 0xc3,
			JP_NZ_nn = 0xc2,
			JP_Z_nn = 0xca,
			JP_NC_nn = 0xd2,
			JP_C_nn = 0xda,
			JP_HL = 0xe9,
			NOP = 0x00,
			INC_A = 0x3c,
			INC_B = 0x04,
			INC_C = 0x0c,
			INC_D = 0x14,
			INC_E = 0x1c,
			INC_H = 0x24,
			INC_L = 0x2c,
			INC_xHL = 0x34,
			DEC_A = 0x3d,
			DEC_B = 0x05,
			DEC_C = 0x0d,
			DEC_D = 0x15,
			DEC_E = 0x1d,
			DEC_H = 0x25,
			DEC_L = 0x2d,
			DEC_xHL = 0x35,
			CP_A = 0xbf,
			CP_B = 0xb8,
			CP_C = 0xb9,
			CP_D = 0xba,
			CP_E = 0xbb,
			CP_H = 0xbc,
			CP_L = 0xbd,
			CP_xHL = 0xbe,
			CP_n = 0xfe,
			AND_A = 0xa7,
			AND_B = 0xa0,
			AND_C = 0xa1,
			AND_D = 0xa2,
			AND_E = 0xa3,
			AND_H = 0xa4,
			AND_L = 0xa5,
			AND_xHL = 0xa6,
			AND_n = 0xe6,
			OR_A = 0xb7,
			OR_B = 0xb0,
			OR_C = 0xb1,
			OR_D = 0xb2,
			OR_E = 0xb3,
			OR_H = 0xb4,
			OR_L = 0xb5,
			OR_xHL = 0xb6,
			OR_n = 0xf6,
			XOR_A = 0xaf,
			XOR_B = 0xa8,
			XOR_C = 0xa9,
			XOR_D = 0xaa,
			XOR_E = 0xab,
			XOR_H = 0xac,
			XOR_L = 0xad,
			XOR_xHL = 0xae,
			XOR_n = 0xee,
			ADC_A = 0x8f,
			ADC_B = 0x88,
			ADC_C = 0x89,
			ADC_D = 0x8a,
			ADC_E = 0x8b,
			ADC_H = 0x8c,
			ADC_L = 0x8d,
			ADC_xHL = 0x8e,
			ADC_n = 0xce,
			ADD_A = 0x87,
			ADD_B = 0x80,
			ADD_C = 0x81,
			ADD_D = 0x82,
			ADD_E = 0x83,
			ADD_H = 0x84,
			ADD_L = 0x85,
			ADD_xHL = 0x86,
			ADD_n = 0xc6,
			SUB_A = 0x97,
			SUB_B = 0x90,
			SUB_C = 0x91,
			SUB_D = 0x92,
			SUB_E = 0x93,
			SUB_H = 0x94,
			SUB_L = 0x95,
			SUB_xHL = 0x96,
			SUB_n = 0xd6,
			SBC_A = 0x9f,
			SBC_B = 0x98,
			SBC_C = 0x99,
			SBC_D = 0x9a,
			SBC_E = 0x9b,
			SBC_H = 0x9c,
			SBC_L = 0x9d,
			SBC_xHL = 0x9e,
			SBC_n = 0xde,
			PUSH_AF = 0xf5,
			PUSH_BC = 0xc5,
			PUSH_DE = 0xd5,
			PUSH_HL = 0xe5,
			POP_BC = 0xc1,
			POP_DE = 0xd1,
			POP_HL = 0xe1,
			POP_AF = 0xf1,
			LDHL_SP_n = 0xf8,
			LD_A_n = 0x3e,
			LD_B_n = 0x06,
			LD_C_n = 0x0e,
			LD_D_n = 0x16,
			LD_E_n = 0x1e,
			LD_H_n = 0x26,
			LD_L_n = 0x2e,
			LD_A_A = 0x7f,
			LD_A_B = 0x78,
			LD_A_C = 0x79,
			LD_A_D = 0x7a,
			LD_A_E = 0x7b,
			LD_A_H = 0x7c,
			LD_A_L = 0x7d,
			LD_A_xBC = 0x0a,
			LD_A_xDE = 0x1a,
			LD_A_xHL = 0x7e,
			LD_A_xnn = 0xfa,
			LD_B_A = 0x47,
			LD_B_B = 0x40,
			LD_B_C = 0x41,
			LD_B_D = 0x42,
			LD_B_E = 0x43,
			LD_B_H = 0x44,
			LD_B_L = 0x45,
			LD_B_xHL = 0x46,
			LD_C_A = 0x4f,
			LD_C_B = 0x48,
			LD_C_C = 0x49,
			LD_C_D = 0x4a,
			LD_C_E = 0x4b,
			LD_C_H = 0x4c,
			LD_C_L = 0x4d,
			LD_C_xHL = 0x4e,
			LD_D_A = 0x57,
			LD_D_B = 0x50,
			LD_D_C = 0x51,
			LD_D_D = 0x52,
			LD_D_E = 0x53,
			LD_D_H = 0x54,
			LD_D_L = 0x55,
			LD_D_xHL = 0x56,
			LD_E_A = 0x5f,
			LD_E_B = 0x58,
			LD_E_C = 0x59,
			LD_E_D = 0x5a,
			LD_E_E = 0x5b,
			LD_E_H = 0x5c,
			LD_E_L = 0x5d,
			LD_E_xHL = 0x5e,
			LD_H_A = 0x67,
			LD_H_B = 0x60,
			LD_H_C = 0x61,
			LD_H_D = 0x62,
			LD_H_E = 0x63,
			LD_H_H = 0x64,
			LD_H_L = 0x65,
			LD_H_xHL = 0x66,
			LD_L_A = 0x6f,
			LD_L_B = 0x68,
			LD_L_C = 0x69,
			LD_L_D = 0x6a,
			LD_L_E = 0x6b,
			LD_L_H = 0x6c,
			LD_L_L = 0x6d,
			LD_L_xHL = 0x6e,
			LD_xHL_A = 0x77,
			LD_xHL_B = 0x70,
			LD_xHL_C = 0x71,
			LD_xHL_D = 0x72,
			LD_xHL_E = 0x73,
			LD_xHL_H = 0x74,
			LD_xHL_L = 0x75,
			LD_xHL_n = 0x36,
			LD_xBC_A = 0x02,
			LD_xDE_A = 0x12,
			LD_xnn_A = 0xea,
			LD_BC_nn = 0x01,
			LD_DE_nn = 0x11,
			LD_HL_nn = 0x21,
			LD_SP_nn = 0x31,
			LD_SP_HL = 0xf9,
			LDH_A_xC = 0xf2,
			LDH_xC_A = 0xe2,
			LD_xnn_SP = 0x08,
			LDH_A_xn = 0xf0,
			LDH_xn_A = 0xe0,
			LDI_A_xHL = 0x2a,
			LDI_xHL_A = 0x22,
			LDD_A_xHL = 0x3a,
			LDD_xHL_A = 0x32,
			DAA = 0x27,
			OpCodeCB = 0xcb,
		}

		#endregion

		#region --- OpCodeCB ---

		public  enum OpCodeCB
		{
			BIT_0_B = 0x40,
			BIT_0_C = 0x41,
			BIT_0_D = 0x42,
			BIT_0_E = 0x43,
			BIT_0_H = 0x44,
			BIT_0_L = 0x45,
			BIT_0_xHL = 0x46,
			BIT_0_A = 0x47,
			BIT_1_B = 0x48,
			BIT_1_C = 0x49,
			BIT_1_D = 0x4a,
			BIT_1_E = 0x4b,
			BIT_1_H = 0x4c,
			BIT_1_L = 0x4d,
			BIT_1_xHL = 0x4e,
			BIT_1_A = 0x4f,
			BIT_2_B = 0x50,
			BIT_2_C = 0x51,
			BIT_2_D = 0x52,
			BIT_2_E = 0x53,
			BIT_2_H = 0x54,
			BIT_2_L = 0x55,
			BIT_2_xHL = 0x56,
			BIT_2_A = 0x57,
			BIT_3_B = 0x58,
			BIT_3_C = 0x59,
			BIT_3_D = 0x5a,
			BIT_3_E = 0x5b,
			BIT_3_H = 0x5c,
			BIT_3_L = 0x5d,
			BIT_3_xHL = 0x5e,
			BIT_3_A = 0x5f,
			BIT_4_B = 0x60,
			BIT_4_C = 0x61,
			BIT_4_D = 0x62,
			BIT_4_E = 0x63,
			BIT_4_H = 0x64,
			BIT_4_L = 0x65,
			BIT_4_xHL = 0x66,
			BIT_4_A = 0x67,
			BIT_5_B = 0x68,
			BIT_5_C = 0x69,
			BIT_5_D = 0x6a,
			BIT_5_E = 0x6b,
			BIT_5_H = 0x6c,
			BIT_5_L = 0x6d,
			BIT_5_xHL = 0x6e,
			BIT_5_A = 0x6f,
			BIT_6_B = 0x70,
			BIT_6_C = 0x71,
			BIT_6_D = 0x72,
			BIT_6_E = 0x73,
			BIT_6_H = 0x74,
			BIT_6_L = 0x75,
			BIT_6_xHL = 0x76,
			BIT_6_A = 0x77,
			BIT_7_B = 0x78,
			BIT_7_C = 0x79,
			BIT_7_D = 0x7a,
			BIT_7_E = 0x7b,
			BIT_7_H = 0x7c,
			BIT_7_L = 0x7d,
			BIT_7_xHL = 0x7e,
			BIT_7_A = 0x7f,
			SET_0_B = 0xc0,
			SET_0_C = 0xc1,
			SET_0_D = 0xc2,
			SET_0_E = 0xc3,
			SET_0_H = 0xc4,
			SET_0_L = 0xc5,
			SET_0_xHL = 0xc6,
			SET_0_A = 0xc7,
			SET_1_B = 0xc8,
			SET_1_C = 0xc9,
			SET_1_D = 0xca,
			SET_1_E = 0xcb,
			SET_1_H = 0xcc,
			SET_1_L = 0xcd,
			SET_1_xHL = 0xce,
			SET_1_A = 0xcf,
			SET_2_B = 0xd0,
			SET_2_C = 0xd1,
			SET_2_D = 0xd2,
			SET_2_E = 0xd3,
			SET_2_H = 0xd4,
			SET_2_L = 0xd5,
			SET_2_xHL = 0xd6,
			SET_2_A = 0xd7,
			SET_3_B = 0xd8,
			SET_3_C = 0xd9,
			SET_3_D = 0xda,
			SET_3_E = 0xdb,
			SET_3_H = 0xdc,
			SET_3_L = 0xdd,
			SET_3_xHL = 0xde,
			SET_3_A = 0xdf,
			SET_4_B = 0xe0,
			SET_4_C = 0xe1,
			SET_4_D = 0xe2,
			SET_4_E = 0xe3,
			SET_4_H = 0xe4,
			SET_4_L = 0xe5,
			SET_4_xHL = 0xe6,
			SET_4_A = 0xe7,
			SET_5_B = 0xe8,
			SET_5_C = 0xe9,
			SET_5_D = 0xea,
			SET_5_E = 0xeb,
			SET_5_H = 0xec,
			SET_5_L = 0xed,
			SET_5_xHL = 0xee,
			SET_5_A = 0xef,
			SET_6_B = 0xf0,
			SET_6_C = 0xf1,
			SET_6_D = 0xf2,
			SET_6_E = 0xf3,
			SET_6_H = 0xf4,
			SET_6_L = 0xf5,
			SET_6_xHL = 0xf6,
			SET_6_A = 0xf7,
			SET_7_B = 0xf8,
			SET_7_C = 0xf9,
			SET_7_D = 0xfa,
			SET_7_E = 0xfb,
			SET_7_H = 0xfc,
			SET_7_L = 0xfd,
			SET_7_xHL = 0xfe,
			SET_7_A = 0xff,
			RES_0_B = 0x80,
			RES_0_C = 0x81,
			RES_0_D = 0x82,
			RES_0_E = 0x83,
			RES_0_H = 0x84,
			RES_0_L = 0x85,
			RES_0_xHL = 0x86,
			RES_0_A = 0x87,
			RES_1_B = 0x88,
			RES_1_C = 0x89,
			RES_1_D = 0x8a,
			RES_1_E = 0x8b,
			RES_1_H = 0x8c,
			RES_1_L = 0x8d,
			RES_1_xHL = 0x8e,
			RES_1_A = 0x8f,
			RES_2_B = 0x90,
			RES_2_C = 0x91,
			RES_2_D = 0x92,
			RES_2_E = 0x93,
			RES_2_H = 0x94,
			RES_2_L = 0x95,
			RES_2_xHL = 0x96,
			RES_2_A = 0x97,
			RES_3_B = 0x98,
			RES_3_C = 0x99,
			RES_3_D = 0x9a,
			RES_3_E = 0x9b,
			RES_3_H = 0x9c,
			RES_3_L = 0x9d,
			RES_3_xHL = 0x9e,
			RES_3_A = 0x9f,
			RES_4_B = 0xa0,
			RES_4_C = 0xa1,
			RES_4_D = 0xa2,
			RES_4_E = 0xa3,
			RES_4_H = 0xa4,
			RES_4_L = 0xa5,
			RES_4_xHL = 0xa6,
			RES_4_A = 0xa7,
			RES_5_B = 0xa8,
			RES_5_C = 0xa9,
			RES_5_D = 0xaa,
			RES_5_E = 0xab,
			RES_5_H = 0xac,
			RES_5_L = 0xad,
			RES_5_xHL = 0xae,
			RES_5_A = 0xaf,
			RES_6_B = 0xb0,
			RES_6_C = 0xb1,
			RES_6_D = 0xb2,
			RES_6_E = 0xb3,
			RES_6_H = 0xb4,
			RES_6_L = 0xb5,
			RES_6_xHL = 0xb6,
			RES_6_A = 0xb7,
			RES_7_B = 0xb8,
			RES_7_C = 0xb9,
			RES_7_D = 0xba,
			RES_7_E = 0xbb,
			RES_7_H = 0xbc,
			RES_7_L = 0xbd,
			RES_7_xHL = 0xbe,
			RES_7_A = 0xbf,
			RR_A = 0x1f,
			RR_B = 0x18,
			RR_C = 0x19,
			RR_D = 0x1a,
			RR_E = 0x1b,
			RR_H = 0x1c,
			RR_L = 0x1d,
			RR_xHL = 0x1e,
			RRC_A = 0x0f,
			RRC_B = 0x08,
			RRC_C = 0x09,
			RRC_D = 0x0a,
			RRC_E = 0x0b,
			RRC_H = 0x0c,
			RRC_L = 0x0d,
			RRC_xHL = 0x0e,
			SLA_A = 0x27,
			SLA_B = 0x20,
			SLA_C = 0x21,
			SLA_D = 0x22,
			SLA_E = 0x23,
			SLA_H = 0x24,
			SLA_L = 0x25,
			SLA_xHL = 0x26,
			SRA_A = 0x2f,
			SRA_B = 0x28,
			SRA_C = 0x29,
			SRA_D = 0x2a,
			SRA_E = 0x2b,
			SRA_H = 0x2c,
			SRA_L = 0x2d,
			SRA_xHL = 0x2e,
			SRL_A = 0x3f,
			SRL_B = 0x38,
			SRL_C = 0x39,
			SRL_D = 0x3a,
			SRL_E = 0x3b,
			SRL_H = 0x3c,
			SRL_L = 0x3d,
			SRL_xHL = 0x3e,
			RL_A = 0x17,
			RL_B = 0x10,
			RL_C = 0x11,
			RL_D = 0x12,
			RL_E = 0x13,
			RL_H = 0x14,
			RL_L = 0x15,
			RL_xHL = 0x16,
			RLC_A = 0x07,
			RLC_B = 0x00,
			RLC_C = 0x01,
			RLC_D = 0x02,
			RLC_E = 0x03,
			RLC_H = 0x04,
			RLC_L = 0x05,
			RLC_xHL = 0x06,
			SWAP_A = 0x37,
			SWAP_B = 0x30,
			SWAP_C = 0x31,
			SWAP_D = 0x32,
			SWAP_E = 0x33,
			SWAP_H = 0x34,
			SWAP_L = 0x35,
			SWAP_xHL = 0x36,
		}

		#endregion

		#region --- Mnemonics ---

		static  string[] Mnemonics = new string[]
		{
			"NOP",                 // 0     0000
			"LD BC,##",            // 1     0001
			"LD (BC),A",           // 2     0002
			"INC BC",              // 3     0003
			"INC B",               // 4     0004
			"DEC B",               // 5     0005
			"LD B,#",              // 6     0006
			"RLC A",               // 7     0007
			"LD (##),SP",          // 8     0008
			"ADD HL,BC",           // 9     0009
			"LD A,(BC)",           // 10    000A
			"DEC BC",              // 11    000B
			"INC C",               // 12    000C
			"DEC C",               // 13    000D
			"LD C,#",              // 14    000E
			"RRC A",               // 15    000F
			"bad opcode",          // 16    0010
			"LD DE,##",            // 17    0011
			"LD (DE),A",           // 18    0012
			"INC DE",              // 19    0013
			"INC D",               // 20    0014
			"DEC D",               // 21    0015
			"LD D,#",              // 22    0016
			"RL A",                // 23    0017
			"JR @",                // 24    0018
			"ADD HL,DE",           // 25    0019
			"LD A,(DE)",           // 26    001A
			"DEC DE",              // 27    001B
			"INC E",               // 28    001C
			"DEC E",               // 29    001D
			"LD E,#",              // 30    001E
			"RR A",                // 31    001F
			"JR NZ,@",             // 32    0020
			"LD HL,##",            // 33    0021
			"LDI (HL),A",          // 34    0022
			"INC HL",              // 35    0023
			"INC H",               // 36    0024
			"DEC H",               // 37    0025
			"LD H,#",              // 38    0026
			"DAA",                 // 39    0027
			"JR Z,@",              // 40    0028
			"ADD HL,HL",           // 41    0029
			"LDI A,(HL)",          // 42    002A
			"DEC HL",              // 43    002B
			"INC L",               // 44    002C
			"DEC L",               // 45    002D
			"LD L,#",              // 46    002E
			"CPL A",               // 47    002F
			"JR NC,@",             // 48    0030
			"LD SP,##",            // 49    0031
			"LDD (HL),A",          // 50    0032
			"INC SP",              // 51    0033
			"INC (HL)",            // 52    0034
			"DEC (HL)",            // 53    0035
			"LD (HL),#",           // 54    0036
			"SCF",                 // 55    0037
			"JR C,@",              // 56    0038
			"ADD HL,SP",           // 57    0039
			"LDD A,(HL)",          // 58    003A
			"DEC SP",              // 59    003B
			"INC A",               // 60    003C
			"DEC A",               // 61    003D
			"LD A,#",              // 62    003E
			"CCF",                 // 63    003F
			"LD B,B",              // 64    0040
			"LD B,C",              // 65    0041
			"LD B,D",              // 66    0042
			"LD B,E",              // 67    0043
			"LD B,H",              // 68    0044
			"LD B,L",              // 69    0045
			"LD B,(HL)",           // 70    0046
			"LD B,A",              // 71    0047
			"LD C,B",              // 72    0048
			"LD C,C",              // 73    0049
			"LD C,D",              // 74    004A
			"LD C,E",              // 75    004B
			"LD C,H",              // 76    004C
			"LD C,L",              // 77    004D
			"LD C,(HL)",           // 78    004E
			"LD C,A",              // 79    004F
			"LD D,B",              // 80    0050
			"LD D,C",              // 81    0051
			"LD D,D",              // 82    0052
			"LD D,E",              // 83    0053
			"LD D,H",              // 84    0054
			"LD D,L",              // 85    0055
			"LD D,(HL)",           // 86    0056
			"LD D,A",              // 87    0057
			"LD E,B",              // 88    0058
			"LD E,C",              // 89    0059
			"LD E,D",              // 90    005A
			"LD E,E",              // 91    005B
			"LD E,H",              // 92    005C
			"LD E,L",              // 93    005D
			"LD E,(HL)",           // 94    005E
			"LD E,A",              // 95    005F
			"LD H,B",              // 96    0060
			"LD H,C",              // 97    0061
			"LD H,D",              // 98    0062
			"LD H,E",              // 99    0063
			"LD H,H",              // 100   0064
			"LD H,L",              // 101   0065
			"LD H,(HL)",           // 102   0066
			"LD H,A",              // 103   0067
			"LD L,B",              // 104   0068
			"LD L,C",              // 105   0069
			"LD L,D",              // 106   006A
			"LD L,E",              // 107   006B
			"LD L,H",              // 108   006C
			"LD L,L",              // 109   006D
			"LD L,(HL)",           // 110   006E
			"LD L,A",              // 111   006F
			"LD (HL),B",           // 112   0070
			"LD (HL),C",           // 113   0071
			"LD (HL),D",           // 114   0072
			"LD (HL),E",           // 115   0073
			"LD (HL),H",           // 116   0074
			"LD (HL),L",           // 117   0075
			"HALT",                // 118   0076
			"LD (HL),A",           // 119   0077
			"LD A,B",              // 120   0078
			"LD A,C",              // 121   0079
			"LD A,D",              // 122   007A
			"LD A,E",              // 123   007B
			"LD A,H",              // 124   007C
			"LD A,L",              // 125   007D
			"LD A,(HL)",           // 126   007E
			"LD A,A",              // 127   007F
			"ADD B",               // 128   0080
			"ADD C",               // 129   0081
			"ADD D",               // 130   0082
			"ADD E",               // 131   0083
			"ADD H",               // 132   0084
			"ADD L",               // 133   0085
			"ADD (HL)",            // 134   0086
			"ADD A",               // 135   0087
			"ADC B",               // 136   0088
			"ADC C",               // 137   0089
			"ADC D",               // 138   008A
			"ADC E",               // 139   008B
			"ADC H",               // 140   008C
			"ADC L",               // 141   008D
			"ADC (HL)",            // 142   008E
			"ADC A",               // 143   008F
			"SUB B",               // 144   0090
			"SUB C",               // 145   0091
			"SUB D",               // 146   0092
			"SUB E",               // 147   0093
			"SUB H",               // 148   0094
			"SUB L",               // 149   0095
			"SUB (HL)",            // 150   0096
			"SUB A",               // 151   0097
			"SBC B",               // 152   0098
			"SBC C",               // 153   0099
			"SBC D",               // 154   009A
			"SBC E",               // 155   009B
			"SBC H",               // 156   009C
			"SBC L",               // 157   009D
			"SBC (HL)",            // 158   009E
			"SBC A",               // 159   009F
			"AND B",               // 160   00A0
			"AND C",               // 161   00A1
			"AND D",               // 162   00A2
			"AND E",               // 163   00A3
			"AND H",               // 164   00A4
			"AND L",               // 165   00A5
			"AND (HL)",            // 166   00A6
			"AND A",               // 167   00A7
			"XOR B",               // 168   00A8
			"XOR C",               // 169   00A9
			"XOR D",               // 170   00AA
			"XOR E",               // 171   00AB
			"XOR H",               // 172   00AC
			"XOR L",               // 173   00AD
			"XOR (HL)",            // 174   00AE
			"XOR A",               // 175   00AF
			"OR B",                // 176   00B0
			"OR C",                // 177   00B1
			"OR D",                // 178   00B2
			"OR E",                // 179   00B3
			"OR H",                // 180   00B4
			"OR L",                // 181   00B5
			"OR (HL)",             // 182   00B6
			"OR A",                // 183   00B7
			"CP B",                // 184   00B8
			"CP C",                // 185   00B9
			"CP D",                // 186   00BA
			"CP E",                // 187   00BB
			"CP H",                // 188   00BC
			"CP L",                // 189   00BD
			"CP (HL)",             // 190   00BE
			"CP A",                // 191   00BF
			"RET NZ",              // 192   00C0
			"POP BC",              // 193   00C1
			"JP NZ,##",            // 194   00C2
			"JP ##",               // 195   00C3
			"CALL NZ,##",          // 196   00C4
			"PUSH BC",             // 197   00C5
			"ADD #",               // 198   00C6
			"RST 00",              // 199   00C7
			"RET Z",               // 200   00C8
			"RET",                 // 201   00C9
			"JP Z,##",             // 202   00CA
			"bad opcode",          // 203   00CB
			"CALL Z,##",           // 204   00CC
			"CALL ##",             // 205   00CD
			"ADC #",               // 206   00CE
			"RST 08",              // 207   00CF
			"RET NC",              // 208   00D0
			"POP DE",              // 209   00D1
			"JP NC,##",            // 210   00D2
			"bad opcode",          // 211   00D3
			"CALL NC,##",          // 212   00D4
			"PUSH DE",             // 213   00D5
			"SUB #",               // 214   00D6
			"RST 10",              // 215   00D7
			"RET C",               // 216   00D8
			"RETI",                // 217   00D9
			"JP C,##",             // 218   00DA
			"bad opcode",          // 219   00DB
			"CALL C,##",           // 220   00DC
			"bad opcode",          // 221   00DD
			"SBC #",               // 222   00DE
			"RST 18",              // 223   00DF
			"LDH (#),A",           // 224   00E0
			"POP HL",              // 225   00E1
			"LDH (C),A",           // 226   00E2
			"bad opcode",          // 227   00E3
			"bad opcode",          // 228   00E4
			"PUSH HL",             // 229   00E5
			"AND #",               // 230   00E6
			"RST 20",              // 231   00E7
			"ADD SP,#",            // 232   00E8
			"JP HL",               // 233   00E9
			"LD (##),A",           // 234   00EA
			"bad opcode",          // 235   00EB
			"bad opcode",          // 236   00EC
			"bad opcode",          // 237   00ED
			"XOR #",               // 238   00EE
			"RST 28",              // 239   00EF
			"LDH A,(#)",           // 240   00F0
			"POP AF",              // 241   00F1
			"LDH A,(C)",           // 242   00F2
			"DI",                  // 243   00F3
			"bad opcode",          // 244   00F4
			"PUSH AF",             // 245   00F5
			"OR #",                // 246   00F6
			"RST 30",              // 247   00F7
			"LDHL SP,#",           // 248   00F8
			"LD SP,HL",            // 249   00F9
			"LD A,(##)",           // 250   00FA
			"EI",                  // 251   00FB
			"bad opcode",          // 252   00FC
			"bad opcode",          // 253   00FD
			"CP #",                // 254   00FE
			"RST 38",              // 255   00FF
		};

		#endregion

		#region --- Cycles ---

		static  int[] Cycles = new int[]
		{
			4,                   // 0     0000
			12,                  // 1     0001
			8,                   // 2     0002
			8,                   // 3     0003
			4,                   // 4     0004
			4,                   // 5     0005
			8,                   // 6     0006
			4,                   // 7     0007
			20,                  // 8     0008
			8,                   // 9     0009
			8,                   // 10    000A
			8,                   // 11    000B
			4,                   // 12    000C
			4,                   // 13    000D
			8,                   // 14    000E
			4,                   // 15    000F
			99,                  // 16    0010
			12,                  // 17    0011
			8,                   // 18    0012
			8,                   // 19    0013
			4,                   // 20    0014
			4,                   // 21    0015
			8,                   // 22    0016
			4,                   // 23    0017
			8,                   // 24    0018
			8,                   // 25    0019
			8,                   // 26    001A
			8,                   // 27    001B
			4,                   // 28    001C
			4,                   // 29    001D
			8,                   // 30    001E
			4,                   // 31    001F
			8,                   // 32    0020
			12,                  // 33    0021
			8,                   // 34    0022
			8,                   // 35    0023
			4,                   // 36    0024
			4,                   // 37    0025
			8,                   // 38    0026
			4,                   // 39    0027
			8,                   // 40    0028
			8,                   // 41    0029
			8,                   // 42    002A
			8,                   // 43    002B
			4,                   // 44    002C
			4,                   // 45    002D
			8,                   // 46    002E
			4,                   // 47    002F
			8,                   // 48    0030
			12,                  // 49    0031
			8,                   // 50    0032
			8,                   // 51    0033
			12,                  // 52    0034
			12,                  // 53    0035
			12,                  // 54    0036
			4,                   // 55    0037
			8,                   // 56    0038
			8,                   // 57    0039
			8,                   // 58    003A
			8,                   // 59    003B
			4,                   // 60    003C
			4,                   // 61    003D
			8,                   // 62    003E
			4,                   // 63    003F
			4,                   // 64    0040
			4,                   // 65    0041
			4,                   // 66    0042
			4,                   // 67    0043
			4,                   // 68    0044
			4,                   // 69    0045
			8,                   // 70    0046
			4,                   // 71    0047
			4,                   // 72    0048
			4,                   // 73    0049
			4,                   // 74    004A
			4,                   // 75    004B
			4,                   // 76    004C
			4,                   // 77    004D
			8,                   // 78    004E
			4,                   // 79    004F
			4,                   // 80    0050
			4,                   // 81    0051
			4,                   // 82    0052
			4,                   // 83    0053
			4,                   // 84    0054
			4,                   // 85    0055
			8,                   // 86    0056
			4,                   // 87    0057
			4,                   // 88    0058
			4,                   // 89    0059
			4,                   // 90    005A
			4,                   // 91    005B
			4,                   // 92    005C
			4,                   // 93    005D
			8,                   // 94    005E
			4,                   // 95    005F
			4,                   // 96    0060
			4,                   // 97    0061
			4,                   // 98    0062
			4,                   // 99    0063
			4,                   // 100   0064
			4,                   // 101   0065
			8,                   // 102   0066
			4,                   // 103   0067
			4,                   // 104   0068
			4,                   // 105   0069
			4,                   // 106   006A
			4,                   // 107   006B
			4,                   // 108   006C
			4,                   // 109   006D
			8,                   // 110   006E
			4,                   // 111   006F
			8,                   // 112   0070
			8,                   // 113   0071
			8,                   // 114   0072
			8,                   // 115   0073
			8,                   // 116   0074
			8,                   // 117   0075
			4,                   // 118   0076
			8,                   // 119   0077
			4,                   // 120   0078
			4,                   // 121   0079
			4,                   // 122   007A
			4,                   // 123   007B
			4,                   // 124   007C
			4,                   // 125   007D
			8,                   // 126   007E
			4,                   // 127   007F
			4,                   // 128   0080
			4,                   // 129   0081
			4,                   // 130   0082
			4,                   // 131   0083
			4,                   // 132   0084
			4,                   // 133   0085
			8,                   // 134   0086
			4,                   // 135   0087
			4,                   // 136   0088
			4,                   // 137   0089
			4,                   // 138   008A
			4,                   // 139   008B
			4,                   // 140   008C
			4,                   // 141   008D
			8,                   // 142   008E
			4,                   // 143   008F
			4,                   // 144   0090
			4,                   // 145   0091
			4,                   // 146   0092
			4,                   // 147   0093
			4,                   // 148   0094
			4,                   // 149   0095
			8,                   // 150   0096
			4,                   // 151   0097
			4,                   // 152   0098
			4,                   // 153   0099
			4,                   // 154   009A
			4,                   // 155   009B
			4,                   // 156   009C
			4,                   // 157   009D
			8,                   // 158   009E
			4,                   // 159   009F
			4,                   // 160   00A0
			4,                   // 161   00A1
			4,                   // 162   00A2
			4,                   // 163   00A3
			4,                   // 164   00A4
			4,                   // 165   00A5
			8,                   // 166   00A6
			4,                   // 167   00A7
			4,                   // 168   00A8
			4,                   // 169   00A9
			4,                   // 170   00AA
			4,                   // 171   00AB
			4,                   // 172   00AC
			4,                   // 173   00AD
			8,                   // 174   00AE
			4,                   // 175   00AF
			4,                   // 176   00B0
			4,                   // 177   00B1
			4,                   // 178   00B2
			4,                   // 179   00B3
			4,                   // 180   00B4
			4,                   // 181   00B5
			8,                   // 182   00B6
			4,                   // 183   00B7
			4,                   // 184   00B8
			4,                   // 185   00B9
			4,                   // 186   00BA
			4,                   // 187   00BB
			4,                   // 188   00BC
			4,                   // 189   00BD
			8,                   // 190   00BE
			4,                   // 191   00BF
			8,                   // 192   00C0
			12,                  // 193   00C1
			12,                  // 194   00C2
			12,                  // 195   00C3
			12,                  // 196   00C4
			16,                  // 197   00C5
			8,                   // 198   00C6
			32,                  // 199   00C7
			8,                   // 200   00C8
			8,                   // 201   00C9
			12,                  // 202   00CA
			0,                   // 203   00CB
			12,                  // 204   00CC
			12,                  // 205   00CD
			8,                   // 206   00CE
			32,                  // 207   00CF
			8,                   // 208   00D0
			12,                  // 209   00D1
			12,                  // 210   00D2
			99,                  // 211   00D3
			12,                  // 212   00D4
			16,                  // 213   00D5
			8,                   // 214   00D6
			32,                  // 215   00D7
			8,                   // 216   00D8
			8,                   // 217   00D9
			12,                  // 218   00DA
			99,                  // 219   00DB
			12,                  // 220   00DC
			99,                  // 221   00DD
			8,                   // 222   00DE
			32,                  // 223   00DF
			12,                  // 224   00E0
			12,                  // 225   00E1
			8,                   // 226   00E2
			99,                  // 227   00E3
			99,                  // 228   00E4
			16,                  // 229   00E5
			8,                   // 230   00E6
			32,                  // 231   00E7
			16,                  // 232   00E8
			4,                   // 233   00E9
			16,                  // 234   00EA
			99,                  // 235   00EB
			99,                  // 236   00EC
			99,                  // 237   00ED
			8,                   // 238   00EE
			32,                  // 239   00EF
			12,                  // 240   00F0
			12,                  // 241   00F1
			8,                   // 242   00F2
			4,                   // 243   00F3
			99,                  // 244   00F4
			16,                  // 245   00F5
			8,                   // 246   00F6
			32,                  // 247   00F7
			12,                  // 248   00F8
			8,                   // 249   00F9
			16,                  // 250   00FA
			4,                   // 251   00FB
			99,                  // 252   00FC
			99,                  // 253   00FD
			8,                   // 254   00FE
			32,                  // 255   00FF
		};

		#endregion

		#region --- MnemonicsCB ---

		static  string[] MnemonicsCB = new string[]
		{
			"RLC B",               // 0     0000
			"RLC C",               // 1     0001
			"RLC D",               // 2     0002
			"RLC E",               // 3     0003
			"RLC H",               // 4     0004
			"RLC L",               // 5     0005
			"RLC (HL)",            // 6     0006
			"RLC A",               // 7     0007
			"RRC B",               // 8     0008
			"RRC C",               // 9     0009
			"RRC D",               // 10    000A
			"RRC E",               // 11    000B
			"RRC H",               // 12    000C
			"RRC L",               // 13    000D
			"RRC (HL)",            // 14    000E
			"RRC A",               // 15    000F
			"RL B",                // 16    0010
			"RL C",                // 17    0011
			"RL D",                // 18    0012
			"RL E",                // 19    0013
			"RL H",                // 20    0014
			"RL L",                // 21    0015
			"RL (HL)",             // 22    0016
			"RL A",                // 23    0017
			"RR B",                // 24    0018
			"RR C",                // 25    0019
			"RR D",                // 26    001A
			"RR E",                // 27    001B
			"RR H",                // 28    001C
			"RR L",                // 29    001D
			"RR (HL)",             // 30    001E
			"RR A",                // 31    001F
			"SLA B",               // 32    0020
			"SLA C",               // 33    0021
			"SLA D",               // 34    0022
			"SLA E",               // 35    0023
			"SLA H",               // 36    0024
			"SLA L",               // 37    0025
			"SLA (HL)",            // 38    0026
			"SLA A",               // 39    0027
			"SRA B",               // 40    0028
			"SRA C",               // 41    0029
			"SRA D",               // 42    002A
			"SRA E",               // 43    002B
			"SRA H",               // 44    002C
			"SRA L",               // 45    002D
			"SRA (HL)",            // 46    002E
			"SRA A",               // 47    002F
			"SWAP B",              // 48    0030
			"SWAP C",              // 49    0031
			"SWAP D",              // 50    0032
			"SWAP E",              // 51    0033
			"SWAP H",              // 52    0034
			"SWAP L",              // 53    0035
			"SWAP (HL)",           // 54    0036
			"SWAP A",              // 55    0037
			"SRL B",               // 56    0038
			"SRL C",               // 57    0039
			"SRL D",               // 58    003A
			"SRL E",               // 59    003B
			"SRL H",               // 60    003C
			"SRL L",               // 61    003D
			"SRL (HL)",            // 62    003E
			"SRL A",               // 63    003F
			"BIT 0,B",             // 64    0040
			"BIT 0,C",             // 65    0041
			"BIT 0,D",             // 66    0042
			"BIT 0,E",             // 67    0043
			"BIT 0,H",             // 68    0044
			"BIT 0,L",             // 69    0045
			"BIT 0,(HL)",          // 70    0046
			"BIT 0,A",             // 71    0047
			"BIT 1,B",             // 72    0048
			"BIT 1,C",             // 73    0049
			"BIT 1,D",             // 74    004A
			"BIT 1,E",             // 75    004B
			"BIT 1,H",             // 76    004C
			"BIT 1,L",             // 77    004D
			"BIT 1,(HL)",          // 78    004E
			"BIT 1,A",             // 79    004F
			"BIT 2,B",             // 80    0050
			"BIT 2,C",             // 81    0051
			"BIT 2,D",             // 82    0052
			"BIT 2,E",             // 83    0053
			"BIT 2,H",             // 84    0054
			"BIT 2,L",             // 85    0055
			"BIT 2,(HL)",          // 86    0056
			"BIT 2,A",             // 87    0057
			"BIT 3,B",             // 88    0058
			"BIT 3,C",             // 89    0059
			"BIT 3,D",             // 90    005A
			"BIT 3,E",             // 91    005B
			"BIT 3,H",             // 92    005C
			"BIT 3,L",             // 93    005D
			"BIT 3,(HL)",          // 94    005E
			"BIT 3,A",             // 95    005F
			"BIT 4,B",             // 96    0060
			"BIT 4,C",             // 97    0061
			"BIT 4,D",             // 98    0062
			"BIT 4,E",             // 99    0063
			"BIT 4,H",             // 100   0064
			"BIT 4,L",             // 101   0065
			"BIT 4,(HL)",          // 102   0066
			"BIT 4,A",             // 103   0067
			"BIT 5,B",             // 104   0068
			"BIT 5,C",             // 105   0069
			"BIT 5,D",             // 106   006A
			"BIT 5,E",             // 107   006B
			"BIT 5,H",             // 108   006C
			"BIT 5,L",             // 109   006D
			"BIT 5,(HL)",          // 110   006E
			"BIT 5,A",             // 111   006F
			"BIT 6,B",             // 112   0070
			"BIT 6,C",             // 113   0071
			"BIT 6,D",             // 114   0072
			"BIT 6,E",             // 115   0073
			"BIT 6,H",             // 116   0074
			"BIT 6,L",             // 117   0075
			"BIT 6,(HL)",          // 118   0076
			"BIT 6,A",             // 119   0077
			"BIT 7,B",             // 120   0078
			"BIT 7,C",             // 121   0079
			"BIT 7,D",             // 122   007A
			"BIT 7,E",             // 123   007B
			"BIT 7,H",             // 124   007C
			"BIT 7,L",             // 125   007D
			"BIT 7,(HL)",          // 126   007E
			"BIT 7,A",             // 127   007F
			"RES 0,B",             // 128   0080
			"RES 0,C",             // 129   0081
			"RES 0,D",             // 130   0082
			"RES 0,E",             // 131   0083
			"RES 0,H",             // 132   0084
			"RES 0,L",             // 133   0085
			"RES 0,(HL)",          // 134   0086
			"RES 0,A",             // 135   0087
			"RES 1,B",             // 136   0088
			"RES 1,C",             // 137   0089
			"RES 1,D",             // 138   008A
			"RES 1,E",             // 139   008B
			"RES 1,H",             // 140   008C
			"RES 1,L",             // 141   008D
			"RES 1,(HL)",          // 142   008E
			"RES 1,A",             // 143   008F
			"RES 2,B",             // 144   0090
			"RES 2,C",             // 145   0091
			"RES 2,D",             // 146   0092
			"RES 2,E",             // 147   0093
			"RES 2,H",             // 148   0094
			"RES 2,L",             // 149   0095
			"RES 2,(HL)",          // 150   0096
			"RES 2,A",             // 151   0097
			"RES 3,B",             // 152   0098
			"RES 3,C",             // 153   0099
			"RES 3,D",             // 154   009A
			"RES 3,E",             // 155   009B
			"RES 3,H",             // 156   009C
			"RES 3,L",             // 157   009D
			"RES 3,(HL)",          // 158   009E
			"RES 3,A",             // 159   009F
			"RES 4,B",             // 160   00A0
			"RES 4,C",             // 161   00A1
			"RES 4,D",             // 162   00A2
			"RES 4,E",             // 163   00A3
			"RES 4,H",             // 164   00A4
			"RES 4,L",             // 165   00A5
			"RES 4,(HL)",          // 166   00A6
			"RES 4,A",             // 167   00A7
			"RES 5,B",             // 168   00A8
			"RES 5,C",             // 169   00A9
			"RES 5,D",             // 170   00AA
			"RES 5,E",             // 171   00AB
			"RES 5,H",             // 172   00AC
			"RES 5,L",             // 173   00AD
			"RES 5,(HL)",          // 174   00AE
			"RES 5,A",             // 175   00AF
			"RES 6,B",             // 176   00B0
			"RES 6,C",             // 177   00B1
			"RES 6,D",             // 178   00B2
			"RES 6,E",             // 179   00B3
			"RES 6,H",             // 180   00B4
			"RES 6,L",             // 181   00B5
			"RES 6,(HL)",          // 182   00B6
			"RES 6,A",             // 183   00B7
			"RES 7,B",             // 184   00B8
			"RES 7,C",             // 185   00B9
			"RES 7,D",             // 186   00BA
			"RES 7,E",             // 187   00BB
			"RES 7,H",             // 188   00BC
			"RES 7,L",             // 189   00BD
			"RES 7,(HL)",          // 190   00BE
			"RES 7,A",             // 191   00BF
			"SET 0,B",             // 192   00C0
			"SET 0,C",             // 193   00C1
			"SET 0,D",             // 194   00C2
			"SET 0,E",             // 195   00C3
			"SET 0,H",             // 196   00C4
			"SET 0,L",             // 197   00C5
			"SET 0,(HL)",          // 198   00C6
			"SET 0,A",             // 199   00C7
			"SET 1,B",             // 200   00C8
			"SET 1,C",             // 201   00C9
			"SET 1,D",             // 202   00CA
			"SET 1,E",             // 203   00CB
			"SET 1,H",             // 204   00CC
			"SET 1,L",             // 205   00CD
			"SET 1,(HL)",          // 206   00CE
			"SET 1,A",             // 207   00CF
			"SET 2,B",             // 208   00D0
			"SET 2,C",             // 209   00D1
			"SET 2,D",             // 210   00D2
			"SET 2,E",             // 211   00D3
			"SET 2,H",             // 212   00D4
			"SET 2,L",             // 213   00D5
			"SET 2,(HL)",          // 214   00D6
			"SET 2,A",             // 215   00D7
			"SET 3,B",             // 216   00D8
			"SET 3,C",             // 217   00D9
			"SET 3,D",             // 218   00DA
			"SET 3,E",             // 219   00DB
			"SET 3,H",             // 220   00DC
			"SET 3,L",             // 221   00DD
			"SET 3,(HL)",          // 222   00DE
			"SET 3,A",             // 223   00DF
			"SET 4,B",             // 224   00E0
			"SET 4,C",             // 225   00E1
			"SET 4,D",             // 226   00E2
			"SET 4,E",             // 227   00E3
			"SET 4,H",             // 228   00E4
			"SET 4,L",             // 229   00E5
			"SET 4,(HL)",          // 230   00E6
			"SET 4,A",             // 231   00E7
			"SET 5,B",             // 232   00E8
			"SET 5,C",             // 233   00E9
			"SET 5,D",             // 234   00EA
			"SET 5,E",             // 235   00EB
			"SET 5,H",             // 236   00EC
			"SET 5,L",             // 237   00ED
			"SET 5,(HL)",          // 238   00EE
			"SET 5,A",             // 239   00EF
			"SET 6,B",             // 240   00F0
			"SET 6,C",             // 241   00F1
			"SET 6,D",             // 242   00F2
			"SET 6,E",             // 243   00F3
			"SET 6,H",             // 244   00F4
			"SET 6,L",             // 245   00F5
			"SET 6,(HL)",          // 246   00F6
			"SET 6,A",             // 247   00F7
			"SET 7,B",             // 248   00F8
			"SET 7,C",             // 249   00F9
			"SET 7,D",             // 250   00FA
			"SET 7,E",             // 251   00FB
			"SET 7,H",             // 252   00FC
			"SET 7,L",             // 253   00FD
			"SET 7,(HL)",          // 254   00FE
			"SET 7,A",             // 255   00FF
		};

		#endregion

		#region --- CyclesCB ---

		static  int[] CyclesCB = new int[]
		{
			8,                   // 0     0000
			8,                   // 1     0001
			8,                   // 2     0002
			8,                   // 3     0003
			8,                   // 4     0004
			8,                   // 5     0005
			16,                  // 6     0006
			8,                   // 7     0007
			8,                   // 8     0008
			8,                   // 9     0009
			8,                   // 10    000A
			8,                   // 11    000B
			8,                   // 12    000C
			8,                   // 13    000D
			16,                  // 14    000E
			8,                   // 15    000F
			8,                   // 16    0010
			8,                   // 17    0011
			8,                   // 18    0012
			8,                   // 19    0013
			8,                   // 20    0014
			8,                   // 21    0015
			16,                  // 22    0016
			8,                   // 23    0017
			8,                   // 24    0018
			8,                   // 25    0019
			8,                   // 26    001A
			8,                   // 27    001B
			8,                   // 28    001C
			8,                   // 29    001D
			16,                  // 30    001E
			8,                   // 31    001F
			8,                   // 32    0020
			8,                   // 33    0021
			8,                   // 34    0022
			8,                   // 35    0023
			8,                   // 36    0024
			8,                   // 37    0025
			16,                  // 38    0026
			8,                   // 39    0027
			8,                   // 40    0028
			8,                   // 41    0029
			8,                   // 42    002A
			8,                   // 43    002B
			8,                   // 44    002C
			8,                   // 45    002D
			16,                  // 46    002E
			8,                   // 47    002F
			8,                   // 48    0030
			8,                   // 49    0031
			8,                   // 50    0032
			8,                   // 51    0033
			8,                   // 52    0034
			8,                   // 53    0035
			16,                  // 54    0036
			8,                   // 55    0037
			8,                   // 56    0038
			8,                   // 57    0039
			8,                   // 58    003A
			8,                   // 59    003B
			8,                   // 60    003C
			8,                   // 61    003D
			16,                  // 62    003E
			8,                   // 63    003F
			8,                   // 64    0040
			8,                   // 65    0041
			8,                   // 66    0042
			8,                   // 67    0043
			8,                   // 68    0044
			8,                   // 69    0045
			16,                  // 70    0046
			8,                   // 71    0047
			8,                   // 72    0048
			8,                   // 73    0049
			8,                   // 74    004A
			8,                   // 75    004B
			8,                   // 76    004C
			8,                   // 77    004D
			16,                  // 78    004E
			8,                   // 79    004F
			8,                   // 80    0050
			8,                   // 81    0051
			8,                   // 82    0052
			8,                   // 83    0053
			8,                   // 84    0054
			8,                   // 85    0055
			16,                  // 86    0056
			8,                   // 87    0057
			8,                   // 88    0058
			8,                   // 89    0059
			8,                   // 90    005A
			8,                   // 91    005B
			8,                   // 92    005C
			8,                   // 93    005D
			16,                  // 94    005E
			8,                   // 95    005F
			8,                   // 96    0060
			8,                   // 97    0061
			8,                   // 98    0062
			8,                   // 99    0063
			8,                   // 100   0064
			8,                   // 101   0065
			16,                  // 102   0066
			8,                   // 103   0067
			8,                   // 104   0068
			8,                   // 105   0069
			8,                   // 106   006A
			8,                   // 107   006B
			8,                   // 108   006C
			8,                   // 109   006D
			16,                  // 110   006E
			8,                   // 111   006F
			8,                   // 112   0070
			8,                   // 113   0071
			8,                   // 114   0072
			8,                   // 115   0073
			8,                   // 116   0074
			8,                   // 117   0075
			16,                  // 118   0076
			8,                   // 119   0077
			8,                   // 120   0078
			8,                   // 121   0079
			8,                   // 122   007A
			8,                   // 123   007B
			8,                   // 124   007C
			8,                   // 125   007D
			16,                  // 126   007E
			8,                   // 127   007F
			8,                   // 128   0080
			8,                   // 129   0081
			8,                   // 130   0082
			8,                   // 131   0083
			8,                   // 132   0084
			8,                   // 133   0085
			16,                  // 134   0086
			8,                   // 135   0087
			8,                   // 136   0088
			8,                   // 137   0089
			8,                   // 138   008A
			8,                   // 139   008B
			8,                   // 140   008C
			8,                   // 141   008D
			16,                  // 142   008E
			8,                   // 143   008F
			8,                   // 144   0090
			8,                   // 145   0091
			8,                   // 146   0092
			8,                   // 147   0093
			8,                   // 148   0094
			8,                   // 149   0095
			16,                  // 150   0096
			8,                   // 151   0097
			8,                   // 152   0098
			8,                   // 153   0099
			8,                   // 154   009A
			8,                   // 155   009B
			8,                   // 156   009C
			8,                   // 157   009D
			16,                  // 158   009E
			8,                   // 159   009F
			8,                   // 160   00A0
			8,                   // 161   00A1
			8,                   // 162   00A2
			8,                   // 163   00A3
			8,                   // 164   00A4
			8,                   // 165   00A5
			16,                  // 166   00A6
			8,                   // 167   00A7
			8,                   // 168   00A8
			8,                   // 169   00A9
			8,                   // 170   00AA
			8,                   // 171   00AB
			8,                   // 172   00AC
			8,                   // 173   00AD
			16,                  // 174   00AE
			8,                   // 175   00AF
			8,                   // 176   00B0
			8,                   // 177   00B1
			8,                   // 178   00B2
			8,                   // 179   00B3
			8,                   // 180   00B4
			8,                   // 181   00B5
			16,                  // 182   00B6
			8,                   // 183   00B7
			8,                   // 184   00B8
			8,                   // 185   00B9
			8,                   // 186   00BA
			8,                   // 187   00BB
			8,                   // 188   00BC
			8,                   // 189   00BD
			16,                  // 190   00BE
			8,                   // 191   00BF
			8,                   // 192   00C0
			8,                   // 193   00C1
			8,                   // 194   00C2
			8,                   // 195   00C3
			8,                   // 196   00C4
			8,                   // 197   00C5
			16,                  // 198   00C6
			8,                   // 199   00C7
			8,                   // 200   00C8
			8,                   // 201   00C9
			8,                   // 202   00CA
			8,                   // 203   00CB
			8,                   // 204   00CC
			8,                   // 205   00CD
			16,                  // 206   00CE
			8,                   // 207   00CF
			8,                   // 208   00D0
			8,                   // 209   00D1
			8,                   // 210   00D2
			8,                   // 211   00D3
			8,                   // 212   00D4
			8,                   // 213   00D5
			16,                  // 214   00D6
			8,                   // 215   00D7
			8,                   // 216   00D8
			8,                   // 217   00D9
			8,                   // 218   00DA
			8,                   // 219   00DB
			8,                   // 220   00DC
			8,                   // 221   00DD
			16,                  // 222   00DE
			8,                   // 223   00DF
			8,                   // 224   00E0
			8,                   // 225   00E1
			8,                   // 226   00E2
			8,                   // 227   00E3
			8,                   // 228   00E4
			8,                   // 229   00E5
			16,                  // 230   00E6
			8,                   // 231   00E7
			8,                   // 232   00E8
			8,                   // 233   00E9
			8,                   // 234   00EA
			8,                   // 235   00EB
			8,                   // 236   00EC
			8,                   // 237   00ED
			16,                  // 238   00EE
			8,                   // 239   00EF
			8,                   // 240   00F0
			8,                   // 241   00F1
			8,                   // 242   00F2
			8,                   // 243   00F3
			8,                   // 244   00F4
			8,                   // 245   00F5
			16,                  // 246   00F6
			8,                   // 247   00F7
			8,                   // 248   00F8
			8,                   // 249   00F9
			8,                   // 250   00FA
			8,                   // 251   00FB
			8,                   // 252   00FC
			8,                   // 253   00FD
			16,                  // 254   00FE
			8,                   // 255   00FF
		};

		#endregion

		public const uint FlagSet_Z = 0x0080;
		public const uint FlagSet_N = 0x0040;
		public const uint FlagSet_H = 0x0020;
		public const uint FlagSet_C = 0x0010;
		public const uint FlagSet_IME = 0x0002;
		public const uint FlagReset_Z = 0xFFFFFF7F;
		public const uint FlagReset_N = 0xFFFFFFBF;
		public const uint FlagReset_H = 0xFFFFFFDF;
		public const uint FlagReset_C = 0xFFFFFFEF;
		public const uint FlagReset_IME = 0xFFFFFFFD;
		int cpuCycles;

		public void ExecOpCode()
		{
			OpCode instruction;

			instruction = (OpCode)(Memory[registers.PC]);
			registers.PC++;
			cpuCycles += Cycles[(int)instruction];

			switch (instruction)
			{
				case OpCode.HALT:
					{
						registers.halt = (byte)(1);
					}
					break;

				case OpCode.RET:
					{
						ushort hb = (ushort)(Memory[registers.SP + 1] << 8);
						ushort lb = (ushort)(Memory[registers.SP]);

						registers.PC = (ushort)(hb + lb);
						registers.SP = (ushort)(registers.SP + 0x2);
					}
					break;

				case OpCode.RET_NZ:
					{
						if (0 == (0x01 & (registers.F >> 7)))
						{
							ushort hb = (ushort)(Memory[registers.SP + 1] << 8);
							ushort lb = (ushort)(Memory[registers.SP]);

							registers.PC = (ushort)(hb + lb);
							registers.SP = (ushort)(registers.SP + 0x2);
						}
					}
					break;

				case OpCode.RET_Z:
					{
						if (0 != (0x01 & (registers.F >> 7)))
						{
							ushort hb = (ushort)(Memory[registers.SP + 1] << 8);
							ushort lb = (ushort)(Memory[registers.SP]);

							registers.PC = (ushort)(hb + lb);
							registers.SP = (ushort)(registers.SP + 0x2);
						}
					}
					break;

				case OpCode.RET_NC:
					{
						if (0 == (0x01 & (registers.F >> 4)))
						{
							ushort hb = (ushort)(Memory[registers.SP + 1] << 8);
							ushort lb = (ushort)(Memory[registers.SP]);

							registers.PC = (ushort)(hb + lb);
							registers.SP = (ushort)(registers.SP + 0x2);
						}
					}
					break;

				case OpCode.RET_C:
					{
						if (0 != (0x01 & (registers.F >> 4)))
						{
							ushort hb = (ushort)(Memory[registers.SP + 1] << 8);
							ushort lb = (ushort)(Memory[registers.SP]);

							registers.PC = (ushort)(hb + lb);
							registers.SP = (ushort)(registers.SP + 0x2);
						}
					}
					break;

				case OpCode.RETI:
					{
						ushort hb = (ushort)(Memory[registers.SP + 1] << 8);
						ushort lb = (ushort)(Memory[registers.SP]);

						registers.PC = (ushort)(hb + lb);
						registers.SP = (ushort)(registers.SP + 0x2);
						registers.IME = (byte)(registers.IME | FlagSet_IME);
					}
					break;

				case OpCode.EI:
					{
						registers.EI_ = (byte)(2);
					}
					break;

				case OpCode.DI:
					{
						registers.DI_ = (byte)(2);
					}
					break;

				case OpCode.RR_A:
					{
						byte carry = (byte)(registers.A & 0x1);
						byte inp = (byte)(((registers.F >> 4) & 0x1) << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A >> 1);
						registers.A = (byte)(registers.A + inp);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCode.RRC_A:
					{
						byte carry = (byte)(registers.A & 0x1);
						byte inp = (byte)(carry << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A >> 1);
						registers.A = (byte)(registers.A + inp);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCode.RL_A:
					{
						byte carry = (byte)(registers.A & 0x80);
						byte inp = (byte)(((registers.F >> 4) & 0x1));

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A << 1);
						registers.A = (byte)(registers.A + inp);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCode.RLC_A:
					{
						byte carry = (byte)(registers.A & 0x80);
						byte inp = (byte)(carry >> 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A << 1);
						registers.A = (byte)(registers.A + inp);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCode.INC_BC:
					{
						registers.BC = (ushort)(registers.BC + 0x1);
					}
					break;

				case OpCode.INC_DE:
					{
						registers.DE = (ushort)(registers.DE + 0x1);
					}
					break;

				case OpCode.INC_HL:
					{
						registers.HL = (ushort)(registers.HL + 0x1);
					}
					break;

				case OpCode.INC_SP:
					{
						registers.SP = (ushort)(registers.SP + 0x1);
					}
					break;

				case OpCode.DEC_BC:
					{
						registers.BC = (ushort)(registers.BC - 0x1);
					}
					break;

				case OpCode.DEC_DE:
					{
						registers.DE = (ushort)(registers.DE - 0x1);
					}
					break;

				case OpCode.DEC_HL:
					{
						registers.HL = (ushort)(registers.HL - 0x1);
					}
					break;

				case OpCode.DEC_SP:
					{
						registers.SP = (ushort)(registers.SP - 0x1);
					}
					break;

				case OpCode.ADD_HL_BC:
					{
						uint res = (uint)(registers.HL + registers.BC);
						ushort aln = (ushort)(registers.HL & 0xFFF);
						ushort bln = (ushort)(registers.BC & 0xFFF);

						if (aln + bln > 0xFFF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFFFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.HL = (ushort)(res);
					}
					break;

				case OpCode.ADD_HL_DE:
					{
						uint res = (uint)(registers.HL + registers.DE);
						ushort aln = (ushort)(registers.HL & 0xFFF);
						ushort bln = (ushort)(registers.DE & 0xFFF);

						if (aln + bln > 0xFFF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFFFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.HL = (ushort)(res);
					}
					break;

				case OpCode.ADD_HL_HL:
					{
						uint res = (uint)(registers.HL + registers.HL);
						ushort aln = (ushort)(registers.HL & 0xFFF);
						ushort bln = (ushort)(registers.HL & 0xFFF);

						if (aln + bln > 0xFFF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFFFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.HL = (ushort)(res);
					}
					break;

				case OpCode.ADD_HL_SP:
					{
						uint res = (uint)(registers.HL + registers.SP);
						ushort aln = (ushort)(registers.HL & 0xFFF);
						ushort bln = (ushort)(registers.SP & 0xFFF);

						if (aln + bln > 0xFFF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFFFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.HL = (ushort)(res);
					}
					break;

				case OpCode.ADD_SP_n:
					{
						sbyte offset = (sbyte)(Memory[registers.PC]);
						byte aln = (byte)(registers.SP & 0x0F);
						byte bln = (byte)(Memory[registers.PC] & 0x0F);
						byte alb = (byte)(registers.SP & 0xFF);

						if (aln + bln > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (alb + Memory[registers.PC] > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_Z);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.SP = (ushort)(registers.SP + offset);
						registers.PC += 1;
					}
					break;

				case OpCode.SCF:
					{
						registers.F = (byte)(registers.F | FlagSet_C);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
					}
					break;

				case OpCode.CCF:
					{
						registers.F = (byte)(registers.F ^ FlagSet_C);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
					}
					break;

				case OpCode.CPL_A:
					{
						registers.A = (byte)(~ registers.A);
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.F = (byte)(registers.F | FlagSet_H);
					}
					break;

				case OpCode.CALL_nn:
					{
						ushort ret = (ushort)(registers.PC + 2);

						int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

						Memory[registers.SP - 1] = (byte)(ret >> 0x8);
						Memory[registers.SP - 2] = (byte)(ret & 0xff);
						registers.SP = (ushort)(registers.SP - 0x2);
						registers.PC = (ushort)(tmp0);
					}
					break;

				case OpCode.CALL_NZ_nn:
					{
						if (0 == (0x01 & (registers.F >> 7)))
						{
							ushort ret = (ushort)(registers.PC + 2);

							int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

							Memory[registers.SP - 1] = (byte)(ret >> 0x8);
							Memory[registers.SP - 2] = (byte)(ret & 0xff);
							registers.SP = (ushort)(registers.SP - 0x2);
							registers.PC = (ushort)(tmp0);
						}
						else
						{
							registers.PC += 2;
						}
					}
					break;

				case OpCode.CALL_Z_nn:
					{
						if (0 != (0x01 & (registers.F >> 7)))
						{
							ushort ret = (ushort)(registers.PC + 2);

							int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

							Memory[registers.SP - 1] = (byte)(ret >> 0x8);
							Memory[registers.SP - 2] = (byte)(ret & 0xff);
							registers.SP = (ushort)(registers.SP - 0x2);
							registers.PC = (ushort)(tmp0);
						}
						else
						{
							registers.PC += 2;
						}
					}
					break;

				case OpCode.CALL_NC_nn:
					{
						if (0 == (0x01 & (registers.F >> 4)))
						{
							ushort ret = (ushort)(registers.PC + 2);

							int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

							Memory[registers.SP - 1] = (byte)(ret >> 0x8);
							Memory[registers.SP - 2] = (byte)(ret & 0xff);
							registers.SP = (ushort)(registers.SP - 0x2);
							registers.PC = (ushort)(tmp0);
						}
						else
						{
							registers.PC += 2;
						}
					}
					break;

				case OpCode.CALL_C_nn:
					{
						if (0 != (0x01 & (registers.F >> 4)))
						{
							ushort ret = (ushort)(registers.PC + 2);

							int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

							Memory[registers.SP - 1] = (byte)(ret >> 0x8);
							Memory[registers.SP - 2] = (byte)(ret & 0xff);
							registers.SP = (ushort)(registers.SP - 0x2);
							registers.PC = (ushort)(tmp0);
						}
						else
						{
							registers.PC += 2;
						}
					}
					break;

				case OpCode.RST_00:
					{
						ushort ret = (ushort)(registers.PC + 0);

						Memory[registers.SP - 1] = (byte)(ret >> 0x8);
						Memory[registers.SP - 2] = (byte)(ret & 0xff);
						registers.SP = (ushort)(registers.SP - 0x2);
						registers.PC = (ushort)(0x00);
					}
					break;

				case OpCode.RST_08:
					{
						ushort ret = (ushort)(registers.PC + 0);

						Memory[registers.SP - 1] = (byte)(ret >> 0x8);
						Memory[registers.SP - 2] = (byte)(ret & 0xff);
						registers.SP = (ushort)(registers.SP - 0x2);
						registers.PC = (ushort)(0x08);
					}
					break;

				case OpCode.RST_10:
					{
						ushort ret = (ushort)(registers.PC + 0);

						Memory[registers.SP - 1] = (byte)(ret >> 0x8);
						Memory[registers.SP - 2] = (byte)(ret & 0xff);
						registers.SP = (ushort)(registers.SP - 0x2);
						registers.PC = (ushort)(0x10);
					}
					break;

				case OpCode.RST_18:
					{
						ushort ret = (ushort)(registers.PC + 0);

						Memory[registers.SP - 1] = (byte)(ret >> 0x8);
						Memory[registers.SP - 2] = (byte)(ret & 0xff);
						registers.SP = (ushort)(registers.SP - 0x2);
						registers.PC = (ushort)(0x18);
					}
					break;

				case OpCode.RST_20:
					{
						ushort ret = (ushort)(registers.PC + 0);

						Memory[registers.SP - 1] = (byte)(ret >> 0x8);
						Memory[registers.SP - 2] = (byte)(ret & 0xff);
						registers.SP = (ushort)(registers.SP - 0x2);
						registers.PC = (ushort)(0x20);
					}
					break;

				case OpCode.RST_28:
					{
						ushort ret = (ushort)(registers.PC + 0);

						Memory[registers.SP - 1] = (byte)(ret >> 0x8);
						Memory[registers.SP - 2] = (byte)(ret & 0xff);
						registers.SP = (ushort)(registers.SP - 0x2);
						registers.PC = (ushort)(0x28);
					}
					break;

				case OpCode.RST_30:
					{
						ushort ret = (ushort)(registers.PC + 0);

						Memory[registers.SP - 1] = (byte)(ret >> 0x8);
						Memory[registers.SP - 2] = (byte)(ret & 0xff);
						registers.SP = (ushort)(registers.SP - 0x2);
						registers.PC = (ushort)(0x30);
					}
					break;

				case OpCode.RST_38:
					{
						ushort ret = (ushort)(registers.PC + 0);

						Memory[registers.SP - 1] = (byte)(ret >> 0x8);
						Memory[registers.SP - 2] = (byte)(ret & 0xff);
						registers.SP = (ushort)(registers.SP - 0x2);
						registers.PC = (ushort)(0x38);
					}
					break;

				case OpCode.JR_s:
					{
						sbyte offset = (sbyte)(Memory[registers.PC]);

						registers.PC = (ushort)(registers.PC + offset + 1);
					}
					break;

				case OpCode.JR_NZ_s:
					{
						if (0 == (0x01 & (registers.F >> 7)))
						{
							sbyte offset = (sbyte)(Memory[registers.PC]);

							registers.PC = (ushort)(registers.PC + offset + 1);
						}
						else
						{
							registers.PC += 1;
						}
					}
					break;

				case OpCode.JR_Z_s:
					{
						if (0 != (0x01 & (registers.F >> 7)))
						{
							sbyte offset = (sbyte)(Memory[registers.PC]);

							registers.PC = (ushort)(registers.PC + offset + 1);
						}
						else
						{
							registers.PC += 1;
						}
					}
					break;

				case OpCode.JR_NC_s:
					{
						if (0 == (0x01 & (registers.F >> 4)))
						{
							sbyte offset = (sbyte)(Memory[registers.PC]);

							registers.PC = (ushort)(registers.PC + offset + 1);
						}
						else
						{
							registers.PC += 1;
						}
					}
					break;

				case OpCode.JR_C_s:
					{
						if (0 != (0x01 & (registers.F >> 4)))
						{
							sbyte offset = (sbyte)(Memory[registers.PC]);

							registers.PC = (ushort)(registers.PC + offset + 1);
						}
						else
						{
							registers.PC += 1;
						}
					}
					break;

				case OpCode.JP_nn:
					{
						int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

						registers.PC = (ushort)(tmp0);
					}
					break;

				case OpCode.JP_NZ_nn:
					{
						if (0 == (0x01 & (registers.F >> 7)))
						{
							int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

							registers.PC = (ushort)(tmp0);
						}
						else
						{
							registers.PC += 2;
						}
					}
					break;

				case OpCode.JP_Z_nn:
					{
						if (0 != (0x01 & (registers.F >> 7)))
						{
							int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

							registers.PC = (ushort)(tmp0);
						}
						else
						{
							registers.PC += 2;
						}
					}
					break;

				case OpCode.JP_NC_nn:
					{
						if (0 == (0x01 & (registers.F >> 4)))
						{
							int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

							registers.PC = (ushort)(tmp0);
						}
						else
						{
							registers.PC += 2;
						}
					}
					break;

				case OpCode.JP_C_nn:
					{
						if (0 != (0x01 & (registers.F >> 4)))
						{
							int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

							registers.PC = (ushort)(tmp0);
						}
						else
						{
							registers.PC += 2;
						}
					}
					break;

				case OpCode.JP_HL:
					{
						registers.PC = (ushort)(registers.HL);
					}
					break;

				case OpCode.NOP:
					{
					}
					break;

				case OpCode.INC_A:
					{
						byte f1 = (byte)(registers.A & 0xF);

						if (f1 == 0xF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(registers.A + 0x1);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.INC_B:
					{
						byte f1 = (byte)(registers.B & 0xF);

						if (f1 == 0xF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.B = (byte)(registers.B + 0x1);
						if (registers.B == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.INC_C:
					{
						byte f1 = (byte)(registers.C & 0xF);

						if (f1 == 0xF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.C = (byte)(registers.C + 0x1);
						if (registers.C == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.INC_D:
					{
						byte f1 = (byte)(registers.D & 0xF);

						if (f1 == 0xF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.D = (byte)(registers.D + 0x1);
						if (registers.D == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.INC_E:
					{
						byte f1 = (byte)(registers.E & 0xF);

						if (f1 == 0xF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.E = (byte)(registers.E + 0x1);
						if (registers.E == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.INC_H:
					{
						byte f1 = (byte)(registers.H & 0xF);

						if (f1 == 0xF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.H = (byte)(registers.H + 0x1);
						if (registers.H == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.INC_L:
					{
						byte f1 = (byte)(registers.L & 0xF);

						if (f1 == 0xF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.L = (byte)(registers.L + 0x1);
						if (registers.L == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.INC_xHL:
					{
						byte f1 = (byte)(Memory[registers.HL] & 0xF);

						if (f1 == 0xF)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						Memory[registers.HL] = (byte)(Memory[registers.HL] + 0x1);
						if (Memory[registers.HL] == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.DEC_A:
					{
						byte f1 = (byte)(registers.A & 0xF);

						if (f1 == 0x0)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(registers.A - 0x1);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.DEC_B:
					{
						byte f1 = (byte)(registers.B & 0xF);

						if (f1 == 0x0)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.B = (byte)(registers.B - 0x1);
						if (registers.B == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.DEC_C:
					{
						byte f1 = (byte)(registers.C & 0xF);

						if (f1 == 0x0)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.C = (byte)(registers.C - 0x1);
						if (registers.C == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.DEC_D:
					{
						byte f1 = (byte)(registers.D & 0xF);

						if (f1 == 0x0)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.D = (byte)(registers.D - 0x1);
						if (registers.D == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.DEC_E:
					{
						byte f1 = (byte)(registers.E & 0xF);

						if (f1 == 0x0)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.E = (byte)(registers.E - 0x1);
						if (registers.E == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.DEC_H:
					{
						byte f1 = (byte)(registers.H & 0xF);

						if (f1 == 0x0)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.H = (byte)(registers.H - 0x1);
						if (registers.H == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.DEC_L:
					{
						byte f1 = (byte)(registers.L & 0xF);

						if (f1 == 0x0)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.L = (byte)(registers.L - 0x1);
						if (registers.L == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.DEC_xHL:
					{
						byte f1 = (byte)(Memory[registers.HL] & 0xF);

						if (f1 == 0x0)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						Memory[registers.HL] = (byte)(Memory[registers.HL] - 0x1);
						if (Memory[registers.HL] == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.CP_A:
					{
						byte f1 = (byte)(registers.A - registers.A);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.A & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.A)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.CP_B:
					{
						byte f1 = (byte)(registers.A - registers.B);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.B & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.B)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.CP_C:
					{
						byte f1 = (byte)(registers.A - registers.C);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.C & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.C)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.CP_D:
					{
						byte f1 = (byte)(registers.A - registers.D);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.D & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.D)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.CP_E:
					{
						byte f1 = (byte)(registers.A - registers.E);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.E & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.E)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.CP_H:
					{
						byte f1 = (byte)(registers.A - registers.H);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.H & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.H)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.CP_L:
					{
						byte f1 = (byte)(registers.A - registers.L);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.L & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.L)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.CP_xHL:
					{
						byte f1 = (byte)(registers.A - Memory[registers.HL]);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(Memory[registers.HL] & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < Memory[registers.HL])
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.CP_n:
					{
						byte f1 = (byte)(registers.A - Memory[registers.PC]);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(Memory[registers.PC] & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < Memory[registers.PC])
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.PC += 1;
					}
					break;

				case OpCode.AND_A:
					{
						registers.A = (byte)(registers.A & registers.A);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.AND_B:
					{
						registers.A = (byte)(registers.A & registers.B);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.AND_C:
					{
						registers.A = (byte)(registers.A & registers.C);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.AND_D:
					{
						registers.A = (byte)(registers.A & registers.D);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.AND_E:
					{
						registers.A = (byte)(registers.A & registers.E);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.AND_H:
					{
						registers.A = (byte)(registers.A & registers.H);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.AND_L:
					{
						registers.A = (byte)(registers.A & registers.L);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.AND_xHL:
					{
						registers.A = (byte)(registers.A & Memory[registers.HL]);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.AND_n:
					{
						registers.A = (byte)(registers.A & Memory[registers.PC]);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
						registers.PC += 1;
					}
					break;

				case OpCode.OR_A:
					{
						registers.A = (byte)(registers.A | registers.A);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.OR_B:
					{
						registers.A = (byte)(registers.A | registers.B);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.OR_C:
					{
						registers.A = (byte)(registers.A | registers.C);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.OR_D:
					{
						registers.A = (byte)(registers.A | registers.D);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.OR_E:
					{
						registers.A = (byte)(registers.A | registers.E);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.OR_H:
					{
						registers.A = (byte)(registers.A | registers.H);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.OR_L:
					{
						registers.A = (byte)(registers.A | registers.L);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.OR_xHL:
					{
						registers.A = (byte)(registers.A | Memory[registers.HL]);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.OR_n:
					{
						registers.A = (byte)(registers.A | Memory[registers.PC]);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
						registers.PC += 1;
					}
					break;

				case OpCode.XOR_A:
					{
						registers.A = (byte)(registers.A ^ registers.A);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.XOR_B:
					{
						registers.A = (byte)(registers.A ^ registers.B);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.XOR_C:
					{
						registers.A = (byte)(registers.A ^ registers.C);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.XOR_D:
					{
						registers.A = (byte)(registers.A ^ registers.D);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.XOR_E:
					{
						registers.A = (byte)(registers.A ^ registers.E);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.XOR_H:
					{
						registers.A = (byte)(registers.A ^ registers.H);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.XOR_L:
					{
						registers.A = (byte)(registers.A ^ registers.L);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.XOR_xHL:
					{
						registers.A = (byte)(registers.A ^ Memory[registers.HL]);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
					}
					break;

				case OpCode.XOR_n:
					{
						registers.A = (byte)(registers.A ^ Memory[registers.PC]);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_C);
						registers.PC += 1;
					}
					break;

				case OpCode.ADC_A:
					{
						ushort res = (ushort)(registers.A + registers.A + ((registers.F >> 4) & 0x1));
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.A & 0x0F);

						if (aln + bln + ((registers.F >> 4) & 0x1) > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADC_B:
					{
						ushort res = (ushort)(registers.A + registers.B + ((registers.F >> 4) & 0x1));
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.B & 0x0F);

						if (aln + bln + ((registers.F >> 4) & 0x1) > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADC_C:
					{
						ushort res = (ushort)(registers.A + registers.C + ((registers.F >> 4) & 0x1));
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.C & 0x0F);

						if (aln + bln + ((registers.F >> 4) & 0x1) > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADC_D:
					{
						ushort res = (ushort)(registers.A + registers.D + ((registers.F >> 4) & 0x1));
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.D & 0x0F);

						if (aln + bln + ((registers.F >> 4) & 0x1) > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADC_E:
					{
						ushort res = (ushort)(registers.A + registers.E + ((registers.F >> 4) & 0x1));
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.E & 0x0F);

						if (aln + bln + ((registers.F >> 4) & 0x1) > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADC_H:
					{
						ushort res = (ushort)(registers.A + registers.H + ((registers.F >> 4) & 0x1));
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.H & 0x0F);

						if (aln + bln + ((registers.F >> 4) & 0x1) > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADC_L:
					{
						ushort res = (ushort)(registers.A + registers.L + ((registers.F >> 4) & 0x1));
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.L & 0x0F);

						if (aln + bln + ((registers.F >> 4) & 0x1) > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADC_xHL:
					{
						ushort res = (ushort)(registers.A + Memory[registers.HL] + ((registers.F >> 4) & 0x1));
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(Memory[registers.HL] & 0x0F);

						if (aln + bln + ((registers.F >> 4) & 0x1) > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADC_n:
					{
						ushort res = (ushort)(registers.A + Memory[registers.PC] + ((registers.F >> 4) & 0x1));
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(Memory[registers.PC] & 0x0F);

						if (aln + bln + ((registers.F >> 4) & 0x1) > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.PC += 1;
					}
					break;

				case OpCode.ADD_A:
					{
						ushort res = (ushort)(registers.A + registers.A);
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.A & 0x0F);

						if (aln + bln > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADD_B:
					{
						ushort res = (ushort)(registers.A + registers.B);
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.B & 0x0F);

						if (aln + bln > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADD_C:
					{
						ushort res = (ushort)(registers.A + registers.C);
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.C & 0x0F);

						if (aln + bln > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADD_D:
					{
						ushort res = (ushort)(registers.A + registers.D);
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.D & 0x0F);

						if (aln + bln > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADD_E:
					{
						ushort res = (ushort)(registers.A + registers.E);
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.E & 0x0F);

						if (aln + bln > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADD_H:
					{
						ushort res = (ushort)(registers.A + registers.H);
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.H & 0x0F);

						if (aln + bln > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADD_L:
					{
						ushort res = (ushort)(registers.A + registers.L);
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(registers.L & 0x0F);

						if (aln + bln > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADD_xHL:
					{
						ushort res = (ushort)(registers.A + Memory[registers.HL]);
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(Memory[registers.HL] & 0x0F);

						if (aln + bln > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.ADD_n:
					{
						ushort res = (ushort)(registers.A + Memory[registers.PC]);
						byte aln = (byte)(registers.A & 0x0F);
						byte bln = (byte)(Memory[registers.PC] & 0x0F);

						if (aln + bln > 0x0F)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (res > 0xFF)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F & FlagReset_N);
						registers.A = (byte)(res);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.PC += 1;
					}
					break;

				case OpCode.SUB_A:
					{
						byte f1 = (byte)(registers.A - registers.A);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.A & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.A)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SUB_B:
					{
						byte f1 = (byte)(registers.A - registers.B);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.B & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.B)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SUB_C:
					{
						byte f1 = (byte)(registers.A - registers.C);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.C & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.C)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SUB_D:
					{
						byte f1 = (byte)(registers.A - registers.D);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.D & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.D)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SUB_E:
					{
						byte f1 = (byte)(registers.A - registers.E);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.E & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.E)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SUB_H:
					{
						byte f1 = (byte)(registers.A - registers.H);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.H & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.H)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SUB_L:
					{
						byte f1 = (byte)(registers.A - registers.L);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(registers.L & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < registers.L)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SUB_xHL:
					{
						byte f1 = (byte)(registers.A - Memory[registers.HL]);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(Memory[registers.HL] & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < Memory[registers.HL])
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SUB_n:
					{
						byte f1 = (byte)(registers.A - Memory[registers.PC]);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(Memory[registers.PC] & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < Memory[registers.PC])
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.PC += 1;
					}
					break;

				case OpCode.SBC_A:
					{
						byte val = (byte)(registers.A + ((registers.F >> 4) & 0x1));
						byte f1 = (byte)(registers.A - val);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(val & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < val)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SBC_B:
					{
						byte val = (byte)(registers.B + ((registers.F >> 4) & 0x1));
						byte f1 = (byte)(registers.A - val);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(val & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < val)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SBC_C:
					{
						byte val = (byte)(registers.C + ((registers.F >> 4) & 0x1));
						byte f1 = (byte)(registers.A - val);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(val & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < val)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SBC_D:
					{
						byte val = (byte)(registers.D + ((registers.F >> 4) & 0x1));
						byte f1 = (byte)(registers.A - val);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(val & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < val)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SBC_E:
					{
						byte val = (byte)(registers.E + ((registers.F >> 4) & 0x1));
						byte f1 = (byte)(registers.A - val);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(val & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < val)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SBC_H:
					{
						byte val = (byte)(registers.H + ((registers.F >> 4) & 0x1));
						byte f1 = (byte)(registers.A - val);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(val & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < val)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SBC_L:
					{
						byte val = (byte)(registers.L + ((registers.F >> 4) & 0x1));
						byte f1 = (byte)(registers.A - val);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(val & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < val)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SBC_xHL:
					{
						byte val = (byte)(Memory[registers.HL] + ((registers.F >> 4) & 0x1));
						byte f1 = (byte)(registers.A - val);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(val & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < val)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.SBC_n:
					{
						byte val = (byte)(Memory[registers.PC] + ((registers.F >> 4) & 0x1));
						byte f1 = (byte)(registers.A - val);
						byte f2 = (byte)(registers.A & 0xF);
						byte f3 = (byte)(val & 0xF);

						if (f2 < f3)
						{
							registers.F = (byte)(registers.F | FlagSet_H);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_H);
						}
						if (registers.A < val)
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
						registers.F = (byte)(registers.F | FlagSet_N);
						registers.A = (byte)(f1);
						if (f1 == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.PC += 1;
					}
					break;

				case OpCode.PUSH_AF:
					{
						Memory[registers.SP - 1] = (byte)(registers.A);
						Memory[registers.SP - 2] = (byte)(registers.F);
						registers.SP -= 2;
					}
					break;

				case OpCode.PUSH_BC:
					{
						Memory[registers.SP - 1] = (byte)(registers.B);
						Memory[registers.SP - 2] = (byte)(registers.C);
						registers.SP -= 2;
					}
					break;

				case OpCode.PUSH_DE:
					{
						Memory[registers.SP - 1] = (byte)(registers.D);
						Memory[registers.SP - 2] = (byte)(registers.E);
						registers.SP -= 2;
					}
					break;

				case OpCode.PUSH_HL:
					{
						Memory[registers.SP - 1] = (byte)(registers.H);
						Memory[registers.SP - 2] = (byte)(registers.L);
						registers.SP -= 2;
					}
					break;

				case OpCode.POP_BC:
					{
						registers.C = (byte)(Memory[registers.SP]);
						registers.B = (byte)(Memory[registers.SP + 1]);
						registers.SP += 2;
					}
					break;

				case OpCode.POP_DE:
					{
						registers.E = (byte)(Memory[registers.SP]);
						registers.D = (byte)(Memory[registers.SP + 1]);
						registers.SP += 2;
					}
					break;

				case OpCode.POP_HL:
					{
						registers.L = (byte)(Memory[registers.SP]);
						registers.H = (byte)(Memory[registers.SP + 1]);
						registers.SP += 2;
					}
					break;

				case OpCode.POP_AF:
					{
						registers.F = (byte)(Memory[registers.SP] & 0xF0);
						registers.A = (byte)(Memory[registers.SP + 1]);
						registers.SP += 2;
					}
					break;

				case OpCode.LDHL_SP_n:
					{
						registers.HL = (ushort)(registers.SP + Memory[registers.PC]);
						registers.PC += 1;
					}
					break;

				case OpCode.LD_A_n:
					{
						registers.A = (byte)(Memory[registers.PC]);
						registers.PC += 1;
					}
					break;

				case OpCode.LD_B_n:
					{
						registers.B = (byte)(Memory[registers.PC]);
						registers.PC += 1;
					}
					break;

				case OpCode.LD_C_n:
					{
						registers.C = (byte)(Memory[registers.PC]);
						registers.PC += 1;
					}
					break;

				case OpCode.LD_D_n:
					{
						registers.D = (byte)(Memory[registers.PC]);
						registers.PC += 1;
					}
					break;

				case OpCode.LD_E_n:
					{
						registers.E = (byte)(Memory[registers.PC]);
						registers.PC += 1;
					}
					break;

				case OpCode.LD_H_n:
					{
						registers.H = (byte)(Memory[registers.PC]);
						registers.PC += 1;
					}
					break;

				case OpCode.LD_L_n:
					{
						registers.L = (byte)(Memory[registers.PC]);
						registers.PC += 1;
					}
					break;

				case OpCode.LD_A_A:
					{
						registers.A = (byte)(registers.A);
					}
					break;

				case OpCode.LD_A_B:
					{
						registers.A = (byte)(registers.B);
					}
					break;

				case OpCode.LD_A_C:
					{
						registers.A = (byte)(registers.C);
					}
					break;

				case OpCode.LD_A_D:
					{
						registers.A = (byte)(registers.D);
					}
					break;

				case OpCode.LD_A_E:
					{
						registers.A = (byte)(registers.E);
					}
					break;

				case OpCode.LD_A_H:
					{
						registers.A = (byte)(registers.H);
					}
					break;

				case OpCode.LD_A_L:
					{
						registers.A = (byte)(registers.L);
					}
					break;

				case OpCode.LD_A_xBC:
					{
						registers.A = (byte)(Memory[registers.BC]);
					}
					break;

				case OpCode.LD_A_xDE:
					{
						registers.A = (byte)(Memory[registers.DE]);
					}
					break;

				case OpCode.LD_A_xHL:
					{
						registers.A = (byte)(Memory[registers.HL]);
					}
					break;

				case OpCode.LD_A_xnn:
					{
						int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

						registers.A = (byte)(Memory[tmp0]);
						registers.PC += 2;
					}
					break;

				case OpCode.LD_B_A:
					{
						registers.B = (byte)(registers.A);
					}
					break;

				case OpCode.LD_B_B:
					{
						registers.B = (byte)(registers.B);
					}
					break;

				case OpCode.LD_B_C:
					{
						registers.B = (byte)(registers.C);
					}
					break;

				case OpCode.LD_B_D:
					{
						registers.B = (byte)(registers.D);
					}
					break;

				case OpCode.LD_B_E:
					{
						registers.B = (byte)(registers.E);
					}
					break;

				case OpCode.LD_B_H:
					{
						registers.B = (byte)(registers.H);
					}
					break;

				case OpCode.LD_B_L:
					{
						registers.B = (byte)(registers.L);
					}
					break;

				case OpCode.LD_B_xHL:
					{
						registers.B = (byte)(Memory[registers.HL]);
					}
					break;

				case OpCode.LD_C_A:
					{
						registers.C = (byte)(registers.A);
					}
					break;

				case OpCode.LD_C_B:
					{
						registers.C = (byte)(registers.B);
					}
					break;

				case OpCode.LD_C_C:
					{
						registers.C = (byte)(registers.C);
					}
					break;

				case OpCode.LD_C_D:
					{
						registers.C = (byte)(registers.D);
					}
					break;

				case OpCode.LD_C_E:
					{
						registers.C = (byte)(registers.E);
					}
					break;

				case OpCode.LD_C_H:
					{
						registers.C = (byte)(registers.H);
					}
					break;

				case OpCode.LD_C_L:
					{
						registers.C = (byte)(registers.L);
					}
					break;

				case OpCode.LD_C_xHL:
					{
						registers.C = (byte)(Memory[registers.HL]);
					}
					break;

				case OpCode.LD_D_A:
					{
						registers.D = (byte)(registers.A);
					}
					break;

				case OpCode.LD_D_B:
					{
						registers.D = (byte)(registers.B);
					}
					break;

				case OpCode.LD_D_C:
					{
						registers.D = (byte)(registers.C);
					}
					break;

				case OpCode.LD_D_D:
					{
						registers.D = (byte)(registers.D);
					}
					break;

				case OpCode.LD_D_E:
					{
						registers.D = (byte)(registers.E);
					}
					break;

				case OpCode.LD_D_H:
					{
						registers.D = (byte)(registers.H);
					}
					break;

				case OpCode.LD_D_L:
					{
						registers.D = (byte)(registers.L);
					}
					break;

				case OpCode.LD_D_xHL:
					{
						registers.D = (byte)(Memory[registers.HL]);
					}
					break;

				case OpCode.LD_E_A:
					{
						registers.E = (byte)(registers.A);
					}
					break;

				case OpCode.LD_E_B:
					{
						registers.E = (byte)(registers.B);
					}
					break;

				case OpCode.LD_E_C:
					{
						registers.E = (byte)(registers.C);
					}
					break;

				case OpCode.LD_E_D:
					{
						registers.E = (byte)(registers.D);
					}
					break;

				case OpCode.LD_E_E:
					{
						registers.E = (byte)(registers.E);
					}
					break;

				case OpCode.LD_E_H:
					{
						registers.E = (byte)(registers.H);
					}
					break;

				case OpCode.LD_E_L:
					{
						registers.E = (byte)(registers.L);
					}
					break;

				case OpCode.LD_E_xHL:
					{
						registers.E = (byte)(Memory[registers.HL]);
					}
					break;

				case OpCode.LD_H_A:
					{
						registers.H = (byte)(registers.A);
					}
					break;

				case OpCode.LD_H_B:
					{
						registers.H = (byte)(registers.B);
					}
					break;

				case OpCode.LD_H_C:
					{
						registers.H = (byte)(registers.C);
					}
					break;

				case OpCode.LD_H_D:
					{
						registers.H = (byte)(registers.D);
					}
					break;

				case OpCode.LD_H_E:
					{
						registers.H = (byte)(registers.E);
					}
					break;

				case OpCode.LD_H_H:
					{
						registers.H = (byte)(registers.H);
					}
					break;

				case OpCode.LD_H_L:
					{
						registers.H = (byte)(registers.L);
					}
					break;

				case OpCode.LD_H_xHL:
					{
						registers.H = (byte)(Memory[registers.HL]);
					}
					break;

				case OpCode.LD_L_A:
					{
						registers.L = (byte)(registers.A);
					}
					break;

				case OpCode.LD_L_B:
					{
						registers.L = (byte)(registers.B);
					}
					break;

				case OpCode.LD_L_C:
					{
						registers.L = (byte)(registers.C);
					}
					break;

				case OpCode.LD_L_D:
					{
						registers.L = (byte)(registers.D);
					}
					break;

				case OpCode.LD_L_E:
					{
						registers.L = (byte)(registers.E);
					}
					break;

				case OpCode.LD_L_H:
					{
						registers.L = (byte)(registers.H);
					}
					break;

				case OpCode.LD_L_L:
					{
						registers.L = (byte)(registers.L);
					}
					break;

				case OpCode.LD_L_xHL:
					{
						registers.L = (byte)(Memory[registers.HL]);
					}
					break;

				case OpCode.LD_xHL_A:
					{
						Memory[registers.HL] = (byte)(registers.A);
					}
					break;

				case OpCode.LD_xHL_B:
					{
						Memory[registers.HL] = (byte)(registers.B);
					}
					break;

				case OpCode.LD_xHL_C:
					{
						Memory[registers.HL] = (byte)(registers.C);
					}
					break;

				case OpCode.LD_xHL_D:
					{
						Memory[registers.HL] = (byte)(registers.D);
					}
					break;

				case OpCode.LD_xHL_E:
					{
						Memory[registers.HL] = (byte)(registers.E);
					}
					break;

				case OpCode.LD_xHL_H:
					{
						Memory[registers.HL] = (byte)(registers.H);
					}
					break;

				case OpCode.LD_xHL_L:
					{
						Memory[registers.HL] = (byte)(registers.L);
					}
					break;

				case OpCode.LD_xHL_n:
					{
						Memory[registers.HL] = (byte)(Memory[registers.PC]);
						registers.PC += 1;
					}
					break;

				case OpCode.LD_xBC_A:
					{
						Memory[registers.BC] = (byte)(registers.A);
					}
					break;

				case OpCode.LD_xDE_A:
					{
						Memory[registers.DE] = (byte)(registers.A);
					}
					break;

				case OpCode.LD_xnn_A:
					{
						int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

						Memory[tmp0] = (byte)(registers.A);
						registers.PC += 2;
					}
					break;

				case OpCode.LD_BC_nn:
					{
						int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

						registers.BC = (ushort)(tmp0);
						registers.PC += 2;
					}
					break;

				case OpCode.LD_DE_nn:
					{
						int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

						registers.DE = (ushort)(tmp0);
						registers.PC += 2;
					}
					break;

				case OpCode.LD_HL_nn:
					{
						int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

						registers.HL = (ushort)(tmp0);
						registers.PC += 2;
					}
					break;

				case OpCode.LD_SP_nn:
					{
						int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

						registers.SP = (ushort)(tmp0);
						registers.PC += 2;
					}
					break;

				case OpCode.LD_SP_HL:
					{
						registers.SP = (ushort)(registers.HL);
					}
					break;

				case OpCode.LDH_A_xC:
					{
						registers.A = (byte)(Memory[0xFF00 + registers.C]);
					}
					break;

				case OpCode.LDH_xC_A:
					{
						Memory[0xFF00 + registers.C] = (byte)(registers.A);
					}
					break;

				case OpCode.LD_xnn_SP:
					{
						int tmp0 = (int)(Memory[registers.PC] + (Memory[registers.PC + 1] << 8));

						Memory[tmp0] = (byte)(registers.SP & 0xff);
						Memory[tmp0 + 1] = (byte)(registers.SP >> 8);
						registers.PC += 2;
					}
					break;

				case OpCode.LDH_A_xn:
					{
						ushort addr = (ushort)(0xFF00 + Memory[registers.PC]);

						registers.A = (byte)(Memory[addr]);
						registers.PC += 1;
					}
					break;

				case OpCode.LDH_xn_A:
					{
						ushort addr = (ushort)(0xFF00 + Memory[registers.PC]);

						Memory[addr] = (byte)(registers.A);
						registers.PC += 1;
					}
					break;

				case OpCode.LDI_A_xHL:
					{
						registers.A = (byte)(Memory[registers.HL]);
						registers.HL ++;
					}
					break;

				case OpCode.LDI_xHL_A:
					{
						Memory[registers.HL] = (byte)(registers.A);
						registers.HL ++;
					}
					break;

				case OpCode.LDD_A_xHL:
					{
						registers.A = (byte)(Memory[registers.HL]);
						registers.HL --;
					}
					break;

				case OpCode.LDD_xHL_A:
					{
						Memory[registers.HL] = (byte)(registers.A);
						registers.HL --;
					}
					break;

				case OpCode.DAA:
					{
						uint result = (uint)(DecimalAdjust ( registers.A ));

						registers.A = (byte)(result);
						registers.F = (byte)(registers.F & FlagReset_H);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
					}
					break;

				case OpCode.OpCodeCB:
					{
						ExecOpCodeCB();
					}
					break;
			}
		}

		public void ExecOpCodeCB()
		{
			OpCodeCB instruction;

			instruction = (OpCodeCB)(Memory[registers.PC]);
			registers.PC++;
			cpuCycles += CyclesCB[(int)instruction];

			switch (instruction)
			{
				case OpCodeCB.BIT_0_B:
					{
						byte mask = (byte)(0x1 << 0);
						byte result = (byte)(registers.B & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_0_C:
					{
						byte mask = (byte)(0x1 << 0);
						byte result = (byte)(registers.C & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_0_D:
					{
						byte mask = (byte)(0x1 << 0);
						byte result = (byte)(registers.D & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_0_E:
					{
						byte mask = (byte)(0x1 << 0);
						byte result = (byte)(registers.E & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_0_H:
					{
						byte mask = (byte)(0x1 << 0);
						byte result = (byte)(registers.H & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_0_L:
					{
						byte mask = (byte)(0x1 << 0);
						byte result = (byte)(registers.L & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_0_xHL:
					{
						byte mask = (byte)(0x1 << 0);
						byte result = (byte)(Memory[registers.HL] & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_0_A:
					{
						byte mask = (byte)(0x1 << 0);
						byte result = (byte)(registers.A & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_1_B:
					{
						byte mask = (byte)(0x1 << 1);
						byte result = (byte)(registers.B & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_1_C:
					{
						byte mask = (byte)(0x1 << 1);
						byte result = (byte)(registers.C & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_1_D:
					{
						byte mask = (byte)(0x1 << 1);
						byte result = (byte)(registers.D & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_1_E:
					{
						byte mask = (byte)(0x1 << 1);
						byte result = (byte)(registers.E & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_1_H:
					{
						byte mask = (byte)(0x1 << 1);
						byte result = (byte)(registers.H & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_1_L:
					{
						byte mask = (byte)(0x1 << 1);
						byte result = (byte)(registers.L & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_1_xHL:
					{
						byte mask = (byte)(0x1 << 1);
						byte result = (byte)(Memory[registers.HL] & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_1_A:
					{
						byte mask = (byte)(0x1 << 1);
						byte result = (byte)(registers.A & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_2_B:
					{
						byte mask = (byte)(0x1 << 2);
						byte result = (byte)(registers.B & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_2_C:
					{
						byte mask = (byte)(0x1 << 2);
						byte result = (byte)(registers.C & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_2_D:
					{
						byte mask = (byte)(0x1 << 2);
						byte result = (byte)(registers.D & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_2_E:
					{
						byte mask = (byte)(0x1 << 2);
						byte result = (byte)(registers.E & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_2_H:
					{
						byte mask = (byte)(0x1 << 2);
						byte result = (byte)(registers.H & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_2_L:
					{
						byte mask = (byte)(0x1 << 2);
						byte result = (byte)(registers.L & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_2_xHL:
					{
						byte mask = (byte)(0x1 << 2);
						byte result = (byte)(Memory[registers.HL] & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_2_A:
					{
						byte mask = (byte)(0x1 << 2);
						byte result = (byte)(registers.A & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_3_B:
					{
						byte mask = (byte)(0x1 << 3);
						byte result = (byte)(registers.B & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_3_C:
					{
						byte mask = (byte)(0x1 << 3);
						byte result = (byte)(registers.C & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_3_D:
					{
						byte mask = (byte)(0x1 << 3);
						byte result = (byte)(registers.D & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_3_E:
					{
						byte mask = (byte)(0x1 << 3);
						byte result = (byte)(registers.E & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_3_H:
					{
						byte mask = (byte)(0x1 << 3);
						byte result = (byte)(registers.H & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_3_L:
					{
						byte mask = (byte)(0x1 << 3);
						byte result = (byte)(registers.L & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_3_xHL:
					{
						byte mask = (byte)(0x1 << 3);
						byte result = (byte)(Memory[registers.HL] & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_3_A:
					{
						byte mask = (byte)(0x1 << 3);
						byte result = (byte)(registers.A & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_4_B:
					{
						byte mask = (byte)(0x1 << 4);
						byte result = (byte)(registers.B & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_4_C:
					{
						byte mask = (byte)(0x1 << 4);
						byte result = (byte)(registers.C & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_4_D:
					{
						byte mask = (byte)(0x1 << 4);
						byte result = (byte)(registers.D & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_4_E:
					{
						byte mask = (byte)(0x1 << 4);
						byte result = (byte)(registers.E & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_4_H:
					{
						byte mask = (byte)(0x1 << 4);
						byte result = (byte)(registers.H & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_4_L:
					{
						byte mask = (byte)(0x1 << 4);
						byte result = (byte)(registers.L & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_4_xHL:
					{
						byte mask = (byte)(0x1 << 4);
						byte result = (byte)(Memory[registers.HL] & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_4_A:
					{
						byte mask = (byte)(0x1 << 4);
						byte result = (byte)(registers.A & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_5_B:
					{
						byte mask = (byte)(0x1 << 5);
						byte result = (byte)(registers.B & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_5_C:
					{
						byte mask = (byte)(0x1 << 5);
						byte result = (byte)(registers.C & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_5_D:
					{
						byte mask = (byte)(0x1 << 5);
						byte result = (byte)(registers.D & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_5_E:
					{
						byte mask = (byte)(0x1 << 5);
						byte result = (byte)(registers.E & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_5_H:
					{
						byte mask = (byte)(0x1 << 5);
						byte result = (byte)(registers.H & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_5_L:
					{
						byte mask = (byte)(0x1 << 5);
						byte result = (byte)(registers.L & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_5_xHL:
					{
						byte mask = (byte)(0x1 << 5);
						byte result = (byte)(Memory[registers.HL] & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_5_A:
					{
						byte mask = (byte)(0x1 << 5);
						byte result = (byte)(registers.A & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_6_B:
					{
						byte mask = (byte)(0x1 << 6);
						byte result = (byte)(registers.B & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_6_C:
					{
						byte mask = (byte)(0x1 << 6);
						byte result = (byte)(registers.C & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_6_D:
					{
						byte mask = (byte)(0x1 << 6);
						byte result = (byte)(registers.D & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_6_E:
					{
						byte mask = (byte)(0x1 << 6);
						byte result = (byte)(registers.E & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_6_H:
					{
						byte mask = (byte)(0x1 << 6);
						byte result = (byte)(registers.H & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_6_L:
					{
						byte mask = (byte)(0x1 << 6);
						byte result = (byte)(registers.L & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_6_xHL:
					{
						byte mask = (byte)(0x1 << 6);
						byte result = (byte)(Memory[registers.HL] & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_6_A:
					{
						byte mask = (byte)(0x1 << 6);
						byte result = (byte)(registers.A & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_7_B:
					{
						byte mask = (byte)(0x1 << 7);
						byte result = (byte)(registers.B & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_7_C:
					{
						byte mask = (byte)(0x1 << 7);
						byte result = (byte)(registers.C & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_7_D:
					{
						byte mask = (byte)(0x1 << 7);
						byte result = (byte)(registers.D & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_7_E:
					{
						byte mask = (byte)(0x1 << 7);
						byte result = (byte)(registers.E & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_7_H:
					{
						byte mask = (byte)(0x1 << 7);
						byte result = (byte)(registers.H & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_7_L:
					{
						byte mask = (byte)(0x1 << 7);
						byte result = (byte)(registers.L & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_7_xHL:
					{
						byte mask = (byte)(0x1 << 7);
						byte result = (byte)(Memory[registers.HL] & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.BIT_7_A:
					{
						byte mask = (byte)(0x1 << 7);
						byte result = (byte)(registers.A & mask);

						if (result == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.F = (byte)(registers.F | FlagSet_H);
						registers.F = (byte)(registers.F & FlagReset_N);
					}
					break;

				case OpCodeCB.SET_0_B:
					{
						byte mask = (byte)(0x1 << 0);

						registers.B = (byte)(registers.B | mask);
					}
					break;

				case OpCodeCB.SET_0_C:
					{
						byte mask = (byte)(0x1 << 0);

						registers.C = (byte)(registers.C | mask);
					}
					break;

				case OpCodeCB.SET_0_D:
					{
						byte mask = (byte)(0x1 << 0);

						registers.D = (byte)(registers.D | mask);
					}
					break;

				case OpCodeCB.SET_0_E:
					{
						byte mask = (byte)(0x1 << 0);

						registers.E = (byte)(registers.E | mask);
					}
					break;

				case OpCodeCB.SET_0_H:
					{
						byte mask = (byte)(0x1 << 0);

						registers.H = (byte)(registers.H | mask);
					}
					break;

				case OpCodeCB.SET_0_L:
					{
						byte mask = (byte)(0x1 << 0);

						registers.L = (byte)(registers.L | mask);
					}
					break;

				case OpCodeCB.SET_0_xHL:
					{
						byte mask = (byte)(0x1 << 0);

						Memory[registers.HL] = (byte)(Memory[registers.HL] | mask);
					}
					break;

				case OpCodeCB.SET_0_A:
					{
						byte mask = (byte)(0x1 << 0);

						registers.A = (byte)(registers.A | mask);
					}
					break;

				case OpCodeCB.SET_1_B:
					{
						byte mask = (byte)(0x1 << 1);

						registers.B = (byte)(registers.B | mask);
					}
					break;

				case OpCodeCB.SET_1_C:
					{
						byte mask = (byte)(0x1 << 1);

						registers.C = (byte)(registers.C | mask);
					}
					break;

				case OpCodeCB.SET_1_D:
					{
						byte mask = (byte)(0x1 << 1);

						registers.D = (byte)(registers.D | mask);
					}
					break;

				case OpCodeCB.SET_1_E:
					{
						ExecOpCodeCB();
					}
					break;
				case OpCodeCB.SET_1_H:
					{
						byte mask = (byte)(0x1 << 1);

						registers.H = (byte)(registers.H | mask);
					}
					break;

				case OpCodeCB.SET_1_L:
					{
						byte mask = (byte)(0x1 << 1);

						registers.L = (byte)(registers.L | mask);
					}
					break;

				case OpCodeCB.SET_1_xHL:
					{
						byte mask = (byte)(0x1 << 1);

						Memory[registers.HL] = (byte)(Memory[registers.HL] | mask);
					}
					break;

				case OpCodeCB.SET_1_A:
					{
						byte mask = (byte)(0x1 << 1);

						registers.A = (byte)(registers.A | mask);
					}
					break;

				case OpCodeCB.SET_2_B:
					{
						byte mask = (byte)(0x1 << 2);

						registers.B = (byte)(registers.B | mask);
					}
					break;

				case OpCodeCB.SET_2_C:
					{
						byte mask = (byte)(0x1 << 2);

						registers.C = (byte)(registers.C | mask);
					}
					break;

				case OpCodeCB.SET_2_D:
					{
						byte mask = (byte)(0x1 << 2);

						registers.D = (byte)(registers.D | mask);
					}
					break;

				case OpCodeCB.SET_2_E:
					{
						byte mask = (byte)(0x1 << 2);

						registers.E = (byte)(registers.E | mask);
					}
					break;

				case OpCodeCB.SET_2_H:
					{
						byte mask = (byte)(0x1 << 2);

						registers.H = (byte)(registers.H | mask);
					}
					break;

				case OpCodeCB.SET_2_L:
					{
						byte mask = (byte)(0x1 << 2);

						registers.L = (byte)(registers.L | mask);
					}
					break;

				case OpCodeCB.SET_2_xHL:
					{
						byte mask = (byte)(0x1 << 2);

						Memory[registers.HL] = (byte)(Memory[registers.HL] | mask);
					}
					break;

				case OpCodeCB.SET_2_A:
					{
						byte mask = (byte)(0x1 << 2);

						registers.A = (byte)(registers.A | mask);
					}
					break;

				case OpCodeCB.SET_3_B:
					{
						byte mask = (byte)(0x1 << 3);

						registers.B = (byte)(registers.B | mask);
					}
					break;

				case OpCodeCB.SET_3_C:
					{
						byte mask = (byte)(0x1 << 3);

						registers.C = (byte)(registers.C | mask);
					}
					break;

				case OpCodeCB.SET_3_D:
					{
						byte mask = (byte)(0x1 << 3);

						registers.D = (byte)(registers.D | mask);
					}
					break;

				case OpCodeCB.SET_3_E:
					{
						byte mask = (byte)(0x1 << 3);

						registers.E = (byte)(registers.E | mask);
					}
					break;

				case OpCodeCB.SET_3_H:
					{
						byte mask = (byte)(0x1 << 3);

						registers.H = (byte)(registers.H | mask);
					}
					break;

				case OpCodeCB.SET_3_L:
					{
						byte mask = (byte)(0x1 << 3);

						registers.L = (byte)(registers.L | mask);
					}
					break;

				case OpCodeCB.SET_3_xHL:
					{
						byte mask = (byte)(0x1 << 3);

						Memory[registers.HL] = (byte)(Memory[registers.HL] | mask);
					}
					break;

				case OpCodeCB.SET_3_A:
					{
						byte mask = (byte)(0x1 << 3);

						registers.A = (byte)(registers.A | mask);
					}
					break;

				case OpCodeCB.SET_4_B:
					{
						byte mask = (byte)(0x1 << 4);

						registers.B = (byte)(registers.B | mask);
					}
					break;

				case OpCodeCB.SET_4_C:
					{
						byte mask = (byte)(0x1 << 4);

						registers.C = (byte)(registers.C | mask);
					}
					break;

				case OpCodeCB.SET_4_D:
					{
						byte mask = (byte)(0x1 << 4);

						registers.D = (byte)(registers.D | mask);
					}
					break;

				case OpCodeCB.SET_4_E:
					{
						byte mask = (byte)(0x1 << 4);

						registers.E = (byte)(registers.E | mask);
					}
					break;

				case OpCodeCB.SET_4_H:
					{
						byte mask = (byte)(0x1 << 4);

						registers.H = (byte)(registers.H | mask);
					}
					break;

				case OpCodeCB.SET_4_L:
					{
						byte mask = (byte)(0x1 << 4);

						registers.L = (byte)(registers.L | mask);
					}
					break;

				case OpCodeCB.SET_4_xHL:
					{
						byte mask = (byte)(0x1 << 4);

						Memory[registers.HL] = (byte)(Memory[registers.HL] | mask);
					}
					break;

				case OpCodeCB.SET_4_A:
					{
						byte mask = (byte)(0x1 << 4);

						registers.A = (byte)(registers.A | mask);
					}
					break;

				case OpCodeCB.SET_5_B:
					{
						byte mask = (byte)(0x1 << 5);

						registers.B = (byte)(registers.B | mask);
					}
					break;

				case OpCodeCB.SET_5_C:
					{
						byte mask = (byte)(0x1 << 5);

						registers.C = (byte)(registers.C | mask);
					}
					break;

				case OpCodeCB.SET_5_D:
					{
						byte mask = (byte)(0x1 << 5);

						registers.D = (byte)(registers.D | mask);
					}
					break;

				case OpCodeCB.SET_5_E:
					{
						byte mask = (byte)(0x1 << 5);

						registers.E = (byte)(registers.E | mask);
					}
					break;

				case OpCodeCB.SET_5_H:
					{
						byte mask = (byte)(0x1 << 5);

						registers.H = (byte)(registers.H | mask);
					}
					break;

				case OpCodeCB.SET_5_L:
					{
						byte mask = (byte)(0x1 << 5);

						registers.L = (byte)(registers.L | mask);
					}
					break;

				case OpCodeCB.SET_5_xHL:
					{
						byte mask = (byte)(0x1 << 5);

						Memory[registers.HL] = (byte)(Memory[registers.HL] | mask);
					}
					break;

				case OpCodeCB.SET_5_A:
					{
						byte mask = (byte)(0x1 << 5);

						registers.A = (byte)(registers.A | mask);
					}
					break;

				case OpCodeCB.SET_6_B:
					{
						byte mask = (byte)(0x1 << 6);

						registers.B = (byte)(registers.B | mask);
					}
					break;

				case OpCodeCB.SET_6_C:
					{
						byte mask = (byte)(0x1 << 6);

						registers.C = (byte)(registers.C | mask);
					}
					break;

				case OpCodeCB.SET_6_D:
					{
						byte mask = (byte)(0x1 << 6);

						registers.D = (byte)(registers.D | mask);
					}
					break;

				case OpCodeCB.SET_6_E:
					{
						byte mask = (byte)(0x1 << 6);

						registers.E = (byte)(registers.E | mask);
					}
					break;

				case OpCodeCB.SET_6_H:
					{
						byte mask = (byte)(0x1 << 6);

						registers.H = (byte)(registers.H | mask);
					}
					break;

				case OpCodeCB.SET_6_L:
					{
						byte mask = (byte)(0x1 << 6);

						registers.L = (byte)(registers.L | mask);
					}
					break;

				case OpCodeCB.SET_6_xHL:
					{
						byte mask = (byte)(0x1 << 6);

						Memory[registers.HL] = (byte)(Memory[registers.HL] | mask);
					}
					break;

				case OpCodeCB.SET_6_A:
					{
						byte mask = (byte)(0x1 << 6);

						registers.A = (byte)(registers.A | mask);
					}
					break;

				case OpCodeCB.SET_7_B:
					{
						byte mask = (byte)(0x1 << 7);

						registers.B = (byte)(registers.B | mask);
					}
					break;

				case OpCodeCB.SET_7_C:
					{
						byte mask = (byte)(0x1 << 7);

						registers.C = (byte)(registers.C | mask);
					}
					break;

				case OpCodeCB.SET_7_D:
					{
						byte mask = (byte)(0x1 << 7);

						registers.D = (byte)(registers.D | mask);
					}
					break;

				case OpCodeCB.SET_7_E:
					{
						byte mask = (byte)(0x1 << 7);

						registers.E = (byte)(registers.E | mask);
					}
					break;

				case OpCodeCB.SET_7_H:
					{
						byte mask = (byte)(0x1 << 7);

						registers.H = (byte)(registers.H | mask);
					}
					break;

				case OpCodeCB.SET_7_L:
					{
						byte mask = (byte)(0x1 << 7);

						registers.L = (byte)(registers.L | mask);
					}
					break;

				case OpCodeCB.SET_7_xHL:
					{
						byte mask = (byte)(0x1 << 7);

						Memory[registers.HL] = (byte)(Memory[registers.HL] | mask);
					}
					break;

				case OpCodeCB.SET_7_A:
					{
						byte mask = (byte)(0x1 << 7);

						registers.A = (byte)(registers.A | mask);
					}
					break;

				case OpCodeCB.RES_0_B:
					{
						byte mask = (byte)(0x1 << 0);

						mask = (byte)(~ mask);
						registers.B = (byte)(registers.B & mask);
					}
					break;

				case OpCodeCB.RES_0_C:
					{
						byte mask = (byte)(0x1 << 0);

						mask = (byte)(~ mask);
						registers.C = (byte)(registers.C & mask);
					}
					break;

				case OpCodeCB.RES_0_D:
					{
						byte mask = (byte)(0x1 << 0);

						mask = (byte)(~ mask);
						registers.D = (byte)(registers.D & mask);
					}
					break;

				case OpCodeCB.RES_0_E:
					{
						byte mask = (byte)(0x1 << 0);

						mask = (byte)(~ mask);
						registers.E = (byte)(registers.E & mask);
					}
					break;

				case OpCodeCB.RES_0_H:
					{
						byte mask = (byte)(0x1 << 0);

						mask = (byte)(~ mask);
						registers.H = (byte)(registers.H & mask);
					}
					break;

				case OpCodeCB.RES_0_L:
					{
						byte mask = (byte)(0x1 << 0);

						mask = (byte)(~ mask);
						registers.L = (byte)(registers.L & mask);
					}
					break;

				case OpCodeCB.RES_0_xHL:
					{
						byte mask = (byte)(0x1 << 0);

						mask = (byte)(~ mask);
						Memory[registers.HL] = (byte)(Memory[registers.HL] & mask);
					}
					break;

				case OpCodeCB.RES_0_A:
					{
						byte mask = (byte)(0x1 << 0);

						mask = (byte)(~ mask);
						registers.A = (byte)(registers.A & mask);
					}
					break;

				case OpCodeCB.RES_1_B:
					{
						byte mask = (byte)(0x1 << 1);

						mask = (byte)(~ mask);
						registers.B = (byte)(registers.B & mask);
					}
					break;

				case OpCodeCB.RES_1_C:
					{
						byte mask = (byte)(0x1 << 1);

						mask = (byte)(~ mask);
						registers.C = (byte)(registers.C & mask);
					}
					break;

				case OpCodeCB.RES_1_D:
					{
						byte mask = (byte)(0x1 << 1);

						mask = (byte)(~ mask);
						registers.D = (byte)(registers.D & mask);
					}
					break;

				case OpCodeCB.RES_1_E:
					{
						byte mask = (byte)(0x1 << 1);

						mask = (byte)(~ mask);
						registers.E = (byte)(registers.E & mask);
					}
					break;

				case OpCodeCB.RES_1_H:
					{
						byte mask = (byte)(0x1 << 1);

						mask = (byte)(~ mask);
						registers.H = (byte)(registers.H & mask);
					}
					break;

				case OpCodeCB.RES_1_L:
					{
						byte mask = (byte)(0x1 << 1);

						mask = (byte)(~ mask);
						registers.L = (byte)(registers.L & mask);
					}
					break;

				case OpCodeCB.RES_1_xHL:
					{
						byte mask = (byte)(0x1 << 1);

						mask = (byte)(~ mask);
						Memory[registers.HL] = (byte)(Memory[registers.HL] & mask);
					}
					break;

				case OpCodeCB.RES_1_A:
					{
						byte mask = (byte)(0x1 << 1);

						mask = (byte)(~ mask);
						registers.A = (byte)(registers.A & mask);
					}
					break;

				case OpCodeCB.RES_2_B:
					{
						byte mask = (byte)(0x1 << 2);

						mask = (byte)(~ mask);
						registers.B = (byte)(registers.B & mask);
					}
					break;

				case OpCodeCB.RES_2_C:
					{
						byte mask = (byte)(0x1 << 2);

						mask = (byte)(~ mask);
						registers.C = (byte)(registers.C & mask);
					}
					break;

				case OpCodeCB.RES_2_D:
					{
						byte mask = (byte)(0x1 << 2);

						mask = (byte)(~ mask);
						registers.D = (byte)(registers.D & mask);
					}
					break;

				case OpCodeCB.RES_2_E:
					{
						byte mask = (byte)(0x1 << 2);

						mask = (byte)(~ mask);
						registers.E = (byte)(registers.E & mask);
					}
					break;

				case OpCodeCB.RES_2_H:
					{
						byte mask = (byte)(0x1 << 2);

						mask = (byte)(~ mask);
						registers.H = (byte)(registers.H & mask);
					}
					break;

				case OpCodeCB.RES_2_L:
					{
						byte mask = (byte)(0x1 << 2);

						mask = (byte)(~ mask);
						registers.L = (byte)(registers.L & mask);
					}
					break;

				case OpCodeCB.RES_2_xHL:
					{
						byte mask = (byte)(0x1 << 2);

						mask = (byte)(~ mask);
						Memory[registers.HL] = (byte)(Memory[registers.HL] & mask);
					}
					break;

				case OpCodeCB.RES_2_A:
					{
						byte mask = (byte)(0x1 << 2);

						mask = (byte)(~ mask);
						registers.A = (byte)(registers.A & mask);
					}
					break;

				case OpCodeCB.RES_3_B:
					{
						byte mask = (byte)(0x1 << 3);

						mask = (byte)(~ mask);
						registers.B = (byte)(registers.B & mask);
					}
					break;

				case OpCodeCB.RES_3_C:
					{
						byte mask = (byte)(0x1 << 3);

						mask = (byte)(~ mask);
						registers.C = (byte)(registers.C & mask);
					}
					break;

				case OpCodeCB.RES_3_D:
					{
						byte mask = (byte)(0x1 << 3);

						mask = (byte)(~ mask);
						registers.D = (byte)(registers.D & mask);
					}
					break;

				case OpCodeCB.RES_3_E:
					{
						byte mask = (byte)(0x1 << 3);

						mask = (byte)(~ mask);
						registers.E = (byte)(registers.E & mask);
					}
					break;

				case OpCodeCB.RES_3_H:
					{
						byte mask = (byte)(0x1 << 3);

						mask = (byte)(~ mask);
						registers.H = (byte)(registers.H & mask);
					}
					break;

				case OpCodeCB.RES_3_L:
					{
						byte mask = (byte)(0x1 << 3);

						mask = (byte)(~ mask);
						registers.L = (byte)(registers.L & mask);
					}
					break;

				case OpCodeCB.RES_3_xHL:
					{
						byte mask = (byte)(0x1 << 3);

						mask = (byte)(~ mask);
						Memory[registers.HL] = (byte)(Memory[registers.HL] & mask);
					}
					break;

				case OpCodeCB.RES_3_A:
					{
						byte mask = (byte)(0x1 << 3);

						mask = (byte)(~ mask);
						registers.A = (byte)(registers.A & mask);
					}
					break;

				case OpCodeCB.RES_4_B:
					{
						byte mask = (byte)(0x1 << 4);

						mask = (byte)(~ mask);
						registers.B = (byte)(registers.B & mask);
					}
					break;

				case OpCodeCB.RES_4_C:
					{
						byte mask = (byte)(0x1 << 4);

						mask = (byte)(~ mask);
						registers.C = (byte)(registers.C & mask);
					}
					break;

				case OpCodeCB.RES_4_D:
					{
						byte mask = (byte)(0x1 << 4);

						mask = (byte)(~ mask);
						registers.D = (byte)(registers.D & mask);
					}
					break;

				case OpCodeCB.RES_4_E:
					{
						byte mask = (byte)(0x1 << 4);

						mask = (byte)(~ mask);
						registers.E = (byte)(registers.E & mask);
					}
					break;

				case OpCodeCB.RES_4_H:
					{
						byte mask = (byte)(0x1 << 4);

						mask = (byte)(~ mask);
						registers.H = (byte)(registers.H & mask);
					}
					break;

				case OpCodeCB.RES_4_L:
					{
						byte mask = (byte)(0x1 << 4);

						mask = (byte)(~ mask);
						registers.L = (byte)(registers.L & mask);
					}
					break;

				case OpCodeCB.RES_4_xHL:
					{
						byte mask = (byte)(0x1 << 4);

						mask = (byte)(~ mask);
						Memory[registers.HL] = (byte)(Memory[registers.HL] & mask);
					}
					break;

				case OpCodeCB.RES_4_A:
					{
						byte mask = (byte)(0x1 << 4);

						mask = (byte)(~ mask);
						registers.A = (byte)(registers.A & mask);
					}
					break;

				case OpCodeCB.RES_5_B:
					{
						byte mask = (byte)(0x1 << 5);

						mask = (byte)(~ mask);
						registers.B = (byte)(registers.B & mask);
					}
					break;

				case OpCodeCB.RES_5_C:
					{
						byte mask = (byte)(0x1 << 5);

						mask = (byte)(~ mask);
						registers.C = (byte)(registers.C & mask);
					}
					break;

				case OpCodeCB.RES_5_D:
					{
						byte mask = (byte)(0x1 << 5);

						mask = (byte)(~ mask);
						registers.D = (byte)(registers.D & mask);
					}
					break;

				case OpCodeCB.RES_5_E:
					{
						byte mask = (byte)(0x1 << 5);

						mask = (byte)(~ mask);
						registers.E = (byte)(registers.E & mask);
					}
					break;

				case OpCodeCB.RES_5_H:
					{
						byte mask = (byte)(0x1 << 5);

						mask = (byte)(~ mask);
						registers.H = (byte)(registers.H & mask);
					}
					break;

				case OpCodeCB.RES_5_L:
					{
						byte mask = (byte)(0x1 << 5);

						mask = (byte)(~ mask);
						registers.L = (byte)(registers.L & mask);
					}
					break;

				case OpCodeCB.RES_5_xHL:
					{
						byte mask = (byte)(0x1 << 5);

						mask = (byte)(~ mask);
						Memory[registers.HL] = (byte)(Memory[registers.HL] & mask);
					}
					break;

				case OpCodeCB.RES_5_A:
					{
						byte mask = (byte)(0x1 << 5);

						mask = (byte)(~ mask);
						registers.A = (byte)(registers.A & mask);
					}
					break;

				case OpCodeCB.RES_6_B:
					{
						byte mask = (byte)(0x1 << 6);

						mask = (byte)(~ mask);
						registers.B = (byte)(registers.B & mask);
					}
					break;

				case OpCodeCB.RES_6_C:
					{
						byte mask = (byte)(0x1 << 6);

						mask = (byte)(~ mask);
						registers.C = (byte)(registers.C & mask);
					}
					break;

				case OpCodeCB.RES_6_D:
					{
						byte mask = (byte)(0x1 << 6);

						mask = (byte)(~ mask);
						registers.D = (byte)(registers.D & mask);
					}
					break;

				case OpCodeCB.RES_6_E:
					{
						byte mask = (byte)(0x1 << 6);

						mask = (byte)(~ mask);
						registers.E = (byte)(registers.E & mask);
					}
					break;

				case OpCodeCB.RES_6_H:
					{
						byte mask = (byte)(0x1 << 6);

						mask = (byte)(~ mask);
						registers.H = (byte)(registers.H & mask);
					}
					break;

				case OpCodeCB.RES_6_L:
					{
						byte mask = (byte)(0x1 << 6);

						mask = (byte)(~ mask);
						registers.L = (byte)(registers.L & mask);
					}
					break;

				case OpCodeCB.RES_6_xHL:
					{
						byte mask = (byte)(0x1 << 6);

						mask = (byte)(~ mask);
						Memory[registers.HL] = (byte)(Memory[registers.HL] & mask);
					}
					break;

				case OpCodeCB.RES_6_A:
					{
						byte mask = (byte)(0x1 << 6);

						mask = (byte)(~ mask);
						registers.A = (byte)(registers.A & mask);
					}
					break;

				case OpCodeCB.RES_7_B:
					{
						byte mask = (byte)(0x1 << 7);

						mask = (byte)(~ mask);
						registers.B = (byte)(registers.B & mask);
					}
					break;

				case OpCodeCB.RES_7_C:
					{
						byte mask = (byte)(0x1 << 7);

						mask = (byte)(~ mask);
						registers.C = (byte)(registers.C & mask);
					}
					break;

				case OpCodeCB.RES_7_D:
					{
						byte mask = (byte)(0x1 << 7);

						mask = (byte)(~ mask);
						registers.D = (byte)(registers.D & mask);
					}
					break;

				case OpCodeCB.RES_7_E:
					{
						byte mask = (byte)(0x1 << 7);

						mask = (byte)(~ mask);
						registers.E = (byte)(registers.E & mask);
					}
					break;

				case OpCodeCB.RES_7_H:
					{
						byte mask = (byte)(0x1 << 7);

						mask = (byte)(~ mask);
						registers.H = (byte)(registers.H & mask);
					}
					break;

				case OpCodeCB.RES_7_L:
					{
						byte mask = (byte)(0x1 << 7);

						mask = (byte)(~ mask);
						registers.L = (byte)(registers.L & mask);
					}
					break;

				case OpCodeCB.RES_7_xHL:
					{
						byte mask = (byte)(0x1 << 7);

						mask = (byte)(~ mask);
						Memory[registers.HL] = (byte)(Memory[registers.HL] & mask);
					}
					break;

				case OpCodeCB.RES_7_A:
					{
						byte mask = (byte)(0x1 << 7);

						mask = (byte)(~ mask);
						registers.A = (byte)(registers.A & mask);
					}
					break;

				case OpCodeCB.RR_A:
					{
						byte carry = (byte)(registers.A & 0x1);
						byte inp = (byte)(((registers.F >> 4) & 0x1) << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A >> 1);
						registers.A = (byte)(registers.A + inp);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RR_B:
					{
						byte carry = (byte)(registers.B & 0x1);
						byte inp = (byte)(((registers.F >> 4) & 0x1) << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.B = (byte)(registers.B >> 1);
						registers.B = (byte)(registers.B + inp);
						if (registers.B == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RR_C:
					{
						byte carry = (byte)(registers.C & 0x1);
						byte inp = (byte)(((registers.F >> 4) & 0x1) << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.C = (byte)(registers.C >> 1);
						registers.C = (byte)(registers.C + inp);
						if (registers.C == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RR_D:
					{
						byte carry = (byte)(registers.D & 0x1);
						byte inp = (byte)(((registers.F >> 4) & 0x1) << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.D = (byte)(registers.D >> 1);
						registers.D = (byte)(registers.D + inp);
						if (registers.D == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RR_E:
					{
						byte carry = (byte)(registers.E & 0x1);
						byte inp = (byte)(((registers.F >> 4) & 0x1) << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.E = (byte)(registers.E >> 1);
						registers.E = (byte)(registers.E + inp);
						if (registers.E == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RR_H:
					{
						byte carry = (byte)(registers.H & 0x1);
						byte inp = (byte)(((registers.F >> 4) & 0x1) << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.H = (byte)(registers.H >> 1);
						registers.H = (byte)(registers.H + inp);
						if (registers.H == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RR_L:
					{
						byte carry = (byte)(registers.L & 0x1);
						byte inp = (byte)(((registers.F >> 4) & 0x1) << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.L = (byte)(registers.L >> 1);
						registers.L = (byte)(registers.L + inp);
						if (registers.L == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RR_xHL:
					{
						byte carry = (byte)(Memory[registers.HL] & 0x1);
						byte inp = (byte)(((registers.F >> 4) & 0x1) << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						Memory[registers.HL] = (byte)(Memory[registers.HL] >> 1);
						Memory[registers.HL] = (byte)(Memory[registers.HL] + inp);
						if (Memory[registers.HL] == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RRC_A:
					{
						byte carry = (byte)(registers.A & 0x1);
						byte inp = (byte)(carry << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A >> 1);
						registers.A = (byte)(registers.A + inp);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RRC_B:
					{
						byte carry = (byte)(registers.B & 0x1);
						byte inp = (byte)(carry << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.B = (byte)(registers.B >> 1);
						registers.B = (byte)(registers.B + inp);
						if (registers.B == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RRC_C:
					{
						byte carry = (byte)(registers.C & 0x1);
						byte inp = (byte)(carry << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.C = (byte)(registers.C >> 1);
						registers.C = (byte)(registers.C + inp);
						if (registers.C == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RRC_D:
					{
						byte carry = (byte)(registers.D & 0x1);
						byte inp = (byte)(carry << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.D = (byte)(registers.D >> 1);
						registers.D = (byte)(registers.D + inp);
						if (registers.D == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RRC_E:
					{
						byte carry = (byte)(registers.E & 0x1);
						byte inp = (byte)(carry << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.E = (byte)(registers.E >> 1);
						registers.E = (byte)(registers.E + inp);
						if (registers.E == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RRC_H:
					{
						byte carry = (byte)(registers.H & 0x1);
						byte inp = (byte)(carry << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.H = (byte)(registers.H >> 1);
						registers.H = (byte)(registers.H + inp);
						if (registers.H == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RRC_L:
					{
						byte carry = (byte)(registers.L & 0x1);
						byte inp = (byte)(carry << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.L = (byte)(registers.L >> 1);
						registers.L = (byte)(registers.L + inp);
						if (registers.L == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RRC_xHL:
					{
						byte carry = (byte)(Memory[registers.HL] & 0x1);
						byte inp = (byte)(carry << 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						Memory[registers.HL] = (byte)(Memory[registers.HL] >> 1);
						Memory[registers.HL] = (byte)(Memory[registers.HL] + inp);
						if (Memory[registers.HL] == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SLA_A:
					{
						byte carry = (byte)(registers.A & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A << 1);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SLA_B:
					{
						byte carry = (byte)(registers.B & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.B = (byte)(registers.B << 1);
						if (registers.B == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SLA_C:
					{
						byte carry = (byte)(registers.C & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.C = (byte)(registers.C << 1);
						if (registers.C == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SLA_D:
					{
						byte carry = (byte)(registers.D & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.D = (byte)(registers.D << 1);
						if (registers.D == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SLA_E:
					{
						byte carry = (byte)(registers.E & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.E = (byte)(registers.E << 1);
						if (registers.E == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SLA_H:
					{
						byte carry = (byte)(registers.H & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.H = (byte)(registers.H << 1);
						if (registers.H == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SLA_L:
					{
						byte carry = (byte)(registers.L & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.L = (byte)(registers.L << 1);
						if (registers.L == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SLA_xHL:
					{
						byte carry = (byte)(Memory[registers.HL] & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						Memory[registers.HL] = (byte)(Memory[registers.HL] << 1);
						if (Memory[registers.HL] == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRA_A:
					{
						byte carry = (byte)(registers.A & 0x1);
						byte msb = (byte)(registers.A & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A >> 1);
						registers.A = (byte)(registers.A + msb);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRA_B:
					{
						byte carry = (byte)(registers.B & 0x1);
						byte msb = (byte)(registers.B & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.B = (byte)(registers.B >> 1);
						registers.B = (byte)(registers.B + msb);
						if (registers.B == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRA_C:
					{
						byte carry = (byte)(registers.C & 0x1);
						byte msb = (byte)(registers.C & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.C = (byte)(registers.C >> 1);
						registers.C = (byte)(registers.C + msb);
						if (registers.C == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRA_D:
					{
						byte carry = (byte)(registers.D & 0x1);
						byte msb = (byte)(registers.D & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.D = (byte)(registers.D >> 1);
						registers.D = (byte)(registers.D + msb);
						if (registers.D == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRA_E:
					{
						byte carry = (byte)(registers.E & 0x1);
						byte msb = (byte)(registers.E & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.E = (byte)(registers.E >> 1);
						registers.E = (byte)(registers.E + msb);
						if (registers.E == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRA_H:
					{
						byte carry = (byte)(registers.H & 0x1);
						byte msb = (byte)(registers.H & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.H = (byte)(registers.H >> 1);
						registers.H = (byte)(registers.H + msb);
						if (registers.H == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRA_L:
					{
						byte carry = (byte)(registers.L & 0x1);
						byte msb = (byte)(registers.L & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.L = (byte)(registers.L >> 1);
						registers.L = (byte)(registers.L + msb);
						if (registers.L == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRA_xHL:
					{
						byte carry = (byte)(Memory[registers.HL] & 0x1);
						byte msb = (byte)(Memory[registers.HL] & 0x80);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						Memory[registers.HL] = (byte)(Memory[registers.HL] >> 1);
						Memory[registers.HL] = (byte)(Memory[registers.HL] + msb);
						if (Memory[registers.HL] == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRL_A:
					{
						byte carry = (byte)(registers.A & 0x1);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A >> 1);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRL_B:
					{
						byte carry = (byte)(registers.B & 0x1);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.B = (byte)(registers.B >> 1);
						if (registers.B == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRL_C:
					{
						byte carry = (byte)(registers.C & 0x1);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.C = (byte)(registers.C >> 1);
						if (registers.C == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRL_D:
					{
						byte carry = (byte)(registers.D & 0x1);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.D = (byte)(registers.D >> 1);
						if (registers.D == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRL_E:
					{
						byte carry = (byte)(registers.E & 0x1);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.E = (byte)(registers.E >> 1);
						if (registers.E == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRL_H:
					{
						byte carry = (byte)(registers.H & 0x1);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.H = (byte)(registers.H >> 1);
						if (registers.H == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRL_L:
					{
						byte carry = (byte)(registers.L & 0x1);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.L = (byte)(registers.L >> 1);
						if (registers.L == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SRL_xHL:
					{
						byte carry = (byte)(Memory[registers.HL] & 0x1);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						Memory[registers.HL] = (byte)(Memory[registers.HL] >> 1);
						if (Memory[registers.HL] == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RL_A:
					{
						byte carry = (byte)(registers.A & 0x80);
						byte inp = (byte)(((registers.F >> 4) & 0x1));

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A << 1);
						registers.A = (byte)(registers.A + inp);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RL_B:
					{
						byte carry = (byte)(registers.B & 0x80);
						byte inp = (byte)(((registers.F >> 4) & 0x1));

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.B = (byte)(registers.B << 1);
						registers.B = (byte)(registers.B + inp);
						if (registers.B == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RL_C:
					{
						byte carry = (byte)(registers.C & 0x80);
						byte inp = (byte)(((registers.F >> 4) & 0x1));

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.C = (byte)(registers.C << 1);
						registers.C = (byte)(registers.C + inp);
						if (registers.C == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RL_D:
					{
						byte carry = (byte)(registers.D & 0x80);
						byte inp = (byte)(((registers.F >> 4) & 0x1));

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.D = (byte)(registers.D << 1);
						registers.D = (byte)(registers.D + inp);
						if (registers.D == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RL_E:
					{
						byte carry = (byte)(registers.E & 0x80);
						byte inp = (byte)(((registers.F >> 4) & 0x1));

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.E = (byte)(registers.E << 1);
						registers.E = (byte)(registers.E + inp);
						if (registers.E == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RL_H:
					{
						byte carry = (byte)(registers.H & 0x80);
						byte inp = (byte)(((registers.F >> 4) & 0x1));

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.H = (byte)(registers.H << 1);
						registers.H = (byte)(registers.H + inp);
						if (registers.H == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RL_L:
					{
						byte carry = (byte)(registers.L & 0x80);
						byte inp = (byte)(((registers.F >> 4) & 0x1));

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.L = (byte)(registers.L << 1);
						registers.L = (byte)(registers.L + inp);
						if (registers.L == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RL_xHL:
					{
						byte carry = (byte)(Memory[registers.HL] & 0x80);
						byte inp = (byte)(((registers.F >> 4) & 0x1));

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						Memory[registers.HL] = (byte)(Memory[registers.HL] << 1);
						Memory[registers.HL] = (byte)(Memory[registers.HL] + inp);
						if (Memory[registers.HL] == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RLC_A:
					{
						byte carry = (byte)(registers.A & 0x80);
						byte inp = (byte)(carry >> 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.A = (byte)(registers.A << 1);
						registers.A = (byte)(registers.A + inp);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RLC_B:
					{
						byte carry = (byte)(registers.B & 0x80);
						byte inp = (byte)(carry >> 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.B = (byte)(registers.B << 1);
						registers.B = (byte)(registers.B + inp);
						if (registers.B == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RLC_C:
					{
						byte carry = (byte)(registers.C & 0x80);
						byte inp = (byte)(carry >> 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.C = (byte)(registers.C << 1);
						registers.C = (byte)(registers.C + inp);
						if (registers.C == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RLC_D:
					{
						byte carry = (byte)(registers.D & 0x80);
						byte inp = (byte)(carry >> 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.D = (byte)(registers.D << 1);
						registers.D = (byte)(registers.D + inp);
						if (registers.D == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RLC_E:
					{
						byte carry = (byte)(registers.E & 0x80);
						byte inp = (byte)(carry >> 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.E = (byte)(registers.E << 1);
						registers.E = (byte)(registers.E + inp);
						if (registers.E == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RLC_H:
					{
						byte carry = (byte)(registers.H & 0x80);
						byte inp = (byte)(carry >> 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.H = (byte)(registers.H << 1);
						registers.H = (byte)(registers.H + inp);
						if (registers.H == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RLC_L:
					{
						byte carry = (byte)(registers.L & 0x80);
						byte inp = (byte)(carry >> 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.L = (byte)(registers.L << 1);
						registers.L = (byte)(registers.L + inp);
						if (registers.L == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.RLC_xHL:
					{
						byte carry = (byte)(Memory[registers.HL] & 0x80);
						byte inp = (byte)(carry >> 7);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						Memory[registers.HL] = (byte)(Memory[registers.HL] << 1);
						Memory[registers.HL] = (byte)(Memory[registers.HL] + inp);
						if (Memory[registers.HL] == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						if (0 != (carry))
						{
							registers.F = (byte)(registers.F | FlagSet_C);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_C);
						}
					}
					break;

				case OpCodeCB.SWAP_A:
					{
						byte hn = (byte)(registers.A >> 4);
						byte ln = (byte)(registers.A << 4);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_C);
						if (registers.A == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.A = (byte)(hn | ln);
					}
					break;

				case OpCodeCB.SWAP_B:
					{
						byte hn = (byte)(registers.B >> 4);
						byte ln = (byte)(registers.B << 4);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_C);
						if (registers.B == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.B = (byte)(hn | ln);
					}
					break;

				case OpCodeCB.SWAP_C:
					{
						byte hn = (byte)(registers.C >> 4);
						byte ln = (byte)(registers.C << 4);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_C);
						if (registers.C == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.C = (byte)(hn | ln);
					}
					break;

				case OpCodeCB.SWAP_D:
					{
						byte hn = (byte)(registers.D >> 4);
						byte ln = (byte)(registers.D << 4);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_C);
						if (registers.D == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.D = (byte)(hn | ln);
					}
					break;

				case OpCodeCB.SWAP_E:
					{
						byte hn = (byte)(registers.E >> 4);
						byte ln = (byte)(registers.E << 4);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_C);
						if (registers.E == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.E = (byte)(hn | ln);
					}
					break;

				case OpCodeCB.SWAP_H:
					{
						byte hn = (byte)(registers.H >> 4);
						byte ln = (byte)(registers.H << 4);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_C);
						if (registers.H == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.H = (byte)(hn | ln);
					}
					break;

				case OpCodeCB.SWAP_L:
					{
						byte hn = (byte)(registers.L >> 4);
						byte ln = (byte)(registers.L << 4);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_C);
						if (registers.L == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						registers.L = (byte)(hn | ln);
					}
					break;

				case OpCodeCB.SWAP_xHL:
					{
						byte hn = (byte)(Memory[registers.HL] >> 4);
						byte ln = (byte)(Memory[registers.HL] << 4);

						registers.F = (byte)(registers.F & FlagReset_N);
						registers.F = (byte)(registers.F & FlagReset_H);
						registers.F = (byte)(registers.F & FlagReset_C);
						if (Memory[registers.HL] == 0)
						{
							registers.F = (byte)(registers.F | FlagSet_Z);
						}
						else
						{
							registers.F = (byte)(registers.F & FlagReset_Z);
						}
						Memory[registers.HL] = (byte)(hn | ln);
					}
					break;

			}
		}
	}
	[StructLayout(LayoutKind.Explicit)]
	public partial class Registers
	{
		[FieldOffset(1)]
		public byte A;
		[FieldOffset(0)]
		public byte F;
		[FieldOffset(3)]
		public byte B;
		[FieldOffset(2)]
		public byte C;
		[FieldOffset(5)]
		public byte D;
		[FieldOffset(4)]
		public byte E;
		[FieldOffset(7)]
		public byte H;
		[FieldOffset(6)]
		public byte L;
		[FieldOffset(8)]
		public byte IFF;
		[FieldOffset(0)]
		public ushort AF;
		[FieldOffset(2)]
		public ushort BC;
		[FieldOffset(4)]
		public ushort DE;
		[FieldOffset(6)]
		public ushort HL;
		[FieldOffset(9)]
		public ushort PC;
		[FieldOffset(11)]
		public ushort SP;
		[FieldOffset(13)]
		public byte IME;
		[FieldOffset(14)]
		public byte EI_;
		[FieldOffset(15)]
		public byte DI_;
		[FieldOffset(16)]
		public byte halt;
	}
}
