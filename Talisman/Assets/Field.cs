using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field {

    public GameObject emptyGameObject;
    public int counter = 0;
    public Card fieldEvent;
    //obraz / id_obrazu
    //opis
    private string fieldDescription;

    public string get_fieldDescription()
    {
        return fieldDescription;
    }
    public void set_fieldDescription(string desc)
    {
        this.fieldDescription = desc;
    }

    public Field()
    {
        this.fieldDescription = "";
    }
}
