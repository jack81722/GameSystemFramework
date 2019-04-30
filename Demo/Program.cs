using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleGame.SimpleGame game = new SimpleGame.SimpleGame();
            game.Init();
            game.StartGame();

            Console.ReadLine();
        }
    }
}
