using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class MainMenu : MonoBehaviour
{
    #region NewGameWindow
    public bool[] flag = { false, false, false, false };
    public Button goToGame;
    public Dropdown heroType;
    public Dropdown numberOfPlayers;
    public Dropdown gameType;
    public TMP_InputField Nickname;
    public TextMeshProUGUI Info;
    public static string nickNameValue;
    public static string heroValue;
    public void CheckField()
    {
        if (Nickname.text != "") flag[0] = false;
        else flag[0] = true;
    }
    public void HeroTypeIndex_changed(int index)
    {
        try
        {
            if (index == 0)
            {
                Info.text = "Not all fields are correct filled";
                flag[1] = true;
            }
            else
            {
                Info.text = "";
                flag[1] = false; 
                Assets.hero_type kek = (Assets.hero_type)index;
                heroValue = kek.ToString();
                nickNameValue = Nickname.text;
            }
        }
        catch (NullReferenceException) { };
        Debug.Log("flaga hero" + flag[1]);
    }
    public void OnlineOflineBoxIndex_changed(int index)
    {
        if (index == 0)
        {
            try
            {
                Info.text = "Not all fields are correct filled";
                flag[2] = true;
            }
            catch (NullReferenceException) { };
        }
        else
            try
            {
                Info.text = ""; flag[2] = false;
            }
            catch (NullReferenceException) { };
        Debug.Log("flaga online" + flag[2]);
    }
    public void NumberOfPlayersIndex_changed(int index)
    {

        if (index == 0)
        {
            try
            {
                Info.text = "Not all fields are correct filled";
                flag[3] = true;
            }
            catch (NullReferenceException) { };
        }
        else
            try
            {
                Info.text = ""; flag[3] = false;
            }
            catch (NullReferenceException) { };
        Debug.Log("flaga gracze" + flag[3]);

        Debug.Log("flaga field" + flag[0]);
    }
    public void HeroTypeBox()
    {
        string[] heroNames = Enum.GetNames(typeof(Assets.hero_type));
        string[] hero = { "Choose Hero" };
        string[] kek = new string[heroNames.Length + hero.Length];
        kek[0] = hero[0];
        for(int i=1;i<heroNames.Length+1;i++)
        {
            kek[i] = heroNames[i - 1];
        }

        List<string> names = new List<string>(kek);
        try
        {
            heroType.AddOptions(names);
        }
        catch (NullReferenceException) { }; 
    }
    public void Check()
    {
        try
        {
            CheckField();
            bool temp_flag = true;
            for (int i = 0; i < 4; i++)
            {
                if (flag[i] == true)
                {
                    temp_flag = false;
                    break;
                }
            }
            goToGame.gameObject.SetActive(temp_flag);
        }
        catch (NullReferenceException) { };
    }
    public void OnlineOfflineBox()
    {
        string[] gameType = {"Choose hero type" ,"Online", "Offline" };
        List<string> names = new List<string>(gameType);
        try
        {
            this.gameType.AddOptions(names);
        }
        catch (NullReferenceException) { };
    }
    public void NumberOfPlayersBox()
    {
        string[] playerNumber = {"Choose number of players", "2", "3" , "4", "5" , "6"};
        List<string> names = new List<string>(playerNumber);
        try
        {
            this.numberOfPlayers.AddOptions(names);
        }
        catch (NullReferenceException) { };
    }
    public void Start()
    {
        HeroTypeBox();
        OnlineOfflineBox();
        NumberOfPlayersBox();
    }
    public void Update()
    {
        Check();
    }
    public static string getNickname()
    {
        return nickNameValue;
    }
    public static string getHero()
    {
        return heroValue;
    }
    #endregion
    #region MainMenuWindow
    // Function to change view of scenes
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Function to close application
    public void Exit()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }
    #endregion
}

