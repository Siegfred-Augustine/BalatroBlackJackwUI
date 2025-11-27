using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
        public virtual void special(Player player, List<Card> normalDeck, List<Card> evilDeck, List<Card> bonusDeck) { }

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

        public override void special(Player player, List<Card> normalDeck, List<Card> evilDeck, List<Card> bonusDeck)
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
        public void Action(int index, Player player, List<Card> normalDeck, List<Card> evilDeck, List<Card> bonusDeck) //Activates the cards' special effect based on the rarity
        {
            switch (index)
            {
                case 0://Adds Joker cards that will reduce a deck to 20 cards
                    for (int i = 0; i < 15; i++)
                    {
                        evilDeck.Add(new EvilCard(eRarity.Evil, valueConverter(14), "Blacks"));
                    }
                    break;
                case 1:
                    EvilCard.cardStealer(normalDeck);
                    break;
                case 2:
                    if (player.multiplier < 1)
                        break;
                    else
                        player.multiplier--;
                    break;
                case 3:
                    while (bonusDeck.Count != 0)
                    {
                        bonusDeck.RemoveAt(0);
                    }
                    break;
                case 4:
                    player.multiplier += 1;
                    break;
                case 5:
                    evilDeck.Clear();
                    EvilCard.initializeDeck(evilDeck);
                    break;
                case 6:
                    normalDeck.Clear();
                    NormalCard.initializeDeck(normalDeck);
                    break;
                case 7:
                    BonusCard.initializeDeck(bonusDeck);
                    break;
                case 8:
                    int rand = RNG.random.Next(2, 10);
                    for (int i = 0; i < rand; i++)
                    {
                        normalDeck.Add(new NormalCard("Ace", "Spades"));
                    }
                    break;
                case 9:
                    player.chips++;
                    break;
                case 10:
                    player.storyLineTrigger = true;
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
        public override void special(Player player, List<Card> normalDeck, List<Card> evilDeck, List<Card> bonusDeck)
        {
            if (this.rarity == eRarity.Evil)
            {
                Action(RNG.random.Next(4), player, normalDeck, evilDeck, bonusDeck);
            }
            if (this.rarity == eRarity.Special)
            {
                Action(RNG.random.Next(4, 10), player, normalDeck, evilDeck, bonusDeck);
            }
            if (this.rarity == eRarity.IDK)
            {
                Action(10, player, normalDeck, evilDeck, bonusDeck);
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
        public void BonusAction(int index, Player player, List<Card> deck)
        {
            switch (index)
            {
                case 0:
                    if (player.chips == 0)
                    {
                        player.chips += 2;
                    }
                    else
                    {
                        player.chips *= 2;
                    }
                    break;
                case 1:
                    player.chips += player.heartDraws + player.diamondDraws;
                    break;
                case 2:
                    player.chips += player.spadeDraws + player.clubDraws;
                    break;
                case 3:
                    player.chips += 200;
                    break;
                case 4:
                    int pos = RNG.random.Next(player.availableItems.Count);
                    if (player.availableItems.Count == 0)
                    {
                        break;
                    }
                    Items item = player.availableItems[pos];

                    player.playerInventory.Add(item);
                    player.availableItems.RemoveAt(pos);
                    Items.itemTrigger(item, player);
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
        public override void special(Player player, List<Card> normalDeck, List<Card> evilDeck, List<Card> bonusDeck)
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
            BonusAction(range, player, bonusDeck);
        }

    }
}

