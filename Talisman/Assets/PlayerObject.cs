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
    public Field[] fields;
    public Player localPlayer;// = new Player("Suavek", new Hero(Assets.hero_type.DRUID), 1);
    public Piece localPiece;

    public static bool RequestMovement = false;
    public static char direction = 'l';
    public static int turn = 0;
    //[SyncVar]
    public static int current = 0;

    // Use this for initialization
    void Start()
    {
        //this.localPlayer = new Player("Suavek", new Hero(Assets.hero_type.DRUID), turn);
        //turn++;
        fields = TalismanBoardScript.outerRing;
        if (!isLocalPlayer)
            return;
        Debug.Log("Created local player piece");
        //localPlayer = new Player("Suavek", new Hero(Assets.hero_type.DRUID), turn);
        //Instantiate(PlayerUnitPrefab);
        CmdspawnPlayerPiece();
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
                Debug.Log("Player name: " + this.localPlayer.name);
                Debug.Log("Turn / current / playerTurn:" + turn + " / " + current + " / " + localPlayer.NET_Turn);
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
        if (RequestMovement)
        {
            var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
            switch (direction)
            {
                case 'l':
                    Debug.Log("moving player : " + current + " left:" + go.diceResult);
                    break;

                case 'r':
                    Debug.Log("moving player : " + current + " right: " + go.diceResult);
                    break;

                default:

                    break;
            }
            if (current < turn - 1)
            {
                current++;
                RpcupdateTurn(current, turn);
            }
            else
                RpcupdateTurn(0, turn);

            RequestMovement = false;
        }

    }
  

    [SyncVar]
    public string playername = "asjkglhalsk";

    //******SERVER_SIDE******
    
    [Command]
    void CmdChangePlayerName(string s)
    {
        playername = s;
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
        this.name = "Piece" + turn;
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
        //fields[localPlayer.hero.startingLocation].counter++;
        
    }

    [Command]
    void CmdRollDice()
    {
        if (!(current == localPlayer.NET_Turn))
            return;
        int k = Random.Range(1, 7);
        //current = k;
        Debug.Log("Server sets current to: " + k);
        localPlayerPiece.transform.position = fields[(localPlayer.NET_RingPos + k) % fields.Length].emptyGameObject.transform.position;
        localPlayer.NET_RingPos = (localPlayer.NET_RingPos + k) % fields.Length;
        localPlayer.boardField = fields[localPlayer.NET_RingPos].fieldEvent;
        //RpcupdateTurn(k);
        if (current < turn-1)
        {
            current++;
            RpcupdateTurn(current, turn);
        }            
        else
            RpcupdateTurn(0, turn);
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
        Debug.Log("Moving: " + abs((localPlayer.NET_RingPos - k) % fields.Length));
        localPlayerPiece.transform.position = fields[abs((localPlayer.NET_RingPos - k) % fields.Length)].emptyGameObject.transform.position;

        localPlayer.NET_RingPos = (localPlayer.NET_RingPos - k) % fields.Length;
        localPlayer.boardField = fields[abs(localPlayer.NET_RingPos)].fieldEvent;
        //RpcupdateTurn(k);
        if (current < turn - 1)
        {
            current++;
            RpcupdateTurn(current, turn);
        }
        else
            RpcupdateTurn(0, turn);

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
        if (current < turn - 1)
        {
            current++;
            RpcupdateTurn(current, turn);
        }
        else
            RpcupdateTurn(0, turn);
        Debug.Log("Moving online player to the right");
    }
    //******CLIENT_SIDE******

    [ClientRpc]
    void RpcupdateTurn(int c, int t)
    {
        turn = t;
        current = c;
        Debug.Log("Server wants " + playername + "to set current to: " + c + " / " + current);
    }

    [ClientRpc]
    void RpcAssignPlayer(string s, int turn)
    {
        Debug.Log("Player " + playername + " Sets new hero");
        Player p = new Player("S", new Hero(Assets.hero_type.CZARNOKSIEZNIK), turn);
        p.boardField = fields[p.NET_RingPos].fieldEvent;
        localPlayer = p;
        CmdtranslatePieceToStart(localPlayer.hero.startingLocation);
    }
}
