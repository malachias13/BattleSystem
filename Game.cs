using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; // easy access to allow pausing (Thread.Sleep)
using BattleSystem;
namespace MurphyJustin_BS
{
    class Game
    {
        //declare game variables
        // int for which character to select from input
        int curSelection = -1;
        // character that will be doing the attacking this turn
        Character Attacker = null;
        // character that will be defending this turn
        Character Defender = null;
        // random number generator for NPC turns
        Random rng = new Random();

        // bool to determine if it's the player's turn
        bool playerTurn = true;

        // bool to check to see if the player and enemy alive
        bool playersAlive = true;
        bool rivalsAlive = true;

        Player[] players = new Player[3];
        Enemy[] Rivals = new Enemy[3];

        Menu menu = new Menu();

        public static int shortDelay = 1200;

        enum InitInput
        {
            Name,
            Hp,
            Strength
        };
        enum MagicAblities
        {
            None,
            Heal,
            AOE
        };

        InitInput initInput = InitInput.Name;
        
        public void TitleScreen(bool isTitleScreen)
        {
            string title = string.Empty;


            title = @"
  ____        _   _   _         _____           _                 
 |  _ \      | | | | | |       / ____|         | |                
 | |_) | __ _| |_| |_| | ___  | (___  _   _ ___| |_ ___ _ __ ___  
 |  _ < / _` | __| __| |/ _ \  \___ \| | | / __| __/ _ \ '_ ` _ \ 
 | |_) | (_| | |_| |_| |  __/  ____) | |_| \__ \ ||  __/ | | | | |
 |____/ \__,_|\__|\__|_|\___| |_____/ \__, |___/\__\___|_| |_| |_|
                                       __/ |                      
                                      |___/                       
";


            if(isTitleScreen)
            {
                ConsoleExtracts.CenterTextLine(title, ConsoleColor.Yellow);
                ConsoleExtracts.ColorTextLine("\nPress any key to continue...",ConsoleColor.Gray);
                ConsoleExtracts.ColorTextLine("\n\n@ Made by: Malachias Harris",ConsoleColor.Cyan);
                Console.ReadKey(true);
                Console.Clear();
            }
            else
            {
                ConsoleExtracts.CenterTextLine(title, ConsoleColor.Yellow);
            }
        }

        public void Init()
        {
            int playerCount = 0;
            // fill the player party array with 3 Players filled out with use of the overloaded constructor
            string name = string.Empty;
            float Hp = 0;
            float Strength = 0;

            bool Validation = false;
            bool isCompleated = false;
            bool isFairValue = false;
                

            #region Importing Data
            Console.WriteLine("To import data type \"ReadFile\". To input data press enter.");
            LoadData loadData = new LoadData();
            string inputCommand;
            inputCommand = Console.ReadLine();
            Console.Clear();
            TitleScreen(false);

            if (inputCommand.ToLower() == "readfile")
            {
                //SampleData.csv
                string strFile;
                bool isCorrectFile = false;
                while (!isCorrectFile)
                {
                    Console.WriteLine("Import existing data\nType Name of cvc file don't worry about the \"@\" at the begining." +
                        "\nType: \"SampleData.csv\" to acess the sample data.");
                    strFile = Console.ReadLine();
                    strFile.Insert(0, "@");
                    isCorrectFile = loadData.LoadDataIn(strFile);
                    ConsoleExtracts.ClearLines(5, 6);
                }
                loadData.LoadDataInGame(ref players);

                // fill the rival party array with 3 Enemies filled out with use of the overloaded constructor
                for (int i = 0; i < Rivals.Length; i++)
                {
                    Enemy enemyPlayer = new Enemy($"Rivals {i + 1}", rng.Next(8, 13), rng.Next(3, 6));
                    Rivals[i] = enemyPlayer;
                }

                PrintParties();
                Console.ReadKey();
                return;
            }
            #endregion



            for (int timesLooped = 0; timesLooped < 3; timesLooped++)
            {
                string input = string.Empty;
                isCompleated = false;

                while(!isCompleated)
                {
                    switch (initInput)
                    {
                        case InitInput.Name:
                            Console.Clear();
                            Console.WriteLine($"Input Name for Player {playerCount + 1}");
                            input = Console.ReadLine();
                            name = input;
                            initInput = InitInput.Hp;
                            break;
                        case InitInput.Hp:
                            while (!Validation || !isFairValue)
                            {
                                Console.Clear();
                                Console.WriteLine($"Input Hp for Player {playerCount + 1}");
                                input = Console.ReadLine();

                                Validation = float.TryParse(input, out Hp);
                                if (!Validation)
                                {
                                    Console.WriteLine("Sorry Floats or integers only.");
                                    Thread.Sleep(shortDelay);
                                }
                                else
                                {
                                    isFairValue = Hp >= 8 && Hp <= 12 ? true : false;
                                    if (isFairValue)
                                        break;
                                    else
                                    {
                                        Console.WriteLine("Only accept values between 8 - 12.");
                                        Thread.Sleep(shortDelay);
                                    }
                                }

                            }
                            initInput = InitInput.Strength;
                            Validation = false;
                            break;
                        case InitInput.Strength:
                            while (!Validation || !isFairValue)
                            {
                                Console.Clear();
                                Console.WriteLine($"Input Strength for Player {playerCount + 1}");
                                input = Console.ReadLine();

                                Validation = float.TryParse(input, out Strength);
                                if (!Validation)
                                {
                                    Console.WriteLine("Sorry Floats or integers only.");
                                    Thread.Sleep(shortDelay);
                                }
                                else
                                {
                                    isCompleated = true;
                                    isFairValue = Strength >= 3 && Strength <= 5 ? true : false;
                                    if (isFairValue)
                                        break;
                                    else
                                    {
                                        Console.WriteLine("Only accept values between 3 - 5");
                                        Thread.Sleep(shortDelay);
                                    }

                                }
                            }
                            initInput = InitInput.Name;
                            Validation = false;
                            break;

                    }

                }
                if (!isCompleated)
                    continue;
                


                for (; playerCount < players.Length;)
                {
                    Player player = new Player(name, Hp, Strength);
                    players[playerCount] = player;
                    playerCount++;
                    if (timesLooped < 9)
                        break;
                }
            }


            // fill the rival party array with 3 Enemies filled out with use of the overloaded constructor
            for (int i = 0; i < Rivals.Length; i++)
            {
                Enemy enemyPlayer = new Enemy($"Rivals {i + 1}", rng.Next(8, 13), rng.Next(3, 6));
                Rivals[i] = enemyPlayer;
            }
        }

