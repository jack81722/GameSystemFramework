using BulletSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore.Components
{
    /// <summary>
    /// Basic collider component
    /// </summary>
    public abstract class Collider : Component
    {
        protected CollisionProxy colProxy;

        public override void Start()
        {
            colProxy.collider = this;
            colProxy.CollisionEvent += ColProxy_CollisionEvent;
        }

        public override void OnDestroy()
        {
            // remove collision proxy from physic engine
            Manager.PhysicEngine.RemoveCollision(colProxy);
        }

        private void ColProxy_CollisionEvent(CollisionProxy colA, CollisionProxy colB)
        {
            OnCollision(colA.collider, colB.collider);
        }

        public virtual void OnCollision(Collider self, Collider other) { }
    }
}
