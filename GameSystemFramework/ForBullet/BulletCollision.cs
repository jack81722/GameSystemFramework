using BulletSharp;
using GameSystem.GameCore;
using GameSystem.GameCore.Components;
using GameSystem.GameCore.SerializableMath;
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
            colObj.UserObject = this;
        }

        public override void SetTransform(Matrix4x4 matrix)
        {
            colObj.WorldTransform = matrix.ToBullet();
        }
    }
}