        //returns false when game is over
        public bool Update()
        {
            // clears console for a fresh start
            Console.Clear();

            //branch who's turn it is and add a line indicating so
            //print that it's the player's turn
            //print the current parties
            //run the player turn
            //else
            //print the rivals turn label
            //print the current parties
            //run the rivals turn

            if(playerTurn)
            {
                Console.WriteLine("Player turn");
                PlayerTurn();
            }
            else
            {
                Console.WriteLine("Rivals turn");
                RivalsTurn();
            }



            //end game check
            return EndTurn();
        }

        void PlayerTurn()
        {
            // Loop until an attacker is chosen
            while (Attacker == null)
            {
                // use num 1-3 to select player party member that is the attacker
                //  ConsoleKeyInfo k = Console.ReadKey();
                // start a new line after user input
                //  Console.WriteLine();

                // Data Validation: make sure the key typed is a valid key
                /*    if (k.KeyChar < '1' || k.KeyChar > '3')
                    {
                        //Repeat instructions if wrong key pressed
                        // loop again
                        continue;
                    }
                    else // convert from key input (1-3) to array element space (0-2)
                        curSelection = int.Parse(k.KeyChar.ToString()) - 1;
                */
                //check to make sure the selected character is alive HP > 0
                //print the attackers name
                //assign the selected character as the attacker
                //else 
                //character's dead choose again
                Console.Clear();
                Console.WriteLine("Player turn");
                PrintParties();
                // print instructions to select an attacker
                Console.WriteLine("Select a player to fight\n");

                // Set position to 14 y
                Player temp;

                temp = menu.DrawPlayerMenu(players);

                if (temp.GetHP() > 0)
                {
                    Console.WriteLine(temp.Name() + " has been slected to attack.");
                    Attacker = temp;
                    Thread.Sleep(shortDelay);
                }
                else
                {
                    Console.WriteLine(temp.Name() + " is dead. Please choose another character to attack.");
                    Thread.Sleep(shortDelay);
                    if(players[0].GetHP() <= 0 && players[1].GetHP() <= 0 && players[2].GetHP() <= 0)
                    {
                        playersAlive = false;
                        return;
                    }
                }

            }


            /*   Console.WriteLine("Would you like to prefrom a magic attack?" +
                   "\n Healing spell: Type \"H\" followed by the player index (starting with one)." +
                   "\n AOE Damage spell: Type \"AOE\"");

               string spells = string.Empty;
               bool hasSlected = false;
             */


            /* while(!hasSlected)
             {
                 spells = Console.ReadLine();

                 if (spells.ToLower() == "h1" || spells.ToLower() == "h2"|| spells.ToLower() == "h3")
                 {
                     hasSlected = true;
                     magicAblities = MagicAblities.Heal;
                 }
                 else if(spells.ToLower() == "aoe")
                 {
                     hasSlected = true;
                     magicAblities = MagicAblities.AOE;
                 }
                 else if(spells.ToLower() == "no")
                 {
                     hasSlected = true;
                     magicAblities = MagicAblities.None;
                 }
                 else
                 {
                     // DO a console set cursor and do console clear.
                     Console.WriteLine("Would you like to prefrom a magic attack?" +
                      "\n Healing spell: Type \"H\" followed by the player index (starting with one)." +
                      "\n AOE Damage spell: Type \"AOE\"");
                 }
             }
            */
            Console.Clear();
            Console.WriteLine("Player turn");
            PrintParties();
            Console.WriteLine("Select Attacks\n");
            menu.ablities = menu.DrawMenu();
            
            if (menu.ablities == Menu.MagicalAblities.None)
            {

                //print instructions for choosing a rival.
                //loop until a defender is choosen
                Console.WriteLine("Slecte a Rivial to attack");
                while (Defender == null)
                {
                    // use 1-3 to select player party member that is the attacker
                    /*   ConsoleKeyInfo k = Console.ReadKey();

                       //add a new line after the user input
                       Console.WriteLine();

                       // Data Validation: make sure the key typed is a valid key
                       if (k.KeyChar < '1' || k.KeyChar > '3')
                       {
                           // repeat instructions
                           // loop again
                           continue;
                       }
                       else // convert from key input (1-3) to array element space (0-2)
                           curSelection = int.Parse(k.KeyChar.ToString()) - 1; //minus one to use as index
                    */
                    //check to make sure the selected character is alive HP > 0
                    //print the defenders name
                    //assign the selected character as the defender
                    //else
                    //print instructions again   
                    Console.Clear();
                    Console.WriteLine("Player turn");
                    PrintParties();
                    Console.WriteLine("Slecte a Rival to attack");

                    Enemy tempE = menu.DrawEnemyMenu(Rivals);

                    if (tempE.GetHP() > 0)
                    {
                        Console.WriteLine("You attacked " + tempE.Name() + ".");
                        Defender = tempE;
                        Thread.Sleep(shortDelay);
                    }
                    else
                    {
                        Console.WriteLine(tempE.Name() + " is dead please slecte another character to attack.");
                        Thread.Sleep(shortDelay);
                        if (Rivals[0].GetHP() <= 0 && Rivals[1].GetHP() <= 0 && Rivals[2].GetHP() <= 0)
                        {
                            rivalsAlive = false;
                            return;
                        }
                    }

                }
            }

            switch (menu.ablities)
            {
                case Menu.MagicalAblities.AOE:
                    Attacker.AttackSpell(Rivals, Attacker.GetStrength());
                    Defender = Rivals[0];
                    break;
                case Menu.MagicalAblities.Healthing:
                    Console.Clear();
                    PrintParties();
                    Console.WriteLine("Pick a player to heal");
                   Attacker.HealthingSpell(menu.DrawPlayerMenu(players),3f);
                    break;
                case Menu.MagicalAblities.None:
                    //damage the defender by the attacker's Strength value
                    Defender.ApplyDamage(Attacker.GetStrength());
                    break;
            }

            /*   switch (menu.ablities)
               {
                   case Menu.MagicalAblities.None:
                       //damage the defender by the attacker's Strength value
                       Defender.ApplyDamage(Attacker.GetStrength());
                       break;
                   case Menu.MagicalAblities.AOE:
                       Attacker.AttackSpell(Rivals, Attacker.GetStrength());
                       Defender = Rivals[0];
                       break;
                   case Menu.MagicalAblities.Healthing:
                       spells = spells.ToLower();
                       spells = spells.Trim('h');

                       int index = Convert.ToInt32(spells);
                       Attacker.HealthingSpell(players[index - 1], 3f);

                       break;
               }
            */



            if (menu.ablities == Menu.MagicalAblities.None)
            {
                //change color for rival team
                Console.BackgroundColor = Defender.GetTeamColor();

                //print the new rival's health
                string output = $"\n{Defender.Name()}: ";
                output += $"Health - {Defender.GetHP()}";

                Console.WriteLine(output);
            }

            //change color back for normal
            Console.BackgroundColor = ConsoleColor.Black;
            //pause for 2 seconds
            Thread.Sleep(2000);

            //reset attacker/defender for next attack
            Attacker = null;
            Defender = null;
        }

