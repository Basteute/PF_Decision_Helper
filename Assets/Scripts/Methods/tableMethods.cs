using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class tableMethods
{
    public static string[] initNamePosition(string [] initalNameByPosition)
    {
        if (TableDataClass.NumberOfPlayer == 2) initalNameByPosition = new string[] { "BU", "BB" };
        else if (TableDataClass.NumberOfPlayer == 3) initalNameByPosition = new string[] { "BU", "SB", "BB" };
        else if (TableDataClass.NumberOfPlayer == 4) initalNameByPosition = new string[] { "BU", "SB", "BB", "CO" };
        else if (TableDataClass.NumberOfPlayer == 5) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG", "CO" };
        else if (TableDataClass.NumberOfPlayer == 6) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG", "HJ", "CO" };
        else if (TableDataClass.NumberOfPlayer == 7) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG1", "UTG2", "CO" };
        else if (TableDataClass.NumberOfPlayer == 8) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG1", "UTG2", "MP1", "CO" };
        else if (TableDataClass.NumberOfPlayer == 9) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG1", "UTG2", "MP1", "MP2", "CO" };
        TableDataClass.NameByPosition = initalNameByPosition;
        return initalNameByPosition;
    }

    public static float [] initBetPosition(float [] initalBetByPosition)
    {
        if (TableDataClass.NumberOfPlayer == 2) initalBetByPosition = new float[] { 0.5f, 1 };
        else if (TableDataClass.NumberOfPlayer == 3) initalBetByPosition = new float[] { 0, 0.5f, 1 };
        else if (TableDataClass.NumberOfPlayer == 4) initalBetByPosition = new float[] { 0, 0.5f, 1, 0 };
        else if (TableDataClass.NumberOfPlayer == 5) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0 };
        else if (TableDataClass.NumberOfPlayer == 6) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0 };
        else if (TableDataClass.NumberOfPlayer == 7) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0, 0 };
        else if (TableDataClass.NumberOfPlayer == 8) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0, 0, 0 };
        else if (TableDataClass.NumberOfPlayer == 9) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0, 0, 0, 0 };
        TableDataClass.BetsByPosition = initalBetByPosition;
        return initalBetByPosition;
    }

    public static bool[] initFoldByPosition(bool [] initialFoldByPosition)
    {
        if (TableDataClass.NumberOfPlayer == 2) initialFoldByPosition = new bool[] { false, false };
        else if (TableDataClass.NumberOfPlayer == 3) initialFoldByPosition = new bool[] { false, false, false };
        else if (TableDataClass.NumberOfPlayer == 4) initialFoldByPosition = new bool[] { false, false, false, false };
        else if (TableDataClass.NumberOfPlayer == 5) initialFoldByPosition = new bool[] { false, false, false, false, false };
        else if (TableDataClass.NumberOfPlayer == 6) initialFoldByPosition = new bool[] { false, false, false, false, false, false };
        else if (TableDataClass.NumberOfPlayer == 7) initialFoldByPosition = new bool[] { false, false, false, false, false, false, false };
        else if (TableDataClass.NumberOfPlayer == 8) initialFoldByPosition = new bool[] { false, false, false, false, false, false, false, false };
        else if (TableDataClass.NumberOfPlayer == 9) initialFoldByPosition = new bool[] { false, false, false, false, false, false, false, false, false };
        TableDataClass.FoldByPosition = initialFoldByPosition;
        return initialFoldByPosition;
    }

    public static void SetBetsOnTable()
    {
        // Set the bets
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            GameObject betPlayer = GameObject.Find(i.ToString() + "BetPlayer");

            if (TableDataClass.BetsByPosition[i] == 0.5)
            {
                betPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/SB"); ;
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (TableDataClass.BetsByPosition[i] == 1)
            {
                betPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/BB"); ;
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (TableDataClass.BetsByPosition[i] == 2)
            {
                betPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/orangeChip");
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (TableDataClass.BetsByPosition[i] == 3)
            {
                betPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/blueChip");
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (TableDataClass.BetsByPosition[i] == 4)
            {
                betPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/yellowChip");
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                TableDataClass.BetsByPosition[i] = 0; // fold
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 0); // Delete the Bet
            }
        }
    }

    public static void SetCurrentPlayer(int playerID)
    {
        // Erase all the previous highlight
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
            GameObject.Find(i.ToString() + "PanelPlayer").GetComponent<Image>().color = new Color(1, 1, 0.67f, 0);


        // Skip the players who have fold
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            // exception at the end of the list
            if (i == TableDataClass.NumberOfPlayer - 1 && TableDataClass.FoldByPosition[i] == true)
            {
                playerID = 0;
                break;
            }

            // Skipper
            if (TableDataClass.FoldByPosition[i] == false && i >= playerID)
            {
                playerID = i;
                break;
            }
        }

        // Highlight the new current player and erase the color of the previous one
        GameObject.Find(playerID.ToString() + "PanelPlayer").GetComponent<Image>().color = new Color(1, 1, 0.67f, 0.5f);

        // Change the name of the current player on the board
        GameObject.Find("TxtCurrentPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[playerID];

        TableDataClass.CurrentPlayer = TableDataClass.NameByPosition[playerID];
    }

    public static int getCurrentPlayerID()
    {
        // Select the player
        string player = GameObject.Find("TxtCurrentPlayer").GetComponent<Text>().text;

        // Find the player ID
        int index = -1;
        for (int i = 0; i < TableDataClass.NameByPosition.Length; i++)
            if (player.Equals(TableDataClass.NameByPosition[i]))
                index = i;

        return index;
    }
}

