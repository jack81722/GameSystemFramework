﻿using GameSystem.GameCore.GameModule.PhysicModule.ShapeInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore.Components
{
    public class SphereCollider : Collider, ISphereShape
    {
        private OnceSetValue<float> radius = new OnceSetValue<float>(0.5f);
        public float Radius { get { return radius; } }

        public override void Start()
        {
            colProxy = Manager.PhysicEngine.CreateSphereCollision(this);
            radius.Lock();
            base.Start();
        }

        public void SetSize(float radius)
        {
            this.radius.Set(radius);
        }
    }
}