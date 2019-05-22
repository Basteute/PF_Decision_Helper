using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsivePlayerNumberManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject panel;
    public GameObject rightBtn;
    public GameObject txtPlayerNumber;
    public GameObject leftBtn;

    // Use this for initialization
    void Start()
    {
        Vector2 panelSize = panel.GetComponent<RectTransform>().rect.size;
        Vector2 mainCanvasSize = mainCanvas.GetComponent<RectTransform>().rect.size;

        // Change the size of the panel
        //panel.GetComponent<RectTransform>().sizeDelta = new Vector2(mainCanvasSize.x * 0.8f, mainCanvasSize.y * 0.3f);

        // Change size of the upArrow
        rightBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(panelSize.x * 0.25f, panelSize.y * 0.5f);

        // Change size of the card
        txtPlayerNumber.GetComponent<RectTransform>().sizeDelta = new Vector2(panelSize.x * 0.5f, panelSize.y * 0.8f);

        // Change size of the downArrow
        leftBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(panelSize.x * 0.25f, panelSize.y * 0.5f);
    }

}
