using System;
namespace Core.Helpers
{
	public static class ConsoleHelper
	{
		public static void WriteWithColor(string text, ConsoleColor color= ConsoleColor.W)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ResetColor();
		}
		// codu tekrarlamayacam helper metoda cixarib cagiracam
	}
}

