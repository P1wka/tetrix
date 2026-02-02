namespace tetris_game
{
    public class InputHandler
    {
        private readonly TimeSpan _softDropCooldown = TimeSpan.FromMilliseconds(5);
        private DateTime _lastSoftDrop = DateTime.MinValue;

        public ConsoleKey? ReadInput()
        {
            if (!Console.KeyAvailable)
                return null;

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.S)
            {
                var now = DateTime.UtcNow;
                if ((now - _lastSoftDrop) < _softDropCooldown)
                {
                    return null;
                }
                _lastSoftDrop = now;
            }

            return key;
        }

        public void Handle(ConsoleKey key, Game game)
        {
            switch (key)
            {
                case ConsoleKey.Escape:
                    game.Exit();
                    break;
            }
        }

        public int GetMove(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.A:
                    return -1;
                case ConsoleKey.D:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
