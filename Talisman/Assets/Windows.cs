using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Windows : MonoBehaviour {
    //Kod odpowiedzialny za informacje o bohaterze i wszystkie informacje na jego temat
    #region Avatary
    //Fragment kodu odpowiedzialny za portrety bohaterów
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
    //funkcja wstawiająca portrety
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
        UpdateStatsToText(playerArray);
    }
    //funkcja wybierająca portrety
    public Sprite HeroPort(Hero i)
    {
        switch (i.name)
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
    #endregion
    #region Resp Playerow
    // Fragment kodu odpowiedzialny za panele i nazwy
    public GameObject _heroPanel1;
    public GameObject _heroPanel2;
    public GameObject _heroPanel3;
    public GameObject _heroPanel4;
    public GameObject _heroPanel5;
    public GameObject _heroPanel6;

    public TextMeshProUGUI _heroName1;
    public TextMeshProUGUI _heroName2;
    public TextMeshProUGUI _heroName3;
    public TextMeshProUGUI _heroName4;
    public TextMeshProUGUI _heroName5;
    public TextMeshProUGUI _heroName6;
    // funkcja wyświetlająca nickname gracza
    public void heroNames(Player[] playerArray)
    {
        switch(playerArray.Length)
        {
            case 1:
                _heroName1.text = playerArray[0].name;
                break;
            case 2:
                _heroName1.text = playerArray[0].name;
                _heroName2.text = playerArray[1].name;
                break;
            case 3:
                _heroName1.text = playerArray[0].name;
                _heroName2.text = playerArray[1].name;
                _heroName3.text = playerArray[2].name;
                break;
            case 4:
                _heroName1.text = playerArray[0].name;
                _heroName2.text = playerArray[1].name;
                _heroName3.text = playerArray[2].name;
                _heroName4.text = playerArray[3].name;
                break;
            case 5:
                _heroName1.text = playerArray[0].name;
                _heroName2.text = playerArray[1].name;
                _heroName3.text = playerArray[2].name;
                _heroName4.text = playerArray[3].name;
                _heroName5.text = playerArray[4].name;
                break;
            case 6:
                _heroName1.text = playerArray[0].name;
                _heroName2.text = playerArray[1].name;
                _heroName3.text = playerArray[2].name;
                _heroName4.text = playerArray[3].name;
                _heroName5.text = playerArray[4].name;
                _heroName6.text = playerArray[5].name;
                break;
        }
        for (int i=0; i<playerArray.Length; i++)
        {
            if (i == 0)
            {
                _heroName1.text += " - czerowny";
            }
            if (i == 1)
            {
                _heroName2.text += " - zielony";
            }
            if (i == 2)
            {
                _heroName3.text += " - niebieski";
            }
            if (i == 3)
            {
                _heroName4.text += " - zolty";
            }
            if (i == 4)
            {
                _heroName5.text += " - seledynowy";
            }
            if (i == 5)
            {
                _heroName6.text += " - fioletowy";
            }
        }
    }
    //funkcja respiąca graczy
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
    #endregion 
    public Button _hero1button;
    public Button _hero2button;
    public Button _hero3button;
    public Button _hero4button;
    public Button _hero5button;
    public Button _hero6button;
    #region Statystyki Graczy
    //Fragment kodu dotyczący statystyk graczy ich pojawiania się na mapie i ich zmian
    // HP = punkty życia
    public TextMeshProUGUI _hero1HP;
    public TextMeshProUGUI _hero2HP;
    public TextMeshProUGUI _hero3HP;
    public TextMeshProUGUI _hero4HP;
    public TextMeshProUGUI _hero5HP;
    public TextMeshProUGUI _hero6HP;
    //STR = siła
    public TextMeshProUGUI _hero1STR;
    public TextMeshProUGUI _hero2STR;
    public TextMeshProUGUI _hero3STR;
    public TextMeshProUGUI _hero4STR;
    public TextMeshProUGUI _hero5STR;
    public TextMeshProUGUI _hero6STR;
    //INT = Moc
    public TextMeshProUGUI _hero1INT;
    public TextMeshProUGUI _hero2INT;
    public TextMeshProUGUI _hero3INT;
    public TextMeshProUGUI _hero4INT;
    public TextMeshProUGUI _hero5INT;
    public TextMeshProUGUI _hero6INT;
    //LOS
    public TextMeshProUGUI _hero1LOS;
    public TextMeshProUGUI _hero2LOS;
    public TextMeshProUGUI _hero3LOS;
    public TextMeshProUGUI _hero4LOS;
    public TextMeshProUGUI _hero5LOS;
    public TextMeshProUGUI _hero6LOS;
    //GOLD = zloto
    public TextMeshProUGUI _hero1GOLD;
    public TextMeshProUGUI _hero2GOLD;
    public TextMeshProUGUI _hero3GOLD;
    public TextMeshProUGUI _hero4GOLD;
    public TextMeshProUGUI _hero5GOLD;
    public TextMeshProUGUI _hero6GOLD;
    //funkcja do hp ukazująca że dany użytkownik jeśli nie ma bazowej ilości hp zamieniany jest kolor hp na czerwony
    public void changeColor(TextMeshProUGUI text, Player player)
    {
        if (player.current_health > player.total_health)
            text.color = Color.green;
        else text.color = Color.blue;
    }
    //funkcja dotycząca statystyk czyli wypisująca i aktualizująca statystyki wszystkich użytkowników na raz
    public void UpdateStatsToText(Player[] playerArray)
    {
        heroNames(playerArray);
        switch (playerArray.Length)
        {
            case 1:
                changeColor(_hero1HP, playerArray[0]);
                _hero1HP.text = (playerArray[0].current_health + playerArray[0].health_modifier).ToString();
                _hero1STR.text = (playerArray[0].strength+ playerArray[0].strength_modifier).ToString();
                _hero1INT.text = playerArray[0].hero.power.ToString();
                _hero1LOS.text = playerArray[0].hero.luck.ToString();
                _hero1GOLD.text = playerArray[0].gold.ToString();
                break;
            case 2:
                //player 1
                changeColor(_hero1HP, playerArray[0]);
                _hero1HP.text = (playerArray[0].current_health.ToString() + playerArray[0].health_modifier).ToString();
                _hero1STR.text = (playerArray[0].strength + playerArray[0].strength_modifier).ToString();
                _hero1INT.text = playerArray[0].hero.power.ToString();
                _hero1LOS.text = playerArray[0].hero.luck.ToString();
                _hero1GOLD.text = playerArray[0].gold.ToString();
                //player 2
                changeColor(_hero2HP, playerArray[1]);
                _hero2HP.text = (playerArray[1].current_health + playerArray[1].health_modifier).ToString();
                _hero2STR.text = (playerArray[1].strength + playerArray[1].strength_modifier).ToString();
                _hero2INT.text = playerArray[1].hero.power.ToString();
                _hero2LOS.text = playerArray[1].hero.luck.ToString();
                _hero2GOLD.text = playerArray[1].gold.ToString();
                break;
            case 3:
                changeColor(_hero1HP, playerArray[0]);
                changeColor(_hero2HP, playerArray[1]);
                changeColor(_hero3HP, playerArray[2]);
                //player 1
                _hero1HP.text = (playerArray[0].current_health + playerArray[0].health_modifier).ToString();
                _hero1STR.text = (playerArray[0].strength + playerArray[0].strength_modifier).ToString();
                _hero1INT.text = playerArray[0].hero.power.ToString();
                _hero1LOS.text = playerArray[0].hero.luck.ToString();
                _hero1GOLD.text = playerArray[0].gold.ToString();
                //player 2
                _hero2HP.text = (playerArray[1].current_health + playerArray[1].health_modifier).ToString();
                _hero2STR.text = (playerArray[1].strength + playerArray[1].strength_modifier).ToString();
                _hero2INT.text = playerArray[1].hero.power.ToString();
                _hero2LOS.text = playerArray[1].hero.luck.ToString();
                _hero2GOLD.text = playerArray[1].gold.ToString();
                //player 3
                _hero3HP.text = (playerArray[2].current_health + playerArray[2].health_modifier).ToString();
                _hero3STR.text = (playerArray[2].strength + playerArray[2].strength_modifier).ToString();
                _hero3INT.text = playerArray[2].hero.power.ToString();
                _hero3LOS.text = playerArray[2].hero.luck.ToString();
                _hero3GOLD.text = playerArray[2].gold.ToString();
                break;
            case 4:

                changeColor(_hero1HP, playerArray[0]);
                changeColor(_hero2HP, playerArray[1]);
                changeColor(_hero3HP, playerArray[2]);
                changeColor(_hero4HP, playerArray[3]);
                //player 1
                _hero1HP.text = (playerArray[0].current_health + playerArray[0].health_modifier).ToString();
                _hero1STR.text = (playerArray[0].strength + playerArray[0].strength_modifier).ToString();
                _hero1INT.text = playerArray[0].hero.power.ToString();
                _hero1LOS.text = playerArray[0].hero.luck.ToString();
                _hero1GOLD.text = playerArray[0].gold.ToString();
                //player 2
                _hero2HP.text = (playerArray[1].current_health + playerArray[1].health_modifier).ToString();
                _hero2STR.text = (playerArray[1].strength + playerArray[1].strength_modifier).ToString();
                _hero2INT.text = playerArray[1].hero.power.ToString();
                _hero2LOS.text = playerArray[1].hero.luck.ToString();
                _hero2GOLD.text = playerArray[1].gold.ToString();
                //player 3
                _hero3HP.text = (playerArray[2].current_health+ playerArray[2].health_modifier).ToString();
                _hero3STR.text = (playerArray[2].strength + playerArray[2].strength_modifier).ToString();
                _hero3INT.text = playerArray[2].hero.power.ToString();
                _hero3LOS.text = playerArray[2].hero.luck.ToString();
                _hero3GOLD.text = playerArray[2].gold.ToString();
                //player 4
                _hero4HP.text = (playerArray[3].current_health + playerArray[3].health_modifier).ToString();
                _hero4STR.text = (playerArray[3].strength + playerArray[3].strength_modifier).ToString();
                _hero4INT.text = playerArray[3].hero.power.ToString();
                _hero4LOS.text = playerArray[3].hero.luck.ToString();
                _hero4GOLD.text = playerArray[3].gold.ToString();
                break;
            case 5:
                changeColor(_hero1HP, playerArray[0]);
                changeColor(_hero2HP, playerArray[1]);
                changeColor(_hero3HP, playerArray[2]);
                changeColor(_hero4HP, playerArray[3]);
                changeColor(_hero5HP, playerArray[4]);
                //Player 1
                _hero1HP.text = (playerArray[0].current_health + playerArray[0].health_modifier).ToString();
                _hero1STR.text = (playerArray[0].strength + playerArray[0].strength_modifier).ToString();
                _hero1INT.text = playerArray[0].hero.power.ToString();
                _hero1LOS.text = playerArray[0].hero.luck.ToString();
                _hero1GOLD.text = playerArray[0].gold.ToString();
                //player 2
                _hero2HP.text = (playerArray[1].current_health + playerArray[1].health_modifier).ToString();
                _hero2STR.text = (playerArray[1].strength + playerArray[1].strength_modifier).ToString();
                _hero2INT.text = playerArray[1].hero.power.ToString();
                _hero2LOS.text = playerArray[1].hero.luck.ToString();
                _hero2GOLD.text = playerArray[1].gold.ToString();
                //player 3
                _hero3HP.text = (playerArray[2].current_health + playerArray[2].health_modifier).ToString();
                _hero3STR.text = (playerArray[2].strength + playerArray[2].strength_modifier).ToString();
                _hero3INT.text = playerArray[2].hero.power.ToString();
                _hero3LOS.text = playerArray[2].hero.luck.ToString();
                _hero3GOLD.text = playerArray[2].gold.ToString();
                //player 4
                _hero4HP.text = (playerArray[3].current_health + playerArray[3].health_modifier).ToString();
                _hero4STR.text = (playerArray[3].strength + playerArray[3].strength_modifier).ToString();
                _hero4INT.text = playerArray[3].hero.power.ToString();
                _hero4LOS.text = playerArray[3].hero.luck.ToString();
                _hero4GOLD.text = playerArray[3].gold.ToString();
                //player 5
                _hero5HP.text = (playerArray[4].current_health + playerArray[4].health_modifier).ToString();
                _hero5STR.text = (playerArray[4].strength + playerArray[4].strength_modifier).ToString();
                _hero5INT.text = playerArray[4].hero.power.ToString();
                _hero5LOS.text = playerArray[4].hero.luck.ToString();
                _hero5GOLD.text = playerArray[4].gold.ToString();
                break;
            case 6:
                changeColor(_hero1HP, playerArray[0]);
                changeColor(_hero2HP, playerArray[1]);
                changeColor(_hero3HP, playerArray[2]);
                changeColor(_hero4HP, playerArray[3]);
                changeColor(_hero5HP, playerArray[4]);
                changeColor(_hero6HP, playerArray[5]);
                //player 1
                _hero1HP.text = (playerArray[0].current_health + playerArray[0].health_modifier).ToString();
                _hero1STR.text = (playerArray[0].strength + playerArray[0].strength_modifier).ToString();
                _hero1INT.text = playerArray[0].hero.power.ToString();
                _hero1LOS.text = playerArray[0].hero.luck.ToString();
                _hero1GOLD.text = playerArray[0].gold.ToString();
                //player 2
                _hero2HP.text = (playerArray[1].current_health + playerArray[1].health_modifier).ToString();
                _hero2STR.text = (playerArray[1].strength + playerArray[1].strength_modifier).ToString();
                _hero2INT.text = playerArray[1].hero.power.ToString();
                _hero2LOS.text = playerArray[1].hero.luck.ToString();
                _hero2GOLD.text = playerArray[1].gold.ToString();
                //player 3
                _hero3HP.text = (playerArray[2].current_health + playerArray[2].health_modifier).ToString();
                _hero3STR.text = (playerArray[2].strength + playerArray[2].strength_modifier).ToString();
                _hero3INT.text = playerArray[2].hero.power.ToString();
                _hero3LOS.text = playerArray[2].hero.luck.ToString();
                _hero3GOLD.text = playerArray[2].gold.ToString();
                //player 4
                _hero4HP.text = (playerArray[3].current_health + playerArray[3].health_modifier).ToString();
                _hero4STR.text = (playerArray[3].strength + playerArray[3].strength_modifier).ToString();
                _hero4INT.text = playerArray[3].hero.power.ToString();
                _hero4LOS.text = playerArray[3].hero.luck.ToString();
                _hero4GOLD.text = playerArray[3].gold.ToString();
                //player 5
                _hero5HP.text = (playerArray[4].current_health + playerArray[4].health_modifier).ToString();
                _hero5STR.text = (playerArray[4].strength + playerArray[4].strength_modifier).ToString();
                _hero5INT.text = playerArray[4].hero.power.ToString();
                _hero5LOS.text = playerArray[4].hero.luck.ToString();
                _hero5GOLD.text = playerArray[4].gold.ToString();
                //player 6
                _hero6HP.text = (playerArray[5].current_health + playerArray[5].health_modifier).ToString();
                _hero6STR.text = (playerArray[5].strength + playerArray[5].strength_modifier).ToString();
                _hero6INT.text = playerArray[5].hero.power.ToString();
                _hero6LOS.text = playerArray[5].hero.luck.ToString();
                _hero6GOLD.text = playerArray[5].gold.ToString();
                break;
        }
    }
    
    #endregion
    #region Kogo Tura
    //Fragment kodu dotyczący wskazania który gracz się teraz porusza
    public GameObject Arrow1;
    public GameObject Arrow2;
    public GameObject Arrow3;
    public GameObject Arrow4;
    public GameObject Arrow5;
    public GameObject Arrow6;
    //funkcja ustawiająca odpowiedni kursor
    public void setCursor(int current)
    {
        Arrow1.SetActive(false);
        Arrow2.SetActive(false);
        Arrow3.SetActive(false);
        Arrow4.SetActive(false);
        Arrow5.SetActive(false);
        Arrow6.SetActive(false);
        switch (current)
        {
            case 0:
                Arrow1.SetActive(true);
                break;
            case 1:
                Arrow2.SetActive(true);
                break;
            case 2:
                Arrow3.SetActive(true);
                break;
            case 3:
                Arrow4.SetActive(true);
                break;
            case 4:
                Arrow5.SetActive(true);
                break;
            case 5:
                Arrow6.SetActive(true);
                break;
        }
    }
    #endregion
    /*
    #region Ekwipunek
    private TalismanBoardScript TBS;
    //Przyciski poszczególnych bohaterów
    public Button _heroItems_1;
    public Button _heroItems_2;
    public Button _heroItems_3;
    public Button _heroItems_4;
    public Button _heroItems_5;
    public Button _heroItems_6;
    public GameObject subPanel_Items;
    //Fragment kodu dotyczący ekwipunku i wyświetlania jego
    //funkcja definiująca którego gracza ekwipunek wyświetlić
    public void PlayerItems()        
    {
        subPanel_Items.SetActive(true);
        string target = EventSystem.current.currentSelectedGameObject.name;
        switch (target)
        {
                case "ButtonCards1":
                    CardDrawer.spawnPlayerItems(TBS.playerArray[0], "Panel Ekwipunku" + TBS.playerArray[0].name);
                    break;
                case "ButtonCards2":
                    CardDrawer.spawnPlayerItems(TBS.playerArray[1], "Panel Ekwipunku" + TBS.playerArray[1].name);
                    break;
                case "ButtonCards3":
                    CardDrawer.spawnPlayerItems(TBS.playerArray[2], "Panel Ekwipunku" + TBS.playerArray[2].name);
                    break;
                case "ButtonCards4":
                    CardDrawer.spawnPlayerItems(TBS.playerArray[3], "Panel Ekwipunku" + TBS.playerArray[3].name);
                    break;
                case "ButtonCards5":
                    CardDrawer.spawnPlayerItems(TBS.playerArray[4], "Panel Ekwipunku" + TBS.playerArray[4].name);
                    break;
                case "ButtonCards6":
                    CardDrawer.spawnPlayerItems(TBS.playerArray[5], "Panel Ekwipunku" + TBS.playerArray[5].name);
                    break;
            }
        
        //CardDrawer.spawnPlayerItems(playerArray[0], playerArray[0].name);
    }
    #endregion   
    */
    #region ScrollView
    public TextMeshProUGUI scrollcontetnt;
    private string texts;

    public void addToHistory(string x)
    {
        Debug.Log("Logging message");
        texts += x;
        scrollcontetnt.text = texts;
    }
    public void Start()
    {
        scrollcontetnt = gameObject.AddComponent<TextMeshProUGUI>();
        //addToHistory("Nanna Bryndís Hilmarsdóttir (ur. 6 maja 1989 w Garður) – islandzka muzyk, piosenkarka i gitarzystka zespołu Of Monsters and Men. Spis treści 1   Biografia2   Of Monsters and Men3   Upodobania muzyczne4   PrzypisyBiografiaNanna Bryndís Hilmarsdóttir spędziła swoje dzieciństwo w Garður, miejscowości w południowo - zachodniej Islandii[1].Jako nastolatka uczęszczała do szkoły muzycznej. Zanim powstał zespół Of Monsters and Men Nanna Bryndís Hilmarsdóttir miała własny projekt muzyczny o nazwie Songbird, w ramach którego tworzyła utwory muzyczne i występowała w Reykjavíku.Była również pracownikiem sklepu muzycznego[2].W 2016 roku Nanna Bryndís Hilmarsdóttir wystąpiła gościnnie w serialu Gra o tron jako jeden z muzyków z Braavos[3][4].Of Monsters and MenW ramach poszerzenia projektu Songbird Nanna Bryndís Hilmarsdóttir nawiązała współpracę z pięcioma muzykami, z którymi stworzyła zespół Of Monsters and Men w 2010 roku: Brynjarem Leifssonem, Ragnarem Þórhallssonem, Arnarem Rósenkranzem Hilmarssonem, Árnim Guðjónssonem(od 2012 były członek zespołu) oraz Kristjánem Pállem Kristjánssonem[2][5]. Po tygodniu współpracy wygrali konkurs muzyczny Músíktilraunir[2].Pod koniec 2011 zespół wydał swój pierwszy album My Head Is an Animal, dzięki któremu zyskał popularność poza Islandią[6].Upodobania muzyczneNanna Bryndís Hilmarsdóttir przytaczała w wywiadach swoich ulubionych muzyków oraz zespoły muzyczne, m.in. zespół indie rockowy Gayngs, Arcade Fire, Lianne La Havas, Leslie Feist oraz Justina Vernona[7][8]. Wyraziła również chęć współpracy z zespołem Bon Iver[9].");

    }
    #endregion
}
