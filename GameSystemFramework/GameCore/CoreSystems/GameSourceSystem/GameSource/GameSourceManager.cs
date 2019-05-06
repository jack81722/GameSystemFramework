using GameSystem.GameCore.Physics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ExCollection;
using System.Reflection;
using GameSystem.GameCore.Debugger;

namespace GameSystem.GameCore
{
    public class GameSourceManager : IGameModule
    {   
        /// <summary>
        /// All game sources
        /// </summary>
        private KeyedList<uint, GameSource> GSList;

        /// <summary>
        /// Source water id
        /// </summary>
        private uint serialSID;

        public bool updating { get; private set; }
        public TimeSpan DeltaTime { get; private set; }

        #region Temp of add/remove game sources
        // temp of added/removed game source which would execute start/destroy phase
        private List<GameSource> added;
        private List<GameSource> removed;

        // temp of adding/removing game sources for adding/removing 
        private List<GameSource> temp_adding;
        private List<GameSource> temp_remove;
        #endregion

        // temp of state refreshing game sources
        private List<GameSource> temp_stateRefresh;

        public PhysicEngineProxy PhysicEngine { get; private set; }

        private IDebugger Debugger;

        #region Constructor
        public GameSourceManager(PhysicEngineProxy physicEngine, IDebugger debugger)
        {
            serialSID = 0;
            GSList = new KeyedList<uint, GameSource>();
            
            added = new List<GameSource>();
            removed = new List<GameSource>();

            temp_adding = new List<GameSource>();
            temp_remove = new List<GameSource>();

            temp_stateRefresh = new List<GameSource>();

            PhysicEngine = physicEngine;
            Debugger = debugger;
        }
        #endregion

        /// <summary>
        /// Get primary SID
        /// </summary>
        public uint NewSID()
        {
            // what will happen if overflow
            uint id = serialSID++;
            while (GSList.ContainsKey(id))
                id = serialSID++;
            return id;
        }

        #region IGameModule methods
        public void Initialize()
        {
            
        }

        public void Update(TimeSpan deltaTime)
        {
            DeltaTime = deltaTime;
            updating = true;
            // start phase (only process non-started game source)
            _startPhase();

            // update phase
            _updatePhase();

            // late update phase
            _lateupdatePhase();
            updating = false;

            // refresh game source every end of frame
            RefreshGS();

            // execute OnDestroy method after destroy
            _onDestroyPhase();
        }
        #endregion

        #region Phase methods
        public void _startPhase()
        {
            for (int i = 0; i < added.Count; i++)
            {
                try
                {
                    added[i].Start();
                }
                catch (Exception e)
                {
                    Debugger.LogError(string.Format("{0} {1}", e.Message, e.StackTrace));
                }
            }
            added.Clear();
        }

        public void _updatePhase()
        {
            for (int i = 0; i < GSList.Count; i++)
            {
                // need try/catch ...
                try
                {
                    if (GSList[i].executing)
                        GSList[i].Update();
                }
                catch (Exception e)
                {
                    Debugger.LogError(string.Format("{0} {1}", e.Message, e.StackTrace));
                }
            }
        }

        public void _lateupdatePhase()
        {
            for (int i = 0; i < GSList.Count; i++)
            {
                try
                {
                    if (GSList[i].executing)
                        GSList[i].LateUpdate();
                }
                catch (Exception e)
                {
                    Debugger.LogError(string.Format("{0} {1}", e.Message, e.StackTrace));
                }
            }
        }

        public void _onDestroyPhase()
        {
            for (int i = 0; i < removed.Count; i++)
            {
                try
                {
                    removed[i].OnDestroy();
                }
                catch (Exception e)
                {
                    Debugger.LogError(string.Format("{0} {1}", e.Message, e.StackTrace));
                }
            }
            removed.Clear();
        }
        #endregion

        /// <summary>
        /// Execute while end of frame
        /// </summary>
        private void RefreshGS()
        {   
            for (int i = 0; i < temp_adding.Count; i++)
                _addGS(temp_adding[i]);
            for (int i = 0; i < temp_stateRefresh.Count; i++)
                temp_stateRefresh[i].OnEndOfFrame();
            for(int i = 0; i < temp_remove.Count; i++)
                _removeGS(temp_remove[i]);
        }

        public void ChageExecuteState(GameSource source)
        {
            temp_stateRefresh.Add(source);
        }

        #region Add/Remove GS
        /// <summary>
        /// Add gamer source
        /// </summary>
        /// <param name="source">game source</param>
        public void AddGameSource(GameSource source)
        {
            if (updating)
            {
                lock (temp_adding)
                    temp_adding.Add(source);
            }
            else
                _addGS(source);
        }

        private void _addGS(GameSource source)
        {
            lock (GSList)
            {
                if (!GSList.TryAdd(source)) return;
                added.Add(source);
            }
        }

        /// <summary>
        /// Remove game source
        /// </summary>
        /// <param name="source">game source</param>
        public void RemoveGameSource(GameSource source)
        {
            if (updating)
            {
                lock (temp_remove)
                    temp_remove.Add(source);
            }
            else
                _removeGS(source);
        }

        private void _removeGS(GameSource source)
        {
            lock (GSList)
            {
                GSList.Remove(source);
                removed.Add(source);
            }
        }
        #endregion

        public T Create<T>() where T : GameSource, new()
        {
            T source = new T() { Manager = this, SID = NewSID() };
            AddGameSource(source);
            return source;
        }

        public void Clear()
        {
            lock(GSList)
                GSList.Clear();
        }

        #region Find object methods
        public T FindObjectOfType<T>() where T : GameSource
        {
            return (T)GSList.Find(gs => gs.GetType() == typeof(T));
        }

        public IEnumerable<T> FindObjectsOfType<T>() where T : GameSource
        {
            return GSList.FindAll(gs => gs.GetType() == typeof(T)).Select(gs => (T)gs);
        }
        #endregion

        #region Log methods
        public void Log(object obj)
        {
            Debugger.Log(obj);
        }

        public void LogError(object obj)
        {
            Debugger.LogError(obj);
        }

        public void LogWarning(object obj)
        {
            Debugger.LogWarning(obj);
        }
        #endregion
    }
}
