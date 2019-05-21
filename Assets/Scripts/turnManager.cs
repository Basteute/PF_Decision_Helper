using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnManager : MonoBehaviour
{
    bool[] initialFoldByPosition;

    private void Start()
    {
        //initFoldByPosition();

        // Set the current player
        if (TableDataClass.NumberOfPlayer == TableDataClass.GetIndexOfPosition("BU") + 3)
            SetCurrentPlayer(0);
        else if ((TableDataClass.NumberOfPlayer + 1 == TableDataClass.GetIndexOfPosition("BU") + 3))
            SetCurrentPlayer(1);
        else if((TableDataClass.NumberOfPlayer + 2 == TableDataClass.GetIndexOfPosition("BU") + 3))
            SetCurrentPlayer(2);
        else
            SetCurrentPlayer(TableDataClass.GetIndexOfPosition("BU") + 3);

        // init for Undo button
        TableDataClass.PlayersHistory.Clear();
        TableDataClass.ActionsHistory.Clear();
        TableDataClass.BetsHistory.Clear();
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
        if(playerPosition == 0)
        {
          // stop the game because hero has fold
          SetCurrentPlayer(0);
        }
        else if (playerPosition == TableDataClass.NumberOfPlayer - 1)
        {
          SetCurrentPlayer(0);
          TableDataClass.PlayersHistory.Add(TableDataClass.NameByPosition[playerPosition]);
          TableDataClass.ActionsHistory.Add("fold");
          TableDataClass.BetsHistory.Add(TableDataClass.BetsByPosition[playerPosition]);
        }
        else
        {
          SetCurrentPlayer(playerPosition + 1);
          TableDataClass.PlayersHistory.Add(TableDataClass.NameByPosition[playerPosition]);
          TableDataClass.ActionsHistory.Add("fold");
          TableDataClass.BetsHistory.Add(TableDataClass.BetsByPosition[playerPosition]);
        }

        SetBetsOnTable();
    }

    public void Raise()
    {
        int index = getCurrentPlayerID();

        // For Undo Button
        TableDataClass.BetsHistory.Add(TableDataClass.BetsByPosition[index]);

        // Increase the players's bet
        int[] maxBet = maxBetOnTable();
        TableDataClass.BetsByPosition[index] = maxBet[0] + 1f;

        // Change the current player
        if (index == TableDataClass.NumberOfPlayer - 1)
            SetCurrentPlayer(0);
        else
            SetCurrentPlayer(index + 1);

        SetBetsOnTable();
        ChangeRaiseButtonIcons();

        // For Undo Button
        TableDataClass.PlayersHistory.Add(TableDataClass.NameByPosition[index]);
        TableDataClass.ActionsHistory.Add("raise");

    }

    public void Call()
    {
        int index = getCurrentPlayerID();

        // For Undo Button
        TableDataClass.BetsHistory.Add(TableDataClass.BetsByPosition[index]);

        // Call
        int[] maxBet = maxBetOnTable();
        TableDataClass.BetsByPosition[index] = (float)maxBet[0];

        // Change the current player
        if (index == TableDataClass.NumberOfPlayer - 1)
            SetCurrentPlayer(0);
        else
            SetCurrentPlayer(index + 1);

        SetBetsOnTable();

        // For Undo Button
        TableDataClass.PlayersHistory.Add(TableDataClass.NameByPosition[index]);
        TableDataClass.ActionsHistory.Add("call");

    }

    public void Undo()
    {
      if (TableDataClass.PlayersHistory.Count > 0)
      {
          // Find the previous player
          int previousPlayer = -1;
           for(int i = 0; i < TableDataClass.NumberOfPlayer; i++)
          {
            if(TableDataClass.PlayersHistory[TableDataClass.PlayersHistory.Count - 1] == TableDataClass.NameByPosition[i])
            {
              previousPlayer = i;
            }
          }

          //display the raise button
          int[] maxBet = maxBetOnTable();
          if (maxBet[0] <= 4)
          {
              GameObject.Find("BtnRaise").GetComponent<Button>().interactable = true;
              GameObject.Find("BtnRaise").GetComponent<Image>().color = new Color(1, 1, 1, 1);
              GameObject.Find("ImgRaise").GetComponent<Image>().color = new Color(1, 1, 1, 1);
              GameObject.Find("TxtRaise").GetComponent<Text>().text = "Raise";
          }

          // reset the currentPlayer player's bet
          TableDataClass.BetsByPosition[previousPlayer] = (float) TableDataClass.BetsHistory[TableDataClass.BetsHistory.Count - 1];

          // if the previous player has fold
          if(TableDataClass.ActionsHistory[TableDataClass.ActionsHistory.Count - 1].Equals("fold"))
          {
              GameObject.Find(previousPlayer.ToString() + "BtnCardPlayer").GetComponent<Image>().color = new Color(1, 1, 1, 1);
              GameObject.Find(previousPlayer.ToString() + "BtnCardPlayer").GetComponent<Button>().interactable = true;
              TableDataClass.FoldByPosition[previousPlayer] = false;
          }

          // Erase the previous highlight
          GameObject.Find(getCurrentPlayerID().ToString() + "PanelPlayer").GetComponent<Image>().color = new Color(1, 1, 0.67f, 0);

          // Highlight the new current player and erase the color of the previous one
          GameObject.Find(previousPlayer.ToString() + "PanelPlayer").GetComponent<Image>().color = new Color(1, 1, 0.67f, 0.5f);

          // Change the name of the current player on the board
          GameObject.Find("TxtCurrentPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[previousPlayer];
          TableDataClass.CurrentPlayer = TableDataClass.NameByPosition[previousPlayer];

          // Erase the previous actions
          TableDataClass.PlayersHistory.RemoveAt(TableDataClass.PlayersHistory.Count - 1);
          TableDataClass.ActionsHistory.RemoveAt(TableDataClass.ActionsHistory.Count - 1);
          TableDataClass.BetsHistory.RemoveAt(TableDataClass.BetsHistory.Count - 1);

          SetBetsOnTable();
          ChangeRaiseButtonIcons();
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

    public void SetCurrentPlayer(int playerID)
    {
        // Erase all the previous highlight
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
            GameObject.Find(i.ToString() + "PanelPlayer").GetComponent<Image>().color = new Color(1, 1, 0.67f, 0);

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
        TableDataClass.CurrentPlayer = TableDataClass.NameByPosition[playerID];
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
            else if (TableDataClass.BetsByPosition[i] == 1f)
            {
                betPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/BB");
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (TableDataClass.BetsByPosition[i] == 2f)
            {
                betPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/orangeChip");
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (TableDataClass.BetsByPosition[i] == 3f)
            {
                betPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/blueChip");
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (TableDataClass.BetsByPosition[i] == 4f)
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

    void ChangeRaiseButtonIcons()
    {
        // change the icon from the board buttons
        int[] maxBet = maxBetOnTable();
        switch (maxBet[0])
        {
            case 1:
                GameObject.Find("ImgCall").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/BB");
                GameObject.Find("ImgRaise").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/orangeChip");
                GameObject.Find("BtnRaise").GetComponent<Image>().color = new Color(1, 1, 1, 1);

                //display the raise button
                GameObject.Find("BtnRaise").GetComponent<Button>().interactable = true;
                GameObject.Find("BtnRaise").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.Find("ImgRaise").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.Find("TxtRaise").GetComponent<Text>().text = "Raise";
                break;

            case 2:
                GameObject.Find("ImgCall").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/orangeChip");
                GameObject.Find("ImgRaise").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/blueChip");
                GameObject.Find("BtnRaise").GetComponent<Image>().color = new Color(1, 1, 1, 1);

                //display the raise button
                GameObject.Find("BtnRaise").GetComponent<Button>().interactable = true;
                GameObject.Find("BtnRaise").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.Find("ImgRaise").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.Find("TxtRaise").GetComponent<Text>().text = "Raise";
                break;

            case 3:
                GameObject.Find("ImgCall").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/blueChip");
                GameObject.Find("ImgRaise").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/yellowChip");

                //display the raise button
                GameObject.Find("BtnRaise").GetComponent<Button>().interactable = true;
                GameObject.Find("BtnRaise").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.Find("ImgRaise").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.Find("TxtRaise").GetComponent<Text>().text = "Raise";
                break;



            default:
                GameObject.Find("ImgCall").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/yellowChip");
                GameObject.Find("BtnRaise").GetComponent<Button>().interactable = false;
                GameObject.Find("BtnRaise").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.Find("ImgRaise").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.Find("TxtRaise").GetComponent<Text>().text = "";
                break;
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
