using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jojo
{
    public abstract class Event
    {
        private byte ID;

        public Event(byte ID)
        {
            this.ID = ID;
        }

        public byte GetID()
        {
            return ID;
        }

        public void Call(params object[] data)
        {
            var options = new RaiseEventOptions();
            options.Receivers = ReceiverGroup.All;
            PhotonNetwork.RaiseEvent(ID, data, true, options);
        }
    }
}
