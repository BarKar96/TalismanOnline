using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class ItemsListener : MonoBehaviour
{
    public void useItem(string name)
    {
        var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        Player[] tempPlayerArray = go.playerArray;
        int tempPlayerIndex = go.playerIndex;
        Deck deck = go.deckOfCards;
        Card c = deck.fullDeck.Find(x => x.getName().Equals("zbroja"));
        if (c.getName().Equals("zbroja"))
        {
            Debug.Log(tempPlayerArray[tempPlayerIndex].current_health);
            tempPlayerArray[tempPlayerIndex].current_health += c.getSpecialCardEvents()[0].get_roll()[0];
            Debug.Log(tempPlayerArray[tempPlayerIndex].current_health);
        }
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
