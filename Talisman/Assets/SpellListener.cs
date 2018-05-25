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
            playerDMG += 10;
            tempPlayerArray[tempPlayerIndex].getSpells().Remove(c);
            temp.toggleSpellPanel();
            temp.toggleSpellPanel();
            StartCoroutine(messager("Użyto zaklęcia: " + c.getName()));
            temp.textPrzebieg.text += "Użyto zaklęcia: " + c.getName() + "\n"; 


        }
        
     
       
    }

    public IEnumerator messager(string message)
    {
        text.text = message;
        yield return new WaitForSeconds(2);
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
