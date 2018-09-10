using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceCheckZoneScript : MonoBehaviour
{
    public static int result = 0;
    public Button btnright;
    public Button btnleft;
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
            //Debug.Log("tutaj" + col.gameObject.name);
            if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
            {
                var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
                var combat = GameObject.Find("Combat").GetComponent<Combat>();
                var temp = GameObject.Find("D6").GetComponent<DiceScript>();
                var specialevents = GameObject.Find("SpecialEvents").GetComponent<SpecialFields>();

                if (combat.combatDiceBlock == false && specialevents.specialDiceBlock == false)
                {
                    
                    temp.buttonLewo.SetActive(true);
                    temp.buttonPrawo.SetActive(true);
                    temp.buttonRzutKoscia.SetActive(false);
                }
               
                switch (col.gameObject.name)
                {
                    case "Side1":
                        Debug.Log("1");
                        result = 1;
                        go.diceResult = 1;
                        if (combat.turaGracza == true)
                        {
                            combat.rzut_Ataku_gracza = 1;
                        }
                        else if (combat.turaIstoty == true)
                        {
                            combat.rzut_Ataku_istoty = 1;
                        }
                        this.enabled = false;
                        break;
                    case "Side2":
                        Debug.Log("2");
                        result = 2;
                        go.diceResult = 2;
                        if (combat.turaGracza == true)
                        {
                            combat.rzut_Ataku_gracza = 2;
                        }
                        else if (combat.turaIstoty == true)
                        {
                            combat.rzut_Ataku_istoty = 2;
                        }
                        this.enabled = false;
                        break;
                    case "Side3":
                        Debug.Log("3");
                        result = 3;
                        go.diceResult = 3;
                        if (combat.turaGracza == true)
                        {
                            combat.rzut_Ataku_gracza = 3;
                        }
                        else if (combat.turaIstoty == true)
                        {
                            combat.rzut_Ataku_istoty = 3;
                        }
                        this.enabled = false;
                        break;
                    case "Side4":
                        Debug.Log("4");
                        result = 4;
                        go.diceResult = 4;
                        if (combat.turaGracza == true)
                        {
                            combat.rzut_Ataku_gracza = 4;
                        }
                        else if (combat.turaIstoty == true)
                        {
                            combat.rzut_Ataku_istoty = 4;
                        }
                        this.enabled = false;
                        break;
                    case "Side5":
                        Debug.Log("5");
                        result = 5;
                        go.diceResult = 5;
                        if (combat.turaGracza == true)
                        {
                            combat.rzut_Ataku_gracza = 5;
                        }
                        else if (combat.turaIstoty == true)
                        {
                            combat.rzut_Ataku_istoty = 5;
                        }
                        this.enabled = false;
                        break;
                    case "Side6":
                        Debug.Log("6");
                        result = 6;
                        go.diceResult = 6;
                        if (combat.turaGracza == true)
                        {
                            combat.rzut_Ataku_gracza = 6;
                        }
                        else if (combat.turaIstoty == true)
                        {
                            combat.rzut_Ataku_istoty = 6;
                        }
                        this.enabled = false;
                        break;
                }
            }
            
            
        }
        
        return result;
    }
}
