using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class CardDrawer
{
    public static List<GameObject> itemsList = new List<GameObject>();
    public static List<GameObject> heroCardList = new List<GameObject>();
    public static int counter;
    public static void spawnCard(string name, int x)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat = Resources.Load(name, typeof(Material)) as Material;
        go.transform.localScale = new Vector3(0.2f, 0.1f, 0.3f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.transform.SetParent(GameObject.Find("SpellItemBoard").transform);
        go.transform.localPosition = new Vector3(0, 2, 0);
        go.GetComponent<Renderer>().material = newMat;
        
        itemsList.Add(go);
    }

    public static void spawnPlayerItems(Player p)
    {
        CardDrawer.itemsList.Clear();
        counter = 0;
        //p.getItems().Add(new Card(card_type.MAGIC_ITEM, new event_type[] { event_type.GAIN_HEALTH }));
        //////////////       
        foreach (Card c in p.getItems())
        {
            CardDrawer.spawnCard("smok", counter);
            counter++;
        }
        //////////////
    }
    public static void spawnPlayerHeroCard(string name)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat = Resources.Load(name, typeof(Material)) as Material;
        go.transform.localScale = new Vector3(0.3f, 0.10f, 0.4f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.transform.position = new Vector3(-12.5f, 0, 2.9f);
        go.GetComponent<Renderer>().material = newMat;
        heroCardList.Add(go);
    }

}
