﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnManager : MonoBehaviour
{
    bool[] initialFoldByPosition;

    private void Start()
    {
        initFoldByPosition();

        if (TableDataClass.NumberOfPlayer == 3)
            SetCurrentPlayer(0);
        else
            SetCurrentPlayer(3);

        // init for Undo button
        if(TableDataClass.PreviousBet.Count == 0)
        {
            TableDataClass.PreviousBet.Add(0.5f);
            TableDataClass.PreviousBet.Add(1);
        }
    }

    public void Fold()
    {
        int playerPosition = getCurrentPlayerID();

        // Hide the player's cards
        if(playerPosition != 0)
        {
            GameObject.Find(playerPosition.ToString() + "BtnCardPlayer").GetComponent<Image>().color = new Color(1, 1, 1, 0);
            GameObject.Find(playerPosition.ToString() + "BtnCardPlayer").GetComponent<Button>().interactable = false;
            TableDataClass.FoldByPosition[playerPosition] = true;
        }

        // Change the current player
        if (playerPosition == TableDataClass.NumberOfPlayer - 1)
            SetCurrentPlayer(0);
        else
            SetCurrentPlayer(playerPosition + 1);

        SetBetsOnTable();
        TableDataClass.PreviousAction.Add("fold");
    }

    public void Raise()
    {
        int index = getCurrentPlayerID();

        // Increase the players's bet
        int[] maxBet = maxBetOnTable();
        TableDataClass.BetsByPosition[index] = maxBet[0] + 1;

        // Change the current player
        if (index == TableDataClass.NumberOfPlayer - 1)
            SetCurrentPlayer(0);
        else
            SetCurrentPlayer(index + 1);

        // change the icon from the board buttons       
        switch(maxBet[0] + 1)
        {
            case 2:
                GameObject.Find("ImgCall").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/orangeChip");
                GameObject.Find("ImgRaise").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/blueChip");
                break;

            case 3:
                GameObject.Find("ImgCall").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/blueChip");
                GameObject.Find("ImgRaise").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/yellowChip");
                break;
        }

        SetBetsOnTable();
        TableDataClass.PreviousAction.Add("raise");
        TableDataClass.PreviousBet.Add(maxBet[0] + 1f);
    }

    public void Call()
    {
        int index = getCurrentPlayerID();

        // Call
        int[] maxBet = maxBetOnTable();
        TableDataClass.BetsByPosition[index] = maxBet[0];

        // Change the current player
        if (index == TableDataClass.NumberOfPlayer - 1)
            SetCurrentPlayer(0);
        else
            SetCurrentPlayer(index + 1);

        SetBetsOnTable();
        TableDataClass.PreviousAction.Add("call");
        TableDataClass.PreviousBet.Add((float)maxBet[0]);
    }

    public void Undo()
    {
        if(TableDataClass.PreviousAction.Count > 0)
        {
            // find the previous player
            int previousPlayer;
            if (getCurrentPlayerID() == 0)
                previousPlayer = TableDataClass.NumberOfPlayer - 1;
            else
                previousPlayer = getCurrentPlayerID() - 1;

            if (TableDataClass.PreviousAction[TableDataClass.PreviousAction.Count - 1].Equals("fold"))
            {
                if(previousPlayer != 0)
                {
                    GameObject.Find(previousPlayer.ToString() + "BtnCardPlayer").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    GameObject.Find(previousPlayer.ToString() + "BtnCardPlayer").GetComponent<Button>().interactable = true;
                    TableDataClass.FoldByPosition[previousPlayer] = false;
                }             
            }
            else // if the previous action was to call or raise 
            {
                if(TableDataClass.PreviousAction.Count >= TableDataClass.NumberOfPlayer - 1)
                {                 
                    TableDataClass.BetsByPosition[previousPlayer] = (float)TableDataClass.PreviousBet[TableDataClass.PreviousAction.Count - (TableDataClass.NumberOfPlayer - 1)];
                }
                else
                {
                    TableDataClass.BetsByPosition[previousPlayer] = 0;
                }

                TableDataClass.PreviousBet.RemoveAt(TableDataClass.PreviousBet.Count - 1);
            }

            // Erase the previous highlight
            GameObject.Find(getCurrentPlayerID().ToString() + "PanelPlayer").GetComponent<Image>().color = new Color(1, 1, 0.67f, 0);

            // Highlight the new current player and erase the color of the previous one
            GameObject.Find(previousPlayer.ToString() + "PanelPlayer").GetComponent<Image>().color = new Color(1, 1, 0.67f, 0.5f);

            // Change the name of the current player on the board
            GameObject.Find("TxtCurrentPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[previousPlayer];

            SetBetsOnTable();
            TableDataClass.PreviousAction.RemoveAt(TableDataClass.PreviousAction.Count - 1);
        }
    }

    // return the max number of bet (2Bet, 3Bet...) on the table and where it was
    int[] maxBetOnTable()
    {
        int numberOfPlayer = TableDataClass.BetsByPosition.Length;
        float maxBet = 1;
        int index = -1;
        for (int i = 0; i < numberOfPlayer; i++)
        {
            if (TableDataClass.BetsByPosition[i] > maxBet)
            {
                maxBet = TableDataClass.BetsByPosition[i];
                index = i;
            }
        }

        int[] result = new int[] { (int)maxBet, index };
        return result;
    }

    int getCurrentPlayerID()
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

    void SetCurrentPlayer(int playerID)
    {
        // Erase the previous highlight
        if (playerID == 0)
            GameObject.Find((TableDataClass.NumberOfPlayer - 1).ToString() + "PanelPlayer").GetComponent<Image>().color = new Color(1, 1, 0.67f, 0);
        else
            GameObject.Find((playerID - 1).ToString() + "PanelPlayer").GetComponent<Image>().color = new Color(1, 1, 0.67f, 0);

        // Skip the players who have fold
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            // exception at the end of the list
            if(i == TableDataClass.NumberOfPlayer - 1 && TableDataClass.FoldByPosition[i] == true)
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
    }

    void SetBetsOnTable()
    {
        // Set the bets
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            GameObject betPlayer = GameObject.Find(i.ToString() + "BetPlayer");

            if (TableDataClass.BetsByPosition[i] == 0.5)
            {
                betPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/SB");
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (TableDataClass.BetsByPosition[i] == 1)
            {
                betPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/BB");
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

    bool[] moveNext(bool[] array)
    {
        // init the final array
        bool[] result = new bool[array.Length];
        result[0] = array[array.Length - 1];

        // fill the final array
        for (int i = 1; i < array.Length; i++)
        {
            result[i] = array[i - 1];
        }

        return result;
    }

    void initFoldByPosition()
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
    }
}
