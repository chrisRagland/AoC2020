using System;
using System.Drawing;
using System.IO;

namespace AoC
{
	public class Day12
	{
		public void RunDay()
		{
			var input = File.ReadAllText("Day12.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			Part1(input);
			Part2(input);
		}

		public enum Direction
		{
			North,
			West,
			South,
			East
		}

		public void Part1(string[] input)
		{
			var direction = Direction.East;
			Point ship = new Point(0, 0);
			foreach (var item in input)
			{
				int value = Convert.ToInt32(item.Substring(1));
				switch (item[0])
				{
					case 'N':
						ship.Y += value;
						break;
					case 'W':
						ship.X -= value;
						break;
					case 'S':
						ship.Y -= value;
						break;
					case 'E':
						ship.X += value;
						break;
					case 'L':
					case 'R':
						var spin = item[0];
						if ((value == 90 && spin == 'L') || (value == 270 && spin == 'R'))
						{
							switch (direction)
							{
								case Direction.North:
									direction = Direction.West;
									break;
								case Direction.West:
									direction = Direction.South;
									break;
								case Direction.South:
									direction = Direction.East;
									break;
								case Direction.East:
									direction = Direction.North;
									break;
								default:
									break;
							}
						}
						else if (value == 180)
						{
							switch (direction)
							{
								case Direction.North:
									direction = Direction.South;
									break;
								case Direction.West:
									direction = Direction.East;
									break;
								case Direction.South:
									direction = Direction.North;
									break;
								case Direction.East:
									direction = Direction.West;
									break;
								default:
									break;
							}
						}
						else if ((value == 270 && spin == 'L') || (value == 90 && spin == 'R'))
						{
							switch (direction)
							{
								case Direction.North:
									direction = Direction.East;
									break;
								case Direction.West:
									direction = Direction.North;
									break;
								case Direction.South:
									direction = Direction.West;
									break;
								case Direction.East:
									direction = Direction.South;
									break;
								default:
									break;
							}
						}
						break;
					case 'F':
						switch (direction)
						{
							case Direction.North:
								ship.Y += value;
								break;
							case Direction.West:
								ship.X -= value;
								break;
							case Direction.South:
								ship.Y -= value;
								break;
							case Direction.East:
								ship.X += value;
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}
			Console.WriteLine("Day 12 - Part 1: " + (Math.Abs(ship.X) + Math.Abs(ship.Y)));
		}

		public void Part2(string[] input)
		{
			Point ship = new Point(0,0);
			Point waypoint = new Point(10, 1);
			foreach (var item in input)
			{
				int value = Convert.ToInt32(item.Substring(1));
				switch (item[0])
				{
					case 'N':
						waypoint.Y += value;
						break;
					case 'W':
						waypoint.X -= value;
						break;
					case 'S':
						waypoint.Y -= value;
						break;
					case 'E':
						waypoint.X += value;
						break;
					case 'L':
					case 'R':
						var spin = item[0];

						if ((value == 90 && spin == 'L') || (value == 270 && spin == 'R'))
							waypoint = new Point(-1 * waypoint.Y, waypoint.X);
						else if (value == 180)
							waypoint = new Point(-1 * waypoint.X, -1 * waypoint.Y);
						else if ((value == 270 && spin == 'L') || (value == 90 && spin == 'R'))
							waypoint = new Point(waypoint.Y, -1 * waypoint.X);

						break;
					case 'F':
						ship.Offset(waypoint.X * value, waypoint.Y * value);
						break;
					default:
						break;
				}
			}
			Console.WriteLine("Day 12 - Part 2: " + (Math.Abs(ship.X) + Math.Abs(ship.Y)));
		}
	}
}
