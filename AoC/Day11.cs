using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC
{
	public class Day11
	{
		private int width;

		public void RunDay()
		{
			var input = File.ReadAllText("Day11.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
			width = input[0].Length;
			Part1(input);
			Part2(input);
		}

		private void Part1(string[] input)
		{
			string[] current;
			var newInput = input.Clone() as string[];
			int changed = -1;
			while (changed != 0)
			{
				changed = 0;
				current = newInput.Clone() as string[];
				for (int i = 0; i < current.Length; i++)
				{
					var thisString = new StringBuilder();
					for (int j = 0; j < width; j++)
					{
						if (current[i][j] == '.')
						{
							thisString.Append('.');
							continue;
						}

						var adjCount = 0;

						//Top
						if (i > 0)
						{
							if (j > 0 && current[(i - 1)][(j - 1)] == '#')
								adjCount++;

							if (current[(i - 1)][j] == '#')
								adjCount++;

							if (j < (width - 1) && current[(i - 1)][(j + 1)] == '#')
								adjCount++;
						}

						//Middle
						if (j > 0 && current[i][(j - 1)] == '#')
							adjCount++;

						if (j < (width - 1) && current[i][(j + 1)] == '#')
							adjCount++;

						//Bottom
						if (i < (current.Length - 1))
						{
							if (j > 0 && current[(i + 1)][(j - 1)] == '#')
								adjCount++;

							if (current[(i + 1)][j] == '#')
								adjCount++;

							if (j < (width - 1) && current[(i + 1)][(j + 1)] == '#')
								adjCount++;
						}

						if (current[i][j] == 'L')
						{
							if (adjCount == 0)
							{
								changed++;
								thisString.Append('#');
							}
							else
							{
								thisString.Append('L');
							}
						}
						else
						{
							if (adjCount >= 4)
							{
								changed++;
								thisString.Append('L');
							}
							else
							{
								thisString.Append('#');
							}
						}
					}
					newInput[i] = thisString.ToString();
				}
			}

			int seatCount = 0;
			for (int i = 0; i < newInput.Length; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (newInput[i][j] == '#')
						seatCount++;
				}
			}
			Console.WriteLine("Day 11 - Part 1: " + seatCount);
		}

		private void Part2(string[] input)
		{
			string[] current;
			var newInput = input.Clone() as string[];
			int changed = -1;
			while (changed != 0)
			{
				changed = 0;
				current = newInput.Clone() as string[];
				for (int i = 0; i < current.Length; i++)
				{
					var thisString = new StringBuilder();
					for (int j = 0; j < width; j++)
					{
						if (current[i][j] == '.')
						{
							thisString.Append('.');
							continue;
						}

						var adjCount = 0;

						//Top Left
						int iOffset = (i - 1);
						int jOffset = (j - 1);
						while (iOffset >= 0 && jOffset >= 0)
						{
							if (current[iOffset][jOffset] == '#')
							{
								adjCount++;
								break;
							}
							else if (current[iOffset][jOffset] == 'L')
								break;

							iOffset--;
							jOffset--;
						}

						//Top
						iOffset = (i - 1);
						jOffset = j;
						while (iOffset >= 0)
						{
							if (current[iOffset][jOffset] == '#')
							{
								adjCount++;
								break;
							}
							else if (current[iOffset][jOffset] == 'L')
								break;

							iOffset--;
						}

						//Top Right
						iOffset = (i - 1);
						jOffset = (j + 1);
						while (iOffset >= 0 && jOffset < width)
						{
							if (current[iOffset][jOffset] == '#')
							{
								adjCount++;
								break;
							}
							else if (current[iOffset][jOffset] == 'L')
								break;

							iOffset--;
							jOffset++;
						}

						//Left
						iOffset = i;
						jOffset = (j - 1);
						while (jOffset >= 0)
						{
							if (current[iOffset][jOffset] == '#')
							{
								adjCount++;
								break;
							}
							else if (current[iOffset][jOffset] == 'L')
								break;

							jOffset--;
						}

						//Right
						iOffset = i;
						jOffset = (j + 1);
						while (jOffset < width)
						{
							if (current[iOffset][jOffset] == '#')
							{
								adjCount++;
								break;
							}
							else if (current[iOffset][jOffset] == 'L')
								break;

							jOffset++;
						}

						//Bottom Left
						iOffset = (i + 1);
						jOffset = (j - 1);
						while (iOffset < input.Length && jOffset >= 0)
						{
							if (current[iOffset][jOffset] == '#')
							{
								adjCount++;
								break;
							}
							else if (current[iOffset][jOffset] == 'L')
								break;

							iOffset++;
							jOffset--;
						}

						//Bottom
						iOffset = (i + 1);
						jOffset = j;
						while (iOffset < input.Length)
						{
							if (current[iOffset][jOffset] == '#')
							{
								adjCount++;
								break;
							}
							else if (current[iOffset][jOffset] == 'L')
								break;

							iOffset++;
						}

						//Bottom Right
						iOffset = (i + 1);
						jOffset = (j + 1);
						while (iOffset < input.Length && jOffset < width)
						{
							if (current[iOffset][jOffset] == '#')
							{
								adjCount++;
								break;
							}
							else if (current[iOffset][jOffset] == 'L')
								break;

							iOffset++;
							jOffset++;
						}

						if (current[i][j] == 'L')
						{
							if (adjCount == 0)
							{
								changed++;
								thisString.Append('#');
							}
							else
							{
								thisString.Append('L');
							}
						}
						else
						{
							if (adjCount >= 5)
							{
								changed++;
								thisString.Append('L');
							}
							else
							{
								thisString.Append('#');
							}
						}
					}
					newInput[i] = thisString.ToString();
				}
			}

			int seatCount = 0;
			for (int i = 0; i < newInput.Length; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (newInput[i][j] == '#')
						seatCount++;
				}
			}
			Console.WriteLine("Day 11 - Part 2: " + seatCount);
		}
	}
}
