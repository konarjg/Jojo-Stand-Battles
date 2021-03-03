using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event
{
    public int ID
    {
        get
        {
            return Events.GetID(GetType());
        }
    }

    public Event()
    {
        Events.RegisterEvent(this);
    }

    public void Call()
    {
        var options = new RaiseEventOptions();
        var sendOptions = new SendOptions();
        PhotonNetwork.RaiseEvent((byte)Convert.ChangeType(ID, typeof(byte)), null, options, sendOptions);
    }

    public abstract void OnEvent();
}
