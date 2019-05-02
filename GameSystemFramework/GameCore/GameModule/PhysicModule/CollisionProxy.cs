﻿using GameSystem.GameCore.Components;
using GameSystem.GameCore.SerializableMath;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    /// <summary>
    /// Abstract collision proxy class
    /// </summary>
    public abstract class CollisionProxy
    {
        #region Delegate handler of collision
        public delegate void CollisionHandler(CollisionProxy colA, CollisionProxy colB);
        #endregion

        /// <summary>
        /// Collider component of game system
        /// </summary>
        public Collider collider;

        /// <summary>
        /// Event instance of collision handler
        /// </summary>
        public event CollisionHandler CollisionEvent;

        /// <summary>
        /// Collision object of physic engine
        /// </summary>
        public virtual object CollisionObject { get; set; }

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
