using System;
using System.Threading;

namespace GruntXProductions.Quasar.VM
{
	public partial class Emulator
	{
		private void interpretShl(Instruction ins)
		{
			if(ins.Operand1.OperandAddressingMode == AddressingMode.DIRECT_REGISTER)
			{
				uint flags = GetGeneralPurposeRegister(Register.R12);
				Register dest = (Register)ins.Operand1.Value;
				uint op1 = GetGeneralPurposeRegister(dest);
				uint op2 = 0;
				if(ins.Operand2.OperandAddressingMode == AddressingMode.IMMEDIATE_32)
				{
					op2 = (uint)ins.Operand2.Value;
				}
				else if (ins.Operand2.OperandAddressingMode == AddressingMode.DIRECT_REGISTER)
				{
					op2 = GetGeneralPurposeRegister((Register)ins.Operand2.Value);
				}
				else
				{
					throw new InvalidOpcodeException(ins);
				}
				
				uint res = op1 << (int)op2;
		
				if((int)res < 0)
					flags |= Flags.SIGN;
				else
					flags &= ~Flags.SIGN;
				if(res == 0)
					flags |= Flags.ZERO;
				else
					flags &= ~Flags.ZERO;
				SetGeneralPurposeRegister(dest, res);
		
				SetGeneralPurposeRegister(Register.R12, flags);
				
			}
			else
				throw new InvalidOpcodeException(ins);
		}
		
		
	}
	
}