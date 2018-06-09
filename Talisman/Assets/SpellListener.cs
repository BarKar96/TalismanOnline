using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellListener : MonoBehaviour
{

    public Text text;
    public int playerDMG=0;
    public int opponentDMG=0;
   
    public void useSpell(string name)
    {
        var tbs = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        var temp = GameObject.Find("Combat").GetComponent<Combat>();
        
        Player[] tempPlayerArray = tbs.playerArray;
        int tempPlayerIndex = tbs.playerIndex;
        Deck deck = tbs.deckOfCards;
        Card c = deck.fullDeck.Find(x => x.getName().Equals(name));

        if (c != null)
        {
            if(!temp.player1SpellsDone)
            {
                playerDMG += c.strength;
                temp.player1.getSpells().Remove(c);
                temp.toggleSpellPanel(temp.player1);
                temp.toggleSpellPanel(temp.player1);
                temp.textPrzebieg.text += temp.player1.name + " użył zaklęcia: " + c.getName() + "\n";

            }
            else
            {
                opponentDMG += c.strength;
                temp.player2.getSpells().Remove(c);
                temp.toggleSpellPanel(temp.player2);
                temp.toggleSpellPanel(temp.player2);
                temp.textPrzebieg.text += temp.player2.name+" użył zaklęcia: " + c.getName() + "\n";
            }
            StartCoroutine(messager("Użyto zaklęcia: " + c.getName()));
           



        }
        
     
       
    }

    public IEnumerator messager(string message)
    {
        text.text = message;
        yield return new WaitForSeconds(1);
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.gameObject.name);
                useSpell(hit.transform.gameObject.name);
            }


        }
    }
}
