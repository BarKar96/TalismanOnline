using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public  class Combat : MonoBehaviour
{
    TalismanBoardScript tal;

    Player p;
    Card c;

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


    public Button ButtonOK;
    public TextMeshProUGUI textGracz;
    public TextMeshProUGUI textPrzeciwnik;
    public TextMeshProUGUI textPrzebieg;

    public bool atakujButton = false;
    public bool wymknijButton = false;

    public void toggleEnterCombatPanel()
    {
        
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
    public void toggleSpellPanel()
    {
        _subPanel_Spell_Opened = !_subPanel_Spell_Opened;
        setSubPanelVisibility(subPanel_Spell, _subPanel_Spell_Opened);
        if (_subPanel_Spell_Opened == true)
        {
            CardDrawer.spawnPlayerSpells(p, "PanelZaklecWalki");
        }
        else
        {
            clearPlayerPanelView(CardDrawer.spellsListCombat);
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
        this.p = p;
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
    public IEnumerator rzuty()
    {
        var script = GameObject.Find("D6").GetComponent<DiceScript>();
        var dice = GameObject.Find("D6");
        dice.gameObject.transform.localPosition = new Vector3(-100,0,0);
        script.roll();
        //czekaj az spadnie 1sza kostka
        yield return new WaitForSeconds(5);
        script.enabled = true;
        turaGracza = false;
        turaIstoty = true;
        script.roll();
    }
   

    public IEnumerator rozpocznijWalke()
    {
        var spellListener = GameObject.Find("PanelZaklecWalki").GetComponent<SpellListener>();

        int spellDMG = spellListener.playerDMG;
        toggleSpellPanel();
        turaGracza = true;
        turaIstoty = false;
        spawnCombatCards(p, c);
       // var script = GameObject.Find("D6").GetComponent<DiceScript>();

       
        StartCoroutine(rzuty());

        //czekaj az spadnie druga kostka
        yield return new WaitForSeconds(10);
        //rzut ataku gracza
        
        textPrzebieg.text += "Gracz wyrzucił: " + rzut_Ataku_gracza;
        skutecznosc_Ataku_gracza = rzut_Ataku_gracza + p.strength + spellDMG;
        textPrzebieg.text += "\nSkuteczność ataku gracza: " + skutecznosc_Ataku_gracza ;

        //rzut ataku istoty
        textPrzebieg.text += "\n\nPrzeciwnik wyrzucił: " + rzut_Ataku_istoty;
        skutecznosc_Ataku_istoty = rzut_Ataku_istoty + c.strength;
        textPrzebieg.text += "\nSkuteczność ataku przeciwnika: " + skutecznosc_Ataku_istoty;

        if (skutecznosc_Ataku_gracza > skutecznosc_Ataku_istoty)
        {
            //istota zostala pokonana wyczyszczenie pola planszy z tej kart;
            textPrzebieg.text += "\n\nPrzeciwnik pokonany! ";

        }
        else if (skutecznosc_Ataku_gracza < skutecznosc_Ataku_istoty)
        {
            textPrzebieg.text += "\n\nPonosisz porażkę! ";
            p.current_health--;

        }
        else if (skutecznosc_Ataku_gracza == skutecznosc_Ataku_istoty)
        {
            textPrzebieg.text += "\n\nRemis! ";
            //istota nie zostaje pokonana
        }
        ButtonOK.gameObject.SetActive(true);
    }
    public void wymykanie()
    {
        System.Random rnd = new System.Random();
        int x = rnd.Next(1, 6);
        Debug.Log(x);
        if (x>3)
        {
            subPanel_enterCombat.SetActive(false);
            tal.buttonSetNextTurnButtonOn();
        }
        else
        {
            toggleSpellPanel();
            textPrzebieg.text += "Ucieczka nie powiodła się!" + "\n";
            toggleEnterCombatPanel();
        }
    }

    public  int rzutAtaku()
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
        go.transform.localScale = new Vector3(2.2f, 0.3f, 2.6f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.transform.SetParent(GameObject.Find("PanelWalki").transform);
        go.transform.localPosition = new Vector3(-300, 100, -2);
        go.GetComponent<Renderer>().material = newMat;

        //karta przeciwnika
        GameObject go1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat1 = Resources.Load(c.getName(), typeof(Material)) as Material;
        go1.transform.localScale = new Vector3(2.2f, 0.3f, 2.6f);
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
        toggleCombatPanel();
    }
    public void SpellOK_Button()
    {
        
        StartCoroutine(rozpocznijWalke());
    }
    public void Atakuj_Button()
    {
        textPrzebieg.text += "Zaatakowałeś przeciwnika!" + "\n";
        toggleEnterCombatPanel();
        toggleSpellPanel();
        CardDrawer.spawnPlayerSpells(p, "PanelZaklecWalki");

       
    }
    public void Wymykanie_Button()
    {
        wymykanie();
    }
}
