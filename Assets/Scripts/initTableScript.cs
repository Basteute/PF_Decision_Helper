using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initTableScript : MonoBehaviour
{
    string[] cardColor = { "H", "S", "D", "C" };
    string[] initalNameByPosition;
    float[] initalBetByPosition;

    private void Awake()
    {
        if (TableDataClass.CurrentPlayer == null) //if this is the first time that the app is openend
        {
            TableDataClass.initNamePosition();
            TableDataClass.initBetPosition();
            TableDataClass.initFoldByPosition();
        }


        // Init bets and positions
        initalNameByPosition = TableDataClass.initalNameByPosition;
        initalBetByPosition = TableDataClass.initalBetByPosition;

        initTable(true);
        /*
        // init the table
        if (TableDataClass.CurrentPlayer == null || TableDataClass.GetIndexOfPosition("BU") == TableDataClass.NumberOfPlayer - 1)
        {
            initTable(false);
        }
        else
        {
            initTable(true);
        }
        */

        // Disable all button cards in vilain panel
        for (int i = 1; i < TableDataClass.NumberOfPlayer; i++)
        {
            GameObject.Find(i.ToString() + "BtnCardPlayer").GetComponent<Button>().interactable = false;
            GameObject.Find(i.ToString() + "BtnPlayer").GetComponent<Button>().interactable = false;
        }

        tableMethods.SetBetsOnTable();
    }

    void initTable(bool moveButton)
    {
        // Display the face up cards of hero
        string cardNameL = PlayerPrefs.GetInt("cardValueL").ToString() + cardColor[PlayerPrefs.GetInt("cardColorL")];
        string cardNameR = PlayerPrefs.GetInt("cardValueR").ToString() + cardColor[PlayerPrefs.GetInt("cardColorR")];
        GameObject.Find("0CardL").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardNameL);
        GameObject.Find("0CardR").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardNameR);

        // Set the positions on table ("BU", "SB", "BB", "UTG" ...)
        if (!moveButton)
        {
            for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
            {
                GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text = initalNameByPosition[i];
            }
        }
        else
        {
            // Move each player position
            TableDataClass.NameByPosition = commonMethods.moveArrayStrNext(TableDataClass.NameByPosition);
            TableDataClass.BetsByPosition = commonMethods.moveArrayFltNext(TableDataClass.BetsByPosition);

            for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
            {
                GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[i];
            }
        }

        // Hide all the D tokens except the first one (at hero)
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            // Begin with hero as Dealer
            GameObject BtnDealer = GameObject.Find("BtnD" + i.ToString());
            if (i != TableDataClass.GetIndexOfPosition("BU"))
            {
                BtnDealer.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                BtnDealer.GetComponent<Button>().interactable = false;
            }
        }
    }

}
