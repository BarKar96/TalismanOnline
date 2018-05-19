using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class CardDrawer
{
    public static List<GameObject> itemsList = new List<GameObject>();
    public static List<GameObject> spellsList = new List<GameObject>();
    public static List<GameObject> heroCardList = new List<GameObject>();
   
    public static int counter;
  

    public static void spawnCard(string name,string panelName, int x, int y)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat = Resources.Load(name, typeof(Material)) as Material;
        go.transform.localScale = new Vector3(0.8f, 0.2f, 1.2f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.transform.SetParent(GameObject.Find(panelName).transform);
        Debug.Log(GameObject.Find(panelName).transform);
        go.transform.localPosition = new Vector3(-330+counter,100-y,-2);
        go.GetComponent<Renderer>().material = newMat;
        
        if (panelName == "PanelEkwipunku")
        {
            itemsList.Add(go);
        }
        if (panelName == "PanelZaklec")
        {
            spellsList.Add(go);
        }
       
        
    }

    public static void spawnPlayerItems(Player p)
    {
        CardDrawer.itemsList.Clear();
        counter = 0;
        int temp = 0;
        int help = 0;
        //////////////       
        foreach (Card c in p.getItems())
        {
            //if (c.equipable == true)
            //{
            //    var go = GameObject.Find("Slider1").GetComponent<Slider>();
            //    Debug.Log("znalazlem");
            //    go.gameObject.SetActive(true);
            //}
            if (temp > 3)
            {
                if (help == 0)
                {
                    counter = 0;
                    help = 1;
                }
                CardDrawer.spawnCard(c.getName(),"PanelEkwipunku", counter, 220);
                
                
            }
            else
            {
                CardDrawer.spawnCard(c.getName(), "PanelEkwipunku", counter, 0);
            }
            counter+=200;
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

    public static void spawnPlayerSpells(Player p)
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
                CardDrawer.spawnCard(c.getName(), "PanelZaklec", counter, 220);

            }
            else
            {
                CardDrawer.spawnCard(c.getName(), "PanelZaklec", counter, 0);
            }
            counter += 200;
            temp++;
        }
        //////////////
    }
   
}
