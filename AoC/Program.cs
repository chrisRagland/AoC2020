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
			/*
			// Day 1 - Part 1
			var hsLines = File.ReadAllLines("Day1.txt").Select(x => Convert.ToInt32(x)).OrderBy(x => x).ToHashSet<int>();
			bool found = false;
			foreach (var item in hsLines)
			{
				if (hsLines.Contains(2020-item))
				{
					Console.WriteLine("Day 1 - Part 1: " + (item * (2020-item)));
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
					if (hsLines.Contains(2020-item-innerItem))
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
			*/
			/*
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
				if((passwordComponents[2][firstLocation] == requiredChar) ^ (passwordComponents[2][secondLocation] == requiredChar))
					valid++;
			}
			Console.WriteLine("Day 2 - Part 2: " + valid);
			*/
			/*
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
			var part2Answer = trees.Aggregate(1,(x,y) => x * y);
			Console.WriteLine("Day 3 - Part 1: " + trees[1]);
			Console.WriteLine("Day 3 - Part 2: " + part2Answer);
			*/

			//Day 4 - Parts 1 & 2
			//Declare
			var passportLines = File.ReadAllText("Day4.txt").Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			var requiredFields = new List<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
			var passports = new List<Dictionary<string,string>>();

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

				validPassportsPart2++;
			}

			Console.WriteLine("Day 4 - Part 1: " + validPart1Passports.Count());
			Console.WriteLine("Day 4 - Part 2: " + validPassportsPart2);

		}
	}
}
