using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VilainBehaviour : MonoBehaviour
{
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
    }

    public void Fold(int playerPosition)
    {
        //GARDER LES BLINDES
        TableDataClass.BetsByPosition[playerPosition] = 0;
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
}
