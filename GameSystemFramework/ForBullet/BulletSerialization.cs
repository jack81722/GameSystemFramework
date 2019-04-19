using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.ForBullet
{
    public static class BulletSerialization
    {
        public static BulletSharp.Math.Vector3 ToBullet(this GameCore.SerializableMath.Vector3 v)
        {
            return new BulletSharp.Math.Vector3(v.x, v.y, v.z);
        }

        public static GameCore.SerializableMath.Vector3 ToSerialize(this BulletSharp.Math.Vector3 v)
        {
            return new GameCore.SerializableMath.Vector3(v.X, v.Y, v.Z);
        }

        public static GameCore.SerializableMath.Matrix4x4 ToSerialize(this BulletSharp.Math.Matrix m)
        {   
            return new GameCore.SerializableMath.Matrix4x4(m.ToArray());
        }

        public static BulletSharp.Math.Matrix ToBullet(this GameCore.SerializableMath.Matrix4x4 m)
        {
            return new BulletSharp.Math.Matrix(m.ToArray());
        }
    }
}
