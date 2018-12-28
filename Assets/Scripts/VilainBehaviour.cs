using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VilainBehaviour : MonoBehaviour
{
    public GameObject TxtCurrentPlayer;

    // Onclick function to make the player call, raise or fold
    public void CallRaiseFold(int playerPosition)
    {
        // init
        float playerOwnBet = TableDataClass.BetsByPosition[playerPosition];
        int[] maxBet = maxBetOnTable();

        // Decision
        if(playerOwnBet < maxBet[0]) // Call
        {
            playerOwnBet = maxBet[0];
            Debug.Log("call to " + maxBet[0]);
        }
        //else if(playerOwnBet == maxBet[0] && maxBet[1] < playerPosition) // Raise 2Bet
        else if(playerOwnBet == maxBet[0]) // Raise 
        {
            playerOwnBet++;
            Debug.Log("raise, it's a " + playerOwnBet + "Bet");
        }

        // Fill the global table of bets 
        TableDataClass.BetsByPosition[playerPosition] = playerOwnBet;

        SetBetsOnTable();
    }

    public void Fold(int playerPosition)
    {
    
        if(TableDataClass.NameByPosition[playerPosition].Equals("SB"))
        {
            TableDataClass.BetsByPosition[playerPosition] = 0.5f;
        }
        if (TableDataClass.NameByPosition[playerPosition].Equals("BB"))
        {
            TableDataClass.BetsByPosition[playerPosition] = 1;
        }
        else
        {
            TableDataClass.BetsByPosition[playerPosition] = 0;
        }
        SetBetsOnTable();
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
}
