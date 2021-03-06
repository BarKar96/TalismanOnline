﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets;

public class SpecialFields : MonoBehaviour
{
    #region Variables and GUI
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Descryption;
    public TextMeshProUGUI MessageText;
    public GameObject panel;
    public GameObject MessageBox;
    public Button diceRollButton;
    public Button Yes;
    public Button No;
    public Button Gold;
    public Button HP;
    public Button HPplus1;
    public Button HPplus2;
    public Button HPplus3;
    public Button OKplus;
    private bool Guard = false;
    private bool SecretGate = false;
    public bool specialDiceBlock = false;
    private int hp = 0;
    #endregion
    #region Sets
    //  ********************
    //  Włączniki
    //  ********************
    public void SetRycerzOn()
    {
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        panel.SetActive(true);
        if(tbs.playerArray[tbs.playerIndex].gold>0)
        {
            Gold.gameObject.SetActive(true);
        }
        HP.gameObject.SetActive(true);
        CzarnyRycerz();
    }
    public void SetZamekOn()
    {
        Zamek();
    }
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
    public void SetTajemneWrotaOn()
    {
        panel.SetActive(true);
        Yes.gameObject.SetActive(true);
        No.gameObject.SetActive(true);
        TajemneWrota();
    }
    public void SetWiezaWDolinieOn()
    {
        panel.SetActive(true);
        WiezaWDolinie();
    }
    public void SetSmiercOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        Smierc();
    }
    public void SetWilkolakOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        JamaWilkolaka();
    }
    public void SetKryptaOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        Krypta();
    }
    public void SetKopalniaOn()
    {
        panel.SetActive(true);
        diceRollButton.gameObject.SetActive(true);
        Kopalnia();
    }
    public void StraznikMessage(bool meh)
    {
        MessageBox.SetActive(true);
        if (meh)
        {
            Guard = true;
            MessageText.text = "Gratulacje pokonałeś strażnika - przenosisz się do innej krainy.";
        }
        else
        {
            MessageText.text = "Przykro mi nie udało Ci się pokonać strażnika tracisz 1 punkt hp.";
        }

    }
    public void TajemneWrotaMessage(bool meh)
    {
        MessageBox.SetActive(true);
        if (meh)
        {
            SecretGate = true;
            MessageText.text = "Gratulacje rozbiłeś lustro - przechodzisz przez wrota do innej krainy.";
        }
        else
        {
            MessageText.text = "Przykro mi nie udało Ci się pokonać siebie tracisz 1 punkt hp.";
        }

    }
    #endregion
    #region Events
    //  ********************
    //  Zdarzenia
    //  ********************
    private void resetPlayerRings()
    {
        var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();   
        go.playerArray[go.playerIndex].innerRing = false;
        go.playerArray[go.playerIndex].outerRing = false;
        go.playerArray[go.playerIndex].middleRing = false;
    }
    private void CzarnyRycerz()
    {
        specialDiceBlock = true;
        Name.text = "Czarny Rycerz";
        Descryption.text = "Spotykasz czarnego rycerza możesz wybrać albo stracić sztukę złota albo stracić punkt życia. (jak nie masz złota nie możesz zapłacić). Wybierz opcję.";
    }
    private void Kaplica()
    {
        specialDiceBlock = true;
        Name.text = "Kaplica";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1 - 4) Nic się nie dzieję - Twoje modły nie zostały wysłuchane. \n(5) Bogowie Ciebie wysłuchują. Otrzymujesz błogosławieństwo w postaci 1 punktu siły. \n(6) Świętokradztwo - tracisz jeden punkt życia i dwa punkty siły.";
    }
    private void Cmentarz()
    {
        specialDiceBlock = true;
        Name.text = "Cmentarz";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1 - 4) Nic się nie dzieję. \n(5) Zmarli wysłuchali Twe modły otrzymujesz 1 punkt życia. \n(6) Zmarli nie są zadowoleni z Twojej obecności tracisz dwa punkty życia i jeden punkt siły. ";
    }
    private void Las()
    {
        specialDiceBlock = true;
        Name.text = "Las";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1) Otrzymujesz 1 punkt życia\n(2) Tracisz 1 punkt życia.\n(3) Tracisz 1 punkt siły.\n(4) Otrzymujesz 1 punkt siły.\n(5) Nic się nie dzieje.\n(6) Otrzymujesz 1 sztukę złota.";
    }
    private void Arena()
    {
        specialDiceBlock = true;
        Name.text = "Arena";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1 - 4) Nic się nie dzieję bo Twój miecz nie wytrzymuje testu wytrzymałości oręża.\n(5) Pokonujesz potężnego przeciwnika - Otrzymujesz 1 punkt siły. \n(6) Zwycięsto hyaaaaaaaaaaah sam król Ciebie nagradza - Otrzymujesz 2 sztuki złota.";
    }
    private void Czarodziejka()
    {
        specialDiceBlock = true;
        Name.text = "Czarodziejka";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1) Nic się nie dzieję.\n(2) Czarodziejka rzuca na Ciebie osłabienie - Tracisz 1 punkt siły.\n(3) Czarodziejka zauracza Cię czarem i okrada Cię - Tracisz 1 sztukę złota.\n(4) Czarodziejka nagradza Cię - otrzymujesz 1 sztukę złota.\n(5) Czarodziejka wyzywa Cię na pojedynek wygrywasz - Otrzymujesz 1 punkt siły.\n(6) Czarodziejka błogosławi Ciebie - Otrzymujesz 2 punkty siły.";
    }
    private void Gospoda()
    {
        specialDiceBlock = true;
        Name.text = "Gospoda";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1) Upiłeś się i zasnąłeś w kącie - tracisz turę. \n(2) Upiłeś się i wdałeś w bójkę z chłopem tracisz 1 hp. \n(3) Grałeś w karty i przegrałeś 1 sztukę złota. \n(4) Grałeś w karty i wygrałeś jedną sztukę złota. \n(5) Potknąłeś się wpadłeś do piwnicy. Znalazłeś dwie sztuki złota. \n(6) Karateka uczy Ciebie sztuk walki (+1 punkt Siły.)";
    }
    private void WiezaWampira()
    {
        specialDiceBlock = true;
        Name.text = "Wieża wampira";
        Descryption.text = "Tracisz Krew - Rzuć 1 kością by przekonać się ile krwi wyssał z Ciebie wampir. (1-2) Tracisz 1 punkt życia \n (3-4) Tracisz 2 punkty życia \n (5-6) Tracisz 3 punkty życia.";
    }
    private void Przepasc()
    {
        specialDiceBlock = true;
        Name.text = "Przepaść";
        Descryption.text = "Głęboko tu. Rzuć kością. Jeśli liczba oczek jest parzysta uda Ci się przeskoczyć. Jeśli nieparzysta musisz odrzucić swój ekwipunek by doskoczyć na drugą stronę.";
    }
    private void Straznik()
    {
        specialDiceBlock = true;
        Name.text = "Strażnik";
        Descryption.text = "Przebywając równinę zauważasz dziwne miejsce strzeżone przez silnego (Siła 7) strażnika. Strażnik mówi że jeśli go pokonasz będziesz mógł przejść przez portal przenoszący Ci do innej krainy. Podejmujesz wyzwanie?";
    }
    private void WiezaWDolinie()
    {
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        var windows = GameObject.Find("Windows").GetComponent<Windows>();
        if (tbs.playerArray[tbs.playerIndex].getItems().Find(x => x.getName().Equals("talisman"))!= null)
        {
            windows.WinningGame();
        }
        else
        {
            tbs.innerRing[tbs.playerArray[tbs.playerIndex].playerPiece.indexOfField].cardsOnField.Add(tbs.deckOfCards.drawCard());
            //tbs.[tbs.playerArray[tbs.playerIndex]].cardsOnField.Add(tbs.deckOfCards.drawCard());
        }
    }
    private void Zamek()
    {
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        var windows = GameObject.Find("Windows").GetComponent<Windows>();
        if (tbs.playerArray[tbs.playerIndex].getItems().Find(x => x.getName().Equals("Książę")) != null || tbs.playerArray[tbs.playerIndex].getItems().Find(x => x.getName().Equals("Księżniczka")) != null)
        {
            MessageBox.SetActive(true);
            if(tbs.playerArray[tbs.playerIndex].total_health - tbs.playerArray[tbs.playerIndex].current_health >= 2)
            {
                MessageText.text = "Przywrócono Ci dwa brakujące punkty życia gdyż posiadasz Księcia lub Księżniczkę jako swojego kompana.";
                tbs.playerArray[tbs.playerIndex].current_health += 2;
            }
            else if (tbs.playerArray[tbs.playerIndex].total_health - tbs.playerArray[tbs.playerIndex].current_health == 1)
            {
                MessageText.text = "Przywrócono Ci brakujący punkt życia gdyż posiadasz Księcia lub Księżniczkę jako swojego kompana.";
                tbs.playerArray[tbs.playerIndex].current_health += 1;
            }
            else
            {
                MessageText.text = "Nic się nie stało koniec tury.";
            }
        }
        else if (tbs.playerArray[tbs.playerIndex].current_health == tbs.playerArray[tbs.playerIndex].total_health)
        {
            MessageBox.SetActive(true);
            MessageText.text = "Nie możesz nic zrobić bo masz całe życie.";
        }

        else if (tbs.playerArray[tbs.playerIndex].gold <= 0)
        {
            MessageBox.SetActive(true);
            MessageText.text = "Nie możesz nic zrobić bo nie masz złota.";
        }
        else
        {
            panel.SetActive(true);
            specialDiceBlock = true;
            Name.text = "Zamek";
            Descryption.text = "Możesz odzyskać punkt życia u nadwornego medyka. Za każdą sztukę złota jaką posiadasz (max 3), lub kliknij OK jeśli nie zamierzasz nic robić.";
            if (tbs.playerArray[tbs.playerIndex].gold > 0 && tbs.playerArray[tbs.playerIndex].current_health < tbs.playerArray[tbs.playerIndex].total_health)
            {
                HPplus1.gameObject.SetActive(true);
            }
            if (tbs.playerArray[tbs.playerIndex].gold > 1 && tbs.playerArray[tbs.playerIndex].current_health < tbs.playerArray[tbs.playerIndex].total_health && tbs.playerArray[tbs.playerIndex].total_health - tbs.playerArray[tbs.playerIndex].current_health > 1)
            {
                HPplus2.gameObject.SetActive(true);
            }
            if (tbs.playerArray[tbs.playerIndex].gold > 2 && tbs.playerArray[tbs.playerIndex].current_health < tbs.playerArray[tbs.playerIndex].total_health && tbs.playerArray[tbs.playerIndex].total_health - tbs.playerArray[tbs.playerIndex].current_health > 2)
            {
                HPplus3.gameObject.SetActive(true);
            }
            OKplus.gameObject.SetActive(true);
        }

    }
    private void Smierc()
    {
        specialDiceBlock = true;
        Name.text = "Śmierć";
        Descryption.text = "Gra ze śmiercią - Jeśli wyrzucisz 1 umierasz. Takie życie.";
    }
    private void JamaWilkolaka()
    {
        specialDiceBlock = true;
        Name.text = "Jama Wilkołaka";
        Descryption.text = "Jama Wilkołaka - Rzuć kością. Jeśli suma oczek z Twoją siłą jest większa od sumy oczek z Twoim życiem pokonujesz wilkołaka. Inaczej zadaje on tobie tyle obrażeń ile oczek na kostce";
    }
    private void Kopalnia()
    {
        specialDiceBlock = true;
        Name.text = "Kopalnia";
        Descryption.text = "Rzuć kością - Natychmiast przesuwasz się na obszar: (1) Pozostajesz w tym miejscu. (2) Równina Grozy. (3-4) Tajemne Wrota. (5) Jaskinia Czarownika. (6) Gospoda.";
    }
    private void Krypta()
    {
        specialDiceBlock = true;
        Name.text = "Krypta";
        Descryption.text = "Rzuć kością - Natychmiast przesuwasz się na obszar: (1) Pozostajesz w tym miejscu. (2) Równina Grozy. (3-4) Tajemne Wrota. (5) Jaskinia Czarownika. (6) Miasto.";
    }
    private void TajemneWrota()
    {
        specialDiceBlock = true;
        Name.text = "Tajemne Wrota";
        Descryption.text = "Przemierzając pola lasy zauważasz drzwi, na środku pustyni. Podchodzisz do drzwi i słyszysz intrukcję. Musisz pokonać swoje odbicie w walce jeśli wygrasz możesz przejść do kolejnej krainy jeśli nie. Odbicie wykorzystuje Twoje przedmioty i nie możesz używać magii na nich.";
    }
    #endregion
    #region UI
    public void OK_BUTTON()
    {
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        if (!Guard&&!SecretGate)
        {
            specialDiceBlock = false;
            diceRollButton.gameObject.SetActive(false);
        }
        else if(Guard)
        {
            resetPlayerRings();
            tbs.playerArray[tbs.playerIndex].middleRing = true;
            tbs.movePiece(tbs.playerIndex, 1);
            Guard = false;
            specialDiceBlock = false;
        }
        else if(SecretGate)
        {
            resetPlayerRings();
            tbs.playerArray[tbs.playerIndex].innerRing = true;
            tbs.movePiece(tbs.playerIndex, 1);
            SecretGate = false;
            specialDiceBlock = false;
        }
    }
    public void YES_BUTTON()
    {
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        if (Name.text == "Strażnik")
        {
            tbs.playerArray[tbs.playerIndex].CombatStraznik();
        }
        else if(Name.text == "Tajemne Wrota")
        {
            tbs.playerArray[tbs.playerIndex].CombatWrota();
        }
        panel.SetActive(false);
        specialDiceBlock = false;
        Yes.gameObject.SetActive(false);
        No.gameObject.SetActive(false);
    }
    public void NO_BUTTON()
    {
        panel.SetActive(false);
        specialDiceBlock = false;
        Yes.gameObject.SetActive(false);
        No.gameObject.SetActive(false);
    }
    public void GOLD_BUTTON()
    {
        panel.SetActive(false);
        specialDiceBlock = false;
        var wnd = GameObject.Find("Windows").GetComponent<Windows>();
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        Gold.gameObject.SetActive(false);
        HP.gameObject.SetActive(false);
        tbs.playerArray[tbs.playerIndex].gold--;
        wnd.UpdateStatsToText();
    }
    public void HP_BUTTON()
    {
        panel.SetActive(false);
        specialDiceBlock = false;
        var wnd = GameObject.Find("Windows").GetComponent<Windows>();
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        Gold.gameObject.SetActive(false);
        HP.gameObject.SetActive(false);
        tbs.playerArray[tbs.playerIndex].current_health--;
        wnd.UpdateStatsToText();
    }
    public void HPPLUS1_BUTTON()
    {
        MessageBox.SetActive(true);
        MessageText.text = "Wykupiłeś sobie 1 punkt życia";
        panel.SetActive(false);
        specialDiceBlock = false;
        var wnd = GameObject.Find("Windows").GetComponent<Windows>();
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        HPplus1.gameObject.SetActive(false);
        HPplus2.gameObject.SetActive(false);
        HPplus3.gameObject.SetActive(false);
        OKplus.gameObject.SetActive(false);
        tbs.playerArray[tbs.playerIndex].gold--;
        tbs.playerArray[tbs.playerIndex].current_health++;
        wnd.UpdateStatsToText();
    }
    public void HPPLUS2_BUTTON()
    {
        MessageBox.SetActive(true);
        MessageText.text = "Wykupiłeś sobie 2 punkty życia";
        panel.SetActive(false);
        specialDiceBlock = false;
        var wnd = GameObject.Find("Windows").GetComponent<Windows>();
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        HPplus1.gameObject.SetActive(false);
        HPplus2.gameObject.SetActive(false);
        HPplus3.gameObject.SetActive(false);
        OKplus.gameObject.SetActive(false);
        tbs.playerArray[tbs.playerIndex].gold-=2;
        tbs.playerArray[tbs.playerIndex].current_health+=2;
        wnd.UpdateStatsToText();
    }
    public void HPPLUS3_BUTTON()
    {
        MessageBox.SetActive(true);
        MessageText.text = "Wykupiłeś sobie 3 punkty życia";
        panel.SetActive(false);
        specialDiceBlock = false;
        var wnd = GameObject.Find("Windows").GetComponent<Windows>();
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        HPplus1.gameObject.SetActive(false);
        HPplus2.gameObject.SetActive(false);
        HPplus3.gameObject.SetActive(false);
        OKplus.gameObject.SetActive(false);
        tbs.playerArray[tbs.playerIndex].gold-=3;
        tbs.playerArray[tbs.playerIndex].current_health+=3;
        wnd.UpdateStatsToText();
    }
    public void OKPLUS_BUTTON()
    {
        panel.SetActive(false);
        specialDiceBlock = false;
        HPplus1.gameObject.SetActive(false);
        HPplus2.gameObject.SetActive(false);
        HPplus3.gameObject.SetActive(false);
        OKplus.gameObject.SetActive(false);
    }
    #endregion
    #region Dice and Events
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
                    if(!go.playerArray[go.playerIndex].gold.Equals(0))
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
                    if (!go.playerArray[go.playerIndex].gold.Equals(0))
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
        else if (Name.text.Equals("Wieża Wampira"))
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
                    MessageText.text = "Wampir wysysa z Ciebie 2 punkty życia.";
                    go.playerArray[go.playerIndex].current_health-=2;
                    break;
                case 5:
                case 6:
                    MessageText.text = "Wampir zawołał kolegów i wyssał z Ciebie 3 punkty życia";
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
                    //  Nic się nie dzieje i gracz może iść dalej.
                    break;
                case 2:
                case 4:
                case 6:
                    MessageText.text = "Z takim brzuchem musiałeś odrzucić ekwipunek. Ale się udało.";
                    //  Odrzuć ekwipunek poza talizmanem jeśli jakiś ma        
                    go.playerArray[go.playerIndex].armor = null;
                    go.playerArray[go.playerIndex].weapon = null;
                    go.playerArray[go.playerIndex].health_modifier = 0;
                    go.playerArray[go.playerIndex].strength_modifier = 0;
                    //  Jeśli gracz ma talizman to go zachowuje
                    Card c = go.playerArray[go.playerIndex].getItems().Find(x => x.getName().Equals("talizman"));
                    if(c != null)
                    {
                        go.playerArray[go.playerIndex].getItems().Clear();
                        go.playerArray[go.playerIndex].getItems().Add(c);
                    }
                    else
                    {
                        go.playerArray[go.playerIndex].getItems().Clear();
                    }
                    break;
            }
        }
        else if (Name.text.Equals("Śmierć"))
        {
            if(DiceScript.getResult().Equals(Random.Range(1, 6)))
            {
                MessageText.text = "Przegrałeś z śmiercią w kości. Wyskakuj z ciała!";
                go.playerArray[go.playerIndex].current_health = 0;
            }
            MessageText.text = "Lata nałogowego hazardu opłaciły się. Pokonałeś śmierć w kości."; 
        }
        else if (Name.text.Equals("Jama Wilkołaka"))
        {
            if(DiceScript.getResult() + go.playerArray[go.playerIndex].current_health < 
                go.playerArray[go.playerIndex].strength + go.playerArray[go.playerIndex].strength_modifier + DiceScript.getResult())
            {
                MessageText.text = "Wilkołak zobaczył Twoje mięśnie nabłyszczane olejkiem i nawet nie fikał. Możesz przejść.";
            }
            else
            {
                MessageText.text = "Nie jesteś wcale taki twardy. Wilkołak wyciosał Ci ładną bliznę na ciele.";
                go.playerArray[go.playerIndex].current_health -= DiceScript.getResult();
            }
            
        }
        else if (Name.text.Equals("Krypta") || Name.text.Equals("Kopalnia"))
        {
      /*(1) Pozostajesz w tym miejscu. 
        (2) Równina Grozy. i 4 
        (3-4) Tajemne Wrota. m 8
        (5) Jaskinia Czarownika. m 0
        (6) gospoda. o 6*/
            switch(DiceScript.getResult()){
                case 2:
                    resetPlayerRings();
                    go.playerArray[go.playerIndex].innerRing=true;
                    go.movePiece(go.playerIndex, 4);
                    break;
                case 3:
                case 4:
                    resetPlayerRings();
                    go.playerArray[go.playerIndex].middleRing=true;
                    go.movePiece(go.playerIndex, 8);
                    break;
                case 5:
                    resetPlayerRings();
                    go.playerArray[go.playerIndex].middleRing=true;
                    go.movePiece(go.playerIndex, 0);
                    break;
                case 6:
                    resetPlayerRings();
                    go.playerArray[go.playerIndex].outerRing=true;
                    go.movePiece(go.playerIndex, 6);
                    break;
            }
        }
    }
    #endregion
}
