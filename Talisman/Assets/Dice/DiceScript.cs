using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{

    static Rigidbody rb;
    public static Vector3 diceVelocity;
    public GameObject buttonLewo;
    public GameObject buttonPrawo;
    public GameObject buttonRzutKoscia;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        var go = GameObject.Find("DiceCheckZone").GetComponent<DiceCheckZoneScript>();
        go.GetComponent<DiceCheckZoneScript>().enabled = false;
    }
    public void roll()
    {
        //GameObject.Find("Tile").GetComponent<TalismanBoardScript>().HelperTextBox.GetComponent<Text>().text = "Wybierz Kierunek ruchu!";
        var go = GameObject.Find("DiceCheckZone").GetComponent<DiceCheckZoneScript>();
        go.GetComponent<DiceCheckZoneScript>().enabled = true;
        diceVelocity = rb.velocity;
        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);
        transform.localPosition = new Vector3(0, 15, -50);
        transform.rotation = Quaternion.identity;
        rb.AddForce(transform.up * 500);
        rb.AddTorque(dirX, dirY, dirZ);
    }
    public static int getResult()
    {
        return DiceCheckZoneScript.result;
    }
    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;
    }
}
