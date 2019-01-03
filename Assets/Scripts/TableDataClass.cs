using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TableDataClass
{
    private static float[] betsByPosition;
    private static string[] nameByPosition;
    private static int numberOfPlayer = 3;
    private static bool[] foldByPosition;
    private static ArrayList previousAction = new ArrayList();
    private static ArrayList previousBet = new ArrayList();
    private static string currentPlayer;

    // Who has bet and how much
    public static float[] BetsByPosition
    {
        get
        {
            return betsByPosition;
        }
        set
        {
            betsByPosition = value;
        }
    }

    // What is the name of each player ("BU", "SB", "BB"...)
    public static string[] NameByPosition
    {
        get
        {
            return nameByPosition;
        }
        set
        {
            nameByPosition = value;
        }
    }

    // Number of player around the table
    public static int NumberOfPlayer
    {
        get
        {
            return numberOfPlayer;
        }
        set
        {
            numberOfPlayer = value;
        }
    }


    public static bool [] FoldByPosition
    {
        get
        {
            return foldByPosition;
        }
        set
        {
            foldByPosition = value;
        }
    }

    public static ArrayList PreviousBet
    {
        get
        {
            return previousBet;
        }
        set
        {
            previousBet = value;
        }
    }

    public static ArrayList PreviousAction
    {
        get
        {
            return previousAction;
        }
        set
        {
            previousAction = value;
        }
    }

    // What is the name of each player ("BU", "SB", "BB"...)
    public static string CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }
        set
        {
            currentPlayer = value;
        }
    }

    // Return the index of "BB" for instance
    public static int GetIndexOfPosition(string position)
    {
        int result = -1;

        for(int i = 0; i < numberOfPlayer; i++)       
            if(position.Equals(nameByPosition[i]))        
                return i;
            
        
        return result;
    }

}