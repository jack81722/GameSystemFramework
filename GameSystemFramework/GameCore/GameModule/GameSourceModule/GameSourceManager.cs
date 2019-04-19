using GameSystem.GameCore.SerializableMath;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ExCollection;

namespace GameSystem.GameCore
{
    public class GameSourceManager : IGameModule
    {   
        private KeyedList<uint, GameSource> GSList;
        private uint source_id;

        public bool updating { get; private set; }
        public TimeSpan DeltaTime { get; private set; }

        // temp of added/removed game source for start/destroy phase
        private List<GameSource> added;
        private List<GameSource> removed;

        // temp of adding/removing game source for refresh 
        private List<GameSource> temp_adding;
        private List<GameSource> temp_remove;

        private List<GameSource> temp_stateRefresh;

        public PhysicEngineProxy PhysicEngine { get; private set; }

        public GameSourceManager(PhysicEngineProxy physicEngine)
        {
            source_id = 0;
            GSList = new KeyedList<uint, GameSource>();
           
            added = new List<GameSource>();
            removed = new List<GameSource>();

            temp_adding = new List<GameSource>();
            temp_remove = new List<GameSource>();

            temp_stateRefresh = new List<GameSource>();

            PhysicEngine = physicEngine;
        }
        
        /// <summary>
        /// Get primary SID
        /// </summary>
        public uint NewSID()
        {
            // what will happen if overflow
            uint id = source_id++;
            while (GSList.ContainsKey(id))
                id = source_id++;
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
            for (int i = 0; i < added.Count; i++)
            {
                // need try/catch ...
                added[i].Start();
            }
            added.Clear();

            // update phase
            for (int i = 0; i < GSList.Count; i++)
            {
                // need try/catch ...
                if(GSList[i].executing)
                    GSList[i].Update();
            }
            for (int i = 0; i < GSList.Count; i++)
            {
                // need try/catch ...
                GSList[i].OnDestroy();
            }
            updating = false;

            // refresh game source every end of frame
            RefreshGS();
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

        public void ChageState(GameSource source)
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
                if (!GSList.TryAdd(source)) Console.WriteLine("add same key gs.");
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
                GSList.Remove(source);
        }
        #endregion

        #region Instantiate methods
        public T Create<T>() where T : GameSource, new()
        {
            T source = new T() { Manager = this, SID = NewSID() };
            AddGameSource(source);
            return source;
        }

        public GameSource Instantiate(GameSource prefab)
        {
            GameSource gs = new GameSource() { Manager = this, SID = NewSID(), transform = new Transform() };
            AddGameSource(gs);
            return gs;
        }

        public GameSource Instantiate(GameSource prefab, Vector3 position)
        {
            GameSource gs = new GameSource() { Manager = this, SID = NewSID(), transform = new Transform(position) };
            AddGameSource(gs);
            return gs;
        }

        public GameSource Instantiate(GameSource prefab, Vector3 position, Quaternion rotation)
        {
            GameSource gs = new GameSource() { Manager = this, SID = NewSID(), transform = new Transform(position, rotation) };
            AddGameSource(gs);
            return gs;
        }

        public GameSource Instantiate(GameSource prefab, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            GameSource gs = new GameSource() { Manager = this, SID = NewSID(), transform = new Transform(position, rotation, scale) };
            AddGameSource(gs);
            return gs;
        }

        public T Instantiate<T>(T source) where T : GameSource, new()
        {
            T newSource = new T() { Manager = this, SID = NewSID() };
            AddGameSource(source);
            return newSource;
        }

        public T Instantiate<T>(T source, Vector3 position) where T : GameSource, new()
        {
            T newSource = new T() { Manager = this, SID = NewSID(), transform = new Transform(position) };
            AddGameSource(source);
            return newSource;
        }

        public T Instantiate<T>(T source, Vector3 position, Quaternion rotation) where T : GameSource, new()
        {
            T newSource = new T() { Manager = this, SID = NewSID(), transform = new Transform(position, rotation) };
            AddGameSource(source);
            return newSource;
        }

        public T Instantiate<T>(T source, Vector3 position, Quaternion rotation, Vector3 scale) where T : GameSource, new()
        {
            T newSource = new T() { Manager = this, SID = NewSID(), transform = new Transform(position, rotation, scale) };
            AddGameSource(source);
            return newSource;
        }
        #endregion

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
    }
}