        void RivalsTurn() //switch to rng instead of user input
        {
            PrintParties();
            // print instructions that the rival is choosing which character will attack
            Console.WriteLine("\nRival is choosing a character to attack with.");
            // pause for 2 seconds
            Thread.Sleep(2000);
            // loop until a valid attacker is found
            while (Attacker == null)
            {
                // randomly select an attacker from the rival party
                curSelection = rng.Next(3); //assumes 3 party members

                // check if the attacker has health
                // print name of the attacker chosen
                // assign the selected player as the attacker

                if(Rivals[curSelection].GetHP() > 0)
                {
                    Attacker = Rivals[curSelection];
                    Console.WriteLine("\n"+Attacker.Name() + " is the attacker.");
                    Thread.Sleep(shortDelay);
                }
                else if(Rivals[0].GetHP() <= 0 && Rivals[1].GetHP() <= 0 && Rivals[2].GetHP() <= 0)
                {
                    rivalsAlive = false;
                    return;  
                }

            }

            //print that the rival is choosing which player member to attack
            Console.WriteLine("Rival is choosing which player member to attack");
            // pause for 2 seconds
            Thread.Sleep(2000);

            Console.Clear();
            // loop until a valid defender is found
            while (Defender == null)
            {
                // randomly choose a player's character
                curSelection = rng.Next(3); //assumes 3 party members

                // check if the player is alive
                // print the name of the chosen defender
                // assign the selected player as the defender

                if(players[curSelection].GetHP() > 0)
                {
                    Defender = players[curSelection];
                    Console.WriteLine(Defender.Name()+ " is being attacked!");
                    Thread.Sleep(shortDelay);
                }
                else if(players[0].GetHP() <= 0 && players[1].GetHP() <= 0 && players[2].GetHP() <= 0)
                {
                    playersAlive = false;
                    return;
                }

            }

            //damage the defender by the attacker's Strength value
            Defender.ApplyDamage(Attacker.GetStrength());
            //change color for rival team
            Console.BackgroundColor = Defender.GetTeamColor();
            //print the new player's health
            string output = $"\n{Defender.Name()}:";
            output += $"Health - {Defender.GetHP()}";

            Console.WriteLine(output);
            //change color back for normal
            Console.BackgroundColor = ConsoleColor.Black;
            //pause for 2 seconds
            Thread.Sleep(2000);

            //reset attacker/defender for next attack
            Attacker = null;
            Defender = null;
        }

