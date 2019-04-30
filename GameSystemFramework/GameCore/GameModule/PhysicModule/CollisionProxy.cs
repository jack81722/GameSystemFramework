using GameSystem.GameCore.Components;
using GameSystem.GameCore.SerializableMath;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    public abstract class CollisionProxy
    {
        #region Delegate handler of collision
        public delegate void OnCollisionHandler(CollisionProxy colA, CollisionProxy colB);
        #endregion

        public Collider collider;
        public event OnCollisionHandler CollisionEvent;

        public virtual object CollisionData { get; set; }

        public CollisionProxy(Collider collider)
        {
            this.collider = collider;
        }

        public void OnCollision(CollisionProxy colA, CollisionProxy colB)
        {
            CollisionEvent.Invoke(colA, colB);
        }

        public abstract void SetTransform(Matrix4x4 matrix);
    }
}
