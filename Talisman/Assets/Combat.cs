using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class Combat : MonoBehaviour
{


    public List<GameObject> combatCardList = new List<GameObject>();
    public int rzut_Ataku_gracza = 0;
    public  int skutecznosc_Ataku_gracza = 0;

    public  int rzut_Ataku_istoty = 0;
    public  int skutecznosc_Ataku_istoty = 0;

    public GameObject subPanel_Combat;
    private bool _subPanel_Combat_Opened = false;


    public Text textGracz;
    public Text textPrzeciwnik;
    public Text textPrzebieg;

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
    public void setSubPanelVisibility(GameObject subPanel, bool b)
    {
        if (subPanel != null)
        {
            subPanel.SetActive(b);
        }


    }
    public  int StartCombat(Player p, Card c)
    {
        //wyswietlanie teksturek
        spawnCombatCards(p, c);

        //wymykanie sie

        //zaklecia

        //rzut ataku gracza
        textPrzebieg.text = "";
        rzut_Ataku_gracza = rzutAtaku();
        textPrzebieg.text += "Gracz wyrzucił: " + rzut_Ataku_gracza;
        skutecznosc_Ataku_gracza = rzut_Ataku_gracza + p.strength;
        textPrzebieg.text += "\nSkuteczność ataku gracza: " + skutecznosc_Ataku_gracza;

        //rzut ataku istoty
        rzut_Ataku_istoty = rzutAtaku();
        textPrzebieg.text += "\n\nPrzeciwnik wyrzucił: " + rzut_Ataku_istoty;
        skutecznosc_Ataku_istoty = rzut_Ataku_istoty + c.getStrength();
        textPrzebieg.text += "\nSkuteczność ataku przeciwnika: " + skutecznosc_Ataku_istoty;

        if (skutecznosc_Ataku_gracza > skutecznosc_Ataku_istoty)
        {
            //istota zostala pokonana wyczyszczenie pola planszy z tej kart;
            textPrzebieg.text += "\n\nPrzeciwnik pokonany! ";
            return 1;
        }
        else if (skutecznosc_Ataku_gracza < skutecznosc_Ataku_istoty)
        {
            textPrzebieg.text += "\n\nPonosisz porażkę! ";
            p.current_health--;
            return 2;
        }
        else if (skutecznosc_Ataku_gracza == skutecznosc_Ataku_istoty)
        {
            textPrzebieg.text += "\n\nRemis! ";
            //istota nie zostaje pokonana
            return 3;
        }
        else
        {
            return 0;
        }



        //porownanie skutecznosci ataku
    }
    public  void wymykanie()
    {

    }
    public  void uzyjZaklecia(Player p)
    {
        //CardDrawer.spawnPlayerSpells(p);
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
        go.transform.localScale = new Vector3(0.8f, 0.2f, 1.2f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.transform.SetParent(GameObject.Find("PanelWalki").transform);
        go.transform.localPosition = new Vector3(-300, 100, -2);
        go.GetComponent<Renderer>().material = newMat;

        //karta przeciwnika
        GameObject go1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat1 = Resources.Load(c.getName(), typeof(Material)) as Material;
        go1.transform.localScale = new Vector3(0.8f, 0.2f, 1.2f);
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
        textPrzeciwnik.text += "Bohater: " + "tomasz";
        textPrzeciwnik.text += "\nSiła:  ";
        textPrzeciwnik.text += "\nHP: ";


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
}
