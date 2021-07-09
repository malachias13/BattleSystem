using System;
using System.Collections.Generic;
using System.Text;
using BattleSystem;

namespace MurphyJustin_BS
{
    public class Menu
    {
        enum MenuSlections
        {
            MagicSpells,
            PhysicalAttacks
        };
        public enum MagicalAblities
        {
            AOE,
            Healthing,
            None
        };
        enum PhysicalAttacks
        {
            Physical
        };

        MenuSlections menuSlections = MenuSlections.MagicSpells;
        public MagicalAblities ablities = MagicalAblities.AOE;
        PhysicalAttacks physicalAttacks = PhysicalAttacks.Physical;

        

        ConsoleKeyInfo KeyPressed;

        private string[] strMenuSlect = new string[2];
        private string[] strMagicAblities = new string[2];
        private string[] strPhysicalAttacks = new string[1];

        int menuIndex = 0;
        bool hasSlected = false;
        bool hasCasted = false;
        // Console Color
        ConsoleColor selectionColor = ConsoleColor.Gray;
        ConsoleColor textColor = ConsoleColor.Black;

        public MagicalAblities DrawMenu() // Put a pram for set console culrsoer 
        {
            Initalizes();
            hasCasted = false;
            hasSlected = false;
            KeyPressed = default(ConsoleKeyInfo);

            while (hasCasted == false)
            {
                Console.SetCursorPosition(0, 15);

                if (!hasSlected)
                {
                    menuSlections = GetMenuSlections(KeyPressed, strMenuSlect, menuSlections);
                }
                else if (hasSlected)
                {
                    if (menuSlections == MenuSlections.MagicSpells)
                    {
                        ablities = GetMenuSlections(KeyPressed, strMagicAblities, ablities);
                    }
                    else if (menuSlections == MenuSlections.PhysicalAttacks)
                    {
                        physicalAttacks = GetMenuSlections(KeyPressed, strPhysicalAttacks, physicalAttacks);
                    }
                }
                switch (menuSlections)
                {
                    case MenuSlections.MagicSpells:
                        if (!hasSlected)
                        {
                            ConsoleExtracts.HighlightTextLine(strMenuSlect[0], selectionColor, textColor);
                            Console.WriteLine(strMenuSlect[1]);
                        }
                        else
                        {
                            ConsoleExtracts.HighlightTextLine($"  *{strMenuSlect[0]}", selectionColor, textColor);
                            DrawMagicAblitiesMenu(ablities);
                            Console.WriteLine(strMenuSlect[1]);

                        }
                        break;
                    case MenuSlections.PhysicalAttacks:
                        if (!hasSlected)
                        {
                            Console.WriteLine(strMenuSlect[0]);
                            ConsoleExtracts.HighlightTextLine(strMenuSlect[1], selectionColor, textColor);
                        }
                        else
                        {
                            Console.WriteLine(strMenuSlect[0]);
                            ConsoleExtracts.HighlightTextLine($"  *{strMenuSlect[1]}", selectionColor, textColor);
                            DrawPhysicalMenu(physicalAttacks);
                        }

                        break;
                }
                if(hasCasted)
                {
                    break;
                }
                KeyPressed = Console.ReadKey(true);
                if (!hasSlected)
                {
                    ConsoleExtracts.ClearLines(3, 6);
                }
                else if(hasSlected)
                {
                    ConsoleExtracts.ClearLines(4, 6);
                }
            }
            return ablities;
        }

