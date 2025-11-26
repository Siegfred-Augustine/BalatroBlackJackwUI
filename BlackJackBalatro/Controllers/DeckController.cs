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
                _gameState.CurrentPlayerPoints = 0;
            else
            {
                int index = RNG.random.Next(_gameState.NormalDeck.Count);
                Card card = _gameState.NormalDeck.ElementAt(index);
                _gameState.CurrentPlayerHand.Add(card);
                _gameState.NormalDeck.RemoveAt(index);

                _gameState.AddPoints(card, true);
            }

                return PartialView("PlayerHand", _gameState);
        }
        public IActionResult PlayerEvilDraw()
        {
            if (_gameState.CurrentPlayerPoints > 21)
                _gameState.CurrentPlayerPoints = 0;
            else
            {
                int index = RNG.random.Next(_gameState.EvilDeck.Count);
                Card card = _gameState.EvilDeck.ElementAt(index);
                _gameState.CurrentPlayerHand.Add(card);
                _gameState.EvilDeck.RemoveAt(index);

                _gameState.AddPoints(card, true);
            }

            return PartialView("PlayerHand", _gameState);
        }
        public IActionResult PlayerBonusDraw()
        {
            if (_gameState.CurrentPlayerPoints > 21)
                _gameState.CurrentPlayerPoints = 0;
            else
            {
                int index = RNG.random.Next(_gameState.BonusDeck.Count);
                Card card = _gameState.BonusDeck.ElementAt(index);
                _gameState.CurrentPlayerHand.Add(card);
                _gameState.BonusDeck.RemoveAt(index);

                _gameState.AddPoints(card, true);
            }

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
                int index = RNG.random.Next(_gameState.NormalDeck.Count);
                Card card = _gameState.NormalDeck.ElementAt(index);
                _gameState.CurrentCompHand.Add(card);
                _gameState.NormalDeck.RemoveAt(index);

                _gameState.AddPoints(card, false);
            }

            return PartialView("ComputerHand", _gameState);
        }
    }
}
