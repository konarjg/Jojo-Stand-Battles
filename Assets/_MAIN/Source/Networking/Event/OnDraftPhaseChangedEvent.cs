using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jojo
{
    public class OnDraftPhaseChangedEvent : Event
    {
        public OnDraftPhaseChangedEvent(byte ID) : base(ID)
        {

        }

        public void OnEvent()
        {
            Draft.NextPhase();
        }
    }
}
