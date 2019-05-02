using Demo.SimpleGame.Scripts;
using GameSystem.GameCore;
using GameSystem.GameCore.Components;
using GameSystem.GameCore.SerializableMath;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SimpleGame
{
    public class SimpleGameBuilder : GameBuilder
    {
        protected override void Building()
        {
            float speed = 3f;

            // create character 1
            GameObject char_go1 = CreateGameObject();
            var char_com = char_go1.AddComponent<Character>();
            char_com.speed = speed;
            var char_col = char_go1.AddComponent<BoxCollider>();
            char_col.SetSize(new Vector3(0.1f));

            // create character 2 cloned by character 1
            GameObject char_go2 = Instantiate(char_go1);
            char_go2.GetComponent<Character>().speed = -speed;


            char_go1.transform.position = new Vector3(-10, 0, 0);
            char_go2.transform.position = new Vector3(10, 0, 0);

            Console.WriteLine($"char_go1 position = {char_go1.transform.position}");
            Console.WriteLine($"char_go2 position = {char_go2.transform.position}");
        }
    }
}
