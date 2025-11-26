using BlackJackBalatro.Models;

namespace BlackJackBalatro.Services
{
    public class GameState
    {
        
        public List<Card> NormalDeck { get; set; }
        public List<Card> EvilDeck { get; set; }
        public List<Card> BonusDeck { get; set; }
        public List<Card> CurrentPlayerHand { get; set; }
        public List<Card> CurrentCompHand { get; set; }
        public int CurrentPlayerPoints { get; set; }
        public int CurrentComputerPoints { get; set; }
        public int CompAces { get; set; }
        public bool isPlayerWin { get; set; }
        public int bet { get; set; }

        public Player Player { get; set; }

        public GameState()
        {
            NormalDeck = new List<Card>();
            EvilDeck = new List<Card>();
            BonusDeck = new List<Card>();
            CurrentPlayerHand = new List<Card>();
            CurrentCompHand = new List<Card>();
            CurrentPlayerPoints = 0;
            CurrentComputerPoints = 0;
            CompAces = 0;
            isPlayerWin = false;
            bet = 0;

            NormalCard.initializeDeck(NormalDeck);
            EvilCard.initializeDeck(EvilDeck);
            BonusCard.initializeDeck(BonusDeck);

            Player = new Player("");
        }
        public void AddPoints(Card card, bool isPlayer)
        {
            int adder = 0;
            switch(card.value){
                case "King":
                case "Queen":
                case "Jack":
                    adder += 10;
                    break;
                case "Ace":
                    if (isPlayer)
                        Player.aces++;
                    else
                        CompAces++;
                    adder += 11;
                    break;
                default:
                    adder += int.Parse(card.value);
                    break;
            }
            if (isPlayer)
            {
                CurrentPlayerPoints += adder;

                while (CurrentPlayerPoints > 21 && Player.aces > 0)
                {
                    CurrentPlayerPoints -= 10;
                    Player.aces--;
                }
                if (CurrentPlayerPoints > 21)
                {
                    //round end logic
                }
            }
            else
            {
                CurrentComputerPoints += adder;

                while (CurrentComputerPoints > 21 && CompAces > 0)
                {
                    CurrentPlayerPoints -= 10;
                    Player.aces--;
                }
                if (CurrentComputerPoints > 21)
                {
                    //round end logic
                }
            }
            
        }
       
    }
}

