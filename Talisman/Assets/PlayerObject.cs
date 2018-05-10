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

    // Use this for initialization
    void Start()
    {
        localPlayer = new Player("Suavek", new Hero(Assets.hero_type.TROLL));
        
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
            CmdTranslatePiece();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CmdRollDice();
            Debug.Log("Roll ");
        }
    }

    [Command]
    void CmdspawnPlayerPiece()
    {
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
        //localPlayer.playerPiece.indexOfField = localPlayer.hero.startingLocation;
        Debug.Log("Fields are: " + fields.Length);
        localPlayerPiece.transform.position = fields[localPlayer.hero.startingLocation].emptyGameObject.transform.position;
        //fields[localPlayer.hero.startingLocation].counter++;
    }

    [Command]
    void CmdRollDice()
    {
        int k = Random.Range(1, 7);
        localPlayerPiece.transform.position = fields[(localPlayer.NET_RingPos + k) % fields.Length].emptyGameObject.transform.position;
        localPlayer.NET_RingPos = (localPlayer.NET_RingPos + k) % fields.Length;
    }
    
    
}
