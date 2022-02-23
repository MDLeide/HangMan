using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    internal class Printer
    {
        public Game Game { get; set; }

        public bool ShowRemainingGuessCount { get; set; } = true;

        public void PrintPlayAgainPrompt()
        {
            Console.Write("Would you like to play again? (y/n)");
        }

        public void PrintWin()
        {
            Console.WriteLine("Congratulations");
            Console.WriteLine("You Won");
            Console.WriteLine();
            Console.WriteLine(Game.Word);
            Console.WriteLine();
        }

        public void PrintLost()
        {
            Console.WriteLine("Sorry - You Lost");
            Console.WriteLine("The word was:");
            Console.WriteLine();
            Console.WriteLine(Game.Word);
            Console.WriteLine();
        }

        public void PrintHangman()
        {
            PrintMan(Game.StartingGuesses - Game.GuessesRemaining);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(Game.Revealed);
            Console.WriteLine();
            Console.WriteLine();
            PrintGuessedLetters();
            Console.WriteLine();
        }

        public void PrintFeedback(string message = null)
        {
            if (string.IsNullOrEmpty(message))
                Console.WriteLine();
            else
                Console.WriteLine(message);
        }

        public void PrintGuessPrompt()
        {
            if (ShowRemainingGuessCount)
                Console.Write($"Enter your guess ({Game.GuessesRemaining} remaining): ");
            else
                Console.Write("Enter your guess: ");
        }

        public void PrintGuessedLetters()
        {
            foreach (var c in Game.LettersGuessed
                         .Where(p => p.Value)
                         .Select(p => p.Key)
                         .OrderBy(p => p))
                Console.Write($"{c} ");
            Console.WriteLine();
        }

        public void ClearPlayArea()
        {
            Console.Clear();
        }

        void PrintMan(int guessesTaken)
        {
            switch (guessesTaken)
            {
                case 0:
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
                case 1:
                    Console.WriteLine("  O");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine("  O");
                    Console.WriteLine(" ---");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
                case 3:
                    Console.WriteLine("  O");
                    Console.WriteLine(" ---");
                    Console.WriteLine("  | ");
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
                case 4:
                    Console.WriteLine("  O");
                    Console.WriteLine(" ---");
                    Console.WriteLine("/ |");
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
                case 5:
                    Console.WriteLine("  O");
                    Console.WriteLine(" ---");
                    Console.WriteLine("/ | \\");
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
                case 6:
                    Console.WriteLine("  O");
                    Console.WriteLine(" ---");
                    Console.WriteLine("/ | \\");
                    Console.WriteLine("  |");
                    Console.WriteLine();
                    break;
                case 7:
                    Console.WriteLine("  O");
                    Console.WriteLine(" ---");
                    Console.WriteLine("/ | \\");
                    Console.WriteLine("  |");
                    Console.WriteLine(" /");
                    break;
                case 8:
                    Console.WriteLine("  O");
                    Console.WriteLine(" ---");
                    Console.WriteLine("/ | \\");
                    Console.WriteLine("  |");
                    Console.WriteLine(" / \\");
                    break;
                default:
                    Console.WriteLine("  O");
                    Console.WriteLine(" ---");
                    Console.WriteLine("/ | \\");
                    Console.WriteLine("  |");
                    Console.WriteLine(" / \\");
                    break;
            }
        }
    }
}
