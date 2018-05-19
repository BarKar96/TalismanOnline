using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    public int result = 0;

    Vector3 diceVelocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.enabled = true;
        }
        diceVelocity = DiceScript.diceVelocity;
    }

    public int OnTriggerStay(Collider col)
    {
        if (this.enabled)
        {
            Debug.Log("tutaj" + col.gameObject.name);
            if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
            {
                var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
                switch (col.gameObject.name)
                {
                    case "Side1":
                        Debug.Log("1");
                        go.diceResult = 1;
                        this.enabled = false;
                        break;
                    case "Side2":
                        Debug.Log("2");
                        go.diceResult = 2;
                        this.enabled = false;
                        break;
                    case "Side3":
                        Debug.Log("3");
                        go.diceResult = 3;
                        this.enabled = false;
                        break;
                    case "Side4":
                        Debug.Log("4");
                        go.diceResult = 4;
                        this.enabled = false;
                        break;
                    case "Side5":
                        Debug.Log("5");
                        go.diceResult = 5;
                        this.enabled = false;
                        break;
                    case "Side6":
                        Debug.Log("6");
                        go.diceResult = 6;
                        this.enabled = false;
                        break;
                }
            }
            
            
        }
        
        return result;
    }
}
