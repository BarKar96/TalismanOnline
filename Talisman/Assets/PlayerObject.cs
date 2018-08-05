using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerObject : NetworkBehaviour
{

    private int rollResult;
    private System.Random r;
    public GameObject PlayerUnitPrefab;
    public GameObject localPlayerPiece;
    public GameObject dice;
    public Field[] fields;
    public Player localPlayer;// = new Player("Suavek", new Hero(Assets.hero_type.DRUID), 1);
    public Piece localPiece;
    Windows a;
    public static int turn = 0;
    //[SyncVar]
    
    public static int current = 0;

    [SyncVar]
    private int nowMoves = 0;

    // Use this for initialization
    void Start()
    {
        dice = GameObject.Find("ButtonRzutKoscia");
        Debug.Log(dice.name);
        //this.localPlayer = new Player("Suavek", new Hero(Assets.hero_type.DRUID), turn);
        //turn++;
        fields = TalismanBoardScript.outerRing;
        a = gameObject.AddComponent<Windows>();
        if (!isLocalPlayer)
            return;
        Debug.Log("Created local player piece");

        //localPlayer = new Player("Suavek", new Hero(Assets.hero_type.DRUID), turn);
        //Instantiate(PlayerUnitPrefab);
        CmdspawnPlayerPiece();
        CmdDiceAvailabilityCheck();
    }



    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Hero type: " + localPlayer.hero.name);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player turn: " + turn);
            CmdTranslatePiece();
           
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Local player exists?" + localPlayer == null);
            if (localPlayer == null) Debug.Log("Player doesn't exist");
            else
            {
                Debug.Log("Player name: " + this.localPlayer.hero.name);
                Debug.Log("Turn / current / playerTurn / nowMoves:" + turn + " / " + current + " / " + localPlayer.NET_Turn + " / " + nowMoves);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CmdRollDice();
           // var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
            //go.ChangeNetworkPlayerState(localPlayer);
            Debug.Log("Roll ");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            string n = "Worked?:" + Random.Range(1,23);
            CmdChangePlayerName(n);
        }

    }
    [ClientRpc]
    void RpcshowDiceAndButtons()
    {
        if(localPlayer != null)
        {
            Debug.Log("Things'Fine: " + localPlayer.hero.name);
        }
        Debug.Log("IF: Wartosci t/lpt: " + current + " / " + nowMoves);
        if (current == localPlayer.NET_Turn)
        {
            Debug.Log("Showing Dice");
            dice.SetActive(true);
        }
        else
        {
            Debug.Log("Hiding Dice");
            dice.SetActive(false);
        }

    }

    [SyncVar]
    public string playername = "asjkglhalsk";

    //******SERVER_SIDE******
    
    [Command]
    void CmdChangePlayerName(string s)
    {
        playername = s;
        /*if(isServer)
            RpcprintStats();*/
        //TargetcheckThis(connectionToClient, 55);
        /*foreach (var conn in NetworkServer.connections)
        {
            TargetcheckThis(conn, 15);
        }*/
    }
    //  ******************WORKZONE[PORTRAITS]****************************
    [Command]
    public void CmdPortrait()
    {
        if (NetworkServer.connections.Count < 2)
            TargetassignPortrait(connectionToClient);
        RpcNewPortrait(localPlayer.name, localPlayer.hero.type, localPlayer.NET_Turn, NetworkServer.connections.Count);
    }

    [ClientRpc]
    public void RpcNewPortrait(string name, hero_type hero, int queue, int playersonserver)
    {
        GameObject.Find("Tile").GetComponent<TalismanBoardScript>().addNewPlayerPortrait(
            new Player(name, new Hero(hero), queue), playersonserver);
    }
    //  ******************WORKZONE[PORTRAITS]****************************

    //  ******************WORKZONE[ONLINECOLLISION]****************************

    [Command]
    void CmdRequestPositions()
    {
        int[] positions = new int[turn];
        for(int i =0; i < turn; i++)
        {
            //positions[i] = 
                TargetReturnCurrentPosition(NetworkServer.connections[i]);
        }
        foreach(int i in positions)
        {
            Debug.Log("THere is a player @ " + i);
        }
    }

    [TargetRpc]
    void TargetReturnCurrentPosition(NetworkConnection nc)
    {
        //return localPlayer.NET_RingPos;
    }
    //  ******************WORKZONE[ONLINECOLLISION]****************************

    [Command]
    public void CmdAssignPortait()
    {
        //TargetassignPortrait(connectionToClient);
        if (NetworkServer.connections.Count > 1)
        {
         
        }
        RpcAddPortrait(this.localPlayer.name, this.localPlayer.hero.type);
    }

    [ClientRpc]
    void RpcAddPortrait(string name, hero_type hero)
    {
        
    /*    GameObject.Find("Tile").GetComponent<TalismanBoardScript>().addNewPlayerPortrait(
            new Player(name, new Hero(hero)));*/
    }

    [Command]
    void CmdspawnPlayerPiece()
    {
        Debug.Log("*********************Spawning one piece*********************");
        GameObject go = Instantiate(PlayerUnitPrefab);
        
        localPlayerPiece = go;
        //Piece p = go.GetComponent<Piece>();
        //localPlayer.playerPiece = p;
        RpcAssignPlayer("asd", turn);
        
 
        nowMoves = turn;
        turn++;
        //      CmdtranslatePieceToStart();
        
        NetworkServer.Spawn(go);
    }
    
    [Command]
    void CmdTranslatePiece()
    {
        if (!localPlayerPiece)
            return;
        localPlayerPiece.transform.Translate(1, 0, 0);
    }
    

    [Command]
    void CmdtranslatePieceToStart(int p)
    {
        
        /*if (!isLocalPlayer)
            return;*/
        //localPlayer.playerPiece.indexOfField = localPlayer.hero.startingLocation;
        Debug.Log("Moving player to " + localPlayer.hero.startingLocation);
        
        localPlayerPiece.transform.position = fields[p].emptyGameObject.transform.position;
        localPlayer.NET_RingPos = localPlayer.hero.startingLocation;
        //fields[localPlayer.hero.startingLocation].counter++;

    }

    [Command]
    void CmdUpdateTurn()
    {

    }

    [Command]
    void CmdDiceAvailabilityCheck(){
        for(int i  = 0; i < turn; i++)
        {
            TargetHideDice(NetworkServer.connections[i]);
        }        
        //RpcupdateTurn(k);
        if (current < turn-1)
        {            
            current++;
            RpcupdateTurn(current, turn);
        }
        else
        {
            current = 0;
            RpcupdateTurn(0, turn);
        }
        TargetShowDice(NetworkServer.connections[current]);
    }

    [Command]
    void CmdRollDice()
    {
        Debug.Log("OF: Wartosci t/lpt: " + current + " / " + nowMoves);
        if (!(current == localPlayer.NET_Turn))
            return;
        int k = Random.Range(1, 7);
        //current = k;
        Debug.Log("Server sets current to: " + k);
        localPlayerPiece.transform.position = fields[(localPlayer.NET_RingPos + k) % fields.Length].emptyGameObject.transform.position;
        localPlayer.NET_RingPos = (localPlayer.NET_RingPos + k) % fields.Length;
        localPlayer.boardField = fields[localPlayer.NET_RingPos].fieldEvent;
        
        //RpcshowDiceAndButtons();
        for(int i  = 0; i < turn; i++)
        {
            TargetHideDice(NetworkServer.connections[i]);
        }
        
        //RpcupdateTurn(k);
        if (current < turn-1)
        {            
            current++;
            RpcupdateTurn(current, turn);
        }
        else
        {
            current = 0;
            RpcupdateTurn(0, turn);
        }
        TargetShowDice(NetworkServer.connections[current]);
    }

    public int abs(int k)
    {
        return k < 0 ? (-1) * k : k;
    }
    
    [Command]
    public void CmdMovePlayerLeft(int k)
    {
        /*if (!(current == localPlayer.NET_Turn))
            return;*/
        Debug.Log("aktualnie" + localPlayer.NET_RingPos);
        Debug.Log("Moving: " + abs((localPlayer.NET_RingPos - k) % fields.Length));
        localPlayerPiece.transform.position = fields[abs((localPlayer.NET_RingPos - k) % fields.Length)].emptyGameObject.transform.position;
        localPlayer.NET_RingPos = (localPlayer.NET_RingPos - k) % fields.Length;
        localPlayer.boardField = fields[abs(localPlayer.NET_RingPos)].fieldEvent;
        //RpcupdateTurn(k);
        /*if (current < turn - 1)
        {
            current++;
            RpcupdateTurn(current, turn);
        }
        else
            RpcupdateTurn(0, turn);*/
        //turnAndDiceReload();
        Debug.Log("Moving online player to the left");
    }
 
    [Command]
    public void CmdMovePlayerRight(int k)
    {
       /* if (!(current == localPlayer.NET_Turn))
             return;*/ 
        Debug.Log("Moving: " + abs((localPlayer.NET_RingPos - k) % fields.Length)); 
        localPlayerPiece.transform.position = fields[abs((localPlayer.NET_RingPos + k) % fields.Length)].emptyGameObject.transform.position;
        localPlayer.NET_RingPos = (localPlayer.NET_RingPos + k) % fields.Length;
        localPlayer.boardField = fields[abs(localPlayer.NET_RingPos)].fieldEvent;
        //RpcupdateTurn(k);
        /*if (current < turn - 1)
        {
            current++;
            RpcupdateTurn(current, turn);
        }
        else
            RpcupdateTurn(0, turn);*/
        //turnAndDiceReload();
        Debug.Log("Moving online player to the right");
    }
 
    [Command]
    public  void CmdReloadDice()
    {
        /*for (int i = 0; i < turn; i++)
        {
            TargetHideDice(NetworkServer.connections[i]);
        }
        TargetShowDice(NetworkServer.connections[current]);*/
        turnAndDiceReload();
    }

    [Command]
    public void CmdMoveRight(int x)
    {
        if(!isLocalPlayer)
            return;
        localPlayerPiece.transform.position = fields[(localPlayer.NET_RingPos + x) % fields.Length].emptyGameObject.transform.position;
        localPlayer.NET_RingPos = (localPlayer.NET_RingPos + x) % fields.Length;
        localPlayer.boardField = fields[localPlayer.NET_RingPos].fieldEvent;

        //RpcshowDiceAndButtons();
        for (int i = 0; i < turn; i++)
        {
            TargetHideDice(NetworkServer.connections[i]);
        }

        //RpcupdateTurn(k);
        if (current < turn - 1)
        {
            current++;
            RpcupdateTurn(current, turn);
        }
        else
        {
            current = 0;
            RpcupdateTurn(0, turn);
        }
        TargetShowDice(NetworkServer.connections[current]);
    }

    void turnAndDiceReload(){
        for (int i = 0; i < turn; i++)
        {
            TargetHideDice(NetworkServer.connections[i]);
        }

        //RpcupdateTurn(k);
        if (current < turn - 1)
        {
            current++;
            RpcupdateTurn(current, turn);
        }
        else
        {
            current = 0;
            RpcupdateTurn(0, turn);
        }
        TargetShowDice(NetworkServer.connections[current]);
    }
    //******CLIENT_SIDE******

    [ClientRpc]
    void RpcupdateTurn(int c, int t)
    {
        turn = t;
        current = c;
        // var go = GameObject.find(Gracz, ktorego jest tura)...
        // go.ustaw_ze_ma_wyswietlic_przyciski_ruchu(true);
        Debug.Log("Server wants " + playername + "to set current to: " + c + " / " + current);
        //GameObject.Find("Tile").GetComponent<TalismanBoardScript>().showDiceAndButtons(current, localPlayer.NET_Turn);
        //showDiceAndButtons();
    }

    [ClientRpc]
    void RpcAssignPlayer(string s, int turn)
    {
        Debug.Log("Player " + playername + " Sets new hero");
        Player p = new Player("S", new Hero(Assets.hero_type.CZARNOKSIEZNIK), turn);
        p.boardField = fields[p.NET_RingPos].fieldEvent;
        localPlayer = p;
        //GameObject.Find("ScrollArea").GetComponent<Windows>().addToHistory("New player entered");
        a.addToHistory("new player entered");
        this.name = "Piece" + turn;
        //CmdAssignPortait();

        CmdPortrait();
        CmdtranslatePieceToStart(localPlayer.hero.startingLocation);
    }

    [TargetRpc]
    public void TargetShowDice(NetworkConnection nc)
    {
        Debug.Log("showing dice");
        dice.SetActive(true);
    }

    [TargetRpc]
    public void TargetHideDice(NetworkConnection nc)
    {
        Debug.Log("showing dice");
        dice.SetActive(false);
    }

    [TargetRpc]
    public void TargetassignPortrait(NetworkConnection nc)
    {
        GameObject.Find("Tile").GetComponent<TalismanBoardScript>().initializePlayers();
    }

    [ClientRpc]
    void RpcprintStats()
    {
        Debug.Log("NETSTATS");
        Debug.Log("current: " + current);
        Debug.Log("Turn: " + turn);
        Debug.Log("PlayerName: " + localPlayer.hero.name);
        Debug.Log("nowMoves: " + nowMoves);
        //Debug.Log("current: " + current);
        Debug.Log("NETSTATS_END");
    }
}
