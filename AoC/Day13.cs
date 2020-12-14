using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day13
	{
		public void RunDay()
		{
			var input = File.ReadAllText("Day13.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			SolvePartOne(input);
			SolvePartTwo(input);
		}

		public void SolvePartOne(string[] input)
		{
			var timestamp = Convert.ToInt32(input[0]);
			var buses = input[1].Split(',').Select((v,i) => new { v, i }).Where(x => x.v != "x").Select(x => new { Value = Convert.ToInt32(x.v), Index = x.i, Offset = Convert.ToInt32(x.v) - timestamp % Convert.ToInt32(x.v) });
			var answer = buses.OrderBy(x => x.Offset).First();
			Console.WriteLine("Day 13 - Part 1: " + (answer.Value * answer.Offset));
		}

		public void SolvePartTwo(string[] input)
		{
			long step = 1;
			long timestamp = 1;
			var buses = input[1].Split(',').Select((v, i) => new { v, i }).Where(x => x.v != "x").Select(x => new { Value = Convert.ToInt64(x.v), Index = x.i });
			foreach (var item in buses)
			{
				long value = item.Value;
				while ((timestamp + item.Index) % value != 0)
				{
					timestamp += step;
				}
				step *= value;
			}
			Console.WriteLine("Day 13 - Part 2: " + timestamp);
		}
	}
}