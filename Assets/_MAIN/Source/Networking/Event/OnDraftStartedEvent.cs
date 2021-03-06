using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jojo
{
    public class OnDraftStartedEvent : Event
    {
        public OnDraftStartedEvent(byte ID) : base(ID)
        {

        }

        public void OnEvent()
        {
            Draft.StartDraft();
        }
    }
}
