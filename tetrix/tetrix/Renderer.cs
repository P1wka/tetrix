using System;

namespace tetris_game
{
    public class Renderer
    {
        public Renderer()
        {
            ClearScreen();
        }

        public readonly int startX = 36;
        public readonly int startY = 4;

        private static readonly char block = '█';
        private static readonly char location = '░';

        private const int CellRenderWidth = 2;

        public static readonly ConsoleColor foreColor = ConsoleColor.White;
        public static readonly ConsoleColor backColor = ConsoleColor.Black;

        public void ClearScreen()
        {
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            Console.Clear();

            for (int y = 0; y < height; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write(new string(' ', width));
            }

            Console.SetCursorPosition(0, 0);
        }

        public void ScoreBoard(Board board, int score)
        {
            const int boxWidth = 8;
            const int boxHeight = 4;

            int positionX = startX + board.width * CellRenderWidth - 40;
            int positionY = startY;

            int contentWidth = boxWidth * CellRenderWidth;
            int contentStartX = positionX + 1;

            Console.SetCursorPosition(positionX, positionY);
            Console.Write('┌');
            Console.Write(new string('─', contentWidth));
            Console.Write('┐');

            for (int y = 0; y < boxHeight; y++)
            {
                Console.SetCursorPosition(positionX, positionY + y + 1);
                Console.Write('│');

                Console.Write(new string(' ', contentWidth));
                Console.Write('│');

                if (y == 0)
                {
                    string label = "Score";
                    string scoreStr = score.ToString();

                    Console.SetCursorPosition(contentStartX + CellRenderWidth, positionY + y + 1);
                    Console.Write(label);

                    int scorePosX = contentStartX + contentWidth - scoreStr.Length - 2;
                    Console.SetCursorPosition(scorePosX, positionY + y + 1);
                    Console.Write(scoreStr);
                }
                else if (y == 2)
                {
                    string label = "Level";
                    string levelStr = (score / 1000 + 1).ToString();

                    Console.SetCursorPosition(contentStartX + CellRenderWidth, positionY + y + 1);
                    Console.Write(label);

                    int levelPosX = contentStartX + contentWidth - levelStr.Length - 2;
                    Console.SetCursorPosition(levelPosX, positionY + y + 1);
                    Console.Write(levelStr);
                }

            }

            Console.SetCursorPosition(positionX, positionY + boxHeight);
            Console.Write('└');
            Console.Write(new string('─', contentWidth));
            Console.Write('┘');
        }

        public void NextBoard(Board board, Tetromino t)
        {
            const int boxWidth = 9;
            const int boxHeight = 6;

            int positionX = startX + board.width * CellRenderWidth + 4;
            int positionY = startY;

            int contentWidth = boxWidth * CellRenderWidth;
            int contentStartX = positionX + 1;

            Console.SetCursorPosition(positionX, positionY);
            Console.Write('┌');
            Console.Write(new string('─', contentWidth));
            Console.Write('┐');

            for (int y = 0; y < boxHeight; y++)
            {
                Console.SetCursorPosition(positionX, positionY + y + 1);
                Console.Write('│');

                char[] symbols = new char[boxWidth];
                ConsoleColor[] colors = new ConsoleColor[boxWidth];

                int offsetX = (boxWidth - 4) / 2 - 3;
                int offsetY = 2;

                for (int x = 0; x < boxWidth; x++)
                {
                    int tx = x - offsetX;
                    int ty = y - offsetY;

                    if (t != null && t.IsOnCell(tx, ty, t.index))
                    {
                        symbols[x] = block;
                        colors[x] = t.color;
                    }
                    else
                    {
                        symbols[x] = ' ';
                        colors[x] = foreColor;
                    }
                }

                try
                {
                    ConsoleColor currentColor = foreColor;
                    Console.SetCursorPosition(contentStartX, positionY + y + 1);
                    for (int i = 0; i < boxWidth; i++)
                    {
                        if (colors[i] != currentColor)
                        {
                            currentColor = colors[i];
                            Console.ForegroundColor = currentColor;
                        }

                        Console.Write(new string(symbols[i], CellRenderWidth));
                    }
                }
                finally
                {
                    Console.ForegroundColor = foreColor;
                }

                Console.SetCursorPosition(positionX + 1 + contentWidth, positionY + y + 1);
                Console.Write('│');
            }

            Console.SetCursorPosition(positionX, positionY + boxHeight);
            Console.Write('└');
            Console.Write(new string('─', contentWidth));
            Console.Write('┘');
        }

