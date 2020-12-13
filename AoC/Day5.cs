using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day5
	{
		public void RunDay()
		{
			var boardingPasses = File.ReadAllLines("Day5.txt").ToArray();
			var seatIds = new List<int>();
			foreach (var item in boardingPasses)
			{
				seatIds.Add(Convert.ToInt32(item.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2));
			}
			var sortedSeatIDs = seatIds.OrderBy(x => x).ToList();
			Console.WriteLine("Day 5 - Part 1: " + sortedSeatIDs.Last());
			List<int> gaps = Enumerable.Range(sortedSeatIDs.First(), sortedSeatIDs.Count()).Except(sortedSeatIDs).ToList();
			Console.WriteLine("Day 5 - Part 2: " + gaps.First());
		}
	}
}
