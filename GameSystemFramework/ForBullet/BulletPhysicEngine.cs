using BulletSharp;
using GameSystem.GameCore;
using GameSystem.GameCore.GameModule.PhysicModule.ShapeInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.ForBullet
{
    class BulletPhysicEngine : PhysicEngineProxy
    {
        private CollisionConfiguration configuration;
        private Dispatcher dispatcher;
        private BroadphaseInterface broadphase;
        private DiscreteDynamicsWorld world;

        public BulletPhysicEngine() : base()
        {
            configuration = new DefaultCollisionConfiguration();
            dispatcher = new CollisionDispatcher(configuration);
            broadphase = new DbvtBroadphase();
            world = new DiscreteDynamicsWorld(dispatcher, broadphase, null, configuration);
        }

        public override void AddCollision(CollisionProxy colProxy)
        {
            world.AddCollisionObject((CollisionObject) colProxy.CollisionData);
        }

        #region Create collision proxy
        public override CollisionProxy CreateBoxCollision(IBoxShape shape)
        {
            CollisionObject co = new CollisionObject();
            co.CollisionShape = new BoxShape(shape.HalfSize.ToBullet());
            world.AddCollisionObject(co);
            return new BulletCollision(co, null);
        }

        public override CollisionProxy CreateCapsuleCollision(ICapsuleShape shape)
        {
            CollisionObject co = new CollisionObject();
            co.CollisionShape = new CapsuleShape(shape.Radius, shape.Height);
            world.AddCollisionObject(co);
            return new BulletCollision(co, null);
        }

        public override CollisionProxy CreateConeCollision(IConeShape shape)
        {
            CollisionObject co = new CollisionObject();
            co.CollisionShape = new ConeShape(shape.Radius, shape.Height);
            world.AddCollisionObject(co);
            return new BulletCollision(co, null);
        }

        public override CollisionProxy CreateOtherCollision(int shapeType, params object[] shapeArgs)
        {
            throw new NotImplementedException();
        }

        public override CollisionProxy CreateSphereCollision(ISphereShape shape)
        {
            CollisionObject co = new CollisionObject();
            co.CollisionShape = new SphereShape(shape.Radius);
            world.AddCollisionObject(co);
            return new BulletCollision(co, null);
        }
        #endregion

        public override void RemoveCollision(CollisionProxy colProxy)
        {
            world.RemoveCollisionObject((CollisionObject)colProxy.CollisionData);
        }

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

                colA.OnCollision(colA, colB);
                colB.OnCollision(colB, colA);
            }
        }
    }
}
