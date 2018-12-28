using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Lang;

public class BtnTableManager : MonoBehaviour
{
    string[] cardColor = { "H", "S", "D", "C" };
    //List<string> initalNameByPosition;
    string [] initalNameByPosition;
    float[] initalBetByPosition;

    // Init from the inspector
    //public GameObject panelDealerSetter3;
    //public GameObject panelDealerSetter5;
    public GameObject panelDealerSetter;
    public Sprite SB;
    public Sprite BB;

    // Init
    private void Start()
    {
        // Init bets and positions
        initNamePosition();
        initBetPosition();

        // Set The first blinds
        GameObject.Find("0BetPlayer").GetComponent<Image>().sprite = SB;
        GameObject.Find("1BetPlayer").GetComponent<Image>().sprite = BB;

        // Display the face up cards of hero
        string cardNameL = PlayerPrefs.GetInt("cardValueL").ToString() + cardColor[PlayerPrefs.GetInt("cardColorL")];
        string cardNameR = PlayerPrefs.GetInt("cardValueR").ToString() + cardColor[PlayerPrefs.GetInt("cardColorR")];
        GameObject.Find("0CardL").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardNameL);
        GameObject.Find("0CardR").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardNameR);

        // Hide all the D tokens except the first one (at hero)
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            GameObject BtnDealer = GameObject.Find("BtnD" + i.ToString());
            // Begin with hero as Dealer
            if (i != 0)
            {
                BtnDealer.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                BtnDealer.GetComponent<Button>().interactable = false;
            }
        }

        // Set the positions on table ("BU", "SB", "BB", "UTG" ...)
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text = initalNameByPosition[i];
        }

        /*
        // set the right panelDealerSetter regarding the number of player
        if (TableDataClass.NumberOfPlayer == 3)
            panelDealerSetter = panelDealerSetter3;
        else if(TableDataClass.NumberOfPlayer == 5)
            panelDealerSetter = panelDealerSetter5;
        */
    }

    private void Update()
    {
        // Set the bets
        for(int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            GameObject betPlayer = GameObject.Find(i.ToString() + "BetPlayer");

            if (TableDataClass.BetsByPosition[i] == 0.5f)
            {
                betPlayer.GetComponent<Image>().sprite = SB;
                betPlayer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            } 
            else if(TableDataClass.BetsByPosition[i] == 1)
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

        // Disable the text "Select a Dealer" when it's not necessary
        //if(!panelDealerSetter.activeSelf)
            //GameObject.Find("TxtActionInfo").GetComponent<Text>().text = "";
    }

    // Open or close the panelDealer selector
    public void OpenDealerSetter()
    {
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
 
        GameObject.Find("TxtActionInfo").GetComponent<Text>().text = "Select a dealer";
    }

    // Set the dealer after clicking on one of the dealer position
    public void DealerSetter(int position)
    {
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

            // Set the SB and BB
            Sprite mySB = Resources.Load<Sprite>("Resources/Icons/SB");
            if (position + 1 == numberOfPlayer)
            {
                ResetBets(0, 1);
                GameObject.Find("0BetPlayer").GetComponent<Image>().sprite = SB;
                GameObject.Find("1BetPlayer").GetComponent<Image>().sprite = BB;
            }
            else if (position + 2 == numberOfPlayer)
            {
                ResetBets(numberOfPlayer - 1, 0);
                GameObject.Find((numberOfPlayer - 1).ToString() + "BetPlayer").GetComponent<Image>().sprite = SB;
                GameObject.Find("0BetPlayer").GetComponent<Image>().sprite = BB;
            }
            else
            {
                ResetBets(position + 1, position + 2);
                GameObject.Find((position + 1).ToString() + "BetPlayer").GetComponent<Image>().sprite = SB;
                GameObject.Find((position + 2).ToString() + "BetPlayer").GetComponent<Image>().sprite = BB;
            }

            // close the window and delete the information text
            panelDealerSetter.SetActive(!panelDealerSetter.activeSelf);
            GameObject.Find("TxtActionInfo").GetComponent<Text>().text = "";

            // Set the positions on table ("BU", "SB", "BB", "UTG" ...)
            TableDataClass.NameByPosition = initalNameByPosition;
            for (int i = 0; i < position; i++)           
                TableDataClass.NameByPosition = moveNext(TableDataClass.NameByPosition);
            
            for (int i = 0; i < numberOfPlayer; i++)
                GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[i];

            // fill the current list of player on the table
            for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
                TableDataClass.NameByPosition[i] = GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text;
        }
        else
        {
            // click from a 'D' button
            OpenDealerSetter();
  
        }
    }

    public void ResetTable()
    {
        int numberOfPlayer = TableDataClass.NumberOfPlayer;

        // reset bets and names by position
        TableDataClass.NameByPosition = initalNameByPosition;
        initBetPosition();

        // fill the current list of player on the table
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
            GameObject.Find(i.ToString() + "TxtPlayer").GetComponent<Text>().text = TableDataClass.NameByPosition[i];

        // hide all the bets except SB and BB
        for (int i = 0; i < numberOfPlayer; i++)
            if (i != 1 && i != 2)
                GameObject.Find(i.ToString() + "BetPlayer").GetComponent<Image>().color = new Color(1, 1, 1, 0);

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
        TableDataClass.NameByPosition = initalNameByPosition;
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
        TableDataClass.BetsByPosition = initalBetByPosition;
    }

    string[] moveNext(string[] array)
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
}

// new List<string>(new string[] { "BU", "SB", "BB", "UTG1", "UTG2", "MP1", "MP2", "CO" });
