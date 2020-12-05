using System;
using System.Collections.Generic;
using System.Drawing;

namespace AoC
{
	public class Passport
	{
		public int BirthYear { get; set; }
		public int IssueYear { get; set; }
		public int ExpirationYear { get; set; }
		public PassportHeight Height { get; set; }
		public Color HairColor { get; set; }
		public EyeColors EyeColor { get; set; }
		public string PassportID { get; set; }
		public string CountryID { get; set; }

		public Passport(Dictionary<string, string> input)
		{
			BirthYear = Convert.ToInt32(input["byr"]);
			IssueYear = Convert.ToInt32(input["iyr"]);
			ExpirationYear = Convert.ToInt32(input["eyr"]);
			Height = new PassportHeight(input["hgt"]);

			HairColor = HexToColor(input["hcl"]);

			EyeColor = (input["ecl"]) switch
			{
				"amb" => EyeColors.Amber,
				"blu" => EyeColors.Blue,
				"brn" => EyeColors.Brown,
				"gry" => EyeColors.Gray,
				"grn" => EyeColors.Green,
				"hzl" => EyeColors.Hazel,
				_ => EyeColors.Other,
			};

			PassportID = input["pid"];

			if (input.ContainsKey("cid"))
				CountryID = input["cid"];
			else
				CountryID = null;
		}

		private Color HexToColor(string value)
		{
			int red = Convert.ToInt32(value.Substring(1, 2), 16);
			int green = Convert.ToInt32(value.Substring(3, 2), 16);
			int blue = Convert.ToInt32(value.Substring(5, 2), 16);
			return Color.FromArgb(red, green, blue);
		}

		public class PassportHeight
		{
			public enum HeightUnit
			{
				Centimeter,
				Inch
			}

			public int Height { get; set; }
			public HeightUnit Unit { get; set; }

			public PassportHeight(string input)
			{
				if (input.EndsWith("cm"))
					Unit = HeightUnit.Centimeter;
				else
					Unit = HeightUnit.Inch;

				Height = Convert.ToInt32(input.Substring(0, input.Length - 2));
			}
		}

		public enum EyeColors
		{
			Amber,
			Blue,
			Brown,
			Gray,
			Green,
			Hazel,
			Other
		}
	}
}