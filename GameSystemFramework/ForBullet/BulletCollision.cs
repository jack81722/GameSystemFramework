using BulletSharp;
using GameSystem.GameCore;
using GameSystem.GameCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.ForBullet
{
    public class BulletCollision : CollisionProxy
    {
        public CollisionObject colObj;
        public override object CollisionData { get { return colObj; } set { colObj = (CollisionObject)value; } }

        public BulletCollision (CollisionObject colObj, Collider collider) : base(collider)
        {
            this.colObj = colObj;
        }
    }
}
