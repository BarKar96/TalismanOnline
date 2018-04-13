using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    string name;
    public Piece playerPiece;
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
        
        
    }

	
	
}
