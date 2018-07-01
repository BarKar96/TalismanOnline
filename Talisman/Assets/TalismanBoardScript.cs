using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TalismanBoardScript : MonoBehaviour
{

    //private Field[][] rings;
    public static Field[] outerRing;
    private Field[] middleRing;
    private Field[] innerRing;
    public Deck deckOfCards;
    public Player[] playerArray;
    public SpecialFields spc;

    public int playerIndex;

    public GameObject piecePrefab;

    public TextMeshProUGUI playerName;

    public Text zaDuzoPrzedmiotow;
    public TextMeshProUGUI fieldDescription;

    Combat combat;

    public Button combatButton;
    public Button nextTurnButton;

    public GameObject messagePanel;
    private int playersCounter;
    public int diceResult;

    public GameObject subPanel_Items;
    private bool _subPanel_Items_Opened = false;

    public GameObject subPanel_Spells;
    private bool _subPanel_Spells_Opened = false;

    Windows windows;

    

    /// <summary>
    /// //////////////////////////////////////////////////////////////
    /// </summary>
    
    private void initializePlayers()
    {
        combat = GameObject.Find("Combat").GetComponent<Combat>();
        windows = GameObject.Find("Windows").GetComponent<Windows>();
        playerIndex = 0;
        playersCounter = 5;
        playerArray = new Player[playersCounter];
        //  Initialise sample players
        playerArray[0] = new Player("Bartek", new Hero(hero_type.CZARNOKSIEZNIK));
        playerArray[0].getItems().Add(new Card(card_type.ITEM,new event_type[] { }));
        playerArray[0].getItems().Add(new Card(card_type.ITEM, new event_type[] { }));
        playerArray[0].getItems().Add(new Card(card_type.ITEM, new event_type[] { }));
        playerArray[1] = new Player("Slawek", new Hero(hero_type.TROLL));
        playerArray[3] = new Player("Slawek", new Hero(hero_type.TROLL));
        playerArray[4] = new Player("Slawek", new Hero(hero_type.TROLL));
        playerArray[1].getItems().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("zbroja")));
        playerArray[1].getItems().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("miecz")));
        playerArray[1].getItems().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("jablko")));
        playerArray[1].getItems().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("zbroja")));
        playerArray[1].getItems().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("miecz")));
        playerArray[1].getItems().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("zbroja")));

       
        playerArray[1].getSpells().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("kula_ognia")));
        playerArray[1].getSpells().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("kula_ognia")));

        playerArray[0].getSpells().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("lodowa_strzala")));
        playerArray[0].getSpells().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("lodowa_strzala")));
        playerArray[0].getSpells().Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("lodowa_strzala")));



        playerArray[1].current_health = 1;
        playerArray[2] = new Player("Darek", new Hero(hero_type.KRASNOLUD));
        for (int i = 0; i < playersCounter; i++)
        {
            GeneratePiece(i);
        }
        windows.wstawPortret(playerArray);
        playerName.text = playerArray[playerIndex].name;
        windows.setCursor(playerIndex);
        // deckOfCards.listCards();
    }

    public void ChangeNetworkPlayerState(Player p){
        p.iterate_cards();

    }

    private void setPieceColor(GameObject g, int rValue,int  gValue,int bValue)
    {

        GameObject whateverGameObject = g;
        Color whateverColor = new Color(rValue, gValue, bValue, 1);

        MeshRenderer gameObjectRenderer = whateverGameObject.GetComponent<MeshRenderer>();

        Material newMaterial = new Material(Shader.Find("Standard"));

        newMaterial.color = whateverColor;
        gameObjectRenderer.material = newMaterial;
    }
    private int rollDice()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(1, 7);
    }

    private void fillFields()
    {
        deckOfCards = new Deck();
        deckOfCards.loadFromFileAndParse();
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
                case 0:
                    outerRing[i].fieldEvent = new Card("tomasz", card_type.BOARDFIELD, new event_type[] { event_type.ENEMY }, "Możesz odwiedzić cyrkulika, alchemika, czarodziejke. Cyrkulik - Możesz odzyskać do 2 punktów życia płacąc za każdy z nich 1 sztukę złota. Alchemik - Możesz odrzucić każdy przedmiot za 1 sztukę złota.");
                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("jablko")));
                    break;
                case 1:
                    outerRing[i].fieldEvent = new Card("Gospoda", card_type.BOARDFIELD, new event_type[] {}, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("spell1")));

                    break;
                case 2:
                    outerRing[i].fieldEvent = new Card("dorozkaze", card_type.BOARDFIELD, new event_type[] { event_type.ENEMY }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    
                    break;
                case 3:
                    outerRing[i].fieldEvent = new Card("zewastobomir", card_type.BOARDFIELD, new event_type[] { event_type.ENEMY }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                   
                    break;
                case 4:
                    outerRing[i].fieldEvent = new Card("pszczoly", card_type.BOARDFIELD, new event_type[] { event_type.ENEMY }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");

                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("jablko")));
                    break;
                case 5:
                    outerRing[i].fieldEvent = new Card("tomasz", card_type.BOARDFIELD, new event_type[] { event_type.ENEMY }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");

                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("spell2")));
                    break;
                case 6:
                    //todo Slavek
                    var Gospoda = new Card("Gospoda", card_type.BOARDFIELD, new event_type[] { }, "Gospoda");                  
                    //  Gospoda
                    /*Gospoda.AssignSpecial(new int[] { 1 }, event_type.LOSE_TURN);
                    Gospoda.AssignSpecial(new int[] { 2 }, event_type.COMBAT);
                    Gospoda.AssignSpecial(new int[] { 3 }, event_type.LOSE_COIN);
                    Gospoda.AssignSpecial(new int[] { 4 }, event_type.ADD_COIN);
                    Gospoda.AssignSpecial(new int[] { 5 }, event_type.ADD_COINx2);
                    Gospoda.AssignSpecial(new int[] { 6 }, event_type.GAIN_STRENGTH);*/
                    outerRing[i].fieldEvent = Gospoda;
                    outerRing[i].cardsOnField.Add(new Card("Gospoda", card_type.BOARDFIELD, new event_type[] { }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje"));
                    break;
                case 7:
                    outerRing[i].fieldEvent = new Card("Pola", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("jablko")));
                    break;
                case 8:
                    outerRing[i].fieldEvent = new Card("Ruiny", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD, event_type.DRAW_CARD }, "Wylosuj 2 karty - jeśli znajdują się tu już jakieś karty, wylosuj ich tylko tyle, aby razem były tu 2 karty");
                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("jablko")));
                    break;
                case 9:
                    outerRing[i].fieldEvent = new Card("Równiny", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("spell3")));
                    break;
                case 10:
                    outerRing[i].fieldEvent = new Card("Las", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE }, "Rzuć 1 kością. (1) Napada na Ciebie rozbójnik (Siła 4) (2-3) Zgubiłeś się, tracisz następną turę. (4-5) Odpoczywasz, nic się nie dzieje. (6) Wyprowadza Cię stąd zwiadowca - otrzymujesz 1 punkt mocy");
                    break;
                case 11:
                    outerRing[i].fieldEvent = new Card("Pola", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    break;
                case 12:
                    outerRing[i].fieldEvent = new Card("Wioska", card_type.BOARDFIELD, new event_type[] { }, "Możesz odwiedzić kowala, medyka lub mistyka. Kowal: (możesz zakupić za złoto przedmioty: Hełm [2 SZ], Miecz [2 SZ], Topór [3 SZ], Tarczę [3 SZ], Zbroję [4 SZ], Medyk odzyskujesz 1 punkt zdrowia za 1 sztuke złota, Mistyk (Rzucasz kostką jeśli wypadnie: (1) Tracisz jeden punkt życia bo otrzymujesz czarem rykoszetem. (2-4) Nic się nie dzieje. (5) Otrzymujesz jeden punkt mocy. (6) Otrzymujesz 1 zaklęcie.");

                    break;
                case 13:
                    outerRing[i].fieldEvent = new Card("Pola", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("spell4")));
                    break;
                case 14:
                    outerRing[i].fieldEvent = new Card("Cmentarz", card_type.BOARDFIELD, new event_type[] { }, "Jeśli jesteś: dobry tracisz jeden punkt życia, neutralny możesz odkupić punkty losu za 1 sztukę złota każdy, zły możesz odzyskać za darmo wszystkie punkty losu lub modlić się [rzucić 1 kością]: (1-4) modły nie zostały wysłuchane - nic się nie dzieje. (5) Otrzymujesz 1 punkt losu. (6) Otrzymujesz 1 zaklęcie.");
                    break;
                case 15:
                    outerRing[i].fieldEvent = new Card("Puszcza", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    break;
                case 16:
                    outerRing[i].fieldEvent = new Card("Strażnik", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje lub jeślizamierzasz przejść przez most do środkowej krainy. Możesz przejść przez most, jeżeli pokonasz strażnika (Siła 9). Nie musisz walczyć ze strażnikiem, jeśli przybywasz ze środkowej krainy");
                    break;
                case 17:
                    outerRing[i].fieldEvent = new Card("Wzgórza", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("spell5")));
                    break;
                case 18:
                    outerRing[i].fieldEvent = new Card("Kapliczka", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE }, "Jeśli jesteś: zły tracisz 1 punkt życia, neutralny odzyskujesz 1 punkt życia za każdą 1 sztukę złota, dobry możesz odzyskać za darmo początkową ilość punktów życia lub modlić się [rzucić 1 kością]: (1-4) modły nie zostały wysłuchane - nic się nie dzieje. (5) Otrzymujesz 1 punkt życia. (6) Otrzymujesz 1 zaklęcie. ");
                    break;
                case 19:
                    outerRing[i].fieldEvent = new Card("Pola", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("spell6")));
                    break;
                case 20:
                    outerRing[i].fieldEvent = new Card("Skały", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE }, "Rzuć 1 kością: (1) zaatakował Ciebie duch (Siła 4). (2-3) Zabłądziłeś tracisz następną turę. (4-5) Nic się nie dzieje. (6) Barbarzyńca wskazuje Ci drogę - otrzymujesz 1 punkt Siły");
                    break;
                case 21:
                    outerRing[i].fieldEvent = new Card("Równiny", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje ");
                    break;
                case 22:
                    outerRing[i].fieldEvent = new Card("Puszcza", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD, event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    outerRing[i].cardsOnField.Add(deckOfCards.fullDeck.Find(x => x.getName().Equals("spell7")));
                    break;
                case 23:
                    outerRing[i].fieldEvent = new Card("Pola", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD, event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < 16; i++)
        {
            switch (i)
            {
                case 0:
                    middleRing[i].fieldEvent = new Card("Jaskinia czarownika", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE }, "Kiedy wypełnisz swoje zadanie, Czarownik natychmiast cię tu teleportuje i w nagrodę wręczy Ci Talizman (jeśli jeszcze jakieś pozostały). Rzut kością decyduje o zadaniu: (1) Wygraj potyczkę z innym poszukiwaczem. (2) Zabij wroga. (3) Odrzuć przyjaciela. (4) Odrzuć magiczny przedmiot. (5) Odrzuć 3 sztuki złota. (6) Odrzuć 2 sztuki złota.");
                    break;
                case 1:
                    middleRing[i].fieldEvent = new Card("Pustynia", card_type.BOARDFIELD, new event_type[] { event_type.LOSE_HEALTH, event_type.DRAW_CARD }, "Tracisz jeden punkt życia i następnie losujesz 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    break;
                case 2:
                    middleRing[i].fieldEvent = new Card("Oaza", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD, event_type.DRAW_CARD }, "Wylosuj 2 karty - jeśli znajdują się tu już jakieś karty, wylosuj ich tylko tyle, aby razem były tu 2 karty");
                    break;
                case 3:
                    middleRing[i].fieldEvent = new Card("Pustynia", card_type.BOARDFIELD, new event_type[] { event_type.LOSE_HEALTH, event_type.DRAW_CARD }, "Tracisz jeden punkt życia i następnie losujesz 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    break;
                case 4:
                    middleRing[i].fieldEvent = new Card("Świątynia", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE, event_type.ROLL_DICE }, "Modlisz się rzuć 2ma koścmi: (2) Tracisz 2 punkty życia. (3) Tracisz jeden punkt życia. (4) Tracisz przyjaciela. (5) Zostałeś uwięziony w kolejnej turze rzucasz kością jeśli nie wylosujesz 4,5,6 tracisz kolejną turę. (6) Otrzymujesz 1 punkt siły. (7) Otrzymujesz 1 punkt mocy. (8-9) Otrzymujesz 1 zaklęcie. (10) Otrzymujesz talizman. (11) Otrzymujesz 2 punkty losu. (12) Otrzymujesz 2 punkty życia.");
                    break;
                case 5:
                    middleRing[i].fieldEvent = new Card("Puszcza", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje ");
                    break;
                case 6:
                    middleRing[i].fieldEvent = new Card("Runy", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje. Każda istota z którą zmierzysz się tutaj na obszarze Runów, dodaje 2 do wyniku swego rzutu ataku");
                    break;
                case 7:
                    middleRing[i].fieldEvent = new Card("Zamek", card_type.BOARDFIELD, new event_type[] { event_type.GAIN_HEALTH }, "Nadworny medyk - możesz odzyskać punkty życia płacąc za każdy z nich 1 sztukę złota. Jeżeli wśród Przyjaciół jest księżniczka lub książę 2 punkty życia odzyskujesz za darmo");
                    break;
                case 8:
                    middleRing[i].fieldEvent = new Card("Tajemne Wrota", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD, event_type.ROLL_DICE, event_type.ROLL_DICE }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje lub jeśli zamierzasz przejść na Równiny Grozy. Tajemne Wrota możesz wyważyć za pomocą swojej siłylub sforsować za pomocą Mocy. Wybierz, której z tych cech użyjesz,a następnie rzuć dwiema kośćmi.");
                    break;
                case 9:
                    middleRing[i].fieldEvent = new Card("Czarny Rycerz", card_type.BOARDFIELD, new event_type[] { event_type.LOSE_HEALTH }, "Tracisz 1 sztukę złota albo tracisz jeden punkt życia");
                    break;
                case 10:
                    middleRing[i].fieldEvent = new Card("Ukryta Dolina", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD, event_type.DRAW_CARD, event_type.DRAW_CARD }, "Wylosuj 3 karty - jeśli znajdują się tu już jakieś karty, wylosuj ich tylko tyle, aby razem były tu 3 karty");
                    break;
                case 11:
                    middleRing[i].fieldEvent = new Card("Wzgórza", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje");
                    break;
                case 12:
                    middleRing[i].fieldEvent = new Card("Przeklęta Polana", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje. Kiedy przebywasz na Przeklętej polanie, przedmioty i magiczne przedmioty nie zapewniają premii do siły i mocy. Co więcej nie możesz korzystać ze specjalnych zdolności Magicznych przedmiotów, ani rzucać zaklęć");
                    break;
                case 13:
                    middleRing[i].fieldEvent = new Card("Runy", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje. Każda istota z którą zmierzysz się tutaj na obszarze Runów, dodaje 2 do wyniku swego rzutu ataku.");
                    break;
                case 14:
                    middleRing[i].fieldEvent = new Card("Przepaść", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE, event_type.LOSE_HEALTH }, "Rzuć raz kością za siebie i każdego swojego przyjaciela. Jeśli wylosujesz 1 lub 2 dla siebie tracisz jeden punkt życia. Jeśli wypadnie 1 lub 2 dla przyjaciela wpada on w przepaść i go tracisz.");
                    break;
                case 15:
                    middleRing[i].fieldEvent = new Card("Runy", card_type.BOARDFIELD, new event_type[] { event_type.DRAW_CARD }, "Wylosuj 1 kartę - nie losujesz, jeśli jakaś karta się tutaj znajduje. Każda istota z którą zmierzysz się tutaj na obszarze Runów, dodaje 2 do wyniku swego rzutu ataku");
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            switch (i)
            {
                case 0:
                    innerRing[i].fieldEvent = new Card("Dolina Ognia", card_type.BOARDFIELD, new event_type[] { }, "Musisz posiadać talizman - aby wejść na ten obszar musisz posiadać talizman. Jeśli go nie posiadasz, musisz zawrócić. Do korony można się dostać tylko z tego obszaru.");
                    break;
                case 1:
                    innerRing[i].fieldEvent = new Card("Jama Wilkołaka", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE, event_type.ROLL_DICE, event_type.LOSE_HEALTH }, "Walka z wilkołakiem. Rzuć 2 koścmi. Suma oczek określa siłę wilkołaka. Musisz z nim walczyć. Jeżeli przegrasz, tracisz jeden punkt życia i musisz w następnej turze ponownie walczyć z tym samym wilkołakiem. Dopóki nie pokonasz wilkołaka, nie możesz iść dalej");
                    break;
                case 2:
                    innerRing[i].fieldEvent = new Card("Śmierć", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE, event_type.ROLL_DICE }, "Gra ze śmiercią - rzuć dwiema koścmi za siebie i za śmierć - jeśli wynik jest taki sam nic się nie dzieje. Jeśli śmierć wygra tracisz jeden punkt życia i zostajesz na tym polu doputy nie wygrasz. Jeśli wygrasz możesz opuścić to pole w następnej turze");
                    break;
                case 3:
                    innerRing[i].fieldEvent = new Card("Krypta", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE, event_type.ROLL_DICE, event_type.ROLL_DICE }, "Rzuć 3 razy kościmi - Od sumy wyrzuconych oczek odejmij swoją siłę. Natychmiast przesuwasz się na obszar: (0) Pozostajesz w tym miejscu. (1) Równina Grozy. (2-3) Tajemne Wrota. (4-5) Jaskinia Czarownika. (6+) Miasto.");
                    break;
                case 4:
                    innerRing[i].fieldEvent = new Card("Równina Grozy", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE, event_type.ROLL_DICE, event_type.ROLL_DICE }, "---");
                    break;
                case 5:
                    innerRing[i].fieldEvent = new Card("Kopalnia", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE, event_type.ROLL_DICE, event_type.ROLL_DICE }, "Rzuć 3 razy kościmi - Od sumy wyrzuconych oczek odejmij swoją siłę. Natychmiast przesuwasz się na obszar: (0) Pozostajesz w tym miejscu. (1) Równina Grozy. (2-3) Tajemne Wrota. (4-5) Jaskinia Czarownika. (6+) Gospoda.");
                    break;
                case 6:
                    innerRing[i].fieldEvent = new Card("Wieża wampira", card_type.BOARDFIELD, new event_type[] { event_type.LOSE_HEALTH }, "Tracisz Krew - Rzuć 1 kością by przekonać się ile krwi wyssał z Ciebie wampir. Możesz odrzucić dowolną liczbę przyjaciół, aby ograniczyć stratę punktów życia. Za każdego odrzuconego przyjaciela tracisz o 1 punkt życia mniej. (1-2) Tracisz 1 punkt życia \n (3-4) Tracisz 2 punkty życia \n (5-6) Tracisz 3 punkty życia.");
                    break;
                case 7:
                    innerRing[i].fieldEvent = new Card("Otchłań", card_type.BOARDFIELD, new event_type[] { event_type.ROLL_DICE }, "Rzuć 1 kością. Wynik rzutu określa liczbę diabłów (Siła 4), z którymi przyjdzie ci walczyć. Będziesz z nimi walczyć po kolei, dopóki nie pokonasz ich wszystkich lub nie stracisz 1 punkty życia. Możesz się poruszyć dopiero podczasnastępnej tury po pokonaniu wszystkich diabłów.");
                    break;
                default:
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
        go.transform.localScale += new Vector3(20, 20, 20);
        Piece p = go.GetComponent<Piece>();
        
        switch(i)
        {
            case 0:
                setPieceColor(p.gameObject, 255, 0, 0);
                break;
            case 1:
                setPieceColor(p.gameObject, 0, 255, 0);
                break;
            case 2:
                setPieceColor(p.gameObject, 0, 0, 255);
                break;
            case 3:
                setPieceColor(p.gameObject, 255, 255, 0);
                break;
            case 4:
                setPieceColor(p.gameObject, 0, 255, 255);
                break;
            case 5:
                setPieceColor(p.gameObject, 255, 0, 255);
                break;

        }
        createMessage(p.gameObject, "lololo");
        playerArray[i].playerPiece = p;
        MovePieceToStartLocation(p, i);
        return p;
    }
    public List<GameObject> list = new List<GameObject>();
    public List<GameObject> list2 = new List<GameObject>();

    public void createMessage(GameObject p, string text)
    {
        
        GameObject ngo = new GameObject("myTextGO");
        ngo.transform.SetParent(p.transform);

        Text myText = ngo.AddComponent<Text>();
        myText.text = text;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        myText.font = ArialFont;
        myText.material = ArialFont.material;



    }
    private void MovePieceToStartLocation(Piece p, int i)
    {
        playerArray[i].playerPiece.indexOfField = playerArray[playerIndex].hero.startingLocation;
        playerArray[i].playerPiece.transform.position = outerRing[playerArray[i].hero.startingLocation].emptyGameObject.transform.position;
        outerRing[playerArray[playerIndex].hero.startingLocation].counter++;
    }


    public static void clearPlayerPanelView(List<GameObject> list)
    {
        foreach (GameObject go in list)
        {
            Destroy(go);
        }
        list.Clear();
    }
    private void nextTurn()
    {
        Debug.Log(deckOfCards.drawCard().getName());    // Returns one random card
        //deckOfCards.listCards();                         //Use to list cards

        if (CardDrawer.heroCardList.Count > 0)
        {
            Destroy(CardDrawer.heroCardList[0]);
            CardDrawer.heroCardList.Clear();
        }  
        playerIndex++;
        if (playerIndex == playersCounter)
        {
            playerIndex = 0;
        }
        //CardDrawer.spawnPlayerHeroCard(playerArray[playerIndex].hero.name);
        Debug.Log(playerArray[playerIndex].name + playerArray[playerIndex].getItems().Count);
        playerArray[playerIndex].boardField = outerRing[playerArray[playerIndex].playerPiece.indexOfField].fieldEvent;
        
        

        //showHeroName();
        //showHeroStatistics();
        //showHeroCards();



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
        playerArray[playerIndex].diceResult = diceResult;
        //obliczanie gdzie przesunac pionek
        int y = playerArray[playerIndex].playerPiece.indexOfField;
        int whereToMove = 0;
        if (playerArray[playerIndex].diceResult > y) { whereToMove = temp + (y - playerArray[playerIndex].diceResult); }
        else { whereToMove = Math.Abs(y - playerArray[playerIndex].diceResult) % temp; }

        //przesuniecie pionka
        movePiece(playerIndex, whereToMove);

        //przesuniecie pionka, aby nie nachodzily na siebie
        cd.movePieceToRightLocation(outerRing); //Debug.Log(playerArray[playerIndex].current_health);

        foreach (Card c in outerRing[playerArray[playerIndex].playerPiece.indexOfField].cardsOnField)
        {
            playerArray[playerIndex].getCards().Add(c);
        }


        buttonCombat();


    }
    public void Right_Button()
    {
        CollisionDetector cd = new CollisionDetector(playerArray, playerIndex);
        playerArray[playerIndex].diceResult = diceResult;
        //obliczanie gdzie przesunac pionek
        int temp = getActualPlayerRingFieldNumber();
        int y = playerArray[playerIndex].playerPiece.indexOfField;
        int whereToMove = (playerArray[playerIndex].diceResult + y) % temp;

        //przesuniecie pionka
        movePiece(playerIndex, whereToMove);


        //przesuniecie pionka, aby nie nachodzily na siebie
        cd.movePieceToRightLocation(outerRing);
        //Card c = deckOfCards.fullDeck.Find(x => x.getName().Equals("tomasz"));
        //playerArray[playerIndex].getCards().Add(c);
        foreach (Card c in outerRing[playerArray[playerIndex].playerPiece.indexOfField].cardsOnField)
        {
            playerArray[playerIndex].getCards().Add(c);
        }



        buttonCombat();


    }

    //panel ekwipunku
    public void Items_Button()
    {
        //Card c = new Card(card_type.ITEM, new event_type[] { });
        //c.equipable = true;
        //playerArray[playerIndex].getItems().Add(c);

        _subPanel_Items_Opened = !_subPanel_Items_Opened;
        setSubPanelVisibility(subPanel_Items, _subPanel_Items_Opened);
        if (_subPanel_Items_Opened == true)
        {
            CardDrawer.spawnPlayerItems(playerArray[playerIndex],"PanelEkwipunku");
        }
        else
        {
            clearPlayerPanelView(CardDrawer.itemsList);
        }
    }
    public void setSubPanelVisibility(GameObject subPanel, bool b)
    {
        if (subPanel != null)
        {
            subPanel.SetActive(b);
        }
        
        
    }
    public void Spells_Button()
    {
        _subPanel_Spells_Opened = !_subPanel_Spells_Opened;
        setSubPanelVisibility(subPanel_Spells, _subPanel_Spells_Opened);
        if (_subPanel_Spells_Opened == true)
        {
            CardDrawer.spawnPlayerSpells(playerArray[playerIndex], "PanelZaklec");
        }
        else
        {
            clearPlayerPanelView(CardDrawer.spellsList);
        }
    }
    //Button walki z graczem
    public void buttonCombat()
    {
        for (int counterOfPlayer = 0; counterOfPlayer < playerArray.Length; counterOfPlayer++)
        {
            if (playerArray[counterOfPlayer] == playerArray[playerIndex]) continue;
            else if (playerArray[counterOfPlayer].playerPiece.indexOfField == playerArray[playerIndex].playerPiece.indexOfField)
            {
                combatButton.gameObject.SetActive(true);
                break;
            }
            else combatButton.gameObject.SetActive(false);
        }
    }
    public void buttonCombat2()
    {
        for (int counterOfPlayer = 0; counterOfPlayer < playerArray.Length; counterOfPlayer++)
        {
            if (playerArray[counterOfPlayer] == playerArray[playerIndex]) continue;
            else if (playerArray[counterOfPlayer].playerPiece.indexOfField == playerArray[playerIndex].playerPiece.indexOfField)
            {
                combat.StartCombat(playerArray[counterOfPlayer], playerArray[playerIndex]);
                break;
            }
        }
    }
    public void buttonExplore()
    {
        playerArray[playerIndex].iterate_cards();
        playerArray[playerIndex].getCards().Clear();
    }
    //to fix
    public void PlayerItems()
    {
        _subPanel_Items_Opened = !_subPanel_Items_Opened;
        setSubPanelVisibility(subPanel_Items, _subPanel_Items_Opened);
        if (_subPanel_Items_Opened == true)
        {
            string target = EventSystem.current.currentSelectedGameObject.name;
            switch (target)
            {
                case "ButtonCards1":
                    CardDrawer.spawnPlayerItems(playerArray[0], "PanelEkwipunku");
                    break;
                case "ButtonCards2":
                    CardDrawer.spawnPlayerItems(playerArray[1], "PanelEkwipunku");
                    break;
                case "ButtonCards3":
                    CardDrawer.spawnPlayerItems(playerArray[2], "PanelEkwipunku");
                    break;
                case "ButtonCards4":
                    CardDrawer.spawnPlayerItems(playerArray[3], "PanelEkwipunku");
                    break;
                case "ButtonCards5":
                    CardDrawer.spawnPlayerItems(playerArray[4], "PanelEkwipunku");
                    break;
                case "ButtonCards6":
                    CardDrawer.spawnPlayerItems(playerArray[5], "PanelEkwipunku");
                    break;
            }
        }
        else
        {
            clearPlayerPanelView(CardDrawer.itemsList);
        }
    }
    public void buttonSetNextTurnButtonOn()
    {
        nextTurnButton.gameObject.SetActive(true);
    }

    // przejscie do kolejnej tury
    public void nextTurn_Button()
    {
        nextTurn();
        playerName.text = playerArray[playerIndex].name;
        //Debug.Log("pozycja gracza w kolejce " + playerIndex);
        windows.setCursor(playerIndex);
    }

    /// <summary>
    /// /////////////////////////////////UI/////////////////////////////////////////////////
    /// </summary>
    //public void showHeroName()
    //{
    //    //playerInformationPanel.color = Color.white;
    //    //playerInformationPanel.transform.position += new Vector3(0, 0, -4);
    //    
    //    playerInformationPanel.text = "Tura gracza: ";
        
    //}
    //public void showHeroStatistics()
    //{
    //    playerInformationPanel.text += "\nHero Type: " + playerArray[playerIndex].hero.name;
    //    playerInformationPanel.text += "\nStrength: " + playerArray[playerIndex].strength;
    //    //text.text += "\nPower: " + playerArray[playerIndex].power;        
    //    playerInformationPanel.text += "\nHP: " + playerArray[playerIndex].current_health + "/" + playerArray[playerIndex].total_health;
    //    playerInformationPanel.text += "\nGold: " + playerArray[playerIndex].gold;
    //}
    //public void showHeroCards()
    //{
    //    playerInformationPanel.text += "\nCards:" + playerArray[playerIndex].getItems().Count;
    //    foreach (Card c in playerArray[playerIndex].getCards())
    //    {
    //        playerInformationPanel.text += "\n" + Enum.GetName(typeof(card_type), c.getCard_Type());
    //        Debug.Log(Enum.GetName(typeof(card_type), card_type.ITEM));
    //    }
    //}
    public void showFieldDescription()
    {
        //fieldDescription.color = Color.white;
        //fieldDescription.transform.position = new Vector3(0, 0, -4);
        fieldDescription.text = outerRing[playerArray[playerIndex].playerPiece.indexOfField].fieldEvent.getDescription();
    }


    public IEnumerator messager(string message)
    {
        zaDuzoPrzedmiotow.text = message;
        yield return new WaitForSeconds(2);
        zaDuzoPrzedmiotow.text = "";
    }
    public void showFullEQMessage()
    {
        StartCoroutine(messager("Masz zbyt wiele przedmiotów,aby podnieść kolejny!"));
    }
    public void exitMessage()
    {
        clearPlayerPanelView(CardDrawer.komunikatList);
        messagePanel.gameObject.SetActive(false);

    }
    /// <summary>
    /// /////////////////////////////////MAIN/////////////////////////////////////////////////
    /// </summary>


    void Start()
    {
        GenerateBoard();
        playerIndex = 0;
        nextTurn();
        playerName.text = playerArray[playerIndex].name;
        Debug.Log("pozycja gracza w kolejce " + playerIndex);
        windows.setCursor(playerIndex);


    }
    void Update()
    {


    }
}
