using Jojo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public enum DraftPhase
{
    BANNING = 30,
    PICKING = 45
}

public class Draft : MonoBehaviour
{
    private static int CurrentPlayer;
    private static DraftPhase CurrentPhase;
    private static float TimeRemaining;

    public void Pick(int id)
    {
        Players.Get(CurrentPlayer).PickedStand = id;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isReading)
        {
            TimeRemaining = (float)stream.ReceiveNext();
        }
        else
        {
            stream.SendNext(TimeRemaining);
        }
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            if (TimeRemaining > 0)
                TimeRemaining -= Time.deltaTime;
            else
            {
                
            }
        }    
    }

    public static void StartDraft()
    {
        CurrentPhase = DraftPhase.BANNING;
        CurrentPlayer = 0;
        TimeRemaining = (float)CurrentPhase;
    }

    public static void NextPhase()
    {
        if (CurrentPhase == DraftPhase.PICKING)
            Events.OnGameStartedEvent.Call();
        else
        {
            CurrentPhase = DraftPhase.PICKING;
            TimeRemaining = (float)CurrentPhase;
            CurrentPlayer = 0;
        }
    }

    public static void NextTurn()
    {
        if (CurrentPlayer == 0)
        {
            TimeRemaining = (float)CurrentPhase;
            CurrentPlayer = 1;
        }
        else
            Events.OnDraftPhaseChangedEvent.Call();
    }
}
