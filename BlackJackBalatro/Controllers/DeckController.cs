using Microsoft.AspNetCore.Mvc;
using BlackJackBalatro.Services;
using BlackJackBalatro.Models;

namespace BlackJackBalatro.Controllers  
{
    public class DeckController : Controller
    {
        private readonly GameState _gameState;
        public DeckController (GameState game)
        {
            _gameState = game;
        }

        public IActionResult GameView()
        {
            return View(_gameState);
        }
        public IActionResult PlayerNormalDraw()
        {
            if (_gameState.CurrentPlayerPoints > 21)
            {
                _gameState.isPlayerWin = false;
                _gameState.CurrentPlayerPoints = 0;
            }
            else
            {
                Card card = DrawLogic(_gameState.CurrentPlayerHand, _gameState.NormalDeck);

                _gameState.AddPoints(card, true);
            }

                return PartialView("PlayerHand", _gameState);
        }
        public IActionResult PlayerEvilDraw()
        {
            if (_gameState.CurrentPlayerPoints > 21)
            {
                _gameState.isPlayerWin = false;
                _gameState.CurrentPlayerPoints = 0;
            }
            else
            {
                Card card = DrawLogic(_gameState.CurrentPlayerHand, _gameState.EvilDeck);
                _gameState.AddPoints(card, true);
            }

            return PartialView("PlayerHand", _gameState);
        }
        public IActionResult PlayerBonusDraw()
        {
            if (_gameState.CurrentPlayerPoints > 21)
            {
                _gameState.isPlayerWin = false;
                _gameState.CurrentPlayerPoints = 0;
            }
            else
            {

                Card card = DrawLogic(_gameState.CurrentPlayerHand, _gameState.BonusDeck);
                _gameState.AddPoints(card, true);
            }

            return PartialView("PlayerHand", _gameState);
        }
        public IActionResult PlayerAreaRefresh()
        {
            return PartialView("PlayerHand", _gameState);
        }
        public IActionResult ComputerDraw()
        {
            if(_gameState.CurrentComputerPoints > 21)
            {
                _gameState.CurrentComputerPoints = 0;
            }
            else
            {
                Card card = DrawLogic(_gameState.CurrentCompHand, _gameState.NormalDeck);

                _gameState.AddPoints(card, false);
            }

            return PartialView("ComputerHand", _gameState);
        }
        public static Card DrawLogic(List<Card> Hand, List<Card> Deck)
        {
            int index = RNG.random.Next(Deck.Count);
            Card card = Deck.ElementAt(index);
            Hand.Add(card);
            Deck.RemoveAt(index);

            return card;
        }
        public IActionResult ComputerTurn()
        {
            while(_gameState.CurrentComputerPoints < 21 && _gameState.CurrentPlayerPoints > _gameState.CurrentComputerPoints)
            {
                ComputerDraw();
            }
            isRoundWin(_gameState);
            return PartialView("ComputerHand", _gameState);
        }
        public static bool isRoundWin(GameState game)
        {
            if (game.CurrentComputerPoints >= game.CurrentPlayerPoints)
                game.isPlayerWin = false;

            if (game.isPlayerWin)
            {
                resetGame(game);
                game.Player.chips += (game.Player.multiplier * game.bet);
                return true;
            }
            resetGame(game);
            return false;
        }
        public static void resetGame(GameState game)
        {
            game.bet = 0;
            game.CurrentPlayerHand.Clear();
            game.CurrentPlayerPoints = 0;
            game.CurrentCompHand.Clear();
            game.CurrentComputerPoints = 0;
            game.CompAces = 0;
            game.Player.aces = 0;
        }
    }
}
