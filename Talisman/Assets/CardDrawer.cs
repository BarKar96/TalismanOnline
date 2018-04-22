using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardDrawer
{

    public void spawnCard(string name,int x)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Material newMat = Resources.Load(name, typeof(Material)) as Material;
        go.transform.localScale = new Vector3(0.2f, 0.1f, 0.3f);
        go.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        go.transform.position = new Vector3(x, 0, 0);
        go.GetComponent<Renderer>().material = newMat;
    }
   
}
