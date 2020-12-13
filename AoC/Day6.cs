using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day6
	{
		public void RunDay()
		{
			var customsAnswers = File.ReadAllText("Day6.txt").Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			var partOneCount = 0;

			foreach (var groupAnswer in customsAnswers)
			{

				var clean = groupAnswer.Replace("\r\n", "");
				var cleanCount = clean.Select(x => x).Distinct().Count();
				partOneCount += cleanCount;
			}
			Console.WriteLine("Day 6 - Part 1: " + partOneCount);

			var countTwo = 0;
			foreach (var item in customsAnswers)
			{
				List<List<char>> answers = new List<List<char>>();
				foreach (var innerItem in item.Split("\r\n", StringSplitOptions.RemoveEmptyEntries))
				{
					answers.Add(innerItem.Select(x => x).Distinct().ToList());
				}

				List<char> common = answers[0];
				for (int i = 1; i < answers.Count(); i++)
				{
					common = common.Intersect(answers[i]).ToList();
				}
				countTwo += common.Count();
			}
			Console.WriteLine("Day 6 - Part 2: " + countTwo);
		}
	}
}
