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
    public Player(string name)
    {
        this.name = name;
        this.outerRing = true;
        this.middleRing = false;
        this.innerRing = false;
        
        
    }

	
	
}
