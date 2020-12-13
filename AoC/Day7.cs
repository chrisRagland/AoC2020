using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC
{
	public class Day7
	{
		private Dictionary<Bag, List<Bag>> bags;
		private List<string> searched;

		public void RunDay()
		{
			var bagsText = File.ReadAllText("Day7.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			int partOneCount = 0;
			int partTwoCount = 0;
			searched = new List<string>();
			bags = new Dictionary<Bag, List<Bag>>();

			foreach (var item in bagsText)
			{
				var input = item.Split(" contain ", StringSplitOptions.RemoveEmptyEntries);
				var holds = input[1].Replace('.', ' ').Split(',', StringSplitOptions.RemoveEmptyEntries);
				var thisHolds = new List<Bag>();

				foreach (var bagtext in holds)
				{
					if (bagtext != "no other bags")
					{
						var bagname = bagtext.Trim();
						int count = 1;
						if (Regex.IsMatch(bagname, "^[0-9]"))
						{
							count = Convert.ToInt32(bagname.Substring(0, bagname.IndexOf(' ')));
							bagname = bagname.Substring(bagname.IndexOf(' ')).Trim();
						}

						if (bagname.EndsWith("s"))
							bagname = bagname.Substring(0, bagname.Length - 1);

						thisHolds.Add(new Bag() { Count = count, Name = bagname });
					}
					else
					{
						thisHolds.Add(new Bag() { Count = 1, Name = "no other bag" });
					}
				}

				var containerName = input[0];
				if (containerName.EndsWith('s'))
				{
					containerName = containerName.Substring(0, containerName.Length - 1);
				}

				bags.Add(new Bag() { Count = 1, Name = containerName }, thisHolds);
			}

			partOneCount = NestedBags("shiny gold bag");
			Console.WriteLine("Day 7 - Part 1: " + partOneCount);

			partTwoCount = BagsInGold("shiny gold bag");
			Console.WriteLine("Day 7 - Part 2: " + partTwoCount);
		}

		public int NestedBags(string bagToFind)
		{
			var currentNesting = 0;
			searched.Add(bagToFind);

			foreach (var item in bags)
			{
				if (item.Value.Any(x => x.Name.Equals(bagToFind)))
				{
					if (!searched.Contains(item.Key.Name))
					{
						currentNesting++;
						currentNesting += NestedBags(item.Key.Name);
					}
				}
			}

			return currentNesting;
		}

		public int BagsInGold(string bagToFind)
		{
			var currentTotal = 0;

			var currentBag = bags.Where(x => x.Key.Name.Equals(bagToFind)).Select(x => x.Value).FirstOrDefault();
			foreach (var item in currentBag)
			{
				if (item.Name == "no other bag")
					return 0;
				else
				{
					currentTotal += ((item.Count * BagsInGold(item.Name)) + item.Count);
				}
			}

			return currentTotal;
		}
	}
}
