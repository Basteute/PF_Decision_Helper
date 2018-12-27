using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadRange : MonoBehaviour
{
    public Image range;

	// Use this for initialization
	void Start ()
    {
        //Sprite[] sprites = (Sprite[])Resources.LoadAll("Resources/Ranges");

        //GameObject[] objs = Resources.LoadAll("Resources/Ranges") as GameObject[];
        //if (!objs.Equals(null)) Debug.Log("1st name = " + objs[0]);

        string position = "UTG";
        Sprite UTGSprite = Resources.Load<Sprite>("Resources/" + position);
        //Debug.Log(UTGSprite.name);
        //range.GetComponent<Sprite>() = UTGSprite;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
