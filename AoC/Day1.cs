using System;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day1
	{
		public void RunDay()
		{
			// Day 1 - Part 1
			var hsLines = File.ReadAllLines("Day1.txt").Select(x => Convert.ToInt32(x)).OrderBy(x => x).ToHashSet<int>();
			bool found = false;
			foreach (var item in hsLines)
			{
				if (hsLines.Contains(2020 - item))
				{
					Console.WriteLine("Day 1 - Part 1: " + (item * (2020 - item)));
					found = true;
				}
				if (found)
					break;
			}
			
			//Day 1 - Part 2
			foreach (var item in hsLines)
			{
				foreach (var innerItem in hsLines)
				{
					if (hsLines.Contains(2020 - item - innerItem))
					{
						Console.WriteLine("Day 1 - Part 2: " + (item * innerItem * (2020 - item - innerItem)));
						return;
					}
					if ((item + innerItem) > 2020)
						break;
				}
			}
		}
	}
}