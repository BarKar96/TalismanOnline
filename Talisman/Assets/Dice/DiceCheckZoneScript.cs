using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{

    Vector3 diceVelocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        diceVelocity = DiceScript.diceVelocity;
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("tutaj" + col.gameObject.name);
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            switch (col.gameObject.name)
            {
                case "Side1":
                    Debug.Log("1");
                    break;
                case "Side2":
                    Debug.Log("2");
                    break;
                case "Side3":
                    Debug.Log("3");
                    break;
                case "Side4":
                    Debug.Log("4");
                    break;
                case "Side5":
                    Debug.Log("5");
                    break;
                case "Side6":
                    Debug.Log("6");
                    break;
            }
        }
    }
}
