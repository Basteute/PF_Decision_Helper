using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDisplayer : MonoBehaviour
{
	public GameObject mainPanel;

	// Use this for initialization
	void Start ()
	{
		//createEmptyRangeArray();
	}

	private void createEmptyRangeArray()
	{
		Vector2 mainPanelSize = mainPanel.GetComponent<RectTransform>().rect.size;
		GameObject originalHandObject = GameObject.Find("HandAA");

		// Set the size and position of the object to clone
		originalHandObject.GetComponent<RectTransform>().sizeDelta =
				new Vector2(mainPanelSize.x * 0.0006f, mainPanelSize.y * 0.0006f);

		originalHandObject.GetComponent<RectTransform>().anchoredPosition =
				new Vector2(mainPanelSize.x * 0.006f, mainPanelSize.y * 0.006f);

	}
}
