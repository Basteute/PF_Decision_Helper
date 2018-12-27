using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsivePanelCardManager : MonoBehaviour {

    public GameObject panel; 
    public GameObject upArrow;
    public GameObject card;
    public GameObject downArrow;

    private void Update()
    {
        Vector2 panelSize = panel.GetComponent<RectTransform>().rect.size;

        // Change size of the upArrow
        upArrow.GetComponent<RectTransform>().sizeDelta = new Vector2(panelSize.x * 0.4f, panelSize.y * 0.15f);

        // Change size of the card
        card.GetComponent<RectTransform>().sizeDelta = new Vector2(panelSize.x * 0.6f, panelSize.y * 0.6f);

        // Change size of the downArrow
        downArrow.GetComponent<RectTransform>().sizeDelta = new Vector2(panelSize.x * 0.4f, panelSize.y * 0.15f);
    }

}
