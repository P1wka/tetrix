using System;
using System.Linq;

namespace tetris_game
{
    public static class Type
    {
        public static void WColorLine(string item, ConsoleColor color)
        {
            var prev = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(item);
            }
            finally
            {
                Console.ForegroundColor = prev;
            }
        }

        public static void WColor(string item, ConsoleColor color)
        {
            var prev = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.Write(item);
            }
            finally
            {
                Console.ForegroundColor = prev;
            }
        }

        public static ConsoleColor GetRandomColor()
        {
            var colors = Enum.GetValues<ConsoleColor>()
                             .Cast<ConsoleColor>()
                             .Where(c => c != ConsoleColor.Black)
                             .ToArray();

            return colors[Random.Shared.Next(colors.Length)];
        }
    }
}
    