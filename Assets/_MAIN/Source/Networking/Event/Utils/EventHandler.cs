using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

namespace Jojo
{
    public class EventHandler : MonoBehaviour
    {
        public void OnEvent(byte code, object content, int senderId)
        {
            for (int i = 0; i < Events.Count; ++i)
            {
                if (code == Events.GetEvent(i).GetID())
                {
                    var _event = Events.GetEvent(i);
                    var type = _event.GetType();

                    type.GetMethod("OnEvent").Invoke(_event, (object[])content);
                    return;
                }
            }
        }

        public void OnEnable()
        {
            PhotonNetwork.OnEventCall += OnEvent;
        }

        public void OnDisable()
        {
            PhotonNetwork.OnEventCall -= OnEvent;
        }
    }
}
