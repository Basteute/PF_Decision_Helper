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
        // Change size of the panel selector
        Vector2 mainPanelSize = mainPanel.GetComponent<RectTransform>().rect.size;
        selectorPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(mainPanelSize.x, mainPanelSize.y);

        // Change position of the panel selector
        Vector2 mainPanelPos = mainPanel.GetComponent<RectTransform>().rect.position;
        selectorPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(mainPanelPos.x * 0.5f,
         mainPanelPos.y * 0.1f);

        // Change the size of the selector buttons
        for(int i = 1; i <= 4; i++)
        {
           float btnWidth = GameObject.Find("BtnColor" + i).GetComponent<RectTransform>().rect.size.x;
           float parentPanelHeight = selectorPanel.GetComponent<RectTransform>().rect.size.y;

           GameObject.Find("BtnColor" + i).GetComponent<RectTransform>().sizeDelta =
            new Vector2(btnWidth, parentPanelHeight * 0.8f);
        }

        // Change the margins between the selectors and their parent
        Vector2 selectorPanelSize = selectorPanel.GetComponent<RectTransform>().rect.size;
        RectOffset tempPadding = new RectOffset((int) (selectorPanelSize.y * 0.02), (int) (selectorPanelSize.y * 0.02),
           (int) (selectorPanelSize.x * 0.02), (int) (selectorPanelSize.y * 0.02));
        selectorPanel.GetComponent<LayoutGroup>().padding = tempPadding;
    }

}
