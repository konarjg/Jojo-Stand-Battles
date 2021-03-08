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
