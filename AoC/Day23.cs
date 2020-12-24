using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
	public class Day23
	{
		int[] cupsCircle;
		int largestValue;

		public void RunDay()
		{
			var cups = "167248359";
			SolveDay(cups, 100);
			SolveDay(cups, 10000000);
		}

		private void SolveDay(string cupInput, int iValue)
		{
			//Get the originalCupOrder
			var originalCupOrder = cupInput.Select(x => Convert.ToInt32(x.ToString())).ToList();
			if (iValue > 100)
				originalCupOrder.AddRange(Enumerable.Range(10, 999991));

			//Set the value we wrap around to if we try to find the 0th cup
			largestValue = originalCupOrder.Count;

			var currentCupValue = originalCupOrder.First();
			originalCupOrder.Add(currentCupValue);

			//Create array to store the next cup's value at the current cup's value as an index (starting with 1)
			cupsCircle = new int[originalCupOrder.Count];
			for (int i = 0; i < originalCupOrder.Count - 1; i++)
				cupsCircle[originalCupOrder[i]] = originalCupOrder[(i + 1)];

			//Make the required moves for the game
			for (int i = 0; i < iValue; i++)
			{
				var cupsToPickup = new List<int>();

				//Get the next three cups
				var nextCup = cupsCircle[currentCupValue];
				for (int j = 0; j < 3; j++)
				{
					cupsToPickup.Add(nextCup);
					nextCup = cupsCircle[nextCup];
				}

				//With the cups "removed" fix the "pointer" so the current cup now points to the 4th cup
				cupsCircle[currentCupValue] = nextCup;

				//Calculate the destination cup value
				var destinationCupValue = currentCupValue;
				while (destinationCupValue == currentCupValue || cupsToPickup.Contains(destinationCupValue))
				{
					destinationCupValue--;
					if (destinationCupValue == 0)
						destinationCupValue = largestValue;
				}

				//Fix the "pointers" now that we know where the three cups are going to be inserted
				cupsCircle[cupsToPickup.Last()] = cupsCircle[destinationCupValue];
				cupsCircle[destinationCupValue] = cupsToPickup.First();

				currentCupValue = cupsCircle[currentCupValue];
			}

			if (iValue <= 100)
			{
				//Solving Part 1
				var result = string.Empty;
				var currentCup = cupsCircle[1];
				for (int i = 0; i < 8; i++)
				{
					result += currentCup.ToString();
					currentCup = cupsCircle[currentCup];
				}
				Console.WriteLine("Day 23 - Part 1: " + result);
			}
			else
			{
				//Solving Part 2
				long result = (long)cupsCircle[1] * (long)cupsCircle[cupsCircle[1]];
				Console.WriteLine("Day 23 - Part 2: " + result.ToString());
			}
		}
	}
}