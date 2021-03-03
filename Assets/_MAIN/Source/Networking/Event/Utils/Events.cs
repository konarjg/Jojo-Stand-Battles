using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    private static List<Event> EventList = new List<Event>();
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

    public static int GetID(Type type)
    {
        for (int i = 0; i < EventList.Count; ++i)
        {
            if (EventList[i].GetType() == type)
                return i;
        }

        return -1;
    }
}
