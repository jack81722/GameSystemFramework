using System;
using System.Collections.Generic;
using System.Text;
using BulletSharp;
using GameSystem.GameCore.Components;
using GameSystem.GameCore.GameModule.PhysicModule.ShapeInterface;

namespace GameSystem.GameCore
{
    public abstract class PhysicEngineProxy : IGameModule
    {
        public List<CollisionProxy> collisions;

        public PhysicEngineProxy()
        {
            collisions = new List<CollisionProxy>();
        }

        public abstract void Update(TimeSpan tick);

        public abstract void AddCollision(CollisionProxy colProxy);

        public abstract void RemoveCollision(CollisionProxy colProxy);

        /// <summary>
        /// Create standard box collision
        /// </summary>
        /// <param name="shape">box shape interface</param>
        public abstract CollisionProxy CreateBoxCollision(IBoxShape shape);

        /// <summary>
        /// Create standard sphere collision
        /// </summary>
        /// <param name="shape">sphere shape interface</param>
        public abstract CollisionProxy CreateSphereCollision(ISphereShape shape);

        /// <summary>
        /// Create standard cone collision
        /// </summary>
        /// <param name="shape">cone shape interface</param>
        public abstract CollisionProxy CreateConeCollision(IConeShape shape);

        /// <summary>
        /// Create standard capsule collision
        /// </summary>
        /// <param name="shape">capsule shape interface</param>
        public abstract CollisionProxy CreateCapsuleCollision(ICapsuleShape shape);

        /// <summary>
        /// Create other type shape collision
        /// </summary>
        /// <param name="shapeType">other shape type</param>
        /// <param name="shapeArgs">other shape argument</param>
        public abstract CollisionProxy CreateOtherCollision(int shapeType, params object[] shapeArgs);

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }

}
