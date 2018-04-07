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
        return counter;
    }
    
    public void movePieceToRightLocation(Field[] nameOfRing)
    {
        int temp = nameOfRing[playerArray[playerIndex].playerPiece.indexOfField].counter;

        int a = 0;
        int b = 0;
        int c = 0;
        int d = 0;

        if (playerArray[playerIndex].outerRing == true)
        {
            a = 6;
            b = 12;
            c = 18;
            d = 24;
        }
        if (playerArray[playerIndex].middleRing == true)
        {
            a = 4;
            b = 8;
            c = 12;
            d = 16;
        }
        if (playerArray[playerIndex].innerRing == true)
        {
            a = 2;
            b = 4;
            c = 6;
            d = 8;
        }


        if (playerArray[playerIndex].playerPiece.indexOfField >= 0 && playerArray[playerIndex].playerPiece.indexOfField < a)
        {
           
            playerArray[playerIndex].playerPiece.transform.position += new Vector3(-0.3f * ((temp) % 3), 0, 0);

            if (temp > 2)
            {
                playerArray[playerIndex].playerPiece.transform.position += new Vector3(0, 0, -0.3f);
            }
            temp++;
        }
        if (playerArray[playerIndex].playerPiece.indexOfField >= a && playerArray[playerIndex].playerPiece.indexOfField < b)
        {

            playerArray[playerIndex].playerPiece.transform.position += new Vector3(0, 0, 0.3f * ((temp) % 3));

            if (temp > 2)
            {
                playerArray[playerIndex].playerPiece.transform.position += new Vector3(-0.3f, 0, 0);
            }
            temp++;
        }
        if (playerArray[playerIndex].playerPiece.indexOfField >= b && playerArray[playerIndex].playerPiece.indexOfField < c)
        {

            playerArray[playerIndex].playerPiece.transform.position += new Vector3(0.3f * ((temp) % 3), 0, 0);

            if (temp > 2)
            {
                playerArray[playerIndex].playerPiece.transform.position += new Vector3(0, 0, 0.3f);
            }
            temp++;
        }
        if (playerArray[playerIndex].playerPiece.indexOfField >= c && playerArray[playerIndex].playerPiece.indexOfField < d)
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

        nameOfRing[playerArray[playerIndex].playerPiece.indexOfField].counter = temp;
    }
}

	

