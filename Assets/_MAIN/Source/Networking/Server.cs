using Jojo;
using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using EventHandler = Jojo.EventHandler;
using Random = UnityEngine.Random;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView), typeof(EventHandler))]
public class Server : MonoBehaviour
{
    private byte MaxPlayers = 2;
    private Menu Menu;

    private float FPS = 0f;
    private int FrameCount = 0;
    private float DT = 0f;
    private float UpdateRate = 4f;

    private bool Searching = false;
    private float SearchTime = 0f;

    public delegate void UpdateSearchTime(float time);
    public static UpdateSearchTime OnSearchTimeUpdatedEvent;
    public delegate void GameFound();
    public static GameFound OnGameFoundEvent;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Menu = GetComponent<Menu>();
        Menu.Init(this);
        PhotonNetwork.ConnectUsingSettings("1.0");
        PhotonNetwork.player.NickName = Random.Range(0, int.MaxValue).ToString();
    }

    private void OnGUI()
    {
        var display = string.Format("{0}FPS {1}ms", Mathf.RoundToInt(FPS), Mathf.RoundToInt(PhotonNetwork.GetPing()));

        GUI.Label(new Rect(10, 10, 200, 50), display);
    }

    private void CountFPS()
    {
        FrameCount++;
        DT += Time.deltaTime;

        if (DT > 1.0f / UpdateRate)
        {
            FPS = FrameCount / DT;
            FrameCount = 0;
            DT -= 1.0f / UpdateRate;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Players.DebugList();

        CountFPS();

        if (Searching)
        {
            SearchTime += Time.deltaTime;
            OnSearchTimeUpdatedEvent?.Invoke(SearchTime);

            if (Players.Count == MaxPlayers)
            {
                OnGameFoundEvent?.Invoke();
                Searching = false;
            }
        }
    }

    public void QuickMatch()
    {
        var table = new RoomOptions();
        PhotonNetwork.JoinRandomRoom(table.CustomRoomProperties, MaxPlayers);
        Searching = true;
        SearchTime = 0f;
    }

    public void RankedMatch()
    {

    }

    public void OnPhotonRandomJoinFailed()
    {
        var options = new RoomOptions();
        options.MaxPlayers = MaxPlayers;
        PhotonNetwork.CreateRoom("", options, null);
    }

    public void OnJoinedLobby()
    {
        Menu.Enable();
    }

    public void OnJoinedRoom()
    {
        photonView.RPC("PlayerConnectedRPC", PhotonTargets.AllBufferedViaServer, PhotonNetwork.player);
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.RemoveRPCs(player);
            photonView.RPC("PlayerDisconnectedRPC", PhotonTargets.AllViaServer, player);
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
