using System;
using System.Text;

namespace tetris_game
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            SetWindowSizeForUnix();

            try
            {
                Console.OutputEncoding = Encoding.UTF8;
            }
            catch {}

            if (args == null || args.Length == 0)
            {
                new Game().Start();
                return 0;
            }

            var cmd = args[0].Trim().ToLowerInvariant();
            switch (cmd)
            {
                case "play":
                case "run":
                case "start":
                case "tetrix":
                    new Game().Start();
                    return 0;

                case "help":
                case "-h":
                case "--help":
                case "?":
                    PrintHelp();
                    return 0;

                case "version":
                case "-v":
                case "--version":
                    Console.WriteLine("tetrix 0.0.1");
                    return 0;

                default:
                    Console.WriteLine($"Unknown command: '{args[0]}'.");
                    Console.WriteLine("Run `tetrix help` for usage.");
                    return 1;
            }
        }

        public static void SetWindowSizeForUnix()
        {
            int w = Math.Min(120, Console.LargestWindowWidth);
            int h = Math.Min(40, Console.LargestWindowHeight);

            Console.SetWindowSize(w, h);
            // buffer is not working on unix platforms, so i removed it
            //Console.SetBufferSize(w, h);
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Usage: tetrix [command]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("  (no args), play, run, tetrix   Start the game");
            Console.WriteLine("  help, -h, --help, ?            Show this help");
            Console.WriteLine("  version, -v, --version         Show version");
        }
    }
}