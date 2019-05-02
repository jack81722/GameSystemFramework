using System;
using System.Collections.Generic;
using System.Text;

namespace BulletEngine
{
    /// <summary>
    /// Converter between Bullet Math and Serialization Math
    /// </summary>
    public static class BulletSerialization
    {
        public static BulletSharp.Math.Vector3 ToBullet(this GameSystem.GameCore.SerializableMath.Vector3 v)
        {
            return new BulletSharp.Math.Vector3(v.x, v.y, v.z);
        }

        public static GameSystem.GameCore.SerializableMath.Vector3 ToSerialize(this BulletSharp.Math.Vector3 v)
        {
            return new GameSystem.GameCore.SerializableMath.Vector3(v.X, v.Y, v.Z);
        }

        public static GameSystem.GameCore.SerializableMath.Matrix4x4 ToSerialize(this BulletSharp.Math.Matrix m)
        {   
            return new GameSystem.GameCore.SerializableMath.Matrix4x4(m.ToArray());
        }

        public static BulletSharp.Math.Matrix ToBullet(this GameSystem.GameCore.SerializableMath.Matrix4x4 m)
        {
            var bMatrix = new BulletSharp.Math.Matrix(
                m.m11, m.m12, m.m13, m.m14,
                m.m21, m.m22, m.m23, m.m24,
                m.m31, m.m32, m.m33, m.m34,
                m.m41, m.m42, m.m43, m.m44);
            return bMatrix;
        }
    }
}
