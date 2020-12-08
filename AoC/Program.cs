using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AoC
{
	class Program
	{
		static void Main(string[] args)
		{
			Day8();
		}

		private static void Day1()
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
			found = false;

			//Day 1 - Part 2
			foreach (var item in hsLines)
			{
				foreach (var innerItem in hsLines)
				{
					if (hsLines.Contains(2020 - item - innerItem))
					{
						Console.WriteLine("Day 1 - Part 2: " + (item * innerItem * (2020 - item - innerItem)));
						found = true;
					}
					if (found || (item + innerItem) > 2020)
						break;
				}
				if (found)
					break;
			}

		}

		private static void Day2()
		{
			var passwords = File.ReadAllLines("Day2.txt").ToArray();
			var valid = 0;
			// Day 2 - Part 1
			foreach (var item in passwords)
			{
				var passwordComponents = item.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				var characterRequirements = passwordComponents[0].Split('-', StringSplitOptions.RemoveEmptyEntries);
				var lowRequirement = Convert.ToInt32(characterRequirements[0]);
				var highRequirement = Convert.ToInt32(characterRequirements[1]);
				char requiredCharacter = passwordComponents[1][0];
				var characterCount = passwordComponents[2].Where(x => x == requiredCharacter).Count();
				if (characterCount >= lowRequirement && characterCount <= highRequirement)
					valid++;
			}
			Console.WriteLine("Day 2 - Part 1: " + valid);

			//Day 2 - Part 2
			valid = 0;
			foreach (var item in passwords)
			{
				var passwordComponents = item.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				var locationRequirements = passwordComponents[0].Split('-', StringSplitOptions.RemoveEmptyEntries);
				//Non-zero Indexed
				var firstLocation = Convert.ToInt32(locationRequirements[0]) - 1;
				var secondLocation = Convert.ToInt32(locationRequirements[1]) - 1;
				char requiredChar = passwordComponents[1][0];
				if ((passwordComponents[2][firstLocation] == requiredChar) ^ (passwordComponents[2][secondLocation] == requiredChar))
					valid++;
			}
			Console.WriteLine("Day 2 - Part 2: " + valid);
		}

		private static void Day3()
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
			var part2Answer = trees.Aggregate(1, (x, y) => x * y);
			Console.WriteLine("Day 3 - Part 1: " + trees[1]);
			Console.WriteLine("Day 3 - Part 2: " + part2Answer);
		}

		private static void Day4()
		{
			//Day 4 - Parts 1 & 2
			//Declare
			var passportLines = File.ReadAllText("Day4.txt").Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			var requiredFields = new List<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
			var passports = new List<Dictionary<string, string>>();
			var realPassports = new List<Passport>();

			//Process
			foreach (var item in passportLines)
			{
				var joinedThenSplitLine = item.Replace("\r\n", " ").Split(' ', StringSplitOptions.RemoveEmptyEntries);
				var currentPassport = new Dictionary<string, string>();
				foreach (var passportComponent in joinedThenSplitLine)
				{
					var kvp = passportComponent.Split(':', StringSplitOptions.RemoveEmptyEntries);
					currentPassport.Add(kvp[0], kvp[1]);
				}
				passports.Add(currentPassport);
			}

			//Validate
			var validPart1Passports = passports.Where(x => requiredFields.All(y => x.ContainsKey(y)));
			var validPassportsPart2 = 0;
			foreach (var item in validPart1Passports)
			{
				if (!Regex.IsMatch(item["byr"], "^(19[2-9][0-9]|200[0-2])$"))
					continue;

				if (!Regex.IsMatch(item["iyr"], "^(201[0-9]|2020)$"))
					continue;

				if (!Regex.IsMatch(item["eyr"], "^(202[0-9]|2030)$"))
					continue;

				if (!Regex.IsMatch(item["hgt"], "^(1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in)$"))
					continue;

				if (!Regex.IsMatch(item["hcl"], "^#[0-9a-f]{6}$"))
					continue;

				if (!Regex.IsMatch(item["ecl"], "^(amb|blu|brn|gry|grn|hzl|oth)$"))
					continue;

				if (!Regex.IsMatch(item["pid"], "^[0-9]{9}$"))
					continue;

				var newPassport = new Passport(item);
				realPassports.Add(newPassport);

				validPassportsPart2++;
			}

			Console.WriteLine("Day 4 - Part 1: " + validPart1Passports.Count());
			Console.WriteLine("Day 4 - Part 2: " + validPassportsPart2);
		}

		private static void Day5()
		{
			var boardingPasses = File.ReadAllLines("Day5.txt").ToArray();
			var seatIds = new List<int>();
			foreach (var item in boardingPasses)
			{
				seatIds.Add(Convert.ToInt32(item.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2));
			}
			var sortedSeatIDs = seatIds.OrderBy(x => x).ToList();
			List<int> gaps = Enumerable.Range(sortedSeatIDs.First(), sortedSeatIDs.Count()).Except(sortedSeatIDs).ToList();
			Console.WriteLine("Day 5 - Part 1: " + sortedSeatIDs.Last());
			Console.WriteLine("Day 5 - Part 2: " + gaps.First());
		}

		private static void Day6()
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

		private static Dictionary<Bag, List<Bag>> bags;
		private static List<string> searched;

		private static void Day7()
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
					} else
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

		public static int NestedBags(string bagToFind)
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

		public static int BagsInGold(string bagToFind)
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

		public static void Day8()
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
