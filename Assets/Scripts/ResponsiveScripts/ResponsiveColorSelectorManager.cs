using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponsiveColorSelectorManager : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject selectorPanel;

    // Use this for initialization
    void Start()
    {
        Vector2 mainPanelSize = mainPanel.GetComponent<RectTransform>().rect.size;
        selectorPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(mainPanelSize.x * 0.9f, mainPanelSize.y * 0.5f);
    }

}
