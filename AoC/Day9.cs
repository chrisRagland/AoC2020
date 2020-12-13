using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day9
	{
		public void RunDay()
		{
			var input = File.ReadAllText("Day9.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt64(x)).ToArray();

			//Load The Buffer
			long[] buffer = new long[25];
			for (int i = 0; i < buffer.Length; i++)
				buffer[i] = input[i];

			//Process
			long target = 0;
			for (int i = buffer.Length; i < input.Length; i++)
			{
				var numberToFind = input[i];
				var currentValues = new List<long>();

				for (int j = 0; j < buffer.Length - 1; j++)
					for (int k = (j + 1); k < buffer.Length; k++)
						currentValues.Add(buffer[j] + buffer[k]);

				if (!currentValues.Contains(numberToFind))
				{
					target = input[i];
					break;
				}

				//Reset the buffer
				for (int j = 0; j < buffer.Length - 1; j++)
					buffer[j] = buffer[j + 1];

				buffer[buffer.Length - 1] = numberToFind;
			}

			Console.WriteLine("Day 9 - Part 1: " + target);

			for (int i = 0; i < input.Length; i++)
			{
				var currentSum = input[i];
				var upperBounds = i + 1;
				while (currentSum < target)
				{
					currentSum = input.Skip(i).Take(upperBounds - i).Sum();
					if (currentSum == target)
					{
						var ourValues = input.Skip(i).Take(upperBounds - i);
						Console.WriteLine("Day 9 - Part 2: " + (ourValues.Min() + ourValues.Max()));
						return;
					}
					upperBounds++;
				}
			}
		}
	}
}
