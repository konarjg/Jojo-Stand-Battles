using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameObject[] MainMenu;
    private GameObject[] MatchmakingMenu;
    private GameObject[] SearchingMenu;
    private GameObject[] AcceptMenu;

    private static Server Server;

    private void OnEnable()
    {
        Server.OnSearchTimeUpdatedEvent += UpdateSearchTime;
    }

    private void OnDisable()
    {
        Server.OnSearchTimeUpdatedEvent -= UpdateSearchTime;
    }

    private void DisplayMenu(string name)
    {
        var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < fields.Length; ++i)
        {
            var value = (GameObject[])fields[i].GetValue(this);

            if (fields[i].Name == name)
            {
                for (int j = 0; j < value.Length; ++j)
                    value[j].SetActive(true);
            }  
            else
            {
                for (int j = 0; j < value.Length; ++j)
                    value[j].SetActive(false);
            }
        }
    }

    public void Init(Server server)
    {
        Server = server;

        var main = GameObject.Find("Main").transform;
        MainMenu = new GameObject[main.childCount];

        for (int i = 0; i < main.childCount; ++i)
        {
            MainMenu[i] = main.GetChild(i).gameObject;
            MainMenu[i].SetActive(false);
        }

        var matchmaking = GameObject.Find("Matchmaking").transform;
        MatchmakingMenu = new GameObject[matchmaking.childCount];

        for (int i = 0; i < matchmaking.childCount; ++i)
        {
            MatchmakingMenu[i] = matchmaking.GetChild(i).gameObject;
            MatchmakingMenu[i].SetActive(false);
        }

        var searching = GameObject.Find("Match Searching").transform;
        SearchingMenu = new GameObject[searching.childCount];

        for (int i = 0; i < searching.childCount; ++i)
        {
            SearchingMenu[i] = searching.GetChild(i).gameObject;
            SearchingMenu[i].SetActive(false);
        }

        var accept = GameObject.Find("Accept").transform;
        AcceptMenu = new GameObject[accept.childCount];

        for (int i = 0; i < accept.childCount; ++i)
        {
            AcceptMenu[i] = accept.GetChild(i).gameObject;
            AcceptMenu[i].SetActive(false);
        }
    }

    public void Enable()
    {
        for (int i = 0; i < MainMenu.Length; ++i)
           MainMenu[i].SetActive(true);
    }

    #region Interactions

    public void Play()
    {
        DisplayMenu("MatchmakingMenu");
    }

    public void Settings()
    {

    }

    public void Exit()
    {
        
    }

    public void Back()
    {
        DisplayMenu("MainMenu");
    }

    public void QuickMatch()
    {
        Server.QuickMatch();
        DisplayMenu("SearchingMenu");
    }

    public void RankedMatch()
    {
        Server.RankedMatch();
    }

    #endregion

    #region Event Listeners

    public void UpdateSearchTime(float time)
    {
        var text = SearchingMenu[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);

        string minutesString = "" + (minutes < 10 ? "0" + minutes : "" + minutes);
        string secondsString = "" + (seconds < 10 ? "0" + seconds : "" + seconds);

        text.text = string.Format("{0}:{1}", minutesString, secondsString);
    }

    public void GameFound()
    {
        DisplayMenu("AcceptMenu");
    }

    #endregion
}
