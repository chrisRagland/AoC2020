using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC
{
	public class Day20
	{
		//404 - Fun not found

		public class TileImage
		{
			public int TileId { get; set; }
			public bool[,] Grid { get; set; }

			public List<string> AllTop { get; set; }
			public List<string> AllRight { get; set; }
			public List<string> AllBot { get; set; }
			public List<string> AllLeft { get; set; }

			public TileImage()
			{
				Grid = new bool[10, 10];

				AllTop = new List<string>();
				AllRight = new List<string>();
				AllBot = new List<string>();
				AllLeft = new List<string>();
			}

			public void GenerateAllPerms()
			{
				for (int i = 0; i < 4; i++)
				{
					Rotate();
					AllTop.Add(Top());
					AllRight.Add(Right());
					AllBot.Add(Bottom());
					AllLeft.Add(Left());
				}
				Flip();
				for (int i = 0; i < 4; i++)
				{
					Rotate();
					AllTop.Add(Top());
					AllRight.Add(Right());
					AllBot.Add(Bottom());
					AllLeft.Add(Left());
				}
			}

			public void Flip()
			{
				for (int i = 0; i < 10; i++)
				{
					var tempLine = new bool[10];
					for (int j = 0; j < 10; j++)
					{
						tempLine[j] = Grid[i, (9 - j)];
					}
					for (int j = 0; j < 10; j++)
					{
						Grid[i, j] = tempLine[j];
					}
				}
			}

			public void Rotate()
			{
				var newGrid = new bool[10, 10];

				for (int i = 0; i < 10; ++i)
				{
					for (int j = 0; j < 10; ++j)
					{
						newGrid[i, j] = Grid[10 - j - 1, i];
					}
				}

				Grid = newGrid;
			}

			public string Left()
			{
				var sb = new StringBuilder();
				for (int i = 0; i < 10; i++)
				{
					sb.Append(Grid[i, 0] ? '#' : '.');
				}
				return sb.ToString();
			}

			public string Top()
			{
				var sb = new StringBuilder();
				for (int i = 0; i < 10; i++)
				{
					sb.Append(Grid[0, i] ? '#' : '.');
				}
				return sb.ToString();
			}

			public string Right()
			{
				var sb = new StringBuilder();
				for (int i = 0; i < 10; i++)
				{
					sb.Append(Grid[i, 9] ? '#' : '.');
				}
				return sb.ToString();
			}

			public string Bottom()
			{
				var sb = new StringBuilder();
				for (int i = 0; i < 10; i++)
				{
					sb.Append(Grid[9, i] ? '#' : '.');
				}
				return sb.ToString();
			}

			public override string ToString()
			{
				var sb = new StringBuilder();
				for (int i = 0; i < 10; i++)
				{
					for (int j = 0; j < 10; j++)
					{
						sb.Append(Grid[i, j] ? '#' : '.');
					}
					sb.Append(Environment.NewLine);
				}
				return sb.ToString();
			}
		}

	}
}
