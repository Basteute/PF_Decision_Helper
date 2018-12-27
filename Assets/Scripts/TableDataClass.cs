using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TableDataClass
{
    private static float[] betsByPosition; 
    private static string [] nameByPosition;
    private static int numberOfPlayer = 3;
    
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
    public static string [] NameByPosition
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


}