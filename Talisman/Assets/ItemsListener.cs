using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsListener : MonoBehaviour
{
    public void useItem(string name)
    {
        var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        Player[] tempPlayerArray = go.playerArray;
        int tempPlayerIndex = go.playerIndex;
        if (name == "zbroja")
        {
            Debug.Log(tempPlayerArray[tempPlayerIndex].current_health);
            tempPlayerArray[tempPlayerIndex].current_health += 10;
            Debug.Log(tempPlayerArray[tempPlayerIndex].current_health);
        }
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                useItem(hit.transform.gameObject.name);
            }
        }
    }
}
