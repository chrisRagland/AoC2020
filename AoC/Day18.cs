using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC
{
	class Day18
	{
		public void RunDay()
		{
            SolveDay(false);
            SolveDay(true);
		}

		public void SolveDay(bool partTwo)
		{
            var lines = File.ReadAllLines("Day18.txt");
            long total = 0;
			foreach (var inputLine in lines)
			{
                var input = new string(inputLine.Where(x => !Char.IsWhiteSpace(x)).ToArray());

                var parsed = Regex.Match(input, "^((\\d+)|([()+*]))*$");

                var regexInfo = parsed.Groups[1].Captures.Select(x => x.ToString());

                var outputQueue = new Queue<string>();
                var operatorStack = new Stack<string>();
                operatorStack.Push(null);

                foreach (var item in regexInfo)
                {
                    if (Regex.IsMatch(item, "^\\d+$"))
                    {
                        outputQueue.Enqueue(item);
                    }
                    else
                    {
                        string topOpp = operatorStack.Peek();
                        switch (item)
                        {
                            case "+":

                                while (topOpp != null)
                                {
                                    if (topOpp == "(" || (partTwo && topOpp == "*"))
                                        break;

                                    outputQueue.Enqueue(operatorStack.Pop());
                                    topOpp = operatorStack.Peek();
                                }

                                operatorStack.Push(item);

                                break;
                            case "*":

                                while (topOpp != null)
                                {
                                    if (topOpp == "(")
                                        break;

                                    outputQueue.Enqueue(operatorStack.Pop());
                                    topOpp = operatorStack.Peek();
                                }

                                operatorStack.Push(item);

                                break;
                            case "(":
                                operatorStack.Push(item);
                                break;
                            case ")":

                                while (topOpp != null)
                                {
                                    if (topOpp == "(")
                                    {
                                        operatorStack.Pop();
                                        break;
                                    }
                                    else
                                    {
                                        outputQueue.Enqueue(operatorStack.Pop());
                                        topOpp = operatorStack.Peek();
                                    }
                                }

                                break;
                            default:
                                break;
                        }
                    }
                }
                while (operatorStack.Peek() != null)
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }

                long output = 0;
                var answerStack = new Stack<long>();

                while (outputQueue.Count > 0)
                {
                    var token = outputQueue.Dequeue();
                    if (long.TryParse(token, out output))
                        answerStack.Push(output);
                    else
                    {
                        if (token == "+")
                            answerStack.Push(answerStack.Pop() + answerStack.Pop());
                        else
                            answerStack.Push(answerStack.Pop() * answerStack.Pop());
                    }
                }

                total += answerStack.Pop();
            }

            Console.WriteLine("Day 18 - Part " + (partTwo ? "2" : "1") + ": " + total);

        }
	}
}