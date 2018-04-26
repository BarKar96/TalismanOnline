using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string name;
    public Piece playerPiece;
    //Control event on field
    public Card boardField;
    //Control occuring event on card draw
    private Card eventCard;
    //Player item inventory
    private Deck deck;


    public int gold;
    private List<Card> cards;
    private List<Card> items;
    private List<Card> spells;
    public int total_health;
    public int current_health;
    public bool outerRing { get; set; }
    public bool middleRing { get; set; }
    public bool innerRing { get; set; }
    public Hero hero;
    public int diceResult;
    public bool fieldCheckedOut = false;

    public Player(string name, Hero hero)
    {
        this.name = name;
        this.outerRing = true;
        this.middleRing = false;
        this.innerRing = false;
        this.hero = hero;
        this.total_health = this.current_health = this.hero.hp;
        this.cards = new List<Card>(5);
        this.items = new List<Card>();
        this.spells = new List<Card>();
        this.deck = new Deck();
    }
    public void rollDice()
    {
        System.Random rnd = new System.Random();
        diceResult = rnd.Next(1, 6);
    }
    public void iterate_cards()
    {
        checkoutCards();
    }

    public void checkoutCards()
    {
        int size = this.cards.Count;
        int current = 0;
        if (!fieldCheckedOut)
        {
            
            if (boardField.isSpecialField())
            {
                Debug.Log("Player has dice roll of: " + diceResult);
                boardField.specialAction(this);
            }
            //  Player can't checkout the same field on next turn
            //fieldCheckedOut = true;
        }

        while (current != size)
        {
            foreach (event_type et in this.cards[current].getEvents())
            {
                switch (et)
                {
                    case event_type.ADD_COIN:
                        this.gold++;
                        break;
                    case event_type.LOSE_HEALTH:
                        this.current_health--;
                        break;
                    case event_type.GAIN_HEALTH:
                        this.current_health++;
                        break;
                    case event_type.ROLL_DICE:
                        Debug.Log("Player " + hero.name + " draws card");
                        this.rollDice();
                        break;

                    case event_type.DRAW_CARD:
                        
                        Card c = deck.drawCard();
                        Debug.Log("draw " + this.name +c.getName());
                        this.items.Add(c);
                       
                        break;
                }
            }
            current++;
        }
    }

    public List<Card> getCards()
    {
        return cards;
    }
    public List<Card> getItems()
    {
        return items;
    }
    public List<Card> getSpells()
    {
        return spells;
    }
    public void Turn()
    {
        //  BoardField.actOnPlayer();
        //  IteratePlayerCards()
        //  EndTurn();
    }

}
