using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    public sealed class GameObject : GameSource
    {
        private ComponentContainer container;

        private bool curActive;
        private bool newActive;
        public bool isActive
        {
            get { return curActive; }
            set
            {
                newActive = value;
                Manager.ChageState(this);
            }
        }
        public sealed override bool executing { get { return isActive; } }

        public GameObject()
        {   
            container = new ComponentContainer(this);
        }

        public T AddComponent<T>() where T : Component, new()
        {
            return container.AddComponent<T>(Manager);
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
    }
}
