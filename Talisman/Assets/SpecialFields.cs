﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpecialFields : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Descryption;
    public TextMeshProUGUI MessageText;
    public GameObject panel;
    public GameObject MessageBox;
    public Button diceRollButton;
    public Button Yes;
    public Button No;
    public bool wait = true;
    public bool TurnFight = false;
    public bool specialDiceBlock = false;
    // Use this for initialization
    //just some random comment
    #region Sets
    //  ********************
    //  Włączniki
    //  ********************

    public void SetGospodaOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        Gospoda();
    }
    public void SetCzarodziejkaOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        Czarodziejka();
    }
    public void SetArenaOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        Arena();
    }
    public void SetKaplicaOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        Kaplica();
    }
    public void SetCmentarzOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        Cmentarz();
    }
    public void SetLasOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        Las();
    }
    public void SetWiezaWampiraOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        WiezaWampira();
    }
    public void SetPrzepascOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        Przepasc();
    }
    public void SetStraznikOn()
    {
        panel.SetActive(true);
        Yes.gameObject.SetActive(true);
        No.gameObject.SetActive(true);
        Straznik();
    }
    #endregion
    #region Events
    //  ********************
    //  Zdarzenia
    //  ********************
    public void StraznikMessage(bool meh)
    {
        MessageBox.SetActive(true);
        if (meh)
        {
            MessageText.text = "Gratulacje pokonałeś strażnika - przenosisz się do innej krainy";
        }
        else
        {
            MessageText.text = "Przykro mi nie udało Ci się pokonać strażnika";
        }
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        tbs.playerArray[tbs.playerIndex].outerRing = false;
        tbs.playerArray[tbs.playerIndex].middleRing = false;
    }
    public void Kaplica()
    {
        specialDiceBlock = true;
        Name.text = "Kaplica";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1 - 4) Nic się nie dzieję - Twoje modły nie zostały wysłuchane. \n(5) Bogowie Ciebie wysłuchują. Otrzymujesz błogosławieństwo w postaci 1 punktu siły. \n(6) Świętokradztwo - tracisz jeden punkt życia i dwa punkty siły.";
    }
    public void Cmentarz()
    {
        specialDiceBlock = true;
        Name.text = "Cmentarz";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1 - 4) Nic się nie dzieję. \n(5) Zmarli wysłuchali Twe modły otrzymujesz 1 punkt życia. \n(6) Zmarli nie są zadowoleni z Twojej obecności tracisz dwa punkty życia i jeden punkt siły. ";
    }
    public void Las()
    {
        specialDiceBlock = true;
        Name.text = "Las";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1) Otrzymujesz 1 punkt życia\n(2) Tracisz 1 punkt życia.\n(3) Tracisz 1 punkt siły.\n(4) Otrzymujesz 1 punkt siły.\n(5) Nic się nie dzieje.\n(6) Otrzymujesz 1 sztukę złota.";
    }
    public void Arena()
    {
        specialDiceBlock = true;
        Name.text = "Arena";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1 - 4) Nic się nie dzieję bo Twój miecz nie wytrzymuje testu wytrzymałości oręża.\n(5) Pokonujesz potężnego przeciwnika - Otrzymujesz 1 punkt siły. \n(6) Zwycięsto hyaaaaaaaaaaah sam król Ciebie nagradza - Otrzymujesz 2 sztuki złota.";
    }
    public void Czarodziejka()
    {
        specialDiceBlock = true;
        Name.text = "Czarodziejka";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1) Nic się nie dzieję.\n(2) Czarodziejka rzuca na Ciebie osłabienie - Tracisz 1 punkt siły.\n(3) Czarodziejka zauracza Cię czarem i okrada Cię - Tracisz 1 sztukę złota.\n(4) Czarodziejka nagradza Cię - otrzymujesz 1 sztukę złota.\n(5) Czarodziejka wyzywa Cię na pojedynek wygrywasz - Otrzymujesz 1 punkt siły.\n(6) Czarodziejka błogosławi Ciebie - Otrzymujesz 2 punkty siły.";
    }
	public void Gospoda()
    {
        specialDiceBlock = true;
        Name.text = "Gospoda";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1) Upiłeś się i zasnąłeś w kącie - tracisz turę. \n(2) Upiłeś się i wdałeś w bójkę z chłopem tracisz 1 hp. \n(3) Grałeś w karty i przegrałeś 1 sztukę złota. \n(4) Grałeś w karty i wygrałeś jedną sztukę złota. \n(5) Potknąłeś się wpadłeś do piwnicy. Znalazłeś dwie sztuki złota. \n(6) Karateka uczy Ciebie sztuk walki (+1 punkt Siły.)";
    }
    public void WiezaWampira()
    {
        specialDiceBlock = true;
        Name.text = "Wieża wampira";
        Descryption.text = "Tracisz Krew - Rzuć 1 kością by przekonać się ile krwi wyssał z Ciebie wampir. Możesz odrzucić dowolną liczbę przyjaciół, aby ograniczyć stratę punktów życia. Za każdego odrzuconego przyjaciela tracisz o 1 punkt życia mniej. (1-2) Tracisz 1 punkt życia \n (3-4) Tracisz 2 punkty życia \n (5-6) Tracisz 3 punkty życia.";
    }
    public void Przepasc()
    {
        specialDiceBlock = true;
        Name.text = "Przepaść";
        Descryption.text = "Głęboko tu. Rzuć kością. Jeśli liczba oczek jest parzysta uda Ci się przeskoczyć. Jeśli nieparzysta musisz odrzucić swój ekwipunek by doskoczyć na drugą stronę.";
    }
    public void Straznik()
    {
        specialDiceBlock = true;
        Name.text = "Strażnik";
        Descryption.text = "Przebywając równinę zauważasz dziwne miejsce strzeżone przez silnego (Siła 7) strażnika. Strażnik mówi że jeśli go pokonasz będziesz mógł przejść przez portal przenoszący Ci do innej krainy. Podejmujesz wyzwanie?";
    }
    #endregion
    #region Panels and Dice events
    public void OK_BUTTON()
    {
        specialDiceBlock = false;
    }
    public void YES_BUTTON()
    {
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        tbs.playerArray[tbs.playerIndex].CombatStraznik();
        panel.SetActive(false);
    }
    public void NO_BUTTON()
    {
        panel.SetActive(false);
    }
    public void Rzuckosciascript()
    {
        StartCoroutine(RzucKoscia());
    }
    public IEnumerator RzucKoscia()
    {
        var script = GameObject.Find("D6").GetComponent<DiceScript>();
        var dice = GameObject.Find("D6");
        var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        dice.gameObject.transform.localPosition = new Vector3(-100, 0, 0);
        script.roll();
        yield return new WaitForSeconds(5);
        MessageBox.SetActive(true);
        if (Name.text.Equals("Gospoda"))
        {
            switch (DiceScript.getResult())
            {
                case 1:
                    MessageText.text = "Hańba Ci przesypiasz swoją turę!!!";
                    break;
                case 2:
                    MessageText.text = "Ha co z Ciebie za poszukiwacz jak przegrywasz ze zwykłym chłopem - tracisz 1 hp.";
                    go.playerArray[go.playerIndex].current_health--;
                    break;
                case 3:
                    MessageText.text = "Hazard nie zawsze popłaca - tracisz jedną sztukę złota.";
                    go.playerArray[go.playerIndex].gold--;
                    break;
                case 4:
                    MessageText.text = "Gratulację wygrałeś w karty jedną sztukę złota.";
                    go.playerArray[go.playerIndex].gold++;
                    break;
                case 5:
                    MessageText.text = "Gratulację znalazłeś dwie sztuki złota.";
                    go.playerArray[go.playerIndex].gold += 2;
                    break;
                case 6:
                    MessageText.text = "Też mi karateka skoro go tak łatwo pokonałem - siła zwiększona o 1";
                    go.playerArray[go.playerIndex].strength++;
                    break;
            }
        }
        else if (Name.text.Equals("Czarodziejka"))
        {
            switch (DiceScript.getResult())
            {
                case 1:
                    MessageText.text = "Hmmm... dziwne nic się nie stało - strata czasu.";
                    break;
                case 2:
                    MessageText.text = "Aaaaaa co się dzieję słabnę nieee nieeeeee.... - tracisz jeden punkt siły.";
                    go.playerArray[go.playerIndex].strength--;
                    break;
                case 3:
                    MessageText.text = "Budzisz się i zauważasz że nie masz sakiewki. - tracisz jedną sztukę złota.";
                    go.playerArray[go.playerIndex].gold--;
                    break;
                case 4:
                    MessageText.text = "Hmm nic nie zainwestowałem i jeszcze zyskałem... nieźle. - otrzymujesz jedną sztukę złota.";
                    go.playerArray[go.playerIndex].gold++;
                    break;
                case 5:
                    MessageText.text = "Trochę haniebny czyn pobić kobietę ale... - siła zwiększona o 1";
                    go.playerArray[go.playerIndex].strength++;
                    break;
                case 6:
                    MessageText.text = "Czarodziejka rzuca na Ciebie czar - Twoja siła rośnie. - siła zwiększona o 2";
                    go.playerArray[go.playerIndex].strength+=2;
                    break;
            }
        }
        else if (Name.text.Equals("Arena"))
        {
            switch (DiceScript.getResult())
            {
                case 5:
                    MessageText.text = "Pokonałeś jednego przeciwnika - otrzymujesz 1 siły.";
                    go.playerArray[go.playerIndex].strength++;
                    break;
                case 6:
                    MessageText.text = "Wygrałeś turniej - otrzymujesz 2 sztuki złota.";
                    go.playerArray[go.playerIndex].gold += 2;
                    break;
                default:
                    MessageText.text = "Nic się nie stało - potrzebujesz większego miecza.";
                    break;
            }
        }
        else if (Name.text.Equals("Kaplica"))
        {
            switch (DiceScript.getResult())
            {
                case 5:
                    MessageText.text = "Twe modły zostały wysłuchane. - Otrzymujesz jeden punkt siły.";
                    go.playerArray[go.playerIndex].strength++;
                    break;
                case 6:
                    MessageText.text = "Gniew bogów spada na Ciebie w postaci pioruna. Słyszysz jak wiatr niesie słowa \"Świętokradztwo\"  - Tracisz dwa punkty siły i jeden punkt życia.";
                    go.playerArray[go.playerIndex].strength-=2;
                    go.playerArray[go.playerIndex].current_health--;
                    break;
                default:
                    MessageText.text = "Nic się nie stało - Bogowie nie wysłuchują Twoich modlitw.";
                    break;
            }
        }
        else if (Name.text.Equals("Cmentarz"))
        {
            switch (DiceScript.getResult())
            {
                case 5:
                    MessageText.text = "Twe modły zostały wysłuchane. - Otrzymujesz jeden punkt siły.";
                    go.playerArray[go.playerIndex].strength++;
                    break;
                case 6:
                    MessageText.text = "Gniew zmarłych spada na Ciebie w postaci trzęsienia ziemi spada na Ciebie konar drzewa. - Tracisz jeden punkt siły i dwa punkt życia.";
                    go.playerArray[go.playerIndex].current_health-=2;
                    go.playerArray[go.playerIndex].strength--;
                    break;
                default:
                    MessageText.text = "Nic się nie stało - Zmarli przecież nie żyją LOL.";
                    break;
            }
        }
        else if (Name.text.Equals("Las"))
        {
            switch (DiceScript.getResult())
            {
                case 1:
                    MessageText.text = "Znajdujesz magiczne źródło - otrzymujesz jeden punkt zdrowia.";
                    go.playerArray[go.playerIndex].current_health++;
                    break;
                case 2:
                    MessageText.text = "Zjadasz wilczą jagodę - tracisz jeden punkt zdrowia.";
                    go.playerArray[go.playerIndex].current_health--;
                    break;
                case 3:
                    MessageText.text = "Spotykasz mroczną elfkę - tracisz jeden punkt siły.";
                    go.playerArray[go.playerIndex].strength--;
                    break;
                case 4:
                    MessageText.text = "Wspiąłeś się na najwyższy szczyt - otrzymujesz jeden punkt siły.";
                    go.playerArray[go.playerIndex].strength++;
                    break;
                case 5:
                    MessageText.text = "Wychodzisz z lasu jak gdyby nigdy nic.";
                    break;
                case 6:
                    MessageText.text = "Widzisz spadającą gwiazdę - otrzymujesz jedna sztukę złota.";
                    go.playerArray[go.playerIndex].gold++;
                    break;
            }
        }
        else if (Name.text.Equals("Wieża wampira"))
        {
            switch (DiceScript.getResult())
            {
                case 1:
                case 2:
                    MessageText.text = "Wampir miał próchnicę, z tymi zębami to tylko 1 punkt zdrowia.";
                    go.playerArray[go.playerIndex].current_health--;
                    break;
                case 3:
                case 4:
                    MessageText.text = "Wspiąłeś się na najwyższy szczyt - otrzymujesz jeden punkt siły.";
                    go.playerArray[go.playerIndex].current_health-=2;
                    break;
                case 5:
                case 6:
                    MessageText.text = "Widzisz spadającą gwiazdę - otrzymujesz jedna sztukę złota.";
                    go.playerArray[go.playerIndex].current_health-=3;
                    break;
            }
        }
        else if (Name.text.Equals("Przepaść"))
        {
            switch (DiceScript.getResult())
            {
                case 1:
                case 3:
                case 5:
                    MessageText.text = "Skaczesz jak pasikonik. Ta przepaść to dla Ciebie nic.";
                    //  Przesuń gracza dalej.

                    break;
                case 2:
                case 4:
                case 6:
                    MessageText.text = "Z takim brzuchem musiałeś odrzucić ekwipunek. Ale się udało.";
                    //  Odrzuć ekwipunek poza talizmanem jeśli jakiś ma
                    
                    break;
            }
        }    
    }
    #endregion
}
