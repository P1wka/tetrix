using System.Text;
namespace tetris_game
{
    public class Game
    {
        bool isRunning = false;

        Board board;
        Board scoreBoard;
        Board nextBoard;
        Board helpBoard;

        Tetromino tetromino;
        Tetromino next;

        Renderer renderer;

        InputHandler inputHandler;

        public int gameScore = 0;

        public Game()
        {
            isRunning = true;

            board = new Board();
            scoreBoard = new Board();
            nextBoard = new Board();
            helpBoard = new Board();

            renderer = new Renderer();

            tetromino = NewTetromino(tetromino);
            next = NewTetromino(next);

            inputHandler = new InputHandler();
        }

        public void Start()
        {
            Console.Clear();

            try
            {
                Console.OutputEncoding = Encoding.UTF8;
            }
            catch
            {
            }

            Console.CursorVisible = false;

            DateTime lastFall = DateTime.Now;

            while (isRunning)
            {
                var key = inputHandler.ReadInput();
                if (key != null)
                {
                    inputHandler.Handle(key.Value, this);
                    Input(key.Value);
                }

                if ((DateTime.Now - lastFall).TotalMilliseconds >= 300)
                {
                    int bottomLimit = board.height - tetromino.GetHeight(tetromino.index);
                    tetromino.FallDown(bottomLimit, board);
                    lastFall = DateTime.Now;
                }
                Update();
                renderer.Draw(board, tetromino);
                Thread.Sleep(5);
            }

            Console.CursorVisible = true;
        }

        void Input(ConsoleKey key)
        {
            if (!tetromino.canMove) return;

            int horizontal = inputHandler.GetMove(key);
            tetromino.Move(board.width, tetromino.index, horizontal);

            if (key == ConsoleKey.W)
            {
                tetromino.Rotate(board.width);
            }

            if (key == ConsoleKey.S)
            {
                int bottomLimit = board.height - tetromino.GetHeight(tetromino.index);
                tetromino.FallDown(bottomLimit, board);
            }
        }

        void Update()
        {
            

            if(!tetromino.canMove)
            {
                SaveTetromino();
                //tetromino = next;
                //NewTetromino();
            }

            int deletedLines = 0;

            for (int y = 0; y < board.height; y++)
            {
                if (board.IsFullLine(y))
                {
                    board.ClearLine(y);
                    deletedLines++;
                    y--;
                }
            }

            switch(deletedLines)
            {
                case 1:
                    gameScore += 100;
                    break;
                case 2:
                    gameScore += 300;
                    break;
                case 3:
                    gameScore += 500;
                    break;
                case 4:
                    gameScore += 800;
                    break;
            }

            if (board.IsSpawnBlocked(tetromino))
            {
                Exit();
            }

            renderer.ScoreBoard(scoreBoard, gameScore);
            renderer.NextBoard(nextBoard, next);
            renderer.HelpBoard(helpBoard);
        }

        public void SaveTetromino()
        {
            for (int y = 0; y < board.height; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    if (tetromino.IsOnCell(x, y, tetromino.index))
                    {   
                        board.SetCell(x, y, '█', tetromino.color);
                    }
                }
            }
            tetromino = next;
            next = NewTetromino(next);
        }

        public Tetromino NewTetromino(Tetromino tetromino)
        {
            tetromino = new Tetromino(3);
            tetromino.SetBoard(board);

            int newIndex = Random.Shared.Next(0, tetromino.shapeList.Count);
            tetromino.index = newIndex;

            tetromino.posX = (board.width - tetromino.GetWidth(tetromino.index)) / 2;
            tetromino.posY = 0;

            return tetromino;
        }

        public void Exit()
        {
            isRunning = false;
        }
    }
}
