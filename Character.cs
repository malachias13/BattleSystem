using System;
using System.Collections.Generic;
using System.Text;

namespace MurphyJustin_BS
{
    //character with stats derives from object to have a name
    public class Character
    {
        //@Shawn, @Sean, @Brian - got lazy here and don't want to change to comments - Murphy
        protected string name = "Default";
        protected float HPMax = 10;
        protected float HP = 10;
        protected float Strength = 3;

        public string Name()
        {
            return name;
        }


        // Accessor/Getter for the current hit points
        public float GetHP()
        {
            return HP;
        }

        // Accessor/Getter for the attack points
        public float GetStrength()
        {
            return Strength;
        }

        // Modifying the health by subtracting the damage passed in
        public void ApplyDamage(float damage)
        {
            HP -= damage;
            HP = MathF.Max(0, HP); //prevents health below 0
        }

        // Magic abilities
        public void HealthingSpell(Player player, float healthing)
        {
              player.HP += healthing;
              if (player.HP > player.HPMax)
              {
                  player.HP = player.HPMax;
              }
        }

        public void AttackSpell(Enemy[] enemies, float damage)
        {
            foreach (Enemy Rivals in enemies)
            {
                Rivals.ApplyDamage(damage);
            }
        }


        // Prints the different stats in a readable output, changes color to 
        // green when full health, yellow for damaged, and black for dead
        public string PrintStats()
        {
            string output = "";
            output += name;
            output += " HP:" + HP;
            output += " Strength:" + Strength;

            //end the line after each character
            output += "\n";
            // change background color based on health
            if (HP == HPMax)//full health
                Console.ForegroundColor = ConsoleColor.Green;
            else if (HP > 0)// partial health
                Console.ForegroundColor = ConsoleColor.Yellow;
            else// no health
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(output);
            //reset color after
            Console.ForegroundColor = ConsoleColor.White;
            return output;
        }


        //virtual so children can override
        public virtual ConsoleColor GetTeamColor()
        {
            return ConsoleColor.Black;
        }

    }

    public class Player : Character
    {
        //Override with the player's team color
        public override ConsoleColor GetTeamColor()
        {
            return ConsoleColor.DarkMagenta;
        }

        // overloaded constructor
        public Player(string _n, float _HP, float _A)
        {
            name = _n;
            HPMax = _HP;
            HP = HPMax;
            Strength = _A;
        }
    }

    public class Enemy : Character
    {
        public override ConsoleColor GetTeamColor()
        {
            return ConsoleColor.Red;
        }

        // overloaded constructor
        public Enemy(string _n, float _HP, float _A)
        {
            name = _n;
            HPMax = _HP;
            HP = HPMax;
            Strength = _A;
        }
    }
}
