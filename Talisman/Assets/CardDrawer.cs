using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class CardDrawer
{
    public static List<GameObject> komunikatList = new List<GameObject>();
    public static List<GameObject> itemsList = new List<GameObject>();
    public static List<GameObject> spellsList = new List<GameObject>();
    public static List<GameObject> spellsListCombat = new List<GameObject>();
    public static List<GameObject> heroCardList = new List<GameObject>();
    public static int counter;
    public static GameObject panelMessage;

    public static void spawnCard(string name,string panelName, int x, int y)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat = Resources.Load(name, typeof(Material)) as Material;
        go.transform.localScale = new Vector3(1.3f, 0.4f, 2f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.name = name;
        //Rigidbody gameObjectsRigidBody = go.AddComponent<Rigidbody>();
        go.transform.SetParent(GameObject.Find(panelName).transform);
        //Debug.Log(GameObject.Find(panelName).transform);
        go.transform.localPosition = new Vector3(-330+counter,120-y,-2);
        go.GetComponent<Renderer>().material = newMat;
        
        if (panelName == "PanelEkwipunku")
        {
            itemsList.Add(go);
        }
        if (panelName == "PanelZaklec")
        {
            spellsList.Add(go);
        }
        if (panelName == "PanelZaklecWalki")
        {
            spellsListCombat.Add(go);
        }
        if (panelName == "Komunikat")
        {
            komunikatList.Add(go);
         
            go.transform.localPosition = new Vector3(0,25,-2);
        }


    }

    public static void spawnPlayerItems(Player p, string name)
    {
        CardDrawer.itemsList.Clear();
        counter = 0;
        int temp = 0;
        int help = 0;
        var q = GameObject.Find("PanelEkwipunku").GetComponent<ItemsListener>();
        if (p.weapon == null)
        {
            q.bron.text = "Założona broń: \n";
        }
        else
        {
            q.bron.text = "Założona broń: \n" + p.weapon.getName();
        }
        if (p.armor == null)
        {
            q.zbroja.text = "Założony pancerz: \n";
        }
        else
        {
            q.zbroja.text = "Założony pancerz: \n" + p.armor.getName();
        }
     
        //////////////       
        foreach (Card c in p.getItems())
        {
            if (temp > 3)
            {
                if (help == 0)
                {
                    counter = 0;
                    help = 1;
                }
                CardDrawer.spawnCard(c.getName(),name, counter, 240);
                
                
            }
            else
            {
                CardDrawer.spawnCard(c.getName(), name, counter, 0);

            }
            counter+=150;
            temp++;
        }
        //////////////
    }
    public static void spawnPlayerHeroCard(string name)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat = Resources.Load(name, typeof(Material)) as Material;
        go.transform.localScale = new Vector3(1, 0.10f, 1f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.GetComponent<Renderer>().material = newMat;
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        go.transform.localPosition = new Vector3(-130, 31, -31);
        heroCardList.Add(go);
    }

    public static void spawnPlayerSpells(Player p, string name)
    {
        CardDrawer.spellsList.Clear();
        counter = 0;
        int temp = 0;
        int help = 0;
        //////////////
        //Debug.Log(p.getSpells().Count);
        foreach (Card c in p.getSpells())
        {
            
            if (temp > 3)
            {
                if (help == 0)
                {
                    counter = 0;
                    help = 1;
                }
                CardDrawer.spawnCard(c.getName(), name, counter, 220);

            }
            else
            {
                CardDrawer.spawnCard(c.getName(), name, counter, 0);
            }
            counter += 200;
            temp++;
        }
        //////////////
    }
    public static void spawnMessage(Card c)
    {
        var q = GameObject.Find("Canvas");
        var tb = FindObject(q, "Komunikat");
        tb.gameObject.SetActive(true);
        spawnCard(c.getName(), "Komunikat", 0, 0);
    }

    public static GameObject FindObject(this GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

}