        void PrintParties()
        {
            //change backround color for player team
            //print label for player's team
            //loop through player party printing each character's stats
            //   Console.BackgroundColor = players[0].GetTeamColor();
            ConsoleExtracts.HighlightTextLine("\n\nPlayer Team",players[0].GetTeamColor(),ConsoleColor.Black);
            foreach (var player in players)
            {
                player.PrintStats();
            }   

            //change background for rival team
            //print label for rival team
            //loop through rival party printing each character's stats
            //change backround color back to default
            //  Console.BackgroundColor = Rivals[0].GetTeamColor();
            ConsoleExtracts.HighlightTextLine("\nRivals",Rivals[0].GetTeamColor(),ConsoleColor.Black);
            foreach (var rivis in Rivals)
            {
                rivis.PrintStats();
            }
            Console.WriteLine(" ");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        bool EndTurn()
        {
            //switch turns for next loop
            playerTurn = !playerTurn;

            // loop through players to see if they're alive and store in a variable counting if they're alive or not
           // bool playersAlive = true;
            // same for rivals
           // bool rivalsAlive = true;
            // if both have things alive start the next round, pause the game, and return true to continue playing
            if (playersAlive && rivalsAlive)
            {
                Console.WriteLine("Next Round Starts in 5 seconds");
                Thread.Sleep(5000);
                return true;
            } // if only the players have members alive you win
            else if (playersAlive)
            {
                //clear screen for results
                Console.Clear();
                //print you've won and parties
                Console.WriteLine("Congrats you win! Final Standings:");
                PrintParties();
                Console.ReadKey();
                return false;
            } // only rival members are alive
            else
            {
                //clear screen for results
                Console.Clear();
                //Print you've lost and parties
                Console.WriteLine("You Lose :( Final Standings:");
                PrintParties();
                Console.ReadKey();
                return false;
            }
        }
    }
}
