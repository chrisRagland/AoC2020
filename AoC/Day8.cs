using System;
using System.Collections.Generic;
using System.IO;

namespace AoC
{
	public class Day8
	{
		public void RunDay()
		{
			var input = File.ReadAllText("Day8.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			var partOneComputer = new Computer(input);
			partOneComputer.Run();
			Console.WriteLine("Day 8 - Part 1: " + partOneComputer.Output);

			var partTwoComputer = new Computer(input);
			partTwoComputer.FixInstructions(
				new List<Computer.CommandName>() {
					Computer.CommandName.jmp,
					Computer.CommandName.nop
			});
			Console.WriteLine("Day 8 - Part 2: " + partTwoComputer.Output);
		}
	}
}
