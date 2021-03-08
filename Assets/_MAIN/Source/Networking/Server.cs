using Jojo;
using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;
using Hashtable = ExitGames.Client.Photon.Hashtable;

[RequireComponent(typeof(PhotonView), typeof(EventHandler))]
public class Server : MonoBehaviour
{
    [SerializeField]
    [Range(2, 4)]
    private int MaxPlayers = 2;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        PhotonNetwork.ConnectUsingSettings("1.0");
        PhotonNetwork.player.NickName = Random.Range(0, int.MaxValue).ToString();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 50), PhotonNetwork.connectionStateDetailed.ToString());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Players.DebugList();
    }

    public void OnJoinedLobby()
    {
        var table = new RoomOptions();
        PhotonNetwork.JoinRandomRoom(table.CustomRoomProperties, 2);
    }

    public void OnPhotonRandomJoinFailed()
    {
        var options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.CreateRoom("", options, null);
    }

    public void OnCreatedRoom()
    {
        if (PhotonNetwork.isMasterClient)
            photonView.RPC("PlayerConnectedRPC", PhotonTargets.AllBuffered, PhotonNetwork.player);
    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        if (PhotonNetwork.isMasterClient)
            photonView.RPC("PlayerConnectedRPC", PhotonTargets.AllBuffered, player);
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.RemoveRPCs(player);
            photonView.RPC("PlayerDisconnectedRPC", PhotonTargets.All, player);
        }
    }

    [PunRPC]
    public void PlayerConnectedRPC(PhotonPlayer player)
    {
        var p = new Player(player.NickName);
        Players.Add(p);
    }

    [PunRPC]
    public void PlayerDisconnectedRPC(PhotonPlayer player)
    {
        var p = Players.Get(player.NickName);
        Players.Remove(p);
    }
}
