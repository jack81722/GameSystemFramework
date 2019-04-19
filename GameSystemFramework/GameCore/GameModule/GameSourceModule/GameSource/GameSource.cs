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

        public GameSource()
        {
            transform = new Transform();
        }

        #region Update phases
        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void OnDestroy() { }
        #endregion

        #region End of frame
        public virtual void OnEndOfFrame() { }
        #endregion

        #region Instantiate methods
        protected GameSource Instantiate(GameSource prefab)
        {   
            return Manager.Instantiate(prefab);
        }

        protected GameSource Instantiate(GameSource prefab, Vector3 position)
        {
            return Manager.Instantiate(prefab, position);
        }

        protected GameSource Instantiate(GameSource prefab, Vector3 position, Quaternion rotation)
        {
            return Manager.Instantiate(prefab, position, rotation);
        }

        protected GameSource Instantiate(GameSource prefab, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            return Manager.Instantiate(prefab, position, rotation, scale);
        }

        protected T Instantiate<T>(T prefab) where T : GameSource, new()
        {
            return Manager.Instantiate(prefab);
        }

        protected T Instantiate<T>(T prefab, Vector3 position) where T : GameSource, new()
        {
            return Manager.Instantiate(prefab, position);
        }

        protected T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation) where T : GameSource, new()
        {
            return Manager.Instantiate(prefab, position, rotation);
        }

        protected T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Vector3 scale) where T : GameSource, new()
        {
            return Manager.Instantiate(prefab, position, rotation, scale);
        }
        #endregion

        public void Destroy(GameSource gs)
        {
            Manager.RemoveGameSource(gs);
        }
    }
}
