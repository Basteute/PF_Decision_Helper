using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initScript : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        // Cards value
        PlayerPrefs.SetInt("cardValueL", 7);
        PlayerPrefs.SetInt("cardValueR", 7);

        // Cards color
        PlayerPrefs.SetInt("cardColorL", 0);
        PlayerPrefs.SetInt("cardColorR", 3);
        PlayerPrefs.SetInt("Suit", 0);

        // Sides
        PlayerPrefs.SetString("cardValueSide", "left");
        PlayerPrefs.SetString("cardColorSide", "left");
	}
	
}
