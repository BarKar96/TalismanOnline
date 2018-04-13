using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field {

    public GameObject emptyGameObject;
    public int counter = 0;
    //obraz / id_obrazu
    //opis
    public Field() 
    {
      
    }

    public void doSomething(Player[] playerArray, int playerIndex)
    {
        if (playerArray[playerIndex].outerRing == true)
        {
            switch (playerArray[playerIndex].playerPiece.indexOfField)
            {
                case 0:
                    break;
            }
        }
        else if (playerArray[playerIndex].middleRing == true)
        {
            switch (playerArray[playerIndex].playerPiece.indexOfField)
            {
                case 0:
                    break;
            }
        }
        else if(playerArray[playerIndex].innerRing == true)
        {
            switch (playerArray[playerIndex].playerPiece.indexOfField)
            {
                case 0:
                    break;
            }
        }

    }
}
