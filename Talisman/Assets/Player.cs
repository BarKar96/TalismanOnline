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
    private int gold;
    private List<Card> cards;
    private int total_health;
    private int current_health;
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
        this.total_health = this.current_health= this.hero.hp;
        this.cards = new List<Card>();
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
