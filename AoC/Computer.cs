using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
	public class Computer
	{
		public List<Instruction> Instructions { get; set; }
		public int Output { get; set; }
		public int CurrentInstruction { get; set; }

		public enum CommandName
		{
			nop,
			acc,
			jmp
		}

		public struct Instruction
		{
			public CommandName Command { get; set; }
			public int Value { get; set; }
		}

		public Computer(string[] originalInstructions)
		{
			
			Instructions = new List<Instruction>();
			ParseInstructions(originalInstructions);

		}

		private void ParseInstructions(string[] originalInstructions)
		{
			for (int i = 0; i < originalInstructions.Length; i++)
			{
				var splitValues = originalInstructions[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
				var newInstruction = new Instruction();
				newInstruction.Value = Convert.ToInt32(splitValues[1]);
				switch (splitValues[0])
				{
					case "nop":
						newInstruction.Command = CommandName.nop;
						break;
					case "jmp":
						newInstruction.Command = CommandName.jmp;
						break;
					case "acc":
						newInstruction.Command = CommandName.acc;
						break;
					default:
						break;
				}
				Instructions.Add(newInstruction);
			}
		}

		public void FixInstructions(List<CommandName> corruptedCommands)
		{
			if (corruptedCommands != null && corruptedCommands.Count >= 2)
			{
				for (int currentIndex = 0; currentIndex < Instructions.Count; currentIndex++)
				{
					if (corruptedCommands.Contains(Instructions[currentIndex].Command))
					{
						foreach (var item in corruptedCommands.Where(x => x != Instructions[currentIndex].Command))
						{
							var oldCommand = Instructions[currentIndex];
							Instructions[currentIndex] = new Instruction() { Command = item, Value = Instructions[currentIndex].Value };

							CurrentInstruction = 0;

							Run();

							if (CurrentInstruction == Instructions.Count)
							{
								return;
							}
							else
							{
								Instructions[currentIndex] = oldCommand;
							}
						}
					}
				}
			}
		}

		public void Run()
		{
			var visitedCommandLocation = new List<int>();
			Output = 0;

			for (; CurrentInstruction < Instructions.Count; CurrentInstruction++)
			{
				if (visitedCommandLocation.Contains(CurrentInstruction))
					return;

				visitedCommandLocation.Add(CurrentInstruction);

				switch (Instructions[CurrentInstruction].Command)
				{
					case CommandName.nop:
						break;
					case CommandName.acc:
						Output += Instructions[CurrentInstruction].Value;
						break;
					case CommandName.jmp:
						if (Instructions[CurrentInstruction].Value == -1)
							return;

						CurrentInstruction += Instructions[CurrentInstruction].Value - 1;
						break;
					default:
						break;
				}
			}

		}
	}
}