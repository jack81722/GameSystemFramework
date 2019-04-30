using GameSystem.GameCore.SerializableMath;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    public sealed class GameObject : GameSource
    {
        public string Name;
        private ComponentContainer container;

        public bool isActive
        {
            get { return curActive; }
            set
            {
                newActive = value;
                Manager.ChageState(this);
            }
        }
        private bool curActive = true;
        private bool newActive;
        
        public sealed override bool executing { get { return isActive; } }

        // Cannot be called straightly ...
        public GameObject()
        {
            container = new ComponentContainer(this);
        }

        public T AddComponent<T>() where T : Component, new()
        {
            return container.AddComponent<T>(Manager);
        }

        public T AddComponent<T>(T component) where T : Component
        {
            return container.AddComponent(component);
        }

        public T GetComponent<T>() where T : Component
        {
            return container.GetComponent<T>();
        }

        public void SetActive(bool active)
        {
            isActive = active;
        }

        public sealed override void OnEndOfFrame()
        {
            curActive = newActive;
        }

        #region Instantiate method
        public static GameObject Instantiate(GameObject prefab)
        {
            GameObject go = prefab.Manager.Create<GameObject>();
            prefab.container.CopyTo(ref go.container);
            go.Name = $"{prefab.Name} (Clone)";
            go.isActive = prefab.isActive;
            return go;
        }

        public static GameObject Instantiate(GameObject prefab, Vector3 position)
        {
            GameObject go = prefab.Manager.Create<GameObject>();
            prefab.container.CopyTo(ref go.container);
            go.Name = $"{prefab.Name} (Clone)";
            go.isActive = prefab.isActive;
            go.transform.position = position;
            return go;
        }

        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject go = prefab.Manager.Create<GameObject>();
            prefab.container.CopyTo(ref go.container);
            go.Name = $"{prefab.Name} (Clone)";
            go.isActive = prefab.isActive;
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }

        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            GameObject go = prefab.Manager.Create<GameObject>();
            prefab.container.CopyTo(ref go.container);
            go.Name = $"{prefab.Name} (Clone)";
            go.isActive = prefab.isActive;
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.transform.scale = scale;
            return go;
        }
        #endregion
    }
}
