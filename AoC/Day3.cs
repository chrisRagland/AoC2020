using System;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day3
	{
		public void RunDay()
		{
			//Day 3 - Part 1 & Part 2
			var sleddingLines = File.ReadAllLines("Day3.txt").ToArray();
			var lineLength = sleddingLines[0].Length;
			int[] trees = { 0, 0, 0, 0, 0 };
			int[] indicies = { 0, 0, 0, 0, 0 };
			int[] stepsRight = { 1, 3, 5, 7, 1 };
			for (int i = 1; i < sleddingLines.Length; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (j < 4 || i % 2 == 0)
						indicies[j] += stepsRight[j];

					indicies[j] %= lineLength;
					if (sleddingLines[i][indicies[j]] == '#' && (j < 4 || (j == 4 && i % 2 == 0)))
						trees[j]++;
				}
			}
			Console.WriteLine("Day 3 - Part 1: " + trees[1]);
			var part2Answer = trees.Aggregate(1, (x, y) => x * y);
			Console.WriteLine("Day 3 - Part 2: " + part2Answer);
		}

	}
}
