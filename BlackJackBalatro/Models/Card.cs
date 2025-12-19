using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BlackJackBalatro.Services;

namespace BlackJackBalatro.Models
{
    public enum eRarity
    {
        Boring,
        Special,
        Evil,
        Bonus,
        IDK
    }

    public static class RNG
    {
        public static Random random = new Random();
    }

    public class Card
    {
        public String suite { get; set; }
        public String value { get; set; }
        public eRarity rarity { get; set; }
        public virtual void special(GameState state) { }

        public static String valueConverter(int value)
        {
            switch (value)
            {
                case 1:
                    return "Ace";
                case 11:
                    return "Jack";
                case 12:
                    return "Queen";
                case 13:
                    return "King";
                case 14:
                    return "Joker";
                default:
                    return Convert.ToString(value);
            }
        }
    }


    public class NormalCard : Card
    {
        public NormalCard(String value, String suite)
        {
            this.value = value;
            this.suite = suite;
            this.rarity = eRarity.Boring;
        }

        public override void special(GameState state)
        {
            String[] messages =
            {
            "This card does nothing",
            "Literally a boring card",
            "What did you expect to happen?"
        };

        }
        public static void initializeDeck(List<Card> deck)
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    deck.Add(new NormalCard(valueConverter(i + 1), "Diamonds"));
                    deck.Add(new NormalCard(valueConverter(i + 1), "Hearts"));
                    deck.Add(new NormalCard(valueConverter(i + 1), "Spades"));
                    deck.Add(new NormalCard(valueConverter(i + 1), "Clubs"));
                }
            }
        }
    }
    public class EvilCard : Card
    {
        public void Action(int index, GameState state) //Activates the cards' special effect based on the rarity
        {
            switch (index)
            {
                case 0://Adds Joker cards that will reduce a deck to 20 cards
                    EvilCard.cardStealer(state.NormalDeck);
                    break;
                    
                case 1:
                    for (int i = 0; i < 15; i++)
                    {
                        state.EvilDeck.Add(new EvilCard(eRarity.Evil, valueConverter(14), "Blacks"));
                    }
                    break;

                case 2:
                    if (state.Player.multiplier < 1)
                        break;
                    else
                        state.Player.multiplier--;
                    break;
                case 3:
                    while (state.BonusDeck.Count != 0)
                    {
                        state.BonusDeck.RemoveAt(0);
                    }
                    break;
                case 4:
                    state.Player.multiplier += 1;
                    break;
                case 5:
                    state.EvilDeck.Clear();
                    EvilCard.initializeDeck(state.EvilDeck);
                    break;
                case 6:
                    state.NormalDeck.Clear();
                    NormalCard.initializeDeck(state.NormalDeck);
                    break;
                case 7:
                    BonusCard.initializeDeck(state.BonusDeck);
                    break;
                case 8:
                    int rand = RNG.random.Next(2, 10);
                    for (int i = 0; i < rand; i++)
                    {
                        state.NormalDeck.Add(new NormalCard("Ace", "Spades"));
                    }
                    break;
                case 9:
                    state.Player.chips++;
                    break;
                case 10:
                    state.Player.storyLineTrigger = true;
                    break;
            }
        }

        public EvilCard(eRarity rarity, String value, String suite)
        {
            this.rarity = rarity;
            this.suite = suite;
            this.value = value;
        }
        static eRarity evilRarityAssigner()
        {
            int rarity = RNG.random.Next(100);

            if (rarity < 1)
            {
                return eRarity.IDK;
            }
            if (rarity < 20)
            {
                return eRarity.Evil;
            }
            else
            {
                return eRarity.Special;
            }
        }
        public static void initializeDeck(List<Card> deck)
        {
            for (int i = 0; i < 13; i++)
            {
                deck.Add(new EvilCard(evilRarityAssigner(), valueConverter(i + 1), "Diamonds"));
                deck.Add(new EvilCard(evilRarityAssigner(), valueConverter(i + 1), "Hearts"));
                deck.Add(new EvilCard(evilRarityAssigner(), valueConverter(i + 1), "Spades"));
                deck.Add(new EvilCard(evilRarityAssigner(), valueConverter(i + 1), "Clubs"));
            }
        }
        public override void special(GameState state)
        {
            if (this.value == "Joker")
            {
                Action(0, state);
                return;
            }
            if (this.rarity == eRarity.Evil)
            {
                Action(RNG.random.Next(1, 4), state);
            }
            if (this.rarity == eRarity.Special)
            {
                Action(RNG.random.Next(4, 10), state);
            }
            if (this.rarity == eRarity.IDK)
            {
                Action(10, state);
            }
        }
        public static void cardStealer(List<Card> deck)
        {
            int count = 20;
            if (deck.Count < 20)
                count = deck.Count;

            for (int i = 0; i < deck.Count; i++)
            {
                int index = RNG.random.Next(deck.Count);
                deck.RemoveAt(index);
            }
        }
    }
    public class BonusCard : Card
    {
        public void BonusAction(int index, GameState state)
        {
            switch (index)
            {
                case 0:
                    if (state.Player.chips == 0)
                    {
                        state.Player.chips += 2;
                    }
                    else
                    {
                        state.Player.chips *= 2;
                    }
                    break;
                case 1:
                    state.Player.chips += state.Player.specialDraws + state.Player.evilDraws;
                    break;
                case 2:
                    state.Player.chips += state.Player.mysteryDraws + state.Player.cardDraws;
                    break;
                case 3:
                    state.Player.chips += 200;
                    break;
                case 4:
                    int pos = RNG.random.Next(state.Player.availableItems.Count);
                    if (state.Player.availableItems.Count == 0)
                    {
                        break;
                    }
                    Items item = state.Player.availableItems[pos];

                    state.Player.playerInventory.Add(item);
                    state.Player.availableItems.RemoveAt(pos);
                    Items.itemTrigger(item, state.Player);
                    break;
            }
        }

        public BonusCard(String suite, String value)
        {
            this.suite = suite;
            this.value = value;
            this.rarity = eRarity.Bonus;
        }
        public static void initializeDeck(List<Card> deck)
        {
            String[] suites = { "Diamonds", "Hearts", "Spades", "Clubs" };

            for (int i = 0; i < RNG.random.Next(15); i++)
            {
                deck.Add(new BonusCard(suites[RNG.random.Next(4)], valueConverter(RNG.random.Next(1, 14))));
            }
        }
        public override void special(GameState state)
        {
            int balancer = RNG.random.Next(100);
            int range = 0;

            if (balancer < 10)
                range = 4;
            else if (balancer < 30)
                range = 3;
            else if (balancer < 50)
                range = 2;
            else if (balancer < 70)
                range = 1;
            else if (balancer < 100)
                range = 0;
            BonusAction(range, state);
        }

    }
}

