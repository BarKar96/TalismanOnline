using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector
{

    Player[] playerArray;
    private int playerIndex;

    public CollisionDetector(Player[] playerArray,int playerIndex)
    {
        this.playerArray = playerArray;
        this.playerIndex = playerIndex;
    }

    private int countPiecesOnField()
    {
        int counter = 0;
        foreach (Player p in playerArray)
        {
            if (p == playerArray[playerIndex])
            {
                continue;
            }
            else
            {
                if (p.playerPiece.indexOfField == playerArray[playerIndex].playerPiece.indexOfField)
                {
                    counter++;
                }
               
            }
        }
        Debug.Log("ilosc pionkow: " + counter);
        return counter;
    }
    
    public void movePieceToRightLocation()
    {
        int caseSwitch = countPiecesOnField();
       
                if (playerArray[playerIndex].playerPiece.indexOfField >=0 && playerArray[playerIndex].playerPiece.indexOfField < 6)
                {
                    playerArray[playerIndex].playerPiece.transform.position += new Vector3(-0.3f* caseSwitch, 0, 0);
                }
             
                if (playerArray[playerIndex].playerPiece.indexOfField >= 6 && playerArray[playerIndex].playerPiece.indexOfField < 12)
                {
                    playerArray[playerIndex].playerPiece.transform.position += new Vector3(0, 0, 0.3f* caseSwitch);
                }
              
                if (playerArray[playerIndex].playerPiece.indexOfField >= 12 && playerArray[playerIndex].playerPiece.indexOfField < 18)
                {
                    playerArray[playerIndex].playerPiece.transform.position += new Vector3(0.3f* caseSwitch, 0, 0);
                }
        if (playerArray[playerIndex].playerPiece.indexOfField >= 18 && playerArray[playerIndex].playerPiece.indexOfField < 24)
        {
            playerArray[playerIndex].playerPiece.transform.position += new Vector3(0, 0, -0.3f* caseSwitch);
        }



    }
}

	

