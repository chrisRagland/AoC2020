using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using System.Threading.Tasks;

namespace AoC
{
	class Program
	{
		public enum Direction
		{
			e,
			se,
			sw,
			w,
			nw,
			ne,
			none
		}

		static void Main(string[] args)
		{
			//var day = new Day23();
			//day.RunDay();

			//six neighbors: east, southeast, southwest, west, northwest, and northeast
			//e, se, sw, w, nw, and ne

			var input = File.ReadAllLines("Test.txt");

			HashSet<Tuple<int, int, int>> flipped = new HashSet<Tuple<int, int, int>>();

			foreach (var item in input)
			{
				int currentX = 0;
				int currentY = 0;
				int currentZ = 0;

				for (int i = 0; i < item.Length; i++)
				{
					Direction nextDirection = Direction.none;
					if (item[i] == 'e')
						nextDirection = Direction.e;
					else if (item[i] == 'w')
						nextDirection = Direction.w;
					else
					{
						var strNextDirection = item.Substring(i, 2);
						if (strNextDirection == "nw")
							nextDirection = Direction.nw;
						else if (strNextDirection == "ne")
							nextDirection = Direction.ne;
						else if (strNextDirection == "se")
							nextDirection = Direction.se;
						else if (strNextDirection == "sw")
							nextDirection = Direction.sw;

						i++;
					}

					switch (nextDirection)
					{
						case Direction.e:
							currentX++;
							currentY--;
							break;
						case Direction.se:
							currentY--;
							currentZ++;
							break;
						case Direction.sw:
							currentX--;
							currentZ++;
							break;
						case Direction.w:
							currentX--;
							currentY++;
							break;
						case Direction.nw:
							currentY++;
							currentZ--;
							break;
						case Direction.ne:
							currentX++;
							currentZ--;
							break;
						default:
							break;
					}

				}

				Tuple<int, int, int> tileToFlip = new Tuple<int, int, int>(currentX, currentY, currentZ);
				if (flipped.Contains(tileToFlip))
					flipped.Remove(tileToFlip);
				else
					flipped.Add(tileToFlip);
			}

			int minX = int.MaxValue;
			int maxX = int.MinValue;
			int minY = int.MaxValue;
			int maxY = int.MinValue;
			int minZ = int.MaxValue;
			int maxZ = int.MinValue;

			for (int i = 1; i <= 100; i++)
			{
				var newFlipped = new HashSet<Tuple<int, int, int>>(flipped);
				foreach (var item in flipped)
				{
					if (item.Item1 > maxX)
						maxX = item.Item1;

					if (item.Item1 < minX)
						minX = item.Item1;

					if (item.Item2 > maxY)
						maxY = item.Item2;

					if (item.Item2 < minY)
						minY = item.Item2;

					if (item.Item3 > maxZ)
						maxZ = item.Item3;

					if (item.Item3 < minZ)
						minZ = item.Item3;

					List<Tuple<int, int, int>> nextToTile = new List<Tuple<int, int, int>>()
					{
						new Tuple<int, int, int>((item.Item1 - 1), (item.Item2 + 1), item.Item3),
						new Tuple<int, int, int>(item.Item1, (item.Item2 + 1), (item.Item3 - 1)),
						new Tuple<int, int, int>((item.Item1 + 1), item.Item2, (item.Item3 - 1)),
						new Tuple<int, int, int>((item.Item1 + 1), (item.Item2 - 1), item.Item3),
						new Tuple<int, int, int>(item.Item1, (item.Item2 - 1), (item.Item3 + 1)),
						new Tuple<int, int, int>((item.Item1 - 1), item.Item2, (item.Item3 + 1))
					};

					int containedCount = nextToTile.Where(x => flipped.Contains(x)).Count();
					if (containedCount == 0 || containedCount > 2)
					{
						newFlipped.Remove(item);
					}
				}

				if (i == 1)
					Console.WriteLine("Day 24 - Part 1: " + flipped.Count);

				var cb = new ConcurrentBag<Tuple<int, int, int>>();
				var loop = Parallel.For((minX - 1), (maxX + 2), x =>
				{
					for (int y = (minY - 1); y <= (maxY + 1); y++)
					{
						for (int z = (minZ - 1); z <= (maxZ + 1); z++)
						{
							Tuple<int, int, int> current = new Tuple<int, int, int>(x, y, z);

							List<Tuple<int, int, int>> nextToTile = new List<Tuple<int, int, int>>()
							{
								new Tuple<int, int, int>((x - 1), (y + 1), z),
								new Tuple<int, int, int>(x, (y + 1), (z - 1)),
								new Tuple<int, int, int>((x + 1), y, (z - 1)),
								new Tuple<int, int, int>((x + 1), (y - 1), z),
								new Tuple<int, int, int>(x, (y - 1), (z + 1)),
								new Tuple<int, int, int>((x - 1), y, (z + 1))
							};

							int containedCount = nextToTile.Where(x => flipped.Contains(x)).Count();
							if (containedCount == 2)
							{
								cb.Add(current);
							}
						}
					}
				});

				foreach (var item in cb)
				{
					newFlipped.Add(item);
				}

				flipped = new HashSet<Tuple<int, int, int>>(newFlipped);
			}

			Console.WriteLine("Day 24 - Part 2: " + flipped.Count);
		}
	}
}