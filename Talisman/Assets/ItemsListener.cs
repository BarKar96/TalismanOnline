﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.UI;

public class ItemsListener : MonoBehaviour
{
    public Text text;
    public Text bron;
    public Text zbroja;
    public void useItem(string name)
    {
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        var windows = GameObject.Find("Windows").GetComponent<Windows>();
        Player[] tempPlayerArray = tbs.playerArray;
        int tempPlayerIndex = tbs.playerIndex;
        Deck deck = tbs.deckOfCards;
        Card c = deck.fullDeck.Find(x => x.getName().Equals(name));
        Debug.Log(tempPlayerArray[tempPlayerIndex].current_health + " " + tempPlayerArray[tempPlayerIndex].strength);
        if (c == null)
        {

        }
        else if (c.itemType == item_type.CONSUMABLE)
        {
            if (!(tempPlayerArray[tempPlayerIndex].current_health == tempPlayerArray[tempPlayerIndex].total_health))
            {
                tempPlayerArray[tempPlayerIndex].current_health += c.getSpecialCardEvents()[0].get_roll()[0];
                tempPlayerArray[tempPlayerIndex].getItems().Remove(c);
                tbs.Items_Button();
                tbs.Items_Button();
              
                StartCoroutine(messager("użyto przedmiotu: " + c.getName()));   
                
                
            }
        }
        else if (c.itemType == item_type.WEAPON)
        {
            
            tempPlayerArray[tempPlayerIndex].strength_modifier = c.getSpecialCardEvents()[0].get_roll()[0];
            bron.text = "Założona broń: \n" + c.getName();
            StartCoroutine(messager("założono broń: " + c.getName()));

           

        }
        else if (c.itemType == item_type.ARMOR)
        {
            tempPlayerArray[tempPlayerIndex].health_modifier = c.getSpecialCardEvents()[0].get_roll()[0];
            zbroja.text = "Założony pancerz: \n"+ c.getName();
            StartCoroutine(messager("założono zbroję: " + c.getName()));
            
        }
        windows.UpdateStatsToText(tempPlayerArray);
    }
    public void detachItem(string name)
    {
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        var windows = GameObject.Find("Windows").GetComponent<Windows>();
        Player[] tempPlayerArray = tbs.playerArray;
        int tempPlayerIndex = tbs.playerIndex;
        Deck deck = tbs.deckOfCards;
        Card c = deck.fullDeck.Find(x => x.getName().Equals(name));
        if (c != null)
        {
            if (c.itemType == item_type.WEAPON)
            {

                tempPlayerArray[tempPlayerIndex].strength_modifier = 0;
                StartCoroutine(messager("zdjęto broń: " + c.getName()));
                bron.text = "Założona broń: \n";
            }
            else if (c.itemType == item_type.ARMOR)
            {
                tempPlayerArray[tempPlayerIndex].health_modifier = 0;
                StartCoroutine(messager("zdjęto zbroję: " + c.getName()));
                zbroja.text = "Założony pancerz: \n";
            }
            windows.UpdateStatsToText(tempPlayerArray);
        }
        
    }
    public void dropItem(string name)
    {
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        var windows = GameObject.Find("Windows").GetComponent<Windows>();
        Player[] tempPlayerArray = tbs.playerArray;
        int tempPlayerIndex = tbs.playerIndex;
        Deck deck = tbs.deckOfCards;
        Card c = deck.fullDeck.Find(x => x.getName().Equals(name));
        if (c != null)
        {
            tempPlayerArray[tempPlayerIndex].getItems().Remove(c);
            tbs.Items_Button();
            tbs.Items_Button();
            StartCoroutine(messager("odrzucono przedmiot: " + c.getName()));
        }
    }
    public IEnumerator messager(string message)
    {
        text.text = message;
        yield return new WaitForSeconds(2);
        text.text = "";
    }

    // Update is called once per frame
    void Update ()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.gameObject.name);
                useItem(hit.transform.gameObject.name);
            }
           
                
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.gameObject.name);
                detachItem(hit.transform.gameObject.name);
            }
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.gameObject.name);
                dropItem(hit.transform.gameObject.name);
            }
        }
    }
}
