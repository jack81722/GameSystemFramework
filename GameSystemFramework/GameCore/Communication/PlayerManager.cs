using ExCollection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore.Communication
{
    public class PlayerManager
    {
        private int serialPID;

        #region Waiting list
        public int WaitingCount { get { return waitingQueue.Count; } }
        private List<Player> waitingQueue;
        #endregion

        #region Playing list
        public int PlayingCount { get { return playingList.Count; } }
        private KeyedList<int, Player> playingList;
        #endregion

        private List<Player> leavingList;

        #region Constructor
        public PlayerManager()
        {
            serialPID = 0;
            waitingQueue = new List<Player>();
            playingList = new KeyedList<int, Player>();
            leavingList = new List<Player>();
        }
        #endregion

        public int Join()
        {
            Player player = new Player();
            player.PID = serialPID++;
            waitingQueue.Add(player);
            return player.PID;
        }

        public int Join(object data)
        {
            Player player = new Player();
            player.PID = serialPID++;
            player.CustomData = data;
            waitingQueue.Add(player);
            return player.PID;
        }

        public void Rejoin(int pid)
        {
            // rejoin player was been disconnected accidently ...
        }

        public void Leave(int pid)
        {
            // leave player which event is player or not ...
        }

        public bool Accept(out Player player)
        {
            if (waitingQueue.Count <= 0)
            {
                player = null;
                return false;
            }
            player = waitingQueue[0];
            playingList.Add(player);
            return true;
        }

        public void Update()
        {
            // NetMgr.PollEvent()...
        }
        
        public void Broadcast(object msg)
        {
            for(int i = 0; i < playingList.Count; i++)
            {
                playingList[i].Notify(msg);
            }
        }

        
    }
}
