using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CardLogic : MonoBehaviour {

    private GameObject cardItself;
    //  To be filled with number of possible card textures
    private int totalCards = 140;
    //  Array for quick card switching
    private Material[] cardTextures = new Material[140];

    // Use this for initialization
    void Start () {
        cardItself = GameObject.Find("Card");
	}
	
	// Update is called once per frame
	void Update () {
        //Face.GetComponent<Renderer>().material.mainTexture = textures[0];
        Texture newTexture = new Texture();
        //newTexture.
        //cardItself.GetComponent<Renderer>().material.mainTexture = 
    }
}
