using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day17
	{
		public void RunDay()
		{
			SolveDay(false);
			SolveDay(true);
		}

		public void SolveDay(bool partTwo)
		{
			var input = File.ReadAllLines("Day17.txt");
			HashSet<HyperVect> currentState = new HashSet<HyperVect>();
			int z = 0;
			int w = 0;
			for (int i = 0; i < input.Length; i++)
			{
				for (int j = 0; j < input[0].Length; j++)
				{
					if (input[i][j] == '#')
						currentState.Add(new HyperVect(i, j, z, w));
				}
			}

			for (int i = 0; i < 6; i++)
			{
				var newState = new HashSet<HyperVect>();
				int minX = currentState.Select(x => x.X).Min() - 1;
				int maxX = currentState.Select(x => x.X).Max() + 1;
				int minY = currentState.Select(x => x.Y).Min() - 1;
				int maxY = currentState.Select(x => x.Y).Max() + 1;
				int minZ = currentState.Select(x => x.Z).Min() - 1;
				int maxZ = currentState.Select(x => x.Z).Max() + 1;
				int minW = currentState.Select(x => x.W).Min() - 1;
				int maxW = currentState.Select(x => x.W).Max() + 1;

				for (int a = minX; a <= maxX; a++)
				{
					for (int b = minY; b <= maxY; b++)
					{
						for (int c = minZ; c <= maxZ; c++)
						{
							if (partTwo)
							{
								for (int d = minW; d <= maxW; d++)
								{
									newState.Add(new HyperVect(a, b, c, d));
								}
							}
							else
							{
								newState.Add(new HyperVect(a, b, c));
							}
						}
					}
				}

				var realNewState = new HashSet<HyperVect>();

				foreach (var item in newState)
				{
					var activeNearby = currentState.Where(x => !x.Equals(item) && Math.Abs(x.X - item.X) < 2 && Math.Abs(x.Y - item.Y) < 2 && Math.Abs(x.Z - item.Z) < 2 && Math.Abs(x.W - item.W) < 2).Count();

					if (currentState.Any(x => x.Equals(item)))
					{
						if (activeNearby == 2 || activeNearby == 3)
							realNewState.Add(item);
					}
					else
					{
						if (activeNearby == 3)
							realNewState.Add(item);
					}

				}

				currentState = realNewState;

			}

			Console.WriteLine("Day 18 - Part " + (partTwo ? "2" : "1") + ": " + currentState.Count);

		}

		public class HyperVect
		{
			public int X { get; set; }
			public int Y { get; set; }
			public int Z { get; set; }
			public int W { get; set; }
			public HyperVect(int a, int b, int c, int d = 0)
			{
				X = a;
				Y = b;
				Z = c;
				W = d;
			}

			public override string ToString()
			{
				return "X: " + X + " || Y: " + Y + " || Z: " + Z + " || W: " + W;
			}

			// override object.Equals
			public override bool Equals(object obj)
			{
				if (obj == null || GetType() != obj.GetType())
				{
					return false;
				}
				else
				{
					var local = obj as HyperVect;
					if (X == local.X && Y == local.Y && Z == local.Z && W == local.W)
					{
						return true;
					}
				}
				return false;
			}

			// override object.GetHashCode
			public override int GetHashCode()
			{
				return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.ToString());
			}
		}

	}
}