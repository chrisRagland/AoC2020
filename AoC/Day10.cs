using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day10
	{
		private int MaxValue = 0;
		private List<int> InputValues = null;
		private long[] foundValues = null;

		public void RunDay()
		{
			InputValues = File.ReadAllText("Day10.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).OrderBy(x => x).ToList();

			Part1();
			Part2();
		}

		private void Part1()
		{
			int Ones = 1;
			int Threes = 1;

			for (int i = 0; i < InputValues.Count() - 1; i++)
			{
				int diff = InputValues[i + 1] - InputValues[i];
				switch (diff)
				{
					case 1:
						Ones++;
						break;
					case 3:
						Threes++;
						break;
					default:
						break;
				}
			}

			Console.WriteLine("Day 10 - Part 1: " + (Ones * Threes));
		}

		private void Part2()
		{
			foundValues = Enumerable.Range(0, InputValues.Count).Select(x => (long)-1).ToArray();
			MaxValue = InputValues.Max();
			Console.WriteLine("Day 10 - Part 2: " + FindPathCount(-1));
		}

		private long FindPathCount(int index)
		{
			long possible = 0;
			int min = 0;
			if (index >= 0)
			{
				min = InputValues[index];

				if (foundValues[index] > 0)
					return foundValues[index];
			}
			if (min == MaxValue)
				return 1;

			int potOne = index + 1;
			int potTwo = index + 2;
			int potThr = index + 3;

			if (InputValues.Count > potOne && (InputValues[potOne] - min) <= 3)
				possible += FindPathCount(index + 1);

			if (InputValues.Count > potTwo && (InputValues[potTwo] - min) <= 3)
				possible += FindPathCount(potTwo);

			if (InputValues.Count > potThr && (InputValues[potThr] - min) <= 3)
				possible += FindPathCount(potThr);

			if (index >= 0)
				if (foundValues[index] < 0)
					foundValues[index] = possible;

			return possible;
		}

	}
}
