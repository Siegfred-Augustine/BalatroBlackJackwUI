using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackBalatro.Models
{

    public class Player
    {
        public String name = "Default Name";
        public int score = 0; //tracks the score of the player.
        public int chips = 0; //tracks players' current chips
        public int cardDraws = 0; //tracks total cards drawn by the player.
        public int evilDraws = 0; //tracks evil cards drawn.
        public int specialDraws = 0; //tracks special cards drawn.
        public int multiplier = 1; //multiplier for chip rewards.
        public int diamondDraws = 0; //can be added for a bonus.
        public int heartDraws = 0; //can be added for a bonus.
        public int spadeDraws = 0; //can be added for a bonus.
        public int clubDraws = 0; //can be added for a bonus.
        public int currentRoundPoints = 0; //tracks current players' total hand
        public int stands = 0; //calls out a player for being a coward.
        public int highestRound = 0; //tracks a trigger minigame.
        public int blackJacks = 0; //triggers a special text.
        public int busts = 0; //triggers pity chips.
        public int notFollowingInstructions = 0; //triggers all in bet.
        public int aces = 0;
        public bool storyLineTrigger = false;
        public Storyline storyline = Storyline.DeadbeatDad;
        public List<Items> availableItems = new List<Items>();
        public List<Items> playerInventory = new List<Items>();

        public Player(String? name)
        {
            if (name == null)
                this.name = "Default";
            else
                this.name = name;
        }
    }
    public enum Storyline
    {
        DeadbeatDad,
        NothingLeftToLose,
        DrownedInSorrow
    }
    public class Items
    {
        public String itemName = "";
        public String itemDesc = "";
        public int itemID;

        public Items(String name, String desc, int ID)
        {
            itemName = name;
            itemDesc = desc;
            itemID = ID;
        }

        public static void intializeItems(Player player, Storyline story)
        {
            List<Items> itemList = new List<Items>();
            switch (story)
            {
                case Storyline.DeadbeatDad:
                    itemList.Add(new Items("Hospital Bill", "A hospital bill under the name of ******", 1));
                    itemList.Add(new Items("Recital Invitation", "It's too crumpled to be read.", 3));
                    break;
                case Storyline.NothingLeftToLose:
                    itemList.Add(new Items("Gas Can", "A cannister for gasoline. It's already empty though.", 4));
                    itemList.Add(new Items("Matches", "A box of matches. Some of them are used.", 5));
                    break;
                case Storyline.DrownedInSorrow:
                    itemList.Add(new Items("Car Keys", "How did you get here?", 7));
                    itemList.Add(new Items("Henessy Bottle", "They don't serve this here.", 9));
                    break;
            }
            player.availableItems = itemList;
        }
        public static void itemTrigger(Items item, Player player)
        {
            switch (item.itemID)
            {
                case 1: Items.hospitalBillTrigger(player); break;
                case 3: Items.recitalInvTrigger(player); break;
                case 4: Items.gasCanTrigger(player); break;
                case 5: Items.matchTrigger(player); break;
                case 7: Items.carKeyTrigger(player); break;
                case 9: Items.hennyBottleTrigger(player); break;

            }
        }
        public static void hospitalBillTrigger(Player player)
        {
            Console.WriteLine("You keep saying to yourself that it's for her.");
            Thread.Sleep(2000);
            Console.WriteLine("You\'ve already won the money yet you\'re still here.");
            Thread.Sleep(2000);
            Console.WriteLine("Why are you still here.");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("*You put your hand in the pocket of your jacket and feel a piece of paper*");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("What was all this for?");
            Thread.Sleep(2000);
            Console.WriteLine("When will you stop");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            player.playerInventory.Add(new Items("Hospital Bill", "A hospital bill under the name of ******", 1));
            Thread.Sleep(2000);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        public void briefCaseTrigger()
        {
            Console.WriteLine("Have you forgotten this?");
            Thread.Sleep(2000);
            Console.WriteLine("Does this remind you of something?");
            Thread.Sleep(2000);
            Console.WriteLine("You were always ambitious.");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("*You look at an empty briefcase*");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("Nothing left.");
            Thread.Sleep(2000);
            Console.WriteLine("This is your reality now.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        public static void recitalInvTrigger(Player player)
        {
            Console.WriteLine("You didn\'t even know what you missed.");
            Thread.Sleep(2000);
            Console.WriteLine("Have you even checked the date?");
            Thread.Sleep(2000);
            Console.WriteLine("This might be her last time.");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("*You remember the invitation inside you wallet.*");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("It's worth it, right?");
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            //player.playerInventory.Add(new Items("Recital Invitation", "It's too crumpled to be read.", 3));
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        public static void gasCanTrigger(Player player)
        {
            Console.WriteLine("You smell something.");
            Thread.Sleep(2000);
            Console.WriteLine("You know what it is.");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("Was it to give up or to start over?");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("*You see a familiar object across the room*");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("Who did you owe again?");
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            //player.playerInventory.Add(new Items("Gas Can", "A cannister for gasoline. It's already empty though.", 4));
            Thread.Sleep(2000);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        public static void matchTrigger(Player player)
        {
            Console.WriteLine("Why the **** would you do that?");
            Thread.Sleep(2000);
            Console.WriteLine("You had no other choice, right?");
            Thread.Sleep(2000);
            Console.WriteLine("You should have never stayed.");
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.WriteLine("There\'s only one thing to do now.");
            Thread.Sleep(2000);
            Console.WriteLine("*A box of matches falls to the floor*");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("Did you hear them?");
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            //player.playerInventory.Add(new Items("Matches", "A box of matches. Some of them are used.", 5));
            Thread.Sleep(2000);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        public void gunTrigger()
        {
            Console.WriteLine("Why did you bring this?");
            Thread.Sleep(2000);
            Console.WriteLine("Was it for your protection?");
            Thread.Sleep(2000);
            Console.WriteLine("Was this all a last bit of fun?");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("*A single bullet shakes in the chamber*");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("Convenient.");
            Thread.Sleep(2000);
            Console.WriteLine("It\'s funny that you would think you would get out of the whole you created for yourself. This doesn\'t float you up.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        public static void carKeyTrigger(Player player)
        {
            Console.WriteLine("How did you even get here?");
            Thread.Sleep(2000);
            Console.WriteLine("Where did you park?");
            Thread.Sleep(2000);
            Console.WriteLine("Does this look like the casino to you?");
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.WriteLine("Who are you?");
            Thread.Sleep(2000);
            Console.WriteLine("*You pick a set of car keys*");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("You\'re afraid but you don\'t know why.");
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            //player.playerInventory.Add(new Items("Car Keys", "How did you get here?", 7));
            Thread.Sleep(2000);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        public void ringTrigger()
        {
            Console.WriteLine("Did you pick this up as a souvinir?");
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine("*You look at the bloody ring*");
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine("Is this a reminder?");
            Thread.Sleep(2000);
            Console.WriteLine("Why are you even here?");
            Thread.Sleep(2000);
            Console.WriteLine("If only there was a way to forgive someone who can\'t be forgiven.");
            Thread.Sleep(2000);
            Console.WriteLine("Remember everything.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        public static void hennyBottleTrigger(Player player)
        {
            Console.WriteLine("*You pick up an empty bottle of Henessy*");
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine("Did you brought it here empty?");
            Thread.Sleep(2000);
            Console.WriteLine("Why is there glass on your shirt?");
            Thread.Sleep(2000);
            Console.WriteLine("Do you hate seeing people alive?");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("Was it not enough?");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("Will you ever feel remorse?");
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            //player.playerInventory.Add(new Items("Henessy Bottle", "They don't serve this here.", 9));
            Thread.Sleep(2000);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        public static Storyline intro(Player player)
        {
            Console.WriteLine("You wake up.");
            Console.WriteLine("You have no idea what day it is.");
            Console.WriteLine("You know you need to go somewhere and do something. You don\'t know what it is.");
            Console.WriteLine("");
            Console.WriteLine("You have to go to the casino either way.");
            Console.WriteLine("");
            Console.WriteLine("Do you?");
            Console.WriteLine("[A] Go to the Casino right now.");
            Console.WriteLine("[B] Stay at home for a bit.");
            Console.WriteLine("[C] Check your calendar to see what you should do.");
            Console.Write("--> ");

            String answer = "a";
            while (true)
            {
                try
                {
                    answer = Console.ReadLine().ToLower();
                    if (answer != "a" && answer != "b" && answer != "c")
                    {
                        Console.WriteLine("Choose a valid option.");
                        Console.Write("-->");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Choose a valid option.");
                    Console.Write("-->");
                    continue;
                }
            }
            String choice = "y";
            switch (answer)
            {
                case "a":
                    Console.WriteLine("You immediately go to the casino");
                    Thread.Sleep(2000);
                    Console.WriteLine("Do you bring your briefcase?[y/n]");

                    while (true)
                    {
                        try
                        {
                            choice = Console.ReadLine().ToLower();
                            if (choice != "y" && choice != "n")
                            {
                                Console.WriteLine("Choose a valid option.");
                                Console.Write("-->");
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Choose a valid option.");
                            Console.Write("-->");
                            continue;
                        }
                    }

                    if (choice == "y")
                    {
                        player.playerInventory.Add(new Items("Briefcase", "An empty black briefcase.", 2));
                        Thread.Sleep(2000);
                        Console.WriteLine("Briefcase has been added to your inventory.");
                    }
                    Thread.Sleep(2000);
                    Console.WriteLine("You go to the Casino.");
                    Console.WriteLine("");
                    Thread.Sleep(3000);
                    return Storyline.DeadbeatDad;

                case "b":
                    Console.WriteLine("You stay at home for a bit.");
                    Thread.Sleep(2000);
                    Console.WriteLine("You decided to drink some liquor before heading out.");
                    Thread.Sleep(2000);
                    Console.WriteLine("You decided to walk to the casino at night.");
                    Thread.Sleep(2000);
                    Console.WriteLine("Do you bring your holster?[y/n]");
                    String a = "y";
                    while (true)
                    {
                        try
                        {
                            choice = Console.ReadLine().ToLower();
                            if (choice != "y" && choice != "n")
                            {
                                Console.WriteLine("Choose a valid option.");
                                Console.Write("-->");
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Choose a valid option.");
                            Console.Write("-->");
                            continue;
                        }
                    }

                    if (choice == "y")
                    {
                        player.playerInventory.Add(new Items("Glock 17", "A gun with a bullet in the chamber.", 6));
                        Thread.Sleep(2000);
                        Console.WriteLine("Glock 17 has been added to your inventory.");
                    }
                    Thread.Sleep(2000);
                    Console.WriteLine("You go to the Casino.");
                    Console.WriteLine("");
                    Thread.Sleep(3000);
                    return Storyline.NothingLeftToLose;

                case "c":
                    Console.WriteLine("You look at the calendar.");
                    Thread.Sleep(2000);
                    Console.WriteLine("You see that you're late to your daughter\'s recital");
                    Thread.Sleep(2000);
                    Console.WriteLine("You got into your car and drove as quickly as you could.");
                    Thread.Sleep(2000);
                    Console.WriteLine("You get to the venue and see nobody was left. You decided to head to the casino.");
                    Thread.Sleep(2000);
                    Console.WriteLine("You saw a Diamond Ring on the way. Do you pick it up? [y/n]");
                    Thread.Sleep(2000);

                    while (true)
                    {
                        try
                        {
                            choice = Console.ReadLine().ToLower();
                            if (choice != "y" && choice != "n")
                            {
                                Console.WriteLine("Choose a valid option.");
                                Console.Write("-->");
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Choose a valid option.");
                            Console.Write("-->");
                            continue;
                        }
                    }

                    if (choice == "y")
                    {
                        player.playerInventory.Add(new Items("Bloody Diamond Ring", "This isn't yours", 8));
                        Thread.Sleep(2000);
                        Console.WriteLine("Bloody Diamond Ring has been added to your inventory.");
                    }
                    Thread.Sleep(2000);
                    Console.WriteLine("You go to the Casino.");
                    Console.WriteLine("");
                    Thread.Sleep(3000);
                    return Storyline.DrownedInSorrow;
                default:
                    return Storyline.DeadbeatDad;
            }
        }
    }   

}
