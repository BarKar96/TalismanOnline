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
    public Player(string name, Hero hero)
    {
        this.name = name;
        this.outerRing = false;
        this.middleRing = true;
        this.innerRing = false;
        this.hero = hero;
        this.total_health = this.current_health = this.hero.hp;
        this.cards = new List<Card>();
    }
    public void iterate_cards()
    {
        foreach (Card C in cards)
        {
            C.iterateEvents(this);
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
