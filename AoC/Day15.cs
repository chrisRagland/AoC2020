using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
	public class Day15
	{
		public void RunDay()
		{
			var input = "20,0,1,11,6,3";
			SolveDay(input.Split(',').Select(x => Convert.ToInt32(x)).ToList(), 2020);
			SolveDay(input.Split(',').Select(x => Convert.ToInt32(x)).ToList(), 30000000);
		}

		private void SolveDay(List<int> input, int target)
		{
			var spoken = new Dictionary<int, int>();
			int originalCount = input.Count;
			var n = 1;

			for (int i = 0; i < input.Count; i++)
			{
				int current = input[i];
				if (spoken.ContainsKey(current))
				{
					int diff = i - spoken[current];
					input.Add(diff);
					spoken[current] = i;
				}
				else
				{
					spoken.Add(current, i);
					if (i >= (originalCount - 1))
					{
						input.Add(0);
					}
				}
				if (n == target)
				{
					Console.WriteLine("Day 15 - Part " + (target == 2020 ? "1" : "2") + ": " + current);
					return;
				}
				n++;
			}
		}
	}
}
