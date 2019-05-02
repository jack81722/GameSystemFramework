using System;
using GameSystem.GameCore.Debugger;
using GameSystem.GameCore.GameModule.PhysicModule.ShapeInterface;

namespace GameSystem.GameCore
{
    public abstract class PhysicEngineProxy : IGameModule
    {
        protected IDebugger Debugger;

        public PhysicEngineProxy(IDebugger debugger)
        {
            Debugger = debugger;
        }

        public virtual void Initialize() { }
        public virtual void Update(TimeSpan tick) { }

        #region Add/Remove collision methods

        public abstract void AddCollision(CollisionProxy colProxy);

        public abstract void RemoveCollision(CollisionProxy colProxy);
        #endregion

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

        #region Log methods
        public void Log(object obj)
        {
            Debugger.Log(obj);
        }

        public void LogError(object obj)
        {
            Debugger.LogError(obj);
        }

        public void LogWarning(object obj)
        {
            Debugger.LogWarning(obj);
        }
        #endregion
    }

}
