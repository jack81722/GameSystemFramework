using ExCollection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore.Communication
{
    public class Player : IKeyable<int>
    {
        private OnceSetValue<int> pid;
        public int PID
        {
            get { return pid; }
            set { pid.Value = value; }
        }

        public int Key { get { return PID; } set { PID = value; } }

        /// <summary>
        /// Player data
        /// </summary>
        public object CustomData;

        public delegate void ReceiveHandler(object msg);
        public event ReceiveHandler OnReceive;

        public Player()
        {
            senderList = new List<ISender>();
        }

        List<ISender> senderList;
        public void RegisterSender(ISender sender)
        {
            if (!senderList.Contains(sender))
                senderList.Add(sender);
        }

        public void UnregisterSender(ISender sender)
        {
            senderList.Remove(sender);
        }

        public void Notify(object msg)
        {
            for (int i = 0; i < senderList.Count; i++)
                senderList[i].Send(msg);
        }
    }
}
