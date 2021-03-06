using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jojo
{
    public static class Events
    {
        private static List<Event> EventList = new List<Event>();

        #region Events
        public static Event OnDraftStartedEvent = new OnDraftStartedEvent(1);
        public static Event OnDraftPhaseChangedEvent = new OnDraftPhaseChangedEvent(2);
        public static Event OnGameStartedEvent = new OnGameStartedEvent(2);
        #endregion

        public static int Count
        {
            get
            {
                return EventList.Count;
            }
        }

        public static void RegisterEvent(Event eventInstance)
        {
            EventList.Add(eventInstance);
        }

        public static Event GetEvent(int id)
        {
            return EventList[id];
        }
    }
}
