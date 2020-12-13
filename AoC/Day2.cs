using System;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day2
	{
		public void RunDay()
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
	}
}
