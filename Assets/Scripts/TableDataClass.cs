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
    private static string currentPlayer = null;
    private static string lastPlayer = "";
    public static string [] initalNameByPosition;
    public static bool[] initalFoldByPosition;
    public static float [] initalBetByPosition;
    public static ArrayList playersHistory = new ArrayList();
    public static ArrayList actionsHistory = new ArrayList();
    public static ArrayList betsHistory = new ArrayList();

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

    public static string LastPlayer
    {
        get
        {
            return lastPlayer;
        }
        set
        {
            lastPlayer = value;
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

    public static void initNamePosition()
    {
        if (NumberOfPlayer == 2) initalNameByPosition = new string[] { "BU", "BB" };
        else if (NumberOfPlayer == 3) initalNameByPosition = new string[] { "BU", "SB", "BB" };
        else if (NumberOfPlayer == 4) initalNameByPosition = new string[] { "BU", "SB", "BB", "CO" };
        else if (NumberOfPlayer == 5) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG", "CO" };
        else if (NumberOfPlayer == 6) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG", "HJ", "CO" };
        else if (NumberOfPlayer == 7) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG1", "UTG2", "CO" };
        else if (NumberOfPlayer == 8) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG1", "UTG2", "MP1", "CO" };
        else if (NumberOfPlayer == 9) initalNameByPosition = new string[] { "BU", "SB", "BB", "UTG1", "UTG2", "MP1", "MP2", "CO" };
        nameByPosition = initalNameByPosition;
    }

    public static void initBetPosition()
    {
        if (NumberOfPlayer == 2) initalBetByPosition = new float[] { 0.5f, 1 };
        else if (NumberOfPlayer == 3) initalBetByPosition = new float[] { 0, 0.5f, 1 };
        else if (NumberOfPlayer == 4) initalBetByPosition = new float[] { 0, 0.5f, 1, 0 };
        else if (NumberOfPlayer == 5) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0 };
        else if (NumberOfPlayer == 6) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0 };
        else if (NumberOfPlayer == 7) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0, 0 };
        else if (NumberOfPlayer == 8) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0, 0, 0 };
        else if (NumberOfPlayer == 9) initalBetByPosition = new float[] { 0, 0.5f, 1, 0, 0, 0, 0, 0, 0 };
        betsByPosition = initalBetByPosition;
    }

    public static void initFoldByPosition()
    {
        if (NumberOfPlayer == 2) initalFoldByPosition = new bool[] { false, false };
        else if (NumberOfPlayer == 3) initalFoldByPosition = new bool[] { false, false, false };
        else if (NumberOfPlayer == 4) initalFoldByPosition = new bool[] { false, false, false, false };
        else if (NumberOfPlayer == 5) initalFoldByPosition = new bool[] { false, false, false, false, false };
        else if (NumberOfPlayer == 6) initalFoldByPosition = new bool[] { false, false, false, false, false, false };
        else if (NumberOfPlayer == 7) initalFoldByPosition = new bool[] { false, false, false, false, false, false, false };
        else if (NumberOfPlayer == 8) initalFoldByPosition = new bool[] { false, false, false, false, false, false, false, false };
        else if (NumberOfPlayer == 9) initalFoldByPosition = new bool[] { false, false, false, false, false, false, false, false, false };
        foldByPosition = initalFoldByPosition;
    }

    // history of players for Undo button
    public static ArrayList PlayersHistory
    {
        get
        {
            return playersHistory;
        }
        set
        {
            playersHistory = value;
        }
    }

    // history of actions for Undo button
    public static ArrayList ActionsHistory
    {
        get
        {
            return actionsHistory;
        }
        set
        {
            actionsHistory = value;
        }
    }

    // history of players for Undo button
    public static ArrayList BetsHistory
    {
        get
        {
            return betsHistory;
        }
        set
        {
            betsHistory = value;
        }
    }
}
