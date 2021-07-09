using System;

namespace MurphyJustin_BS
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();
            Console.Title = "Battle System";
            g.TitleScreen(true);
            g.Init();
            //loop until game returns false
            while (g.Update())
            {
                //game is running
            }
        }
    }
}