        public Player DrawPlayerMenu(Player[] players)
        {
            if(players.Length != 3) { return null; }
            int index = 0;
            KeyPressed = default(ConsoleKeyInfo);
            while (KeyPressed.Key != ConsoleKey.Enter)
            {
                Console.SetCursorPosition(0, 15);

                switch (index)
                {
                    case 0:
                        ConsoleExtracts.HighlightTextLine($"> {players[0].Name()} <", selectionColor, textColor);
                        Console.WriteLine(players[1].Name());
                        Console.WriteLine(players[2].Name());
                        break;
                    case 1:
                        Console.WriteLine(players[0].Name());
                        ConsoleExtracts.HighlightTextLine($"> {players[1].Name()} <", selectionColor, textColor);
                        Console.WriteLine(players[2].Name());
                        break;

                    case 2:
                        Console.WriteLine(players[0].Name());
                        Console.WriteLine(players[1].Name());
                        ConsoleExtracts.HighlightTextLine($"> {players[2].Name()} <", selectionColor, textColor);
                        break;
                }
                KeyPressed = Console.ReadKey(true);
                index = GetMenuSlections(KeyPressed, players,index);
                ConsoleExtracts.ClearLines(3, 3);
            }
            hasSlected = false;
            return players[index];
        }

        public Enemy DrawEnemyMenu(Enemy[] enemies)
        {
            if(enemies.Length != 3) { return null; }
            int index = 0;
            KeyPressed = default(ConsoleKeyInfo);
            while (KeyPressed.Key != ConsoleKey.Enter)
            {
                Console.SetCursorPosition(0, 15);
                switch (index)
                {
                    case 0:
                        ConsoleExtracts.HighlightText($"> {enemies[0].Name()} <", selectionColor, textColor);
                        Console.WriteLine(enemies[1].Name());
                        Console.WriteLine(enemies[2].Name());
                        break;
                    case 1:
                        Console.WriteLine(enemies[0].Name());
                        ConsoleExtracts.HighlightText($"> {enemies[1].Name()} <", selectionColor, textColor);
                        Console.WriteLine(enemies[1].Name());
                        break;
                    case 2:
                        Console.WriteLine(enemies[0].Name());
                        Console.WriteLine(enemies[1].Name());
                        ConsoleExtracts.HighlightText($"> {enemies[2].Name()} <", selectionColor, textColor);
                        break;
                }
                KeyPressed = Console.ReadKey(true);
                index = GetMenuSlections(KeyPressed, enemies, index);
                ConsoleExtracts.ClearLines(5,4);
            }
            hasSlected = false;
            return enemies[index];
        }

        public void Initalizes()
        {
            strMenuSlect[0] = "Magic Spells";
            strMenuSlect[1] = "Physical Attacks";
            
            strMagicAblities[0] = "AOE";
            strMagicAblities[1] = "Healthing";

            strPhysicalAttacks[0] = "Physical";
        }

        private MagicalAblities DrawMagicAblitiesMenu(MagicalAblities magicalAblities)
        {
            ConsoleColor Slecte2Color = ConsoleColor.Yellow;
            ConsoleColor Slecte2TextColor = ConsoleColor.Black;
            switch (magicalAblities)
            {
                case MagicalAblities.AOE:

                    ConsoleExtracts.HighlightText($"> {strMagicAblities[0]} <", Slecte2Color, Slecte2TextColor);
                    Console.WriteLine(strMagicAblities[1]);
                    break;
                case MagicalAblities.Healthing:
                    Console.WriteLine(strMagicAblities[0]);
                    ConsoleExtracts.HighlightText($"> {strMagicAblities[1]} <", Slecte2Color, Slecte2TextColor);
                    break;
            }
            return magicalAblities;
        }

        private PhysicalAttacks DrawPhysicalMenu(PhysicalAttacks physicalAttacks)
        {
            ConsoleColor Slecte2Color = ConsoleColor.Yellow;
            ConsoleColor Slecte2TextColor = ConsoleColor.Black;

            switch (physicalAttacks)
            {
                case PhysicalAttacks.Physical:
                    ConsoleExtracts.HighlightText($"> {strPhysicalAttacks[0]} <", Slecte2Color, Slecte2TextColor);
                    break;
            }
            return physicalAttacks;
        }

