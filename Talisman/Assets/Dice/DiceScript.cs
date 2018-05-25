﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{

    static Rigidbody rb;
    public static Vector3 diceVelocity;


    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        Debug.Log("St");
        //roll();
       
    }
    public void roll()
    {
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
    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;

       
            
    
    }
}