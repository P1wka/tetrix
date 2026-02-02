using System;
using System.Collections.Generic;

namespace tetris_game
{
    public class Tetromino
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int posX { get; set; }
        public int posY { get; set; }

        int[,] shapeO =
        {
            {1,1},
            {1,1},
        };

        int[,] shapeI =
        {
            {1},
            {1},
            {1},
            {1}
        };

        int[,] shapeS =
        {
            {0,1,1},
            {1,1,0}
        };

        int[,] shapeZ =
        {
            {1,1,0},
            {0,1,1},
        };

        int[,] shapeL =
        {
            {1,0},
            {1,0},
            {1,1}
        };

        int[,] shapeJ =
        {
            {0,1},
            {0,1},
            {1,1}
        };

        int[,] shapeT =
        {
            {1,1,1},
            {0,1,0}
        };

        public List<int[,]> shapeList = new List<int[,]>();

        public int index = 0;

        public bool canMove = true;
        public bool canRotate = true;

        public ConsoleColor color;

        Board board;

        public ConsoleColor[] unregisteredColors = new ConsoleColor[]
        {
            ConsoleColor.Black,
            ConsoleColor.White,
            ConsoleColor.DarkBlue
        };

        public Tetromino(int startX)
        {
            X = 3; Y = 0;
            posX = X;
            posY = 0;

            shapeList.Add(shapeO);
            shapeList.Add(shapeI);
            shapeList.Add(shapeS);
            shapeList.Add(shapeZ);
            shapeList.Add(shapeL);
            shapeList.Add(shapeJ);
            shapeList.Add(shapeT);

            do
            {
                color = Type.GetRandomColor();
            }
            while (unregisteredColors.Contains(color));
        }

        public void SetBoard(Board b)
        {
            board = b;
        }

        public bool IsOnCell(int boardX, int boardY, int index)
        {
            var item = shapeList[index];

            for (int y = 0; y < item.GetLength(0); y++)
                for (int x = 0; x < item.GetLength(1); x++)
                {
                    if (item[y, x] == 1)
                    {
                        if (boardX == posX + x && boardY == posY + y)
                            return true;
                    }
                }
            return false;
        }

        // Check if given board coordinates would be occupied by this tetromino
        // when placed at the provided atPosX/atPosY.
        public bool IsOnCellAt(int boardX, int boardY, int index, int atPosX, int atPosY)
        {
            var item = shapeList[index];

            for (int y = 0; y < item.GetLength(0); y++)
                for (int x = 0; x < item.GetLength(1); x++)
                {
                    if (item[y, x] == 1)
                    {
                        if (boardX == atPosX + x && boardY == atPosY + y)
                            return true;
                    }
                }
            return false;
        }
        public int GetGhostY(Board board)
        {
            var shape = shapeList[index];
            int testY = posY;

            while (true)
            {
                bool collision = false;

                for (int sy = 0; sy < shape.GetLength(0); sy++)
                {
                    for (int sx = 0; sx < shape.GetLength(1); sx++)
                    {
                        if (shape[sy, sx] == 0) continue;

                        int boardX = posX + sx;
                        int boardY = testY + sy + 1;

                        if (boardX < 0 || boardX >= board.width)
                        {
                            collision = true;
                            break;
                        }

                        if (boardY >= board.height)
                        {
                            collision = true;
                            break;
                        }

                        Cell cell = board.GetCell(boardX, boardY);
                        if (cell.symbol != ' ')
                        {
                            collision = true;
                            break;
                        }
                    }
                    if (collision) break;
                }

                if (collision)
                    break;

                testY++;
            }

            return testY;
        }

        public void FallDown(int bottomLimit, Board board)
        {

            if (posY + 1 < bottomLimit && canMoveDown(board, index))
            {
                posY++;
            }
            else
            {
                canMove = false;
            }
        }

        public bool canMoveDown(Board board, int index)
        {
            var item = shapeList[index];

            for (int y = 0; y < item.GetLength(0); y++)
                for (int x = 0; x < item.GetLength(1); x++)
                {
                    if (item[y, x] == 1)
                    {
                        int boardX = posX + x;
                        int boardY = posY + y + 1;
                        if (boardY >= board.height)
                        {
                            return false;
                        }
                        Cell cell = board.GetCell(boardX, boardY);
                        if (cell.symbol != ' ')
                        {
                            return false;
                        }
                    }
                }
            return true;
        }

        public bool canMoveHorizontal(int index, int dir)
        {
            var shape = shapeList[index];

            for (int y = 0; y < shape.GetLength(0); y++)
            {
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    if (shape[y, x] == 0) continue;

                    int boardX = posX + x + dir;
                    int boardY = posY + y;

                    if (boardX < 0 || boardX >= board.width)
                        return false;

                    if (board.GetCell(boardX, boardY).symbol != ' ')
                    {
                        canMove = false;
                        return false;
                    }
                }
            }
            return true;
        }
        public bool canMoveRotate(int[,] shape)
        {
            for (int y = 0; y < shape.GetLength(0); y++)
            {
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    if (shape[y, x] == 0) continue;
                    int boardX = posX + x;
                    int boardY = posY + y;

                    if (boardX < 0 || boardX >= board.width)
                        return false;

                    if (board.GetCell(boardX, boardY).symbol != ' ')
                    {
                        canRotate = false;
                        return false;
                    }
                }
            }
            return true;
        }

        public bool canMoveRotate(int newIndex)
        {
            var shape = shapeList[newIndex];
            return canMoveRotate(shape);
        }

        private int[,] RotateMatrix90(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[,] rotated = new int[cols, rows];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    rotated[c, rows - 1 - r] = matrix[r, c];
                }
            }
            return rotated;
        }

        private int[,] GetCurrentShape()
        {
            return shapeList[index];
        }

        private void SetCurrentShape(int[,] shape)
        {
            shapeList[index] = shape;
        }

        public void Rotate(int boardWidth)
        {
            var current = GetCurrentShape();
            var rotated = RotateMatrix90(current);

            int shapeWidth = rotated.GetLength(1);

            if (!canRotate)
                return;

            int[] kicks = new int[] { 0, 1, -1, 2, -2 };
            foreach (int kick in kicks)
            {
                int newPosX = posX + kick;

                if (newPosX < 0 || newPosX + shapeWidth > boardWidth)
                    continue;

                if (canMoveRotateAt(rotated, kick, boardWidth))
                {
                    SetCurrentShape(rotated);
                    posX = newPosX;
                    return;
                }
            }

        }

        private bool canMoveRotateAt(int[,] shape, int offset, int boardWidth)
        {
            for (int y = 0; y < shape.GetLength(0); y++)
            {
                for (int x = 0; x < shape.GetLength(1); x++)
                {
                    if (shape[y, x] == 0) continue;

                    int boardX = posX + x + offset;
                    int boardY = posY + y;

                    if (boardX < 0 || boardX >= boardWidth)
                        return false;

                    if (boardY < 0 || boardY >= board.height)
                        return false;

                    if (board.GetCell(boardX, boardY).symbol != ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void Move(int boardWidth, int index, int dir)
        {
            int newX = posX + dir;
            int shapeWidth = GetWidth(index);

            if (newX >= 0 && newX + shapeWidth <= boardWidth && canMoveHorizontal(index, dir) && canMove)
            {
                posX = newX;
            }
        }

        public int GetHeight(int index)
        {
            return shapeList[index].GetLength(0);
        }

        public int GetWidth(int index)
        {
            return shapeList[index].GetLength(1);
        }

    }
}