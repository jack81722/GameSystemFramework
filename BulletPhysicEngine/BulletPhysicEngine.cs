using BulletSharp;
using GameSystem.GameCore;
using GameSystem.GameCore.Debugger;
using GameSystem.GameCore.GameModule.PhysicModule.ShapeInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletEngine
{
    public class BulletPhysicEngine : PhysicEngineProxy
    {
        private CollisionConfiguration configuration;
        private Dispatcher dispatcher;
        private BroadphaseInterface broadphase;
        private DiscreteDynamicsWorld world;

        public BulletPhysicEngine(IDebugger debugger) : base(debugger)
        {
            configuration = new DefaultCollisionConfiguration();
            dispatcher = new CollisionDispatcher(configuration);
            broadphase = new DbvtBroadphase();
            world = new DiscreteDynamicsWorld(dispatcher, broadphase, null, configuration);
        }

        #region Add/Remove collision methods
        public override void AddCollision(CollisionProxy colProxy, int layer = 1, int mask = -1)
        {
            world.AddCollisionObject((CollisionObject) colProxy.CollisionObject, layer, mask);
        }

        public override void RemoveCollision(CollisionProxy colProxy)
        {
            world.RemoveCollisionObject((CollisionObject) colProxy.CollisionObject);
        }
        #endregion

        #region Create collision proxy
        public override CollisionProxy CreateBoxCollision(IBoxShape shape, int layer = 1, int mask = -1)
        {
            CollisionObject co = new CollisionObject();
            co.CollisionShape = new BoxShape(shape.HalfSize.ToBullet());
            world.AddCollisionObject(co, layer, mask);
            return new BulletCollision(co, null);
        }

        public override CollisionProxy CreateSphereCollision(ISphereShape shape, int layer = 1, int mask = -1)
        {
            CollisionObject co = new CollisionObject();
            co.CollisionShape = new SphereShape(shape.Radius);
            world.AddCollisionObject(co, layer, mask);
            return new BulletCollision(co, null);
        }

        public override CollisionProxy CreateCapsuleCollision(ICapsuleShape shape, int layer = 1, int mask = -1)
        {
            CollisionObject co = new CollisionObject();
            co.CollisionShape = new CapsuleShape(shape.Radius, shape.Height);
            world.AddCollisionObject(co, layer, mask);
            return new BulletCollision(co, null);
        }

        public override CollisionProxy CreateConeCollision(IConeShape shape, int layer = 1, int mask = -1)
        {
            CollisionObject co = new CollisionObject();
            co.CollisionShape = new ConeShape(shape.Radius, shape.Height);
            world.AddCollisionObject(co, layer, mask);
            return new BulletCollision(co, null);
        }

        public override CollisionProxy CreateOtherCollision(int shapeType, int layer = 1, int mask = -1, params object[] shapeArgs)
        {
            throw new NotImplementedException();
        }

        
        #endregion

        public override void Initialize() { }

        public override void Update(TimeSpan deltaTime)
        {
            float second = (float)deltaTime.TotalSeconds;
            world.StepSimulation(second);

            // on collision events
            int numManifolds = world.Dispatcher.NumManifolds;
            for(int i = 0; i < numManifolds; i++)
            {
                PersistentManifold manifold = world.Dispatcher.GetManifoldByIndexInternal(i);
                CollisionProxy colA = (CollisionProxy)manifold.Body0.UserObject;
                CollisionProxy colB = (CollisionProxy)manifold.Body1.UserObject;

                // check if both are not null
                if (colA != null && colB != null)
                {
                    // execute colA -> colB event
                    try
                    {
                        colA.OnCollision(colA, colB);
                    }
                    catch (Exception e)
                    {
                        LogError(string.Format("{0} {1}", e.Message, e.StackTrace));
                    }
                    // execute colB -> colA event
                    try
                    {
                        colB.OnCollision(colB, colA);
                    }
                    catch (Exception e)
                    {
                        LogError(string.Format("{0} {1}", e.Message, e.StackTrace));
                    }
                }
            }
        }

        #region Setting methods
        // To do setting methods (Example : gravity, fixedtime ... etc.)
        #endregion
    }
}
