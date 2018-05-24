using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets
{
    public class Deck
    {
        public List<Card> fullDeck;
        public List<Card> fullItemsDeck;
        public List<Card> fullSpellsDeck;
        public List<Card> fullEnemyDeck;


        private int cardsInDeck;
        private int cardsRemaining;
        private string[] possibleEvents = { "get_gold", "get_spell", "gain_power" };

        public Deck()
        {
            fullDeck = new List<Card>();
            fullItemsDeck = new List<Card>();
            fullSpellsDeck = new List<Card>();
            fullEnemyDeck = new List<Card>();
            loadFromFileAndParse();
            divideFullDeck();
        }
        public int getDeckSize()
        {
            return cardsInDeck;
        }
        public void divideFullDeck()
        {
            foreach (Card c in fullDeck)
            {
                if (c.getCard_Type() == card_type.ENEMY)
                {
                    fullEnemyDeck.Add(c);
                }
                if (c.getCard_Type() == card_type.SPELL)
                {
                    fullSpellsDeck.Add(c);
                }
                if (c.getCard_Type() == card_type.ITEM)
                {
                    fullItemsDeck.Add(c);
                }
            }
        }
        public Card drawCard()
        {
            System.Random rand = new System.Random();
            return fullDeck[rand.Next(0, fullDeck.Count)];
        }

        public event_type translateToEvent(string s)
        {
            switch (s)
            {
                case "get_gold":
                    return event_type.ADD_COIN;
                case "gain_health":
                    return event_type.GAIN_HEALTH;
                case "gain_strength":
                    return event_type.GAIN_STRENGTH;
                case "powr":
                    return event_type.POWER;
                case "strn":
                    return event_type.STRENGTH;
                default:
                    return event_type.ADD_COIN;
            }
        }
        public item_type translateToItemType(string s)
        {
            switch (s)
            {
                case "arm":
                    return item_type.ARMOR;
                case "wep":
                    return item_type.WEAPON;
                case "con":
                    return item_type.CONSUMABLE;
                default:
                    //  Had to throw something in
                    return item_type.CONSUMABLE;
            }
        }
        public card_type translateToType(string s)
        {
            switch (s)
            {               
                case "itm":
                    return card_type.ITEM;
                case "spl":
                    return card_type.SPELL;   
                case "enm":
                    return card_type.ENEMY;
                default:
                    return card_type.BOARDFIELD;
            }
        }
        public void loadFromFileAndParse()
        {
            String[] loaded = File.ReadAllLines("./Assets/Resources/deck_cards.txt");
            foreach (string s in loaded)
            {
                if (s.StartsWith("#"))
                    continue;
                Card newCard;
                string[] parsedRow = s.Split(':');
                int theseInDeck = int.Parse(parsedRow[0]);
                string cardname = parsedRow[2];
                List<event_type> readEvents = new List<event_type>();
                //  Add actions
                if (parsedRow[1].Equals("enm"))
                {
                    newCard = new Card(cardname, translateToType(parsedRow[1]), new event_type[] { event_type.ENEMY }, item_type.ENEMY);
                    for (int fields = 3; fields < parsedRow.Length; fields++)
                    {
                        if (parsedRow[fields].Equals("nospecial"))
                            break;
                        // Debug.Log("Enemy card attrs:" + parsedRow[fields]);
                        string[] attrs = parsedRow[fields].Split('@');
                        switch (attrs[0])
                        {
                            case "powr":
                                newCard.power = int.Parse(attrs[1]);
                                break;
                            case "strn":
                                newCard.strength = int.Parse(attrs[1]);
                                break;

                        }

                    }
                    fullDeck.Add(newCard);
                }
                else
                {
                    int begin = 3;
                    item_type type = item_type.CONSUMABLE;
                    if (parsedRow[1].Equals("itm"))
                    {
                        type = translateToItemType(parsedRow[3]);
                        begin = 4;
                    }
                        
                    for (int fields = begin; fields < parsedRow.Length; fields++)
                    {
                        //  Check what actions does the card have
                        if (possibleEvents.Contains(parsedRow[fields]))
                        {
                            readEvents.Add(translateToEvent(parsedRow[fields]));
                        }
                        //  If field is not special finish building
                        if (parsedRow[fields].Equals("nospecial"))
                        {
                            if(parsedRow[1].Equals("itm"))
                                newCard = new Card(cardname, translateToType(parsedRow[1]), readEvents, type);
                            else
                                newCard = new Card(cardname, translateToType(parsedRow[1]), readEvents);
                            for (int times = 0; times < theseInDeck; times++)
                            {
                                fullDeck.Add(newCard);
                            }
                        }
                        else if (parsedRow[fields].Equals("special"))
                        {
                            int current = fields + 1;
                            if (parsedRow[1].Equals("itm"))
                                newCard = new Card(cardname, translateToType(parsedRow[1]), readEvents, type);
                            else
                                newCard = new Card(cardname, translateToType(parsedRow[1]), readEvents);
                            for (; current < parsedRow.Length; current++)
                            {
                                string[] ev_roll = parsedRow[current].Split('@');
                                newCard.AssignSpecial(new int[] { int.Parse(ev_roll[1]) }, translateToEvent(ev_roll[0]));
                            }
                            for (int times = 0; times < theseInDeck; times++)
                            {
                                fullDeck.Add(newCard);
                            }
                        }
                    }
            }
            }
            listCards();
        }
        public void listCards()
        {
            Debug.Log("Cards in deck: " + fullDeck.Count());
            foreach(Card c in fullDeck)
            {
                switch (c.getCard_Type())
                {
                    case card_type.ENEMY:
                        Debug.Log("Enemy : " +c.getName()+c);
                        c.describeSpecial();
                        break;
                    case card_type.ITEM:
                        Debug.Log("Item : " + c.getName());
                        break;
                    case card_type.SPELL:
                        Debug.Log("Spell : " + c.getName());
                        break;
                    default:
                        Debug.Log("Other? : " + c.getName());
                        break;
                }
                
            } 

        }
    }
}
