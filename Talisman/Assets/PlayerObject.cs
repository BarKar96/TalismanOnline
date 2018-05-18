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
    public Player localPlayer;
    public Piece localPiece;
    [SyncVar]
    public int turn = 0;
    
    public int current = 0;
    // Use this for initialization
    void Start()
    {
        this.localPlayer = new Player("Suavek", new Hero(Assets.hero_type.TROLL), turn);
        fields = TalismanBoardScript.outerRing;
        if (!isLocalPlayer)
            return;
        Debug.Log("Created local player piece");
        
        //Instantiate(PlayerUnitPrefab);
        CmdspawnPlayerPiece();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
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
            Debug.Log("Roll ");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            string n = "Worked?:" + Random.Range(1,23);
            CmdChangePlayerName(n);
        }
    }

    [SyncVar]
    public string playername = "asjkglhalsk";

    [Command]
    void CmdChangePlayerName(string s)
    {
        playername = s;
    }

    [Command]
    void CmdspawnPlayerPiece()
    {
        
        turn++;

        GameObject go = Instantiate(PlayerUnitPrefab);
        localPlayerPiece = go;
        //Piece p = go.GetComponent<Piece>();
        //localPlayer.playerPiece = p;
        CmdtranslatePieceToStart();
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
    void CmdtranslatePieceToStart()
    {
        /*if (!(current == localPlayer.NET_Turn))
            return;*/
        //localPlayer.playerPiece.indexOfField = localPlayer.hero.startingLocation;
        Debug.Log("Fields are: " + fields.Length);
        localPlayerPiece.transform.position = fields[localPlayer.hero.startingLocation].emptyGameObject.transform.position;
        //fields[localPlayer.hero.startingLocation].counter++;
        
    }

    [Command]
    void CmdRollDice()
    {
        int k = Random.Range(1, 7);
        current = k;
        Debug.Log("Server sets current to: " + k);
        localPlayerPiece.transform.position = fields[(localPlayer.NET_RingPos + k) % fields.Length].emptyGameObject.transform.position;
        localPlayer.NET_RingPos = (localPlayer.NET_RingPos + k) % fields.Length;
        RpcupdateTurn(k);
    }
    [ClientRpc]
    void RpcupdateTurn(int c)
    {
        this.current = c;
        Debug.Log("Server wants Clients to set current to: " + c + " / " + current);
    }
    
}
