using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService.Logging
{
	public class ConsoleLogger : ILogger
	{
		public void LogMessage(string message, bool important = false)
		{
			Console.ForegroundColor = important ? ConsoleColor.Red : ConsoleColor.White;

			if (message.Contains("*"))
			{
				foreach (var character in message)
				{
					if (character == '*')
					{
						//toggle between red and white on alternating *
						Console.ForegroundColor =
							Console.ForegroundColor == ConsoleColor.Red
							? ConsoleColor.White
							: ConsoleColor.Red;
					}
					else //don't write the *
					{
						Console.Out.Write(character);
					}
				}
				Console.Out.WriteLine();
			}
			else
			{
				Console.Out.WriteLine(message);
			}
		}
	}
}
