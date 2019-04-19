using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    public class Component : GameSource
    {
        #region Enable field
        private bool curEnable;
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
    }
}
