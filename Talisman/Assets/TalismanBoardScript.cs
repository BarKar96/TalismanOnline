using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TalismanBoardScript : MonoBehaviour {

    Field[] outerRing;
    Field[] middleRing;
    Field[] innerRing;

    Piece[] pieceArray;
    Player[] playerArray;

    public GameObject piecePrefab;
    public int playersCounter;

    private void initializePlayer()
    {
        playerArray = new Player[playersCounter];
        for (int i=0; i<playersCounter; i++)
        {
            playerArray[i] = new Player();
            
        }
    }

    private int rollDice()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(1,6);
    }

    private void fillFields()
    {
        outerRing = new Field[24]; 
        for (int i=0; i<24; i++)
        {
            
            outerRing[i] = new Field();
            outerRing[i].emptyGameObject =  transform.GetChild(i+1).gameObject;
           
        }
    }

    private void movePiece(int indexOfPieceToMove, int indexOfFieldToMoveOn)
    {
       pieceArray[indexOfPieceToMove].transform.position = outerRing[indexOfFieldToMoveOn].emptyGameObject.transform.position;
    }


    private void GenerateBoard()
    {
        playersCounter = 1;
        pieceArray = new Piece[playersCounter];
        for (int i=0; i<playersCounter; i++)
        {
            pieceArray[i] = new Piece();
            GeneratePiece(i);
            initializePlayer();
        }
        fillFields(); 
    }

    private Piece GeneratePiece(int i)
    {
        GameObject go = Instantiate(piecePrefab) as GameObject;
        go.transform.SetParent(transform);
        Piece p = go.GetComponent<Piece>();
        pieceArray[i] = p;
        MovePieceToStartLocation(p, i);
        return p;
    }

    //do nadpisania wg regul gry
    private void MovePieceToStartLocation(Piece p, int i)
    {
       pieceArray[i].transform.position = transform.GetChild(i+1).gameObject.transform.position;
       p.indexOfField = i;
    }
    public void Button_Click()
    {
        int x = rollDice();
        Debug.Log(x);
        int y = pieceArray[0].indexOfField;
        int whereToMove = (x + y) % 24;
        movePiece(0,whereToMove);
        pieceArray[0].indexOfField = whereToMove;

    }
    void Start ()
    {
        GenerateBoard();
        
	}

	void Update ()
    {
	
	}
}
