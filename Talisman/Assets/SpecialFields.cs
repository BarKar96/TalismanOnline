using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpecialFields : MonoBehaviour {
    private TalismanBoardScript tms;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Descryption;
    public TextMeshProUGUI MessageText;
    public GameObject panel;
    public GameObject MessageBox;
	// Use this for initialization
    public void SetPanelOn()
    {
        panel.SetActive(true);
        Gospoda();
    }
	public void Gospoda()
    {
        Name.text = "Gospoda";
        Descryption.text = "Wykonaj 1 rzut kością. \n(1) Upiłeś się i zasnąłeś w kącie - tracisz turę. \n(2) Upiłeś się i wdałeś w bójkę z chłopem tracisz 1 hp. \n(3) Grałeś w karty i przegrałeś 1 sztukę złota. \n(4) Grałeś w karty i wygrałeś jedną sztukę złota. \n(5) Potknąłeś się wpadłeś do piwnicy. Znalazłeś dwie sztuki złota. \n(6) Karateka uczy Ciebie sztuk walki (+1 punkt Siły.)";
    }
    public void Rzuckosciascript()
    {
        StartCoroutine(RzucKoscia());
    }
    public IEnumerator RzucKoscia()
    {
        var script = GameObject.Find("D6").GetComponent<DiceScript>();
        var dice = GameObject.Find("D6");
        dice.gameObject.transform.localPosition = new Vector3(-100, 0, 0);
        script.roll();
        yield return new WaitForSeconds(5);
        MessageBox.SetActive(true);
        switch (DiceScript.getResult())
        {
            case 1:
                MessageText.text = "Hańba Ci przesypiasz swoją turę!!!";
                break;
            case 2:
                MessageText.text = "Ha co z Ciebie za poszukiwacz jak przegrywasz ze zwykłym chłopem - tracisz 1 hp.";
                break;
            case 3:
                MessageText.text = "Hazard nie zawsze popłaca - tracisz jedną sztukę złota.";
                break;
            case 4:
                MessageText.text = "Gratulację wygrałeś w karty jedną sztukę złota.";
                break;
            case 5:
                MessageText.text = "Gratulację znalazłeś dwie sztuki złota.";
                break;
            case 6:
                MessageText.text = "Też mi karateka skoro go tak łatwo pokonałem - siła zwiększona o 1";
                break;
        }
    }
}
