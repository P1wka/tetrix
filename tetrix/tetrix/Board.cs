namespace tetris_game
{

    public struct Cell
    {
        public char symbol;
        public ConsoleColor color;
    }
    public class Board
    {
        public int width { get; }
        public int height { get; }

        private Cell[,] grid;
        public Board(int Width = 10, int Height = 20) 
        {
            width = Width; height = Height;
            grid = new Cell[height, width];
            Clear();
        }

        public void Clear()
        {
            for(int y  = 0; y < height; y++)
            {
                for(int x  = 0; x < width; x++)
                {
                    grid[y, x].symbol = ' ';
                    grid[y, x].color = ConsoleColor.White;
                }
            }
        }

        public Cell GetCell(int x, int y)
        {
            return grid[y, x];
        }

        public void SetCell(int x, int y, char value, ConsoleColor color)
        {
            grid[y, x].symbol = value;
            grid[y, x].color = color;
        }

        public bool IsFullLine(int y)
        {
            for(int x = 0; x < width; x++)
            {
                if(grid[y, x].symbol == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        public void ClearLine(int y)
        {
            for(int x = 0; x < width; x++)
            {
                grid[y, x].symbol = ' ';
                grid[y, x].color = ConsoleColor.White;
            }

            //move all lines above down
            for(int row = y; row > 0; row--)
            {
                for(int x = 0; x < width; x++)
                {
                    grid[row, x] = grid[row - 1, x];
                }
            }

            //clear the top line
            for(int x = 0; x < width; x++)
            {
                grid[0, x].symbol = ' ';
                grid[0, x].color = ConsoleColor.White;
            }
        }


        public bool IsSpawnBlocked(Tetromino tetromino)
        {
            for(int x = 0; x < width; x++)
            {
                if(grid[0, x].symbol != ' ')
                {
                    return true;
                }
            }
            return false;
        }
    }
}
