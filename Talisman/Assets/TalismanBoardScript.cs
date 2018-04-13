using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TalismanBoardScript : MonoBehaviour {

    private Field[] outerRing;
    private Field[] middleRing;
    private Field[] innerRing;

    private Player[] playerArray;

    private int playerIndex;

    public GameObject piecePrefab;
    private int playersCounter;

    private int diceResult;
/// <summary>
/// //////////////////////////////////////////////////////////////
/// </summary>


    private void initializePlayers()
    {
        playerIndex = 0;
        playersCounter = 2;
        playerArray = new Player[playersCounter];
       
       
        for (int i=0; i<playersCounter; i++)
        {
            Hero hero = new Hero(i);
            playerArray[i] = new Player("bartek", hero);
           
            GeneratePiece(i);
        }
 
    }

    private int rollDice()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(1,6);
        //return 1;
    }

    private void fillFields()
    {
        outerRing = new Field[24];
        middleRing = new Field[16];
        innerRing = new Field[8];

        for (int i = 0; i < 24; i++)
        {

            outerRing[i] = new Field();
            outerRing[i].emptyGameObject = transform.GetChild(i + 1).gameObject;
            //print(outerRing[i].emptyGameObject.name);

        }
        for (int i = 0; i < 16; i++)
        {
            middleRing[i] = new Field();
            middleRing[i].emptyGameObject = transform.GetChild(i + 25).gameObject;
            //print(middleRing[i].emptyGameObject.name);

        }
        for (int i = 0; i < 8; i++)
        {
            innerRing[i] = new Field();
            innerRing[i].emptyGameObject = transform.GetChild(i + 41).gameObject;
            //print(innerRing[i].emptyGameObject.name);
        }
    }

    private void movePiece(int indexOfPlayer, int indexOfFieldToMoveOn)
    {
        if (playerArray[indexOfPlayer].outerRing == true)
        {
            playerArray[indexOfPlayer].playerPiece.transform.position = outerRing[indexOfFieldToMoveOn].emptyGameObject.transform.position;
        }
        if (playerArray[indexOfPlayer].middleRing == true)
        {
            playerArray[indexOfPlayer].playerPiece.transform.position = middleRing[indexOfFieldToMoveOn].emptyGameObject.transform.position;
        }
        if (playerArray[indexOfPlayer].innerRing == true)
        {
            playerArray[indexOfPlayer].playerPiece.transform.position = innerRing[indexOfFieldToMoveOn].emptyGameObject.transform.position;
        }
        playerArray[playerIndex].playerPiece.indexOfField = indexOfFieldToMoveOn;

    }

    private void GenerateBoard()
    {
        fillFields();
        initializePlayers();
       
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

    private void MovePieceToStartLocation(Piece p, int i)
    {     
        playerArray[i].playerPiece.indexOfField = playerArray[playerIndex].hero.startingLocation;
        playerArray[i].playerPiece.transform.position = outerRing[playerArray[i].hero.startingLocation].emptyGameObject.transform.position;
        outerRing[playerArray[playerIndex].hero.startingLocation].counter++;
    }

    private void nextTurn()
    {
        playerIndex++;
        if (playerIndex == playersCounter)
        {
            playerIndex = 0;
        }
    }

    private int getActualPlayerRingFieldNumber()
    {
        if (playerArray[playerIndex].outerRing == true) { return 24; }
        else if (playerArray[playerIndex].middleRing == true) { return 16; }
        else if (playerArray[playerIndex].innerRing == true) { return 8; }
        else { return 0; }       
    }

    private void RollADice_Button()
    {       
        diceResult = rollDice();       
    }


    public void Left_Button()
    {
        CollisionDetector cd = new CollisionDetector(playerArray, playerIndex);
        int temp = getActualPlayerRingFieldNumber();

        //obliczanie gdzie przesunac pionek
        int y = playerArray[playerIndex].playerPiece.indexOfField;
        int whereToMove = 0;
        if (diceResult>y) { whereToMove = temp + (y - diceResult);}
        else { whereToMove = Math.Abs(y - diceResult) % temp;}

        //przesuniecie pionka
        movePiece(playerIndex, whereToMove);
        
        //przesuniecie pionka, aby nie nachodzily na siebie
        cd.movePieceToRightLocation(outerRing);
        
        // przejscie do kolejnej tury
        nextTurn();
    }
    public void Right_Button()
    {
        CollisionDetector cd = new CollisionDetector(playerArray, playerIndex);

        //obliczanie gdzie przesunac pionek
        int temp = getActualPlayerRingFieldNumber();
        int y = playerArray[playerIndex].playerPiece.indexOfField;
        int whereToMove = (diceResult + y) % temp;

        //przesuniecie pionka
        movePiece(playerIndex, whereToMove);
        
        
        //przesuniecie pionka, aby nie nachodzily na siebie
        cd.movePieceToRightLocation(outerRing);

        // przejscie do kolejnej tury
        nextTurn();
    }
    
    void Start ()
    {
        GenerateBoard();
	}

	void Update ()
    {
	
	}
}
