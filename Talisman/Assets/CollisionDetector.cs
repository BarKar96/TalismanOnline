using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector
{

    Player[] playerArray;
    private int playerIndex;
    Field[] outerRing;

    public CollisionDetector(Player[] playerArray,int playerIndex, Field[] outerRing)
    {
        this.playerArray = playerArray;
        this.playerIndex = playerIndex;
        this.outerRing = outerRing;
      
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
        return counter;
    }
    
    public void movePieceToRightLocation()
    {
        int temp = outerRing[playerArray[playerIndex].playerPiece.indexOfField].counter;
        
        if (playerArray[playerIndex].playerPiece.indexOfField >= 0 && playerArray[playerIndex].playerPiece.indexOfField < 6)
        {
           
            playerArray[playerIndex].playerPiece.transform.position += new Vector3(-0.3f * ((temp) % 3), 0, 0);

            if (temp > 2)
            {
                playerArray[playerIndex].playerPiece.transform.position += new Vector3(0, 0, -0.3f);
            }
            temp++;
        }
        if (playerArray[playerIndex].playerPiece.indexOfField >= 6 && playerArray[playerIndex].playerPiece.indexOfField < 12)
        {

            playerArray[playerIndex].playerPiece.transform.position += new Vector3(0, 0, 0.3f * ((temp) % 3));

            if (temp > 2)
            {
                playerArray[playerIndex].playerPiece.transform.position += new Vector3(-0.3f, 0, 0);
            }
            temp++;
        }
        if (playerArray[playerIndex].playerPiece.indexOfField >= 12 && playerArray[playerIndex].playerPiece.indexOfField < 18)
        {

            playerArray[playerIndex].playerPiece.transform.position += new Vector3(0.3f * ((temp) % 3), 0, 0);

            if (temp > 2)
            {
                playerArray[playerIndex].playerPiece.transform.position += new Vector3(0, 0, 0.3f);
            }
            temp++;
        }
        if (playerArray[playerIndex].playerPiece.indexOfField >= 18 && playerArray[playerIndex].playerPiece.indexOfField < 24)
        {

            playerArray[playerIndex].playerPiece.transform.position += new Vector3(0, 0, -0.3f * ((temp) % 3));

            if (temp > 2)
            {
                playerArray[playerIndex].playerPiece.transform.position += new Vector3(0.3f, 0, 0);
            }
            temp++;
        }



        if (temp == 6)
        {
            temp = 0;
        }

        outerRing[playerArray[playerIndex].playerPiece.indexOfField].counter = temp;
    }
}

	

