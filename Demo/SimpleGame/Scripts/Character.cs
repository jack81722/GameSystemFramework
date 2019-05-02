using GameSystem.GameCore;
using GameSystem.GameCore.Components;
using GameSystem.GameCore.SerializableMath;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SimpleGame.Scripts
{
    public class Character : Component
    {
        private static int water_id;
        public int id { get; private set; }
        public float speed = 1;
        public Vector3 pos;
        public BoxCollider collider;

        public override void Start()
        {
            id = water_id++;
            pos = transform.position;
            collider = GetComponent<BoxCollider>();
            collider.OnCollisionEvent += Collider_OnCollisionEvent;
        }

        private void Collider_OnCollisionEvent(Collider self, Collider other)
        {   
            if (id < other.GetComponent<Character>().id)
            {
                Log($"{self.Name} Hit {other.Name}");
                Destroy(gameObject);
            }
        }

        public override void OnDestroy()
        {
            Log($"Destroyed {Name}");
        }

        public override void Update()
        {
            pos.x += speed * (float)DeltaTime.TotalSeconds;
            //Log($"{gameObject.Name}:");

            //Log($"{Name} at {transform.position}");

            //Log($"Pos: {pos}, Speed: {speed}, Time:{DeltaTime.TotalSeconds}");
            transform.position = pos;
            //Log(transform.matrix);
        }
    }
}
