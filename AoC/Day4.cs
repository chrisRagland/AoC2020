using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC
{
	public class Day4
	{
		public void RunDay()
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
			Console.WriteLine("Day 4 - Part 1: " + validPart1Passports.Count());

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
			Console.WriteLine("Day 4 - Part 2: " + validPassportsPart2);
		}

	}
}
