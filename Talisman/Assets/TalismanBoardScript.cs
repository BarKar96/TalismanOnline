using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TalismanBoardScript : MonoBehaviour {

    Field[] outerRing;
    Field[] middleRing;
    Field[] innerRing;

    Player[] playerArray;

    private int playerIndex;

    public GameObject piecePrefab;
    public int playersCounter;


    private void initializePlayers()
    {
        playerIndex = 0;
        playersCounter = 2;
        playerArray = new Player[playersCounter];
        for (int i=0; i<playersCounter; i++)
        {
            playerArray[i] = new Player("bartek");
            GeneratePiece(i);
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

    private void movePiece(int indexOfPlayer, int indexOfFieldToMoveOn)
    {
      playerArray[indexOfPlayer].playerPiece.transform.position = outerRing[indexOfFieldToMoveOn].emptyGameObject.transform.position;
    }


    private void GenerateBoard()
    {
        initializePlayers();
        fillFields(); 
    }

    private Piece GeneratePiece(int i)
    {
        GameObject go = Instantiate(piecePrefab) as GameObject;
        go.transform.SetParent(transform);
        Piece p = go.GetComponent<Piece>();
        playerArray[i].playerPiece = p;
        MovePieceToStartLocation(p, i);
        return p;
    }

    //do nadpisania wg regul gry
    private void MovePieceToStartLocation(Piece p, int i)
    {
       playerArray[i].playerPiece.transform.position = transform.GetChild(i+1).gameObject.transform.position;
       p.indexOfField = i;
    }
    public void Button_Click()
    {
        int x = rollDice();
        Debug.Log(x);
        int y = playerArray[playerIndex].playerPiece.indexOfField;
        int whereToMove = (x + y) % 24;




        movePiece(playerIndex,whereToMove);
        playerArray[playerIndex].playerPiece.indexOfField = whereToMove;
        playerIndex++;
        if (playerIndex == playersCounter)
        {
            playerIndex = 0;
        }

    }
    void Start ()
    {
        GenerateBoard();
        
	}

	void Update ()
    {
	
	}
}
