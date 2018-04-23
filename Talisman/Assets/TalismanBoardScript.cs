using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets;
using UnityEngine.UI;

public class TalismanBoardScript : MonoBehaviour
{

    //private Field[][] rings;


    private Field[] outerRing;
    private Field[] middleRing;
    private Field[] innerRing;

    private Player[] playerArray;


    private int playerIndex;

    public GameObject piecePrefab;

    public Text playerInformationPanel;
    public Text fieldDescription;



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
        //  Initialise sample players
        playerArray[0] = new Player("Bartek", new Hero(hero_type.CZARNOKSIEZNIK));
        playerArray[0].getItems().Add(new Card("qweqwewqe", card_type.BOARDFIELD, null));
        playerArray[0].getItems().Add(new Card("qweqwewqe", card_type.BOARDFIELD, null));
        playerArray[1] = new Player("Slawek", new Hero(hero_type.TROLL));
        // playerArray[2] = new Player("Darek", new Hero(hero_type.GHUL));
        for (int i = 0; i < playersCounter; i++)
        {
            GeneratePiece(i);
        }

    }

    private int rollDice()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(1, 6);
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
        }
        for (int i = 0; i < 16; i++)
        {
            middleRing[i] = new Field();
            middleRing[i].emptyGameObject = transform.GetChild(i + 25).gameObject;
        }
        for (int i = 0; i < 8; i++)
        {
            innerRing[i] = new Field();
            innerRing[i].emptyGameObject = transform.GetChild(i + 41).gameObject;
        }
        FieldDescriptor fd = new FieldDescriptor(outerRing, middleRing, innerRing);
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
        showFieldDescription();
    }

    private void addCardsToFields()
    {
        for (int i = 0; i < 24; i++)
        {
            switch (i)
            {
                case 14:
                    outerRing[i].fieldEvent = new Card("Lózko slawka", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD, event_type.DRAW_CARD });
                    break;
                case 16:
                    outerRing[i].fieldEvent = new Card("Jama Kasi", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE });
                    break;
                case 20:
                    outerRing[i].fieldEvent = new Card("Swedowory", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE });
                    break;
                default:
                    outerRing[i].fieldEvent = new Card("Bagno SHreka", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD });
                    break;
            }
        }
    }
    private void GenerateBoard()
    {

        fillFields();
        addCardsToFields();
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


    public static void clearPlayerItemsView()
    {
        foreach (GameObject go in CardDrawer.itemsList)
        {
            Destroy(go);
        }
        CardDrawer.itemsList.Clear();
    }
    private void nextTurn()
    {

        playerArray[playerIndex].boardField = outerRing[playerArray[playerIndex].playerPiece.indexOfField].fieldEvent;


        playerIndex++;
        if (playerIndex == playersCounter)
        {
            playerIndex = 0;
        }
        clearPlayerItemsView();
        CardDrawer.spawnPlayerItems(playerArray[playerIndex]);
        Debug.Log(playerArray[playerIndex].name + playerArray[playerIndex].getItems().Count);






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
        playerArray[playerIndex].diceResult = rollDice();
    }


    public void Left_Button()
    {
        CollisionDetector cd = new CollisionDetector(playerArray, playerIndex);
        int temp = getActualPlayerRingFieldNumber();

        //obliczanie gdzie przesunac pionek
        int y = playerArray[playerIndex].playerPiece.indexOfField;
        int whereToMove = 0;
        if (playerArray[playerIndex].diceResult > y) { whereToMove = temp + (y - playerArray[playerIndex].diceResult); }
        else { whereToMove = Math.Abs(y - playerArray[playerIndex].diceResult) % temp; }

        //przesuniecie pionka
        movePiece(playerIndex, whereToMove);

        //przesuniecie pionka, aby nie nachodzily na siebie
        cd.movePieceToRightLocation(outerRing);
        //Debug.Log(playerArray[playerIndex].current_health);

        playerArray[playerIndex].getCards().Add(outerRing[playerArray[playerIndex].playerPiece.indexOfField].fieldEvent);
        playerArray[playerIndex].iterate_cards();
        playerArray[playerIndex].getCards().Clear();



        showHeroName();
        showHeroStatistics();
        showHeroCards();



    }
    public void Right_Button()
    {
        CollisionDetector cd = new CollisionDetector(playerArray, playerIndex);

        //obliczanie gdzie przesunac pionek
        int temp = getActualPlayerRingFieldNumber();
        int y = playerArray[playerIndex].playerPiece.indexOfField;
        int whereToMove = (playerArray[playerIndex].diceResult + y) % temp;

        //przesuniecie pionka
        movePiece(playerIndex, whereToMove);


        //przesuniecie pionka, aby nie nachodzily na siebie
        cd.movePieceToRightLocation(outerRing);



    }
    // przejscie do kolejnej tury
    public void nextTurn_Button()
    {
        nextTurn();
    }

    /// <summary>
    /// /////////////////////////////////UI/////////////////////////////////////////////////
    /// </summary>
    public void showHeroName()
    {

        playerInformationPanel.text = "Tura gracza: " + playerArray[playerIndex].name;
    }
    public void showHeroStatistics()
    {
        playerInformationPanel.text += "\nHero Type: " + playerArray[playerIndex].hero.name;
        playerInformationPanel.text += "\nStrength: " + playerArray[playerIndex].current_health;
        //text.text += "\nPower: " + playerArray[playerIndex].power;        
        playerInformationPanel.text += "\nHP: " + playerArray[playerIndex].current_health + "/" + playerArray[playerIndex].total_health;
        playerInformationPanel.text += "\nGold: " + playerArray[playerIndex].gold;
    }
    public void showHeroCards()
    {
        playerInformationPanel.text += "\nCards:" + playerArray[playerIndex].getItems().Count;
        foreach (Card c in playerArray[playerIndex].getCards())
        {
            playerInformationPanel.text += "\n" + Enum.GetName(typeof(card_type), c.getCard_Type());
            Debug.Log(Enum.GetName(typeof(card_type), card_type.ITEM));
        }
    }
    public void showFieldDescription()
    {
        fieldDescription.text = outerRing[playerArray[playerIndex].playerPiece.indexOfField].fieldEvent.getName();
    }



    /// <summary>
    /// /////////////////////////////////MAIN/////////////////////////////////////////////////
    /// </summary>


    void Start()
    {
        GenerateBoard();
        nextTurn();
    }

    void Update()
    {

    }
}
