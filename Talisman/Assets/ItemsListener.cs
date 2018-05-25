using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.UI;

public class ItemsListener : MonoBehaviour
{
    public Text text;
    public void useItem(string name)
    {
        var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        Player[] tempPlayerArray = go.playerArray;
        int tempPlayerIndex = go.playerIndex;
        Deck deck = go.deckOfCards;
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
                go.Items_Button();
                go.Items_Button();
                StartCoroutine(messager("użyto przedmiotu: " + c.getName()));               
            }
        }
        else if (c.itemType == item_type.WEAPON)
        {
            
            tempPlayerArray[tempPlayerIndex].strength_modifier = c.getSpecialCardEvents()[0].get_roll()[0];
            StartCoroutine(messager("założono broń: " + c.getName()));
        }
        else if (c.itemType == item_type.ARMOR)
        {
            tempPlayerArray[tempPlayerIndex].health_modifier = c.getSpecialCardEvents()[0].get_roll()[0];
            StartCoroutine(messager("założono zbroję: " + c.getName()));
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
    }
}
