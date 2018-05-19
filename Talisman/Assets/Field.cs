using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field {

    public GameObject emptyGameObject;
    public int counter = 0;
    public Card fieldEvent;
    public List<Card> cardsOnField;
    public Field()
    {
        cardsOnField = new List<Card>();
    }
}
