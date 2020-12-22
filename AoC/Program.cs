using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC
{
	class Program
	{
		static void Main(string[] args)
		{
			//var day = new Day21();
			//day.RunDay();

			Queue<int> PlayerOne = new Queue<int>();
			Queue<int> PlayerTwo = new Queue<int>();

			var input = File.ReadAllLines("Test.txt");
			bool readingOne = false;
			foreach (var item in input)
			{
				if (string.IsNullOrEmpty(item))
					continue;

				if (item.Contains("Player"))
				{
					readingOne = !readingOne;
					continue;
				}

				if (readingOne)
					PlayerOne.Enqueue(int.Parse(item));
				else
					PlayerTwo.Enqueue(int.Parse(item));
			}

			DetermineWinner(PlayerOne, PlayerTwo);

			Queue<int> winner = new Queue<int>();
			if (PlayerOne.Count > 0)
				winner = PlayerOne;
			else
				winner = PlayerTwo;

			int score = 0;

			while (winner.Count > 0)
			{
				score += winner.Count * winner.Dequeue();
			}

			Console.WriteLine(score);

		}

		static int n = 0;

		private static bool DetermineWinner(Queue<int> PlayerOne, Queue<int> PlayerTwo, bool inner = true)
		{
			HashSet<string> previousHands = new HashSet<string>();

			while (PlayerOne.Count > 0 && PlayerTwo.Count > 0)
			{
				var handState = string.Join(",", PlayerOne.Select(x => x).ToArray()) + "|" + string.Join(",", PlayerTwo.Select(x => x).ToArray());
				if (inner)
				{
					Console.WriteLine(n + ": " + PlayerOne.Count + "|" + PlayerTwo.Count);
					n++;
				}
				if (previousHands.Contains(handState))
				{
					PlayerOne.Enqueue(PlayerOne.Dequeue());
					PlayerOne.Enqueue(PlayerTwo.Dequeue());
				}
				else
				{
					previousHands.Add(handState);

					int a = PlayerOne.Dequeue();
					int b = PlayerTwo.Dequeue();

					if ((PlayerOne.Count >= a) && (PlayerTwo.Count >= b))
					{
						var SubPlayerOne = new Queue<int>(PlayerOne.Select(x => x).Take(a));
						var SubPlayerTwo = new Queue<int>(PlayerTwo.Select(x => x).Take(b));
						if (DetermineWinner(new Queue<int>(SubPlayerOne), new Queue<int>(SubPlayerTwo), false))
						{
							PlayerOne.Enqueue(a);
							PlayerOne.Enqueue(b);
						}
						else
						{
							PlayerTwo.Enqueue(b);
							PlayerTwo.Enqueue(a);
						}
					}
					else
					{
						if (a > b)
						{
							PlayerOne.Enqueue(a);
							PlayerOne.Enqueue(b);
						}
						else
						{
							PlayerTwo.Enqueue(b);
							PlayerTwo.Enqueue(a);
						}
					}
				}
			}

			if (PlayerOne.Count > 0)
				return true;
			else
				return false;
		}
	}
}