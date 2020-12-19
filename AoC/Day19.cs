using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace AoC
{
	public class Day19
	{
		private int aRule = 0;
		private int bRule = 0;
		private Dictionary<int, List<List<int>>> rules;

		public void RunDay()
		{
			SolveDay();
		}

		private void SolveDay()
		{
			var lines = File.ReadAllLines("Day19.txt").Where(x => !string.IsNullOrEmpty(x));

			rules = new Dictionary<int, List<List<int>>>();
			var input = new List<string>();

			//Parse the input
			foreach (var item in lines)
			{
				if (item.Contains(":"))
				{
					var bits = item.Split(':', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
					var index = Convert.ToInt32(bits[0]);
					if (bits[1].Contains("a"))
					{
						aRule = index;
					}
					else if (bits[1].Contains("b"))
					{
						bRule = index;
					}
					else
					{
						var ruleBits = bits[1].Split('|').ToArray();
						var dictEntry = new List<List<int>>();
						foreach (var bit in ruleBits)
						{
							var thisBit = bit.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
							dictEntry.Add(thisBit);
						}
						rules.Add(index, dictEntry);
					}
				}
				else
				{
					input.Add(item);
				}
			}

			//Part 1
			//Valid input consists of
			//Rule: 42 42 31

			//Part 2
			//Valid input consists of
			//Rule: 42 {42} 42 {42 31} 31

			var validFourtyTwos = new List<string>();
			validFourtyTwos.AddRange(GetStrings(rules[42][0]));
			validFourtyTwos.AddRange(GetStrings(rules[42][1]));

			var validThirtyOnes = new List<string>();
			validThirtyOnes.AddRange(GetStrings(rules[31][0]));
			validThirtyOnes.AddRange(GetStrings(rules[31][1]));

			var ruleLength = validFourtyTwos.First().Length;
			var partOneAnswer = 0;
			var partTwoAnswer = 0;

			foreach (var item in input)
			{
				var parsed = Enumerable.Range(0, item.Length / ruleLength).Select(i => item.Substring(i * ruleLength, ruleLength)).ToList();
				if (validFourtyTwos.Contains(parsed[0]) && validFourtyTwos.Contains(parsed[1]) && validThirtyOnes.Contains(parsed[parsed.Count - 1]))
				{
					if (parsed.Count == 3)
					{
						partOneAnswer++;
						partTwoAnswer++;
					}
					else
					{
						var remaining = parsed.GetRange(2, parsed.Count - 3);

						var fourtTwoCount = 0;
						var thirtyOneCount = 0;
						var passed = false;
						var valid = true;

						foreach (var inner in remaining)
						{
							if (validFourtyTwos.Contains(inner) && !passed)
								fourtTwoCount++;
							else
							{
								passed = true;
								if (validThirtyOnes.Contains(inner))
									thirtyOneCount++;
								else
								{
									valid = false;
									break;
								}
							}
						}

						if (valid)
							if (thirtyOneCount <= fourtTwoCount)
								partTwoAnswer++;
					}
				}
			}

			Console.WriteLine("Day 19 - Part 1: " + partOneAnswer);
			Console.WriteLine("Day 19 - Part 2: " + partTwoAnswer);

		}

		private IEnumerable<string> GetStrings(List<int> startingRule)
		{
			var validStrings = new List<string>();
			var invalid = new List<List<int>>() { startingRule };

			while (invalid.Count() > 0)
			{
				var thisInvalid = new List<List<int>>();
				foreach (var item in invalid)
				{
					bool anyInvalid = false;
					for (int i = 0; i < item.Count; i++)
					{
						if (item[i] == aRule || item[i] == bRule)
							continue;

						anyInvalid = true;
						var ruleLookup = rules[item[i]];

						var ruleOne = item.Select(x => x).ToList();
						ruleOne.RemoveAt(i);
						ruleOne.InsertRange(i, ruleLookup[0]);
						thisInvalid.Add(ruleOne);

						if (ruleLookup.Count > 1)
						{
							var ruleTwo = item.Select(x => x).ToList();
							ruleTwo.RemoveAt(i);
							ruleTwo.InsertRange(i, ruleLookup[1]);
							thisInvalid.Add(ruleTwo);
						}
						break;
					}
					if (!anyInvalid)
					{
						var thisString = string.Empty;
						for (int i = 0; i < item.Count; i++)
						{
							if (item[i] == aRule)
								thisString += "a";
							else if (item[i] == bRule)
								thisString += "b";
						}
						validStrings.Add(thisString);
					}
				}
				invalid = thisInvalid;
			}
			return validStrings;
		}

	}
}