        private int GetMenuSlections(ConsoleKeyInfo keyPress,Player[] strArray, int menuIndex)
        {
            if (keyPress.Key == ConsoleKey.UpArrow)
            {
                menuIndex = menuIndex <= 0 ? menuIndex = strArray.Length - 1 : Math.Abs((menuIndex - 1) % strArray.Length);
            }
            else if (keyPress.Key == ConsoleKey.DownArrow)
            {
                menuIndex = (menuIndex + 1) % strArray.Length;
            }
            else if (keyPress.Key == ConsoleKey.Enter)
            {
                hasSlected = true;
            }
            else if (keyPress.Key == ConsoleKey.Escape)
            {
                hasSlected = false;
            }
            return menuIndex;
        }
        private int GetMenuSlections(ConsoleKeyInfo keyPress, Enemy[] strArray, int menuIndex)
        {
            if (keyPress.Key == ConsoleKey.UpArrow)
            {
                menuIndex = menuIndex <= 0 ? menuIndex = strArray.Length - 1 : Math.Abs((menuIndex - 1) % strArray.Length);
            }
            else if (keyPress.Key == ConsoleKey.DownArrow)
            {
                menuIndex = (menuIndex + 1) % strArray.Length;
            }
            else if (keyPress.Key == ConsoleKey.Enter)
            {
                hasSlected = true;
            }
            else if (keyPress.Key == ConsoleKey.Escape)
            {
                hasSlected = false;
            }
            return menuIndex;
        }
        private MenuSlections GetMenuSlections(ConsoleKeyInfo keyPress, string[] strArray, MenuSlections menu)
        {

            // menuIndex;

            if (keyPress.Key == ConsoleKey.UpArrow)
            {
                menuIndex = menuIndex <= 0 ? menuIndex = strArray.Length - 1 : Math.Abs((menuIndex - 1) % strArray.Length);
                menu = (MenuSlections)menuIndex;
            }
            else if (keyPress.Key == ConsoleKey.DownArrow)
            {
                menuIndex = (menuIndex + 1) % strArray.Length;
                menu = (MenuSlections)menuIndex;
            }
            else if (keyPress.Key == ConsoleKey.Enter)
            {
                hasSlected = true;
            }
            else if(keyPress.Key == ConsoleKey.Escape)
            {
                hasSlected = false;
            }
            return menu;

        }
        private MagicalAblities GetMenuSlections(ConsoleKeyInfo keyPress, string[] strArray, MagicalAblities menu)
        {

            // menuIndex;

            if (keyPress.Key == ConsoleKey.UpArrow)
            {
                menuIndex = menuIndex <= 0 ? menuIndex = strArray.Length - 1 : Math.Abs((menuIndex - 1) % strArray.Length);
                menu = (MagicalAblities)menuIndex;
            }
            else if (keyPress.Key == ConsoleKey.DownArrow)
            {
                menuIndex = (menuIndex + 1) % strArray.Length;
                menu = (MagicalAblities)menuIndex;
            }
            else if (keyPress.Key == ConsoleKey.Enter)
            {
                hasSlected = true;
                hasCasted = true;
            }
            else if (keyPress.Key == ConsoleKey.Escape)
            {
                hasSlected = false;
            }
            return menu;

        }
        private PhysicalAttacks GetMenuSlections(ConsoleKeyInfo keyPress, string[] strArray, PhysicalAttacks menu)
        {

            // menuIndex;

            if (keyPress.Key == ConsoleKey.UpArrow)
            {
                menuIndex = menuIndex <= 0 ? menuIndex = strArray.Length - 1 : Math.Abs((menuIndex - 1) % strArray.Length);
                menu = (PhysicalAttacks)menuIndex;
            }
            else if (keyPress.Key == ConsoleKey.DownArrow)
            {
                menuIndex = (menuIndex + 1) % strArray.Length;
                menu = (PhysicalAttacks)menuIndex;
            }
            else if (keyPress.Key == ConsoleKey.Enter)
            {
                hasSlected = true;
                hasCasted = true;
                ablities = MagicalAblities.None;
            }
            else if (keyPress.Key == ConsoleKey.Escape)
            {
                hasSlected = false;
            }
            return menu;

        }

    }
}
