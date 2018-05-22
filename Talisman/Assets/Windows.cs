using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Windows : MonoBehaviour {

    public Sprite KAPLAN;
    public Sprite ELF;
    public Sprite ZLODZIEJ;
    public Sprite ZABOJCA;
    public Sprite GHUL;
    public Sprite TROLL;
    public Sprite CZARNOKSIEZNIK;
    public Sprite WROZKA;
    public Sprite CZAROWNICA;
    public Sprite WOJOWNIK;
    public Sprite KRASNOLUD;
    public Sprite DRUID;
    public Sprite MINSTREL;

    public GameObject _heroPanel1;
    public GameObject _heroPanel2;
    public GameObject _heroPanel3;
    public GameObject _heroPanel4;
    public GameObject _heroPanel5;
    public GameObject _heroPanel6;

    public Button _hero1button;
    public Button _hero2button;
    public Button _hero3button;
    public Button _hero4button;
    public Button _hero5button;
    public Button _hero6button;

    public Sprite HeroPort(Hero i)
    {
        switch(i.name)
        {
            case "Kaplan":
                return KAPLAN;
            case "Elf":
                return ELF;
            case "Zlodziej":
                return ZLODZIEJ;
            case "Zabojca":
                return ZABOJCA;
            case "Ghul":
                return GHUL;
            case "Troll":
                return TROLL;
            case "Czarnoksieznik":
                return CZARNOKSIEZNIK;
            case "Wrozka":
                return WROZKA;
            case "Czarownica":
                return CZAROWNICA;
            case "Wojownik":
                return WOJOWNIK;
            case "Krasnolud":
                return KRASNOLUD;
            case "Druid":
                return DRUID;
            case "Minstrel":
                return MINSTREL;
        }
        return KAPLAN;
    }
    public void SpawnPlayers(int x)
    {
        switch (x)
        {
            case 1:
                _heroPanel1.SetActive(true);
                break;
            case 2:
                _heroPanel1.SetActive(true);
                _heroPanel2.SetActive(true);
                break;
            case 3:
                _heroPanel1.SetActive(true);
                _heroPanel2.SetActive(true);
                _heroPanel3.SetActive(true);
                break;
            case 4:
                _heroPanel1.SetActive(true);
                _heroPanel2.SetActive(true);
                _heroPanel3.SetActive(true);
                _heroPanel4.SetActive(true);
                break;
            case 5:
                _heroPanel1.SetActive(true);
                _heroPanel2.SetActive(true);
                _heroPanel3.SetActive(true);
                _heroPanel4.SetActive(true);
                _heroPanel5.SetActive(true);
                break;
            case 6:
                _heroPanel1.SetActive(true);
                _heroPanel2.SetActive(true);
                _heroPanel3.SetActive(true);
                _heroPanel4.SetActive(true);
                _heroPanel5.SetActive(true);
                _heroPanel6.SetActive(true);
                break;
        }
    }
    public void wstawPortret(Player[] playerArray)
    {
        SpawnPlayers(playerArray.Length);
        switch (playerArray.Length)
        {
            case 1:
                _hero1button.GetComponent<Image>().sprite = HeroPort(playerArray[0].hero);
                break;
            case 2:
                _hero1button.GetComponent<Image>().sprite = HeroPort(playerArray[0].hero);
                _hero2button.GetComponent<Image>().sprite = HeroPort(playerArray[1].hero);
                break;
            case 3:
                _hero1button.GetComponent<Image>().sprite = HeroPort(playerArray[0].hero);
                _hero2button.GetComponent<Image>().sprite = HeroPort(playerArray[1].hero);
                _hero3button.GetComponent<Image>().sprite = HeroPort(playerArray[2].hero);
                break;
            case 4:
                _hero1button.GetComponent<Image>().sprite = HeroPort(playerArray[0].hero);
                _hero2button.GetComponent<Image>().sprite = HeroPort(playerArray[1].hero);
                _hero3button.GetComponent<Image>().sprite = HeroPort(playerArray[2].hero);
                _hero4button.GetComponent<Image>().sprite = HeroPort(playerArray[3].hero);
                break;
            case 5:
                _hero1button.GetComponent<Image>().sprite = HeroPort(playerArray[0].hero);
                _hero2button.GetComponent<Image>().sprite = HeroPort(playerArray[1].hero);
                _hero3button.GetComponent<Image>().sprite = HeroPort(playerArray[2].hero);
                _hero4button.GetComponent<Image>().sprite = HeroPort(playerArray[3].hero);
                _hero5button.GetComponent<Image>().sprite = HeroPort(playerArray[4].hero);
                break;
            case 6:
                _hero1button.GetComponent<Image>().sprite = HeroPort(playerArray[0].hero);
                _hero2button.GetComponent<Image>().sprite = HeroPort(playerArray[1].hero);
                _hero3button.GetComponent<Image>().sprite = HeroPort(playerArray[2].hero);
                _hero4button.GetComponent<Image>().sprite = HeroPort(playerArray[3].hero);
                _hero5button.GetComponent<Image>().sprite = HeroPort(playerArray[4].hero);
                _hero6button.GetComponent<Image>().sprite = HeroPort(playerArray[5].hero);
                break;
        }
    }
}
