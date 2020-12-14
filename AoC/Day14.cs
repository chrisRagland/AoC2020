using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day14
	{
		public void RunDay()
		{
			var input = File.ReadAllText("Day14.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			Part1(input);
			Part2(input);
		}

		private void Part1(string[] input)
		{
			string mask = string.Empty;
			Dictionary<long, long> memory = new Dictionary<long, long>();

			for (int i = 0; i < input.Length; i++)
			{
				var splitValue = input[i].Split('=');
				if (splitValue[0].Trim() == "mask")
				{
					mask = splitValue[1].Trim();
				}
				else
				{
					long memLoc = Convert.ToInt64(splitValue[0].Trim().Substring(4, (splitValue[0].Trim().Length - 5)));
					long value = Convert.ToInt64(splitValue[1]);

					string binValue = Convert.ToString(value, 2).PadLeft(36, '0');
					string output = string.Empty;

					for (int j = 0; j < binValue.Length; j++)
					{
						if (mask[j] == 'X')
						{
							output += binValue[j];
						}
						else
						{
							output += mask[j];
						}

					}

					var convertedOutput = Convert.ToInt64(output, 2);

					if (memory.ContainsKey(memLoc))
					{
						memory[memLoc] = convertedOutput;
					}
					else
					{
						memory.Add(memLoc, convertedOutput);
					}
				}
			}
			Console.WriteLine("Day 14 - Part 1: " + memory.Sum(x => x.Value));
		}

		private void Part2(string[] input)
		{
			string mask = string.Empty;
			Dictionary<long, long> memory = new Dictionary<long, long>();

			for (int i = 0; i < input.Length; i++)
			{
				var splitValue = input[i].Split('=');
				if (splitValue[0].Trim() == "mask")
				{
					mask = splitValue[1].Trim();
				}
				else
				{
					long memLoc = Convert.ToInt64(splitValue[0].Trim().Substring(4, (splitValue[0].Trim().Length - 5)));
					long value = Convert.ToInt64(splitValue[1]);

					string memValue = Convert.ToString(memLoc, 2).PadLeft(36, '0');
					string output = string.Empty;

					for (int j = 0; j < memValue.Length; j++)
					{
						if (mask[j] == '0')
						{
							output += memValue[j];
						}
						else
						{
							output += mask[j];
						}

					}

					var allCombos = Combinations(output).Select(x => Convert.ToInt64(x, 2));

					foreach (var item in allCombos)
					{
						if (memory.ContainsKey(item))
						{
							memory[item] = value;
						}
						else
						{
							memory.Add(item, value);
						}
					}

				}
			}
			Console.WriteLine("Day 14 - Part 2: " + memory.Sum(x => x.Value));
		}

		//https://stackoverflow.com/a/28819490
		public IEnumerable<string> Combinations(string input)
		{
			int firstZero = input.IndexOf('X');   // Get index of first '0'
			if (firstZero == -1)      // Base case: no further combinations
				return new string[] { input };

			string prefix = input.Substring(0, firstZero);    // Substring preceding '0'
			string suffix = input.Substring(firstZero + 1);   // Substring succeeding '0'
															  // e.g. Suppose input was "fr0d00"
															  //      Prefix is "fr"; suffix is "d00"

			// Recursion: Generate all combinations of suffix
			// e.g. "d00", "d0o", "do0", "doo"
			var recursiveCombinations = Combinations(suffix);

			// Return sequence in which each string is a concatenation of the
			// prefix, either '0' or 'o', and one of the recursively-found suffixes
			return
				from chr in "01"  // char sequence equivalent to: new [] { '0', 'o' }
				from recSuffix in recursiveCombinations
				select prefix + chr + recSuffix;
		}
	}
}
