﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets
{
    class Deck
    {
        List<Card> fullDeck;
        private int cardsInDeck;
        private int cardsRemaining;
        private string[] possibleEvents = { "get_gold", "get_spell", "gain_power" };

        public Deck()
        {
            fullDeck = new List<Card>();
        }
        public int getDeckSize()
        {
            return cardsInDeck;
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
                case "get_spell":
                    return event_type.GAIN_HEALTH;
                case "gain_power":
                    return event_type.LOSE_HEALTH;
                default:
                    return event_type.ADD_COIN;
            }
        }
        public card_type translateToType(string s)
        {
            switch (s)
            {
                case "str":
                    return card_type.FRIEND;
                case "itm":
                    return card_type.ITEM;
                case "frn":
                    return card_type.FRIEND;
                case "enm":
                    return card_type.ENEMY;
                default:
                    return card_type.BOARDFIELD;
            }
        }
        public void loadFromFileAndParse()
        {
            String[] loaded = File.ReadAllLines("./Assets/Resources/deck_cards.txt");
            foreach(string s in loaded)
            {
                if (s.StartsWith("#"))
                    continue;
                Card newCard;
                string[] parsedRow = s.Split(':');
                int theseInDeck = int.Parse(parsedRow[0]);
                string cardname = parsedRow[2];
                List<event_type> readEvents = new List<event_type>();
                //  Add actions
                for (int fields = 3; fields < parsedRow.Length; fields++)
                {
                    //  Check what actions does the card have
                    if (possibleEvents.Contains(parsedRow[fields]))
                    {
                        readEvents.Add(translateToEvent(parsedRow[fields]));
                    }
                    //  If field is not special finish building
                    if (parsedRow[fields].Equals("nospecial"))
                    {
                        newCard = new Card(cardname, translateToType(parsedRow[1]), readEvents);
                        for (int times = 0; times < theseInDeck; times++)
                        {
                            fullDeck.Add(newCard);
                        }
                    }else if (parsedRow[fields].Equals("special"))
                    {
                        int current = fields + 1;
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
        public void listCards()
        {
            Debug.Log("Cards in deck: " + fullDeck.Count());
            foreach(Card c in fullDeck)
            {
                Debug.Log(c.getName());
            }
        }
    }
}
