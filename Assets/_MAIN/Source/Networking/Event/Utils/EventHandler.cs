using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventHandler : MonoBehaviour, IOnEventCallback
{
    public void OnEvent(EventData data)
    {
        for (int i = 0; i < Events.Count; ++i)
        {
            if (data.Code == i)
            {
                Events.GetEvent(i).OnEvent();
                return;
            }
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
