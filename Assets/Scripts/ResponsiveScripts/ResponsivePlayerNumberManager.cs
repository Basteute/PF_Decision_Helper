using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsivePlayerNumberManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject rightBtn;
    public GameObject txtPlayerNumber;
    public GameObject leftBtn;

    // Use this for initialization
    void Start()
    {
        Vector2 panelSize = panel.GetComponent<RectTransform>().rect.size;

        // Change size of the upArrow
        rightBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(panelSize.x * 0.1f, panelSize.y * 0.5f);

        // Change size of the card
        txtPlayerNumber.GetComponent<RectTransform>().sizeDelta = new Vector2(panelSize.x * 0.7f, panelSize.y * 0.8f);

        // Change size of the downArrow
        leftBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(panelSize.x * 0.1f, panelSize.y * 0.5f);
    }

}