        public void HelpBoard(Board board)
        {
            const int boxWidth = 9;
            const int boxHeight = 12;

            int positionX = startX + board.width * CellRenderWidth + 4;
            int positionY = startY + 8;

            int contentWidth = boxWidth * CellRenderWidth;
            int contentStartX = positionX + 1;

            Console.SetCursorPosition(positionX, positionY);
            Console.Write('┌');
            Console.Write(new string('─', contentWidth));
            Console.Write('┐');

            var lines = new (string Label, string Alt)[] 
            {
                ("Left", "A"),
                ("Right", "D"),
                ("Down", "S"),
                ("Rotate", "W"),
                ("Quit", "ESC")
            };

            int commandsStartY = 1;

            for (int y = 0; y < boxHeight; y++)
            {
                Console.SetCursorPosition(positionX, positionY + y + 1);
                Console.Write('│');

                Console.Write(new string(' ', contentWidth));
                Console.Write('│');

                int cmdIndex = y - commandsStartY;
                if (cmdIndex >= 0 && cmdIndex < lines.Length)
                {
                    var (label, alt) = lines[cmdIndex];
                    Console.SetCursorPosition(contentStartX + CellRenderWidth, positionY + y + 1);
                    Console.Write(label);

                    int altPosX = contentStartX + contentWidth - alt.Length - 2;
                    Console.SetCursorPosition(altPosX, positionY + y + 1);
                    Console.Write(alt);
                }
            }

            Console.SetCursorPosition(positionX, positionY + boxHeight);
            Console.Write('└');
            Console.Write(new string('─', contentWidth));
            Console.Write('┘');
        }

        public void Draw(Board board, Tetromino t)
        {
            try
            {
                Console.SetCursorPosition(startX, startY);

                Console.Write('┌');
                Console.Write(new string('─', board.width * CellRenderWidth));
                Console.Write('┐');

                int ghostY = -1;
                if (t != null && t.canMove)
                {
                    ghostY = t.GetGhostY(board);
                }

                for (int y = 0; y < board.height; y++)
                {
                    Console.SetCursorPosition(startX, startY + y + 1);
                    Console.Write('│');

                    char[] symbols = new char[board.width];
                    ConsoleColor[] colors = new ConsoleColor[board.width];

                    for (int x = 0; x < board.width; x++)
                    {
                        bool drawn = false;

                        if (t != null)
                        {
                            if (t.IsOnCell(x, y, t.index))
                            {
                                symbols[x] = block;
                                colors[x] = t.color;
                                drawn = true;
                            }
                            else if (ghostY >= 0)
                            {
                                if (t.IsOnCellAt(x, y, t.index, t.posX, ghostY))
                                {
                                    var existing = board.GetCell(x, y);
                                    if (existing.symbol == ' ')
                                    {
                                        symbols[x] = location;
                                        colors[x] = ConsoleColor.DarkGray;
                                        drawn = true;
                                    }
                                }
                            }
                        }

                        if (!drawn)
                        {
                            var cell = board.GetCell(x, y);
                            symbols[x] = cell.symbol == ' ' ? ' ' : cell.symbol;
                            colors[x] = cell.color;
                        }
                    }

                    try
                    {
                        ConsoleColor currentColor = foreColor;
                        for (int i = 0; i < board.width; i++)
                        {
                            if (colors[i] != currentColor)
                            {
                                currentColor = colors[i];
                                Console.ForegroundColor = currentColor;
                            }

                            Console.Write(new string(symbols[i], CellRenderWidth));
                        }
                    }
                    finally
                    {
                        Console.ForegroundColor = foreColor;
                    }

                    Console.Write('│');
                    Console.WriteLine();
                }

                Console.SetCursorPosition(startX, startY + board.height);

                Console.Write('└');
                Console.Write(new string('─', board.width * CellRenderWidth));
                Console.Write('┘');
            }
            catch (Exception)
            {
                // Dev: Piwka
            }
        }
    }
}