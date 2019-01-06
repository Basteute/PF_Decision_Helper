using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Lang;

public class BtnTableManager : MonoBehaviour
{
    string[] cardColor = { "H", "S", "D", "C" };
    string [] initalNameByPosition;
    float[] initalBetByPosition;

    // Init from the inspector
    public GameObject panelDealerSetter;
    public Sprite SB;
    public Sprite BB;

    // Init
    private void Start()
    {
        // Init bets and positions
        initalNameByPosition = TableDataClass.initalNameByPosition;
        initalBetByPosition = TableDataClass.initalBetByPosition;
    }

    void initTable(int posBU)
    {
        // Display the face up cards of hero
        string cardNameL = PlayerPrefs.GetInt("cardValueL").ToString() + cardColor[PlayerPrefs.GetInt("cardColorL")];
        string cardNameR = PlayerPrefs.GetInt("cardValueR").ToString() + cardColor[PlayerPrefs.GetInt("cardColorR")];
        GameObject.Find("0CardL").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardNameL);
        GameObject.Find("0CardR").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardNameR);

        // Hide all the D tokens except the first one (at hero)
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            // Begin with hero as Dealer
            GameObject BtnDealer = GameObject.Find("BtnD" + i.ToString());          
            if (i != posBU)
            {
                BtnDealer.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                BtnDealer.GetComponent<Button>().interactable = false;
            }
        }

        // Set the positions on table ("BU", "SB", "BB", "UTG" ...)
        if (posBU == 0)
        {
            for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
            {
                GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text = initalNameByPosition[i];
            }
        }
        else
        {
            // Move each player position
            moveArrayStrNext(TableDataClass.NameByPosition);          
            moveArrayFltNext(TableDataClass.BetsByPosition);   
            
            for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
            {
                GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[i];
            }
        }
    }

    // Open or close the panelDealer selector
    public void OpenDealerSetter()
    {
        ResetTable();

        // Diplay a panel wich mask the table and enable the D buttons
        panelDealerSetter.SetActive(!panelDealerSetter.activeSelf);

        if(panelDealerSetter.activeSelf)
        {
            // enable all the D buttons
            for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
            {
                GameObject BtnDealer = GameObject.Find("BtnD" + i.ToString());
                BtnDealer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                BtnDealer.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            DealerSetter(0);    
        }
 
        GameObject.Find("TxtActionInfo").GetComponent<Text>().text = "Choisissez un Dealer";
        SetBetsOnTable();
    }

    // Set the dealer after clicking on one of the dealer position
    public void DealerSetter(int position)
    {
        ResetTable();
        int numberOfPlayer = TableDataClass.NumberOfPlayer;

        if (panelDealerSetter.activeSelf)
        {
            // Hide all the D tokens
            for (int i = 0; i < numberOfPlayer; i++)
            {
                if (i != position)
                {
                    GameObject BtnDealer = GameObject.Find("BtnD" + i.ToString());
                    BtnDealer.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    BtnDealer.GetComponent<Button>().interactable = false;
                }
                else
                {
                    GameObject BtnDealer = GameObject.Find("BtnD" + i.ToString());
                    BtnDealer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    BtnDealer.GetComponent<Button>().interactable = true;
                }
            }

            // close the window and delete the information text
            panelDealerSetter.SetActive(!panelDealerSetter.activeSelf);
            GameObject.Find("TxtActionInfo").GetComponent<Text>().text = "";

            // Set the positions on table ("BU", "SB", "BB", "UTG" ...)
            TableDataClass.NameByPosition = initalNameByPosition;
            for (int i = 0; i < position; i++)
                TableDataClass.NameByPosition = moveArrayStrNext(TableDataClass.NameByPosition);

            for (int i = 0; i < numberOfPlayer; i++)
                GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[i];

            // fill the current list of player on the table
            for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
                TableDataClass.NameByPosition[i] = GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text;

            // init for Undo button
            TableDataClass.PreviousAction.Clear();
            TableDataClass.PreviousBet.Clear();
            TableDataClass.PreviousBet.Add(0.5f);
            TableDataClass.PreviousBet.Add(1);

            // Set the SB, BB and the current player
            Sprite mySB = Resources.Load<Sprite>("Resources/Icons/SB");
            if (position + 1 == numberOfPlayer)
            {
                ResetBets(0, 1);
                GameObject.Find("0BetPlayer").GetComponent<Image>().sprite = SB;
                GameObject.Find("1BetPlayer").GetComponent<Image>().sprite = BB;
                SetCurrentPlayer(2);
            }
            else if (position + 2 == numberOfPlayer)
            {
                ResetBets(numberOfPlayer - 1, 0);
                GameObject.Find((numberOfPlayer - 1).ToString() + "BetPlayer").GetComponent<Image>().sprite = SB;
                GameObject.Find("0BetPlayer").GetComponent<Image>().sprite = BB;
                SetCurrentPlayer(1);
            }
            else if (position + 3 == numberOfPlayer)
            {
                ResetBets(position + 1, position + 2);
                GameObject.Find((position + 1).ToString() + "BetPlayer").GetComponent<Image>().sprite = SB;
                GameObject.Find((position + 2).ToString() + "BetPlayer").GetComponent<Image>().sprite = BB;
                SetCurrentPlayer(0);
            }
            else
            {
                ResetBets(position + 1, position + 2);
                GameObject.Find((position + 1).ToString() + "BetPlayer").GetComponent<Image>().sprite = SB;
                GameObject.Find((position + 2).ToString() + "BetPlayer").GetComponent<Image>().sprite = BB;
                SetCurrentPlayer(position + 3);
            }
        }
        else
        {
            // click from a 'D' button
            OpenDealerSetter();
        }

        SetBetsOnTable();
    }

    public void ResetTable()
    {
        int numberOfPlayer = TableDataClass.NumberOfPlayer;

        // reset bets and names by position
        TableDataClass.NameByPosition = initalNameByPosition;
        initBetPosition();

        // reset the board
        GameObject.Find("ImgCall").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/BB");
        GameObject.Find("ImgRaise").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/orangeChip");
       
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            // fill the current list of player on the table
            GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[i];

            // hide all the bets except SB and BB
            if (i != 1 && i != 2)
                GameObject.Find(i.ToString() + "BetPlayer").GetComponent<Image>().color = new Color(1, 1, 1, 0);

            // Unfold players
            TableDataClass.FoldByPosition[i] = false;
            if(i != 0)
            {
                GameObject.Find(i.ToString() + "BtnCardPlayer").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.Find(i.ToString() + "BtnCardPlayer").GetComponent<Button>().interactable = true;
            }
        }

        // change the current player on the board           
        if (TableDataClass.NumberOfPlayer == 3)
        {
            GameObject.Find("TxtCurrentPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[TableDataClass.GetIndexOfPosition("BU")];
            SetCurrentPlayer(TableDataClass.GetIndexOfPosition("BU"));
            TableDataClass.CurrentPlayer = "BU";
        }
        else
        {
            GameObject.Find("TxtCurrentPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[TableDataClass.GetIndexOfPosition("BB") + 1];
            SetCurrentPlayer(3);
            TableDataClass.CurrentPlayer = TableDataClass.NameByPosition[TableDataClass.GetIndexOfPosition("BB") + 1]; ;
        }

        // Set SB and BB bets
        GameObject.Find("1BetPlayer").GetComponent<Image>().sprite = SB;
        GameObject.Find("2BetPlayer").GetComponent<Image>().sprite = BB;

        // Hide all the D tokens
        for (int i = 0; i < numberOfPlayer; i++)
        {
            GameObject BtnDealer = GameObject.Find("BtnD" + i.ToString());
            if (i != 0)
            {
                BtnDealer.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                BtnDealer.GetComponent<Button>().interactable = false;
            }
            else
            {
                BtnDealer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                BtnDealer.GetComponent<Button>().interactable = true;
            }
        }

        SetBetsOnTable();
    }

    void ResetBets(int SBPos, int BBPos)
    {
        // Reset Bets
        int numberOfPlayer = TableDataClass.BetsByPosition.Length;
        for (int i = 0; i < numberOfPlayer; i++)
        {
            TableDataClass.BetsByPosition[i] = 0;
        }
        TableDataClass.BetsByPosition[SBPos] = 0.5f;
        TableDataClass.BetsByPosition[BBPos] = 1;

        SetBetsOnTable();
    }

    void initNamePosition()
    {   
        if (TableDataClass.NumberOfPlayer == 2)     initalNameByPosition = new string[] { "BU", "BB" };
        else if(TableDataClass.NumberOfPlayer == 3) initalNameByPosition = new string[] { "BU", "SB", "BB" };
        else if(TableDataClass.NumberOfPlayer == 4) initalNameByPosition = new string[] { "BU", "SB", "BB", "CO" };
        else if(TableDataClass.NumberOfPlayer == 5) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG", "CO" };
        else if(TableDataClass.NumberOfPlayer == 6) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG", "HJ", "CO" };
        else if(TableDataClass.NumberOfPlayer == 7) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG1", "UTG2", "CO" };
        else if(TableDataClass.NumberOfPlayer == 8) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG1", "UTG2", "MP1", "CO" };
        else if(TableDataClass.NumberOfPlayer == 9) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG1", "UTG2", "MP1", "MP2", "CO" };
    }

    void initBetPosition()
    {
        if (TableDataClass.NumberOfPlayer == 2) initalBetByPosition = new float[] { 0.5f, 1 };
        else if (TableDataClass.NumberOfPlayer == 3) initalBetByPosition = new float[] { 0, 0.5f, 1 };
        else if (TableDataClass.NumberOfPlayer == 4) initalBetByPosition = new float[] { 0, 0.5f, 1, 0};
        else if (TableDataClass.NumberOfPlayer == 5) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0};
        else if (TableDataClass.NumberOfPlayer == 6) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0 };
        else if (TableDataClass.NumberOfPlayer == 7) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0, 0 };
        else if (TableDataClass.NumberOfPlayer == 8) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0, 0, 0};
        else if (TableDataClass.NumberOfPlayer == 9) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0, 0, 0, 0 };
    }

    string[] moveArrayStrNext(string[] array)
    {
        // init the final array
        string[] result = new string[array.Length];
        result[0] = array[array.Length - 1];

        // fill the final array
        for (int i = 1; i < array.Length; i++)
        {
            result[i] = array[i - 1];
        }

        return result;
    }

    float[] moveArrayFltNext(float[] array)
    {
        // init the final array
        float[] result = new float[array.Length];
        result[0] = array[array.Length - 1];

        // fill the final array
        for (int i = 1; i < array.Length; i++)
        {
            result[i] = array[i - 1];
        }

        return result;
    }

    void SetBetsOnTable()
    { 
        // Set the bets
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            GameObject betPlayer = GameObject.Find(i.ToString() + "BetPlayer");

            if (TableDataClass.BetsByPosition[i] == 0.5)
            {
                betPlayer.GetComponent<Image>().sprite = SB;
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else if (TableDataClass.BetsByPosition[i] == 1)
            {
                betPlayer.GetComponent<Image>().sprite = BB;
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

    public void SetCurrentPlayer(int playerID)
    {
        // Erase all the previous highlight
        for(int i = 0; i < TableDataClass.NumberOfPlayer; i++)     
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
}
/*
for(int i = 0; i < TableDataClass.NumberOfPlayer; i++)
{
	
}
*/
