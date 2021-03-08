using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Players
{
    private static List<Player> PlayerList = new List<Player>();

    public static int Count
    {
        get
        {
            return PlayerList.Count;
        }
    }

    public static void Add(Player player)
    {
        PlayerList.Add(player);
    }

    public static void Remove(Player player)
    {
        PlayerList.Remove(player);
    }

    public static Player Get(string NickName)
    {
        return PlayerList.Find(p => p.NickName == NickName);
    }

    public static Player Get(int index)
    {
        return PlayerList[index];
    }

    public static void DebugList()
    {
        for (int i = 0; i < PlayerList.Count; ++i)
            Debug.LogError(PlayerList[i].NickName + " " + PlayerList[i].PickedStand);
    }
}
