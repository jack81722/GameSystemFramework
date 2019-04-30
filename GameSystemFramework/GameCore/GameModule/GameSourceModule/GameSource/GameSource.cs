using GameSystem.GameCore.SerializableMath;
using ExCollection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    public class GameSource : IKeyable<uint>
    {
        private bool lockManager = false;
        private GameSourceManager _manager;
        public GameSourceManager Manager
        {
            get { return _manager; }
            set { if (!lockManager) { lockManager = true; _manager = value; } }
        }
        private bool lockSID = false;
        private uint _sid;
        public uint SID
        {
            get { return _sid; }
            set { if (!lockSID) { lockSID = true; _sid = value; } }
        }

        public uint Key { get { return SID; } set { SID = value; } }

        public virtual bool executing { get; }
        public Transform transform;

        public TimeSpan DeltaTime { get { return Manager.DeltaTime; } }

        public GameSource()
        {
            transform = new Transform();
        }

        #region Update phases
        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void LateUpdate() { }

        public virtual void OnDestroy() { }
        #endregion

        #region End of frame
        public virtual void OnEndOfFrame() { }
        #endregion

        public void Destroy(GameSource gs)
        {
            Manager.RemoveGameSource(gs);
        }

        #region Log methods
        public void Log(object obj)
        {
            Manager.Log(obj);
        }

        public void LogError(object obj)
        {
            Manager.LogError(obj);
        }

        public void LogWarning(object obj)
        {
            Manager.LogWarning(obj);
        }
        #endregion
    }
}
