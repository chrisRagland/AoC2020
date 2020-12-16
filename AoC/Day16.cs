using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AoC
{
	public class Day16
	{
		public void RunDay()
		{
			var input = File.ReadAllText("Day16.txt").Split(new string[] { "\r\n" }, StringSplitOptions.None).ToArray();
			var rules = new Dictionary<string, List<int>>();
			List<int> myTicket = new List<int>();
			List<List<int>> otherTickets = new List<List<int>>();
			List<List<int>> validTickets = new List<List<int>>();

			bool rulesProcessed = false;
			bool myTicketProcessed = false;

			//Parse rules
			foreach (var item in input)
			{
				if (item.Equals(string.Empty))
				{
					if (!rulesProcessed)
					{
						rulesProcessed = true;
					}
					else
					{
						myTicketProcessed = true;
					}
				}
				else
				{
					if (!rulesProcessed)
					{
						var firstSplit = item.Split(':');
						var ruleName = firstSplit[0];
						var secondSplit = firstSplit[1].Split("or");
						var thirdSplit = secondSplit[0].Split('-').Select(x => Convert.ToInt32(x)).ToArray();

						var valueOne = thirdSplit[0];
						var valueTwo = thirdSplit[1];
						thirdSplit = secondSplit[1].Split('-').Select(x => Convert.ToInt32(x)).ToArray();
						var valueThree = thirdSplit[0];
						var valueFour = thirdSplit[1];

						var values = new List<int>();
						values.AddRange(Enumerable.Range(valueOne, (valueTwo - valueOne) + 1));
						values.AddRange(Enumerable.Range(valueThree, (valueFour - valueThree) + 1));
						rules.Add(ruleName, values.Distinct().ToList());
					}
					else if (!myTicketProcessed)
					{
						if (item.Contains("your"))
							continue;

						myTicket = item.Split(',').Select(x => Convert.ToInt32(x)).ToList();
					}
					else
					{
						if (item.Contains("nearby"))
							continue;

						otherTickets.Add(item.Split(',').Select(x => Convert.ToInt32(x)).ToList());
					}
				}
			}

			//Process
			var errorTotals = new List<int>();
			foreach (var item in otherTickets)
			{
				var validTicket = false;
				for (int i = 0; i < item.Count; i++)
				{
					int value = item[i];
					bool found = false;
					foreach (var innerItem in rules)
					{
						if (innerItem.Value.Contains(value))
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						errorTotals.Add(value);
						validTicket = false;
						break;
					}
					else
						validTicket = true;

				}
				if (validTicket)
					validTickets.Add(item);

			}

			Console.WriteLine("Day 16 - Part 1: " + errorTotals.Sum());

			var possibleContainer = new List<List<int>>();

			for (int i = 0; i < rules.Count; i++)
			{
				var possibleRules = new List<int>();
				for (int k = 0; k < validTickets[0].Count; k++)
				{
					var allValid = true;
					for (int j = 0; j < validTickets.Count; j++)
					{
						if (!rules.ElementAt(i).Value.Contains(validTickets[j][k]))
						{
							allValid = false;
							break;
						}
					}
					if (allValid)
						possibleRules.Add(k);

				}
				possibleContainer.Add(possibleRules);
			}

			while (possibleContainer.Sum(x => x.Count) > 20)
			{
				for (int i = 0; i < possibleContainer.Count; i++)
				{
					if (possibleContainer[i].Count == 1)
					{
						int valueToRemove = possibleContainer[i].First();
						for (int j = 0; j < possibleContainer.Count; j++)
						{
							if (i == j)
								continue;

							possibleContainer[j].Remove(valueToRemove);
						}
					}
				}
			}

			long partTwoAnswer = 1;
			for (int i = 0; i < rules.Count; i++)
			{
				if (rules.ElementAt(i).Key.Contains("departure"))
					partTwoAnswer *= myTicket[possibleContainer[i].First()];
			}
			Console.WriteLine("Day 16 - Part 2: " + partTwoAnswer);
		}
	}
}
