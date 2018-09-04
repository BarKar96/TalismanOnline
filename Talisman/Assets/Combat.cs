using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public  class Combat : MonoBehaviour
{
    TalismanBoardScript tal;

    public bool combatDiceBlock = false;
    public bool combatWin = false;
    public Player player1 = null;
    public Player player2 = null;
    public bool player2SpellsDone = false;
    public bool player1SpellsDone = false;
    public bool Guard = false;
    public bool TajemneWrota = false;
    Card c = null;

    public bool turaIstoty = false;
    public bool turaGracza = true;
    

    public List<GameObject> combatCardList = new List<GameObject>();
    public List<GameObject> spellList = new List<GameObject>();

    public int rzut_Ataku_gracza = 0;
    public  int skutecznosc_Ataku_gracza = 0;

    public  int rzut_Ataku_istoty = 0;
    public  int skutecznosc_Ataku_istoty = 0;

    public GameObject subPanel_Combat;
    private bool _subPanel_Combat_Opened = false;

    public GameObject subPanel_enterCombat;
    private bool _subPanel_enterCombat_Opened    = false;

    public GameObject subPanel_Spell;
    private bool _subPanel_Spell_Opened = false;

    public GameObject subPanel_Reward;
    private bool _subPanel_Reward_Opened = false;


    public Button ButtonOK;
    public TextMeshProUGUI textGracz;
    public TextMeshProUGUI textPrzeciwnik;
    public TextMeshProUGUI textPrzebieg;

    public bool atakujButton = false;
    public bool wymknijButton = false;

    public void reset()
    {
        
        
        player1 = null;
        player2 = null;
        player2SpellsDone = false;
        player1SpellsDone = false;
        c = null;
        turaIstoty = false;
        turaGracza = true;
        
    }
    public void toggleEnterCombatPanel()
    {
        combatDiceBlock = true;
        
        _subPanel_enterCombat_Opened = !_subPanel_enterCombat_Opened;
        setSubPanelVisibility(subPanel_enterCombat, _subPanel_enterCombat_Opened);
        if (_subPanel_Combat_Opened == true)
        {

        }
        else
        {
            clearPlayerPanelView(combatCardList);
        }
    }

    public void toggleCombatPanel()
    {
        _subPanel_Combat_Opened = !_subPanel_Combat_Opened;
        setSubPanelVisibility(subPanel_Combat, _subPanel_Combat_Opened);
        if (_subPanel_Combat_Opened == true)
        {
            
        }
        else
        {
            clearPlayerPanelView(combatCardList);
        }
    }
    public void toggleSpellPanel(Player player)
    {
        _subPanel_Spell_Opened = !_subPanel_Spell_Opened;
        setSubPanelVisibility(subPanel_Spell, _subPanel_Spell_Opened);
        if (_subPanel_Spell_Opened == true)
        {
            var sl = GameObject.Find("PanelZaklecWalki").GetComponent<SpellListener>();
            sl.text.text = "";
            CardDrawer.spawnPlayerSpells(player, "PanelZaklecWalki");
            sl.text.text = "Panel zaklęć gracza: " + player.name ;
        }
        else
        {
            clearPlayerPanelView(CardDrawer.spellsListCombat);
        }
    }
    public void toggleRewardPanel(Player player)
    {
        _subPanel_Reward_Opened = !_subPanel_Reward_Opened;
        setSubPanelVisibility(subPanel_Reward, _subPanel_Reward_Opened);
        if (_subPanel_Spell_Opened == true)
        {
            
            CardDrawer.spawnPlayerItems(player,"PanelWyboruNagrody");
        }
        else
        {
            clearPlayerPanelView(CardDrawer.itemsList);
        }
    }
    public void setSubPanelVisibility(GameObject subPanel, bool b)
    {
       
        
        if (subPanel != null)
        {
            subPanel.SetActive(b);
        }


    }
    public void StartCombat(Player p, Card c)
    {
        combatDiceBlock = true;
        this.player1 = p;
        this.c = c;

        textPrzebieg.text = "";

        toggleEnterCombatPanel();
        //wyswietlanie teksturek
        GameObject go1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat1 = Resources.Load(c.getName(), typeof(Material)) as Material;
        go1.transform.localScale = new Vector3(2.2f, 0.6f, 2.6f);
        go1.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go1.transform.SetParent(GameObject.Find("PanelWejsciaDoWalki").transform);
        go1.transform.localPosition = new Vector3(0, 50, -2);
        go1.GetComponent<Renderer>().material = newMat1;

        combatCardList.Add(go1);  
    }
    public void StartCombat(Player p1, Player p2)
    {
        combatDiceBlock = true;
        this.player1 = p1;
        this.player2 = p2;

        textPrzebieg.text = "";

        toggleEnterCombatPanel();
        //wyswietlanie teksturek
        GameObject go1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat1 = Resources.Load(p2.hero.name, typeof(Material)) as Material;
        go1.transform.localScale = new Vector3(2.2f, 0.6f, 2.6f);
        go1.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go1.transform.SetParent(GameObject.Find("PanelWejsciaDoWalki").transform);
        go1.transform.localPosition = new Vector3(0, 50, -2);
        go1.GetComponent<Renderer>().material = newMat1;

        combatCardList.Add(go1);
    }
    public IEnumerator rzuty()
    {
        var script = GameObject.Find("D6").GetComponent<DiceScript>();
        var dice = GameObject.Find("D6");
        dice.gameObject.transform.localPosition = new Vector3(-100,0,0);
        script.roll();
        //czekaj az spadnie 1sza kostka
        yield return new WaitForSeconds(6);
        script.enabled = true;
        turaGracza = false;
        turaIstoty = true;
        script.roll();
    }
   

    public IEnumerator rozpocznijWalke()
    {
        int spellPlayerDMG = 0;
        int spellOpponentDMG = 0;
        if (player1 == player2)
        {
        }
        else
        {

            var spellListener = GameObject.Find("PanelZaklecWalki").GetComponent<SpellListener>();

            spellPlayerDMG = spellListener.playerDMG;
            spellOpponentDMG = spellListener.opponentDMG;
        }

        if (player1 == player2)
        {
            turaGracza = true;
            turaIstoty = false;
            spawnCombatCards(player1, player2);
        }
        else if (player2 != null)
        {
            toggleSpellPanel(player2);
            turaGracza = true;
            turaIstoty = false;
            spawnCombatCards(player1, player2);
        }
        else 
        {
            toggleSpellPanel(player1);
            turaGracza = true;
            turaIstoty = false;
            spawnCombatCards(player1, c);
        }
        

       // var script = GameObject.Find("D6").GetComponent<DiceScript>();

       
        StartCoroutine(rzuty());

        //czekaj az spadnie druga kostka
        yield return new WaitForSeconds(12);
        //rzut ataku gracza
        if (player1 == player2)
        {
            textPrzebieg.text += "\n\nPrzeciwnik wyrzucił: " + rzut_Ataku_gracza;
            skutecznosc_Ataku_gracza = rzut_Ataku_gracza + player1.strength;
            textPrzebieg.text += "\nSkuteczność ataku gracza: " + skutecznosc_Ataku_gracza;
        }
        else
        {
            textPrzebieg.text += "Gracz wyrzucił: " + rzut_Ataku_gracza;
            skutecznosc_Ataku_gracza = rzut_Ataku_gracza + player1.strength + spellPlayerDMG;
            textPrzebieg.text += "\nSkuteczność ataku gracza: " + skutecznosc_Ataku_gracza;
        }

        //rzut ataku istoty
        if(player1 == player2)
        {
            textPrzebieg.text += "\n\n Lustrzane odbicie wyrzucił: " + rzut_Ataku_istoty;
            skutecznosc_Ataku_istoty = rzut_Ataku_istoty + player2.strength;
            textPrzebieg.text += "\nSkuteczność ataku lustrzanego odbicia: " + skutecznosc_Ataku_istoty;
        }
        else if (player2 != null)
        {
            textPrzebieg.text += "\n\nPrzeciwnik wyrzucił: " + rzut_Ataku_istoty;
            skutecznosc_Ataku_istoty = rzut_Ataku_istoty + player2.strength + spellOpponentDMG;
            textPrzebieg.text += "\nSkuteczność ataku przeciwnika: " + skutecznosc_Ataku_istoty; 

        }
        else
        {
            textPrzebieg.text += "\n\nPrzeciwnik wyrzucił: " + rzut_Ataku_istoty;
            skutecznosc_Ataku_istoty = rzut_Ataku_istoty + c.strength;
            textPrzebieg.text += "\nSkuteczność ataku przeciwnika: " + skutecznosc_Ataku_istoty;

        }

        if (skutecznosc_Ataku_gracza > skutecznosc_Ataku_istoty)
        {
            combatWin = true;
            //istota zostala pokonana wyczyszczenie pola planszy z tej kart;
            textPrzebieg.text += "\n\n Udało ci się pokonać stwora!";
            if (player2 != null)
            {
                player2.current_health--;
                textPrzebieg.text += "\n\n" + player1.name + " wygrał! ";
                //  TODO gracz wygrał - sprawdź bossa i przejdź 
            }


        }
        else if (skutecznosc_Ataku_gracza < skutecznosc_Ataku_istoty)
        {
            combatWin = false;
            player1.current_health--;
            if (player2 != null)
            {
                textPrzebieg.text += "\n\n" + player2.name + " wygrał! ";
                

            }
            else
            {
                textPrzebieg.text += "\n\n Ponosisz porażkę!";
                
            }

        }
        else if (skutecznosc_Ataku_gracza == skutecznosc_Ataku_istoty)
        {
            textPrzebieg.text += "\n\nRemis! ";
            combatWin = false;
            //istota nie zostaje pokonana
        }
        ButtonOK.gameObject.SetActive(true);
        
        reset();
    }
    public void wymykanie()
    {
        System.Random rnd = new System.Random();
        int x = rnd.Next(1, 7);
        // Debug.Log(x);
        if (x>3)
        {
            subPanel_enterCombat.SetActive(false);
            var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
            go.buttonSetNextTurnButtonOn();
        }
        else
        {
            toggleSpellPanel(player1);
            textPrzebieg.text += "Ucieczka nie powiodła się!" + "\n";
            toggleEnterCombatPanel();
        }
    }

    public int rzutAtaku()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(1, 6);
    }
    public void spawnCombatCards(Player p, Card c)
    {
        toggleCombatPanel();
        //karta bohatera
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat = Resources.Load(p.hero.name, typeof(Material)) as Material;
        go.transform.localScale = new Vector3(1.0f, 0.3f, 1.0f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.transform.SetParent(GameObject.Find("PanelWalki").transform);
        go.transform.localPosition = new Vector3(-300, 100, -2);
        go.GetComponent<Renderer>().material = newMat;

        //karta przeciwnika
        GameObject go1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat1 = Resources.Load(c.getName(), typeof(Material)) as Material;
        go1.transform.localScale = new Vector3(2.0f, 0.3f, 2.6f);
        go1.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go1.transform.SetParent(GameObject.Find("PanelWalki").transform);
        go1.transform.localPosition = new Vector3(300, 100, -2);
        go1.GetComponent<Renderer>().material = newMat1;

        combatCardList.Add(go);
        combatCardList.Add(go1);


        textGracz.text = "";
        textGracz.text += "Bohater: " + p.hero.name;
        textGracz.text += "\nSiła:  " + p.hero.strength;
        textGracz.text += "\nHP: " + p.current_health + "/" + p.total_health;

        textPrzeciwnik.text = "";
        textPrzeciwnik.text += "Bohater: " + c.getName();
        textPrzeciwnik.text += "\nSiła:  " + c.strength;
        if (c.getName() == "straznik")
        {
            Guard = true;
        }
        else Guard = false;        
    }
    public void spawnCombatCards(Player p1, Player p2)
    {
        toggleCombatPanel();
        if (p1 == p2)
        {
            TajemneWrota = true;
        }
        else TajemneWrota = false;
        //karta bohatera
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat = Resources.Load(p1.hero.name, typeof(Material)) as Material;
        go.transform.localScale = new Vector3(1.0f, 0.3f, 1.0f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.transform.SetParent(GameObject.Find("PanelWalki").transform);
        go.transform.localPosition = new Vector3(-300, 100, -2);
        go.GetComponent<Renderer>().material = newMat;

        //karta przeciwnika
        GameObject go1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat1 = Resources.Load(p2.hero.name, typeof(Material)) as Material;
        go1.transform.localScale = new Vector3(1.0f, 0.3f, 1.0f);
        go1.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go1.transform.SetParent(GameObject.Find("PanelWalki").transform);
        go1.transform.localPosition = new Vector3(300, 100, -2);
        go1.GetComponent<Renderer>().material = newMat1;

        combatCardList.Add(go);
        combatCardList.Add(go1);


        textGracz.text = "";
        textGracz.text += "Bohater: " + p1.hero.name;
        textGracz.text += "\nSiła:  " + p1.hero.strength;
        textGracz.text += "\nHP: " + p1.current_health + "/" + p1.total_health;

        textPrzeciwnik.text = "";
        textPrzeciwnik.text += "Bohater: " + p2.hero.name;
        textPrzeciwnik.text += "\nSiła:  " + p2.hero.strength;
        textPrzeciwnik.text += "\nHP: " + p2.current_health + "/" + p2.total_health;
    }
    public static void clearPlayerPanelView(List<GameObject> list)
    {
        foreach (GameObject go in list)
        {
            Destroy(go);
        }
        list.Clear();
    }
    public void OK_Button()
    {
        combatDiceBlock = false;
        toggleCombatPanel();
        
        var g1 = GameObject.Find("SpecialEvents").GetComponent<SpecialFields>();
        var g2 = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        if (Guard)
        {
            g1.StraznikMessage(this.combatWin);
        }
        else if(TajemneWrota)
        {
            g1.TajemneWrotaMessage(this.combatWin);
        }
        else
        {
            g2.nextTurnButton.gameObject.SetActive(true);
        }
    }
    public void SpellOK_Button()
    {
        player1SpellsDone = true;
        if (player2 != null)
        {
            if (player2SpellsDone)
            {
               
                StartCoroutine(rozpocznijWalke());
            }
            else
            {

                toggleSpellPanel(player1);
                toggleSpellPanel(player2);
                player2SpellsDone = true;
            }
            
            
        }
        else
        {
            StartCoroutine(rozpocznijWalke());
        }
        
    }
    public void Atakuj_Button()
    {
        textPrzebieg.text += "Zaatakowałeś przeciwnika!" + "\n";
        toggleEnterCombatPanel();
        if(player1==player2)
        {
            StartCoroutine(rozpocznijWalke());
        }
        else
        {
            toggleSpellPanel(player1);
            var sl = GameObject.Find("PanelZaklecWalki").GetComponent<SpellListener>();
            sl.opponentDMG = 0;
            sl.playerDMG = 0;
        }
        
        //CardDrawer.spawnPlayerSpells(player1, "PanelZaklecWalki");


    }
    public void Wymykanie_Button()
    {
        wymykanie();
    }
}
