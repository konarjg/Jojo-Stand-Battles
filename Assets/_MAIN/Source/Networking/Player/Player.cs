using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string NickName { get; set; }
    public int PickedStand { get; set; }

    public Player(PhotonPlayer player)
    {
        NickName = player.NickName;
    }

    public Player(string nick)
    {
        NickName = nick;
        PickedStand = -1;
    }
}
