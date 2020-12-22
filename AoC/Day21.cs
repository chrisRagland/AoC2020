using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
	public class Day21
	{
		public void RunDay()
		{
			var lines = File.ReadAllLines("Day21.txt");
			List<string> allIngredients = new List<string>();
			List<string> safeIngredients = new List<string>();
			Dictionary<string, List<string>> food = new Dictionary<string, List<string>>();
			foreach (var item in lines)
			{
				var splitParts = item.Split('(', StringSplitOptions.RemoveEmptyEntries);
				List<string> ingredients = splitParts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
				allIngredients.AddRange(ingredients);
				safeIngredients.AddRange(ingredients);
				var allergies = splitParts[1].Substring(9, splitParts[1].Length - 10).Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

				foreach (var allergy in allergies)
				{
					if (food.ContainsKey(allergy))
					{
						var intersects = food[allergy].Intersect(ingredients).ToList();
						food[allergy] = intersects;
					}
					else
					{
						food.Add(allergy, ingredients);
					}
				}

			}

			foreach (var item in food)
			{
				safeIngredients = safeIngredients.Except(item.Value).ToList();
			}

			Console.WriteLine("Day 21 - Part 1: " + allIngredients.Count(x => safeIngredients.Contains(x)));

			//Part 2
			List<string> foundItems = new List<string>();
			var badCount = food.Sum(x => x.Value.Count);

			while (badCount != food.Count)
			{
				var bads = food.Where(x => x.Value.Count == 1).Select(x => x.Value.First()).ToList();
				foreach (var item in bads)
				{
					if (!foundItems.Contains(item))
						foundItems.Add(item);

					for (int i = 0; i < food.Count; i++)
					{
						if (food.ElementAt(i).Value.Contains(item) && food.ElementAt(i).Value.Count > 1)
						{
							food[food.ElementAt(i).Key] = food.ElementAt(i).Value.Except(new List<string>() { item }).ToList();
						}
					}
				}
				badCount = food.Sum(x => x.Value.Count);
			}

			Console.WriteLine("Day 21 - Part 2: " + food.OrderBy(x => x.Key).Select(x => x.Value.First()).Aggregate((a, b) => a + "," + b));

		}
	}
}
