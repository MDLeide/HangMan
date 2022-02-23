namespace HangMan
{
    public class GameRunner
    {
        readonly Random _random = new Random();
        readonly Printer _printer = new Printer();

        Game _game; 
        
        public void Start()
        {
            InitializeNewGame();
            GameLoop();
        }

        void InitializeNewGame()
        {
            _game = new Game(PickWord());
            _printer.Game = _game;
        }

        void GameLoop()
        {
            string lastMessage = null;
            while (true)
            {
                if (_game.GameIsWon)
                {
                    if (!Won())
                        return;
                    InitializeNewGame();
                    lastMessage = null;
                }
                else if (_game.GameIsLost)
                {
                    if (!Lost())
                        return;
                    InitializeNewGame();
                    lastMessage = null;
                }
                else
                {
                    lastMessage = GetGuess(lastMessage);
                }
            }
        }

        bool Won()
        {
            _printer.ClearPlayArea();
            _printer.PrintWin();
            return PromptToPlayAgain();
        }

        bool Lost()
        {
            _printer.ClearPlayArea();
            _printer.PrintLost();
            return PromptToPlayAgain();
        }
        
        string GetGuess(string message)
        {
            _printer.ClearPlayArea();
            _printer.PrintHangman();
            _printer.PrintFeedback(message);
            _printer.PrintGuessPrompt();

            var input = Console.ReadKey(true);
            while (input.KeyChar < 'a' && input.KeyChar > 'z')
                input = Console.ReadKey(true);

            Console.Write(input.KeyChar);

            if (!_game.CanGuessCharacter(input.KeyChar))
                return "Already chose that letter. Try again.";

            if (_game.MakeGuess(input.KeyChar))
                return "Got a letter.";

            return "Missed that guess.";
        }
        
        string PickWord()
        {
            var allWords = WordDatabase.GetAllWords();
            return allWords[_random.Next(0, allWords.Length)];
        }

        bool PromptToPlayAgain()
        {
            _printer.PrintPlayAgainPrompt();

            var input = Console.ReadKey(true);

            while (input.Key != ConsoleKey.Y && input.Key != ConsoleKey.N)
                input = Console.ReadKey(true);

            return input.Key == ConsoleKey.Y;
        }
    }
}
