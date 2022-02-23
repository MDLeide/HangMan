namespace HangMan
{
    public class Game
    {
        public const int DefaultStartingGuesses = 9;
        public const char Space = '_';

        public Game(string word)
        {
            Word = word;
            Revealed = string.Empty;

            InitLettersGuessed();
            UpdateRevealed();
        }

        public string Word { get; }
        public string Revealed { get; set; }
        public Dictionary<char, bool> LettersGuessed { get; } = new Dictionary<char, bool>();
        public int StartingGuesses { get; set; } = DefaultStartingGuesses;
        public int GuessesRemaining { get; set; } = DefaultStartingGuesses;

        public bool GameIsWon => Revealed == Word;
        public bool GameIsLost => GuessesRemaining <= 0 && !GameIsWon;

        public void UpdateRevealed()
        {
            Revealed = string.Empty;
            foreach (var c in Word)
            {
                if (LettersGuessed[c])
                    Revealed += c;
                else
                    Revealed += Space;
            }
        }
        
        public bool MakeGuess(char character)
        {
            if (GuessesRemaining <= 0)
                throw new InvalidOperationException();

            if (character < 'a' || character > 'z')
                throw new ArgumentOutOfRangeException(nameof(character));

            if (LettersGuessed[character])
                throw new InvalidOperationException();

            LettersGuessed[character] = true;
            UpdateRevealed();
            if (Word.Contains(character))
                return true;

            --GuessesRemaining;
            return false;
        }

        public bool CanGuessCharacter(char character)
        {
            if (character < 'a' || character > 'z')
                throw new ArgumentOutOfRangeException(nameof(character));

            return !LettersGuessed[character];
        }

        void InitLettersGuessed()
        {
            const int offset = 97;

            for (int i = 0; i < 26; i++)
                LettersGuessed.Add((char)(i + offset), false);
        }
    }
}