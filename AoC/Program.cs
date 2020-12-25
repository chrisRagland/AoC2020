using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC
{
	class Program
	{
		static void Main(string[] args)
		{
			SolveDay25();
		}

		private static void SolveDay25()
		{
			long doorPublicKey = 9232416;
			long cardPublicKey = 14144084;
			long divideBy = 20201227;
			long subjectNumber = 7;
			long value = 1;
			int n = 0;
			while (true)
			{
				n++;
				value *= subjectNumber;
				value %= divideBy;
				if (value == cardPublicKey)
					break;
			}
			subjectNumber = doorPublicKey;
			value = 1;
			for (int i = 0; i < n; i++)
			{
				value *= subjectNumber;
				value %= divideBy;
			}
			Console.WriteLine("Day 25: " + value);
		}

		private static void SolveDay24()
		{
			var input = File.ReadAllLines("Test.txt");

			HashSet<Tuple<int, int>> flipped = new HashSet<Tuple<int, int>>();

			foreach (var item in input)
			{
				int currentX = 0;
				int currentY = 0;

				for (int i = 0; i < item.Length; i++)
				{
					if (item[i] == 'e')
						currentX++;
					else if (item[i] == 'w')
						currentX--;
					else
					{
						var strNextDirection = item.Substring(i, 2);
						if (strNextDirection == "nw")
							currentY++;
						else if (strNextDirection == "ne")
						{
							currentX++;
							currentY++;
						}
						else if (strNextDirection == "se")
							currentY--;
						else if (strNextDirection == "sw")
						{
							currentX--;
							currentY--;
						}

						i++;
					}
				}

				var tileToFlip = new Tuple<int, int>(currentX, currentY);
				if (flipped.Contains(tileToFlip))
					flipped.Remove(tileToFlip);
				else
					flipped.Add(tileToFlip);
			}

			Console.WriteLine("Day 24 - Part 1: " + flipped.Count);

			for (int i = 1; i <= 100; i++)
			{
				var newFlipped = new HashSet<Tuple<int, int>>(flipped);

				foreach (var item in flipped)
				{
					
					var containedCount = 0;
					if (flipped.Contains(new Tuple<int, int>(item.Item1 - 1, item.Item2)))
						containedCount++;

					if (flipped.Contains(new Tuple<int, int>(item.Item1, item.Item2 - 1)))
						containedCount++;

					if (flipped.Contains(new Tuple<int, int>(item.Item1 - 1, item.Item2 - 1)))
						containedCount++;

					if (containedCount > 2)
					{
						newFlipped.Remove(item);
						continue;
					}

					if (flipped.Contains(new Tuple<int, int>(item.Item1 + 1, item.Item2)))
						containedCount++;

					if (containedCount > 2)
					{
						newFlipped.Remove(item);
						continue;
					}

					if (flipped.Contains(new Tuple<int, int>(item.Item1, item.Item2 + 1)))
						containedCount++;

					if (containedCount == 1)
						continue;
					else if (containedCount > 2)
					{
						newFlipped.Remove(item);
						continue;
					}

					if (flipped.Contains(new Tuple<int, int>(item.Item1 + 1, item.Item2 + 1)))
						containedCount++;

					if (containedCount == 0 || containedCount > 2)
					{
						newFlipped.Remove(item);
						continue;
					}

				}

				int minX = flipped.Select(x => x.Item1).Min();
				int maxX = flipped.Select(x => x.Item1).Max();
				int minY = flipped.Select(x => x.Item2).Min();
				int maxY = flipped.Select(x => x.Item2).Max();

				var cb = new ConcurrentBag<Tuple<int, int>>();

				Parallel.For((minX - 1), (maxX + 2), x =>
				{
					for (int y = (minY - 1); y <= (maxY + 1); y++)
					{
						var current = new Tuple<int, int>(x, y);

						var nextToTile = new List<Tuple<int, int>>()
						{
							new Tuple<int, int>((x - 1), y),
							new Tuple<int, int>(x, (y - 1)),
							new Tuple<int, int>((x - 1), (y - 1)),
							new Tuple<int, int>((x + 1), y),
							new Tuple<int, int>(x, (y + 1)),
							new Tuple<int, int>((x + 1), (y + 1)),
						};

						int containedCount = nextToTile.Where(x => flipped.Contains(x)).Count();
						if (containedCount == 2)
							cb.Add(current);
					}
				});

				foreach (var item in cb)
					newFlipped.Add(item);

				flipped = new HashSet<Tuple<int, int>>(newFlipped);
				//if (i % 25 == 0)
				//	Console.WriteLine(i + " : " + flipped.Count);
			}

			Console.WriteLine("Day 24 - Part 2: " + flipped.Count);

		}
	}
}