using System;
using System.Collections.Generic;
using System.Text;
using GameSystem.GameCore.SerializableMath;

namespace GameSystem.GameCore
{
    public class Component : GameSource
    {
        #region Enable field
        private bool curEnable = true;
        private bool newEnable;
        public bool enable
        {
            get { return curEnable; }
            set
            {
                newEnable = value;
                Manager.ChageState(this);
            }
        }
        #endregion

        public sealed override bool executing { get { return (gameObject != null ? gameObject.isActive : false) && enable; } }

        public GameObject gameObject { get; set; }

        public Component() { }

        public void SetEnable(bool isEnable)
        {
            enable = isEnable;
        }

        public sealed override void OnEndOfFrame()
        {
            curEnable = newEnable;
        }

        public T GetComponent<T>() where T : Component
        {
            return gameObject.GetComponent<T>();
        }

        #region Instantiate methods
        public static GameObject Instantiate(GameObject prefab)
        {
            return GameObject.Instantiate(prefab);
        }

        public static GameObject Instantiate(GameObject prefab, Vector3 position)
        {
            return GameObject.Instantiate(prefab, position);
        }

        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return GameObject.Instantiate(prefab, position, rotation);
        }

        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            return GameObject.Instantiate(prefab, position, rotation, scale);
        }
        #endregion
    }
}
