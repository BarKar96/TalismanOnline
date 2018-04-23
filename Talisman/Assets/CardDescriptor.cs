using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class CardDescriptor
    {
      
        public List<Card> allCardsList;
        public CardDescriptor()
        {
            allCardsList = new List<Card>();
            addCard("krasnolud", card_type.ENEMY);


        }
        public void addCard(string name, card_type c_t)
        {
            Card c = new Card(name, c_t, null);
            allCardsList.Add(c);
        }


    }
}