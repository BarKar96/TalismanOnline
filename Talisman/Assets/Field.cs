﻿using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field {

    public GameObject emptyGameObject;
    public int counter = 0;
    private int current_position;
    public int get_currentposition() { return current_position; }
    public Card fieldEvent;
    //obraz / id_obrazu
    //opis


    public Field()
    {

    }
}
