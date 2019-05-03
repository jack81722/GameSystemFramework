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
        /// <summary>
        /// Add collision object
        /// </summary>
        /// <param name="colProxy">collision proxy object</param>
        /// <param name="layer">layer of collision</param>
        /// <param name="mask">mask of collision</param>
        public abstract void AddCollision(CollisionProxy colProxy, int layer = 1, int mask = -1);

        /// <summary>
        /// Remove collision object
        /// </summary>
        /// <param name="colProxy">collision proxy object</param>
        public abstract void RemoveCollision(CollisionProxy colProxy);
        #endregion

        /// <summary>
        /// Create standard box collision
        /// </summary>
        /// <param name="shape">box shape interface</param>
        /// <param name="layer">layer of collision</param>
        /// <param name="mask">mask of collision</param>
        public abstract CollisionProxy CreateBoxCollision(IBoxShape shape, int layer = 1, int mask = -1);

        /// <summary>
        /// Create standard sphere collision
        /// </summary>
        /// <param name="shape">sphere shape interface</param>
        /// <param name="layer">layer of collision</param>
        /// <param name="mask">mask of collision</param>
        public abstract CollisionProxy CreateSphereCollision(ISphereShape shape, int layer = 1, int mask = -1);

        /// <summary>
        /// Create standard capsule collision
        /// </summary>
        /// <param name="shape">capsule shape interface</param>
        /// <param name="layer">layer of collision</param>
        /// <param name="mask">mask of collision</param>
        public abstract CollisionProxy CreateCapsuleCollision(ICapsuleShape shape, int layer = 1, int mask = -1);

        /// <summary>
        /// Create standard cone collision
        /// </summary>
        /// <param name="shape">cone shape interface</param>
        /// <param name="layer">layer of collision</param>
        /// <param name="mask">mask of collision</param>
        public abstract CollisionProxy CreateConeCollision(IConeShape shape, int layer = 1, int mask = -1);

        /// <summary>
        /// Create other type shape collision
        /// </summary>
        /// <param name="shapeType">other shape type</param>
        /// <param name="layer">layer of collision</param>
        /// <param name="mask">mask of collision</param>
        /// <param name="shapeArgs">other shape argument</param>
        public abstract CollisionProxy CreateOtherCollision(int shapeType, int layer = 1, int mask = -1, params object[] shapeArgs);

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
