using BulletSharp;
using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultCollisionConfiguration configuration = new DefaultCollisionConfiguration();

            SimpleGame.SimpleGame game = new SimpleGame.SimpleGame();
            game.Init();
            game.StartGame();

            Console.ReadLine();
        }
    }
}
