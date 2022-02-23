using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangMan.Tests
{
    [TestClass]
    public class GameTests
    {
        const string TestWord = "test";

        static Game GetGame(string testWord = TestWord)
        {
            return new Game(testWord);
        }

        [TestMethod]
        public void CanGuessCharacter_NoGuesses_ReturnsTrue()
        {
            var game = GetGame();
            var result = game.CanGuessCharacter('a');
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void CanGuessCharacter_NotGuessed_ReturnsTrue()
        {
            var game = GetGame();
            game.LettersGuessed['b'] = true;
            game.LettersGuessed['c'] = true;
            game.LettersGuessed['d'] = true;
            var result = game.CanGuessCharacter('a');
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanGuessCharacter_AlreadyGuessed_ReturnsFalse()
        {
            var game = GetGame();
            game.LettersGuessed['b'] = true;
            game.LettersGuessed['c'] = true;
            game.LettersGuessed['d'] = true;
            var result = game.CanGuessCharacter('c');
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MakeGuess_AlreadyGuessed_Throws()
        {
            var game = GetGame();
            game.MakeGuess('a');
            game.MakeGuess('a');
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MakeGuess_NoGuessesRemaining_Throws()
        {
            var game = GetGame();
            game.GuessesRemaining = 0;
            game.MakeGuess('a');
        }

        [TestMethod]
        public void MakeGuess_MissedGuess_DecrementsGuessesRemaining()
        {
            var game = GetGame();
            game.GuessesRemaining = 5;
            game.MakeGuess('a');
            Assert.AreEqual(4, game.GuessesRemaining);
        }

        [TestMethod]
        public void MakeGuess_HasLetter_DoesNotDecrementsGuessesRemaining()
        {
            var game = GetGame("test");
            game.GuessesRemaining = 5;
            game.MakeGuess('e');
            Assert.AreEqual(5, game.GuessesRemaining);
        }

        [TestMethod]
        public void MakeGuess_HasLetter_UpdatesRevealed()
        {
            var game = GetGame("test");
            game.MakeGuess('e');
            Assert.AreEqual("_e__", game.Revealed);
        }

        [TestMethod]
        public void MakeGuess_UpdatesLettersGuessed()
        {
            var game = GetGame();
            game.MakeGuess('a');
            Assert.IsTrue(game.LettersGuessed['a']);
        }
    }
}