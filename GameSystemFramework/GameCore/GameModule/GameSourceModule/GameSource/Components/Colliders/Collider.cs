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
        public event OnCollisionHandler OnCollisionEvent;

        public override void Start()
        {
            colProxy.collider = this;
            colProxy.SetTransform(transform.matrix);
            colProxy.CollisionEvent += ColProxy_CollisionEvent;
        }

        public override void OnDestroy()
        {
            // remove collision proxy from physic engine
            Manager.PhysicEngine.RemoveCollision(colProxy);
        }

        public override void LateUpdate()
        {
            colProxy.SetTransform(transform.matrix);
        }

        private void ColProxy_CollisionEvent(CollisionProxy colA, CollisionProxy colB)
        {
            OnCollisionEvent.Invoke(colA.collider, colB.collider);
        }
    }

    public delegate void OnCollisionHandler(Collider self, Collider other);

}
