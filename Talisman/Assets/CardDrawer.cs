using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class CardDrawer
{
    public static List<GameObject> itemsList = new List<GameObject>();
    public static int counter;
    public static void spawnCard(string name, int x)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat = Resources.Load(name, typeof(Material)) as Material;
        go.transform.localScale = new Vector3(0.2f, 0.1f, 0.3f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.transform.position = new Vector3(7 + x * 2, 0, 0);
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

}
