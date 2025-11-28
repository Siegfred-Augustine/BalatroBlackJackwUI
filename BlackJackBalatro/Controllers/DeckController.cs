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

        public IActionResult GameView(String playerName)
        {
            _gameState.Player.name = playerName;
            return View(_gameState);
        }
        public IActionResult PlayerNormalDraw()
        {
            if (_gameState.CurrentPlayerPoints > 21)
            {
                _gameState.isPlayerWin = false;
                _gameState.canPlayerDraw = false;
                _gameState.CurrentPlayerPoints = 0;
                _gameState.Player.busts++;
            }
            else
            {
                Card card = DrawLogic(_gameState.CurrentPlayerHand, _gameState.NormalDeck);
                _gameState.Player.cardDraws++;
                card.special(_gameState);

                _gameState.AddPoints(card, true);
            }
            if(_gameState.CurrentPlayerPoints > 21)
            {
                _gameState.isPlayerWin = false;
                _gameState.canPlayerDraw = false;
                _gameState.Player.busts++;
                return PartialView("LoseScreen", _gameState);
            }
            if (_gameState.CurrentPlayerPoints == 21)
                _gameState.Player.blackJacks++;
            return PartialView("PlayerHand", _gameState);
        }
        public IActionResult PlayerEvilDraw()
        {
            if (_gameState.CurrentPlayerPoints > 21)
            {
                _gameState.isPlayerWin = false;
                _gameState.CurrentPlayerPoints = 0;
                _gameState.Player.busts++;
            }
            else
            {
                Card card = DrawLogic(_gameState.CurrentPlayerHand, _gameState.EvilDeck);
                _gameState.AddPoints(card, true);
                _gameState.Player.cardDraws++;
                card.special(_gameState);

                switch (card.rarity)
                {
                    case eRarity.Evil: _gameState.Player.evilDraws++; break;
                    case eRarity.Special: _gameState.Player.specialDraws++; break;
                    default: _gameState.Player.mysteryDraws++; break;
                }
            }
            if(_gameState.CurrentPlayerPoints > 21)
            {
                _gameState.isPlayerWin = false;
                _gameState.canPlayerDraw = false;
                _gameState.Player.busts++;
                return PartialView("LoseScreen", _gameState);
            }
            if (_gameState.CurrentPlayerPoints == 21)
                _gameState.Player.blackJacks++;
            return PartialView("PlayerHand", _gameState);
        }
        public IActionResult PlayerBonusDraw()
        {
            if (_gameState.CurrentPlayerPoints > 21)
            {
                _gameState.isPlayerWin = false;
                _gameState.CurrentPlayerPoints = 0;
                _gameState.Player.busts++;
            }
            else
            {

                Card card = DrawLogic(_gameState.CurrentPlayerHand, _gameState.BonusDeck);
                _gameState.AddPoints(card, true);
                card.special(_gameState);
                _gameState.Player.bonusDraws++;
            }
            if (_gameState.CurrentPlayerPoints > 21)
            {
                _gameState.isPlayerWin = false;
                _gameState.canPlayerDraw = false;
                _gameState.Player.busts++;
                return PartialView("LoseScreen", _gameState);
            }
            if (_gameState.CurrentPlayerPoints == 21)
                _gameState.Player.blackJacks++;

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
            if(_gameState.CurrentComputerPoints > 21)
            {
                _gameState.CurrentComputerPoints = 0;
            }
            isRoundWin(_gameState);
            return PartialView("ComputerHand", _gameState);
        }
        public static bool isRoundWin(GameState game)
        {
            if (game.CurrentComputerPoints >= game.CurrentPlayerPoints)
            {
                game.isPlayerWin = false;
                game.canPlayerDraw = false;
            }
            else if (game.CurrentComputerPoints < game.CurrentPlayerPoints)
            {
                game.isPlayerWin = true;
                game.canPlayerDraw = false;
            }

            if (game.isPlayerWin)
            {
                game.Player.chips += (game.Player.multiplier * game.bet);
                return true;
            }
            game.bet = 0;
            game.CurrentPlayerPoints = 0;
            game.CurrentComputerPoints = 0;
            return false;
        }
        public IActionResult Reset()
        {
            _gameState.resetGame();
            return View("GameView", _gameState);
        }
        public IActionResult statRefresh()
        {
            return PartialView("Stats", _gameState);
        }
        public IActionResult PlayerAreaRefresh()
        {
            return PartialView("PlayerHand", _gameState);
        }
        public IActionResult ComputerAreaRefresh()
        {
            return PartialView("ComputerHand", _gameState);
        }
        public IActionResult PlaceBet(int amount)
        {
            if (amount <= 0)
                return BadRequest("Invalid bet");

            _gameState.bet = amount;
            _gameState.Player.chips -= amount;

            return Ok();
        }
    }
}
