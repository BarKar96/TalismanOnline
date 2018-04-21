using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    string name;
    public Piece playerPiece;
    //Control event on field
    private Card boardField;
    //Control occuring event on card draw
    private Card eventCard;
    //Player item inventory
    private Card[] items;
    public int gold;
    private List<Card> cards;
    public int total_health;
    public int current_health;
    public bool outerRing { get; set; }
    public bool middleRing { get; set; }
    public bool innerRing { get; set; }
    public Hero hero;
    public int diceResult;

    public Player(string name, Hero hero)
    {
        this.name = name;
        this.outerRing = true;
        this.middleRing = false;
        this.innerRing = false;
        this.hero = hero;
        this.total_health = this.current_health = this.hero.hp;
        this.cards = new List<Card>();
    }
    public void rollDice()
    {
        System.Random rnd = new System.Random();     
        diceResult = rnd.Next(1, 6);
    }
    public void iterate_cards()
    {
        /*foreach (Card C in cards)
        {
            Debug.Log("at least made it here");
            C.iterateEvents(this);
        }*/
        checkoutCards();
    }

    public void checkoutCards()
    {
        int size = this.cards.Count;
        int current = 0;
        while(current != size)
        {
            foreach(event_type et in this.cards[current].getEvents())
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
                        this.rollDice();
                        Debug.Log("rzut kostka z eventu" + this.diceResult);
                        break;
                    case event_type.DRAW_CARD:
                        //  Throws Error
                        this.cards.Add(new Card(card_type.ITEM, new event_type[] { event_type.LOSE_HEALTH}));
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
    public void Turn()
    {
        //  BoardField.actOnPlayer();
        //  IteratePlayerCards()
        //  EndTurn();
    }
	
}
