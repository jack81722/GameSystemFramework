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
            Log($"{gameObject.Name} {transform.position} hit {other.gameObject.Name} {other.transform.position}");
            //Destroy(this);
        }

        public override void Update()
        {
            pos.x += speed * (float)DeltaTime.TotalSeconds;
            //Log($"{gameObject.Name}:");
            //Log($"Pos: {pos}, Speed: {speed}, Time:{DeltaTime.TotalSeconds}");
            transform.position = pos;
        }
    }
}
