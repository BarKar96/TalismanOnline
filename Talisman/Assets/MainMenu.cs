﻿using System.Collections;
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
    public bool[] flag = { true, true, true, true };
    public bool flag2 = false;
    public bool flag3 = false;
    public Button goToGame;
    public Dropdown[] heroType;
    public Dropdown numberOfPlayers;
    public Dropdown gameType;
    public TMP_InputField[] Nickname;
    public TextMeshProUGUI[] Players;
    public TextMeshProUGUI Info;
    public static string[] nickNameValue;
    public static string[] heroValue;
    public static int onoff = 0;
    public static int playerscount = 0;
    public MainMenu()
    {
        heroValue = new string[6];
        nickNameValue = new string[6];
        heroType = new Dropdown[6];
        Nickname = new TMP_InputField[6];
    }
    private void CheckNickName()
    {
        if (playerscount == 0) return;
        if (nickNameValue == null) return;
        for (int i = 0; i < playerscount; i++)
        {
            for (int j = i + 1; j < playerscount; j++)
            {
                if (nickNameValue[i] == nickNameValue[j])
                {
                    flag3 = true;
                    return;
                }
            }
            flag3 = false;
        }
    }
    private void CheckField()
    {
        for (int i = 0; i < playerscount; i++)
        {
            if (Nickname[i].text != "") flag[0] = false;
            else
            {
                flag[0] = true;
                break;
            }
            if (heroType[i].value == 0)
            {
                flag[1] = true;
                break;
            }
            else flag[1] = false;
            if (flag3 == true) flag[0] = true;
        }
    }
    public void HeroTypeIndex_changed(int index)
    {
        try
        {
            if (index == 0)
            {
                Info.text = "";
                flag[1] = true;
            }
            else
            {
                Info.text = "";
                flag[1] = false;
            }
        }
        catch (NullReferenceException) { };
    }
    public void NumberOfPlayersIndex_changed(int index)
    {
        TurnOffAll();
        playerscount = index + 1;
        if (index == 0)
        {
            try
            {
                Info.text = "Nie możesz grać jako jeden gracz - jeśli nie masz przyjaciół zagraj sam ze sobą.";
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
    }
    public void OnlineOflineBoxIndex_changed(int index)
    {
        if (index == 1)
        {
            onoff = 1;
            TurnOffAll();
            playerscount = 1;

        }
        if (index == 2) onoff = 2;
        if (index == 0)
        {
            onoff = 0;
            try
            {
                Info.text = "Nie wszystkie pola są wypełnione prawidłowo";
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
        //Debug.Log("flaga online" + flag[3]);
    }
    private void OnlineOfflineBox()
    {
        string[] gameType = { "Wybierz typ rozgrywki", "Online", "Offline" };
        List<string> names = new List<string>(gameType);
        try
        {
            this.gameType.AddOptions(names);
        }
        catch (NullReferenceException) { };
    }
    private void HeroTypeBox()
    {
        string[] heroNames = Enum.GetNames(typeof(Assets.hero_type));
        string[] hero = { "Wybierz bohatera" };
        string[] kek = new string[heroNames.Length + hero.Length];
        kek[0] = hero[0];
        for(int i=0;i<heroNames.Length;i++)
        {

            kek[i+1] = heroNames[i];
        }

        List<string> names = new List<string>(kek);
        try
        {
            heroType[0].AddOptions(names);
            heroType[1].AddOptions(names);
            heroType[2].AddOptions(names);
            heroType[3].AddOptions(names);
            heroType[4].AddOptions(names);
            heroType[5].AddOptions(names);
        }
        catch (Exception) { }; 
    }
    private void Check()
    {
        try
        {

            if (onoff == 0)
            {
                numberOfPlayers.gameObject.SetActive(false);
            }
            else if (onoff == 1)
            {
                flag[3] = true;
                numberOfPlayers.gameObject.SetActive(false);
                try
                {
                    CheckNickName();
                    CheckField();
                    bool temp_flag = true;
                    for (int i = 0; i < 3; i++)
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
            else if (onoff == 2)
            {
                numberOfPlayers.gameObject.SetActive(true);
                try
                {
                    CheckNickName();
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
        }
        catch (NullReferenceException) { };
        
    }
    private void NumberOfPlayersBox()
    {
        string[] playerNumber = {"Liczba graczy", "2", "3" , "4", "5" , "6"};
        List<string> names = new List<string>(playerNumber);
        try
        {
            this.numberOfPlayers.AddOptions(names);
        }
        catch (NullReferenceException) { };
    }
    public void Start()
    {
        OnlineOfflineBox();
        HeroTypeBox();
        NumberOfPlayersBox();
    }
    public void Update()
    {
        Check();
        SpawnPlayer();
        UpdateData();
    }
    private static int getOnOff()
    {
        return onoff;
    }
    private void TurnOffAll()
    {
        for (int i = 1; i < 6; i++)
        {
            Players[i].gameObject.SetActive(false);
            heroType[i].gameObject.SetActive(false);
            Nickname[i].gameObject.SetActive(false);
        }
    }
    private void UpdateData()
    {
        try
        {
            Assets.hero_type temp;
            for(int i=0;i<playerscount;i++)
            {
                if ((Assets.hero_type)heroType[i].value != 0)
                {
                    temp = (Assets.hero_type)heroType[i].value-1;
                    heroValue[i] = temp.ToString();
                }
                else heroValue[i] = null;
            }
            for(int i=0;i<playerscount;i++)
            {
                nickNameValue[i] = Nickname[i].text;
            }
        }
        catch (Exception) { };
    }
    private void SpawnPlayer()
    {
        try
        {
            for (int i = 1; i < playerscount; i++)
            {
                Players[i].gameObject.SetActive(true);
                heroType[i].gameObject.SetActive(true);
                Nickname[i].gameObject.SetActive(true);
            }
        }
        catch (IndexOutOfRangeException) { };
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

