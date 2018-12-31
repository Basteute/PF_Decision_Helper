using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PFDecisionMaker : MonoBehaviour
{
    public GameObject panelDealerSetter;
    bool displayDecision = true;

    private void Update()
    {      
        // Enter Hero's hand
        int bestHeroCard = Mathf.Max(PlayerPrefs.GetInt("cardValueR"), PlayerPrefs.GetInt("cardValueL"));
        int minHeroCard = Mathf.Min(PlayerPrefs.GetInt("cardValueR"), PlayerPrefs.GetInt("cardValueL"));

        string heroHand = getCardName(bestHeroCard) + getCardName(minHeroCard);
        if (!bestHeroCard.Equals(minHeroCard))
        {
            if (PlayerPrefs.GetString("cardColorL").Equals(PlayerPrefs.GetString("cardColorR")))
                heroHand += "s";
            else
                heroHand += "o";
        }
        if(!panelDealerSetter.activeSelf)
            GameObject.Find("Decision").GetComponent<Text>().text = PFDecision(heroHand);
        else
            GameObject.Find("Decision").GetComponent<Text>().text = "";
            
    }

    public void BtnTest(string range)
    {
        string result = "";
        ArrayList arrayRange = getRangeFromString(range);
        for(int i = 0; i < arrayRange.Count; i++)
        {
            result += arrayRange[i] + ", ";
        }
        GameObject.Find("TxtText").GetComponent<Text>().text = result;
    }
    
    // return the adviced Pre-Flop decision "call", "3BetFold", "3BetALL", "open" or "fold"
    string PFDecision(string hand)
    {
        string result = "error";

        // check if anyone has opened before hero THIS DOES NOT HANDLE HEADS UP
        int numberOfPlayer = TableDataClass.NumberOfPlayer;
        bool vilainOpened = false;
		int numberOfBBCall = 0;
		for (int i = 0; i < numberOfPlayer; i++)
		{
			if (TableDataClass.BetsByPosition[i] == 1)
                numberOfBBCall++;
			if (TableDataClass.BetsByPosition[i] > 1)
				vilainOpened = true;
		}
		if(numberOfBBCall > 1)
			vilainOpened = true;

        // select the advice decision regarding it's an open raise or not
        if (!vilainOpened)
            result = getOpenDecision(hand);
        else
            result = getRaiseDecision(hand);

        return result;
    }

    // Return the open raise decision
    string getOpenDecision(string hand)
    {
        bool open = false;

        //Debug.Log("Number = " + TableDataClass.NumberOfPlayer + ");

        switch (TableDataClass.NumberOfPlayer)
        {
            case 3:
                if ("BU".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, "22+, A2s+, K2s+, Q7s+, J7s+, T7s+, 96s+, 85s+, 75s+, 64s+, 54s, A2o+, K8o+, Q9o+, J9o+, T9o, 98o");
                else if ("SB".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, "22+,A2s+,K8s+,Q9s+,J9s+,T8s+,98s,87s,A9o+,KJo+,QJo");
                else
                    open = true;
                break;

            case 4:
                if ("BU".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeBU6);
                else if ("SB".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeSB6);
                else if ("CO".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeCO6);
                else
                    open = true;
                break;

            case 5:
                if ("BU".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeBU6);
                else if ("SB".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeSB6);
                else if ("UTG".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeUTG6);
                else if ("CO".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeCO6);
                else
                    open = true;
                break;

            case 6:
                if ("BU".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeBU6);
                else if ("SB".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeSB6);
                else if ("UTG".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeUTG6);
                else if ("CO".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeCO6);
                else if ("HJ".Equals(TableDataClass.NameByPosition[0]))
                    open = compareHandWithRange(hand, OpenRangeClass.OpenRangeMP6);
                else
                    open = true;
                break;

            case 7:
                open = compareHandWithRange(hand, "KK+, A2s");
                break;

            case 8:
                open = compareHandWithRange(hand, "KK+, A2s");
                break;

            case 9:
                open = compareHandWithRange(hand, "KK+, A2s");
                break;
        }

        if (open)
            return "open";
        else
            return "fold";
    }

    // Return 3Bet, 4Bet, 5Bet ... decision
    string getRaiseDecision(string hand)
    {
        // check if people has limped before Hero
        int numberOfBBCall = 0;
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
            if (TableDataClass.BetsByPosition[i] == 1)
                numberOfBBCall++;

        // hero has face a limp situation
        if (numberOfBBCall > 1)
            return getLimpDecision(hand, numberOfBBCall - 1);

        // check the number of player who raised
        int maxRaise = 1;
        int numberOfRaiser = 0;
        for (int i = 0; i < TableDataClass.NumberOfPlayer; i++)
        {
            if (TableDataClass.BetsByPosition[i] > maxRaise)
            {
                maxRaise++;
                numberOfRaiser = 1;
            }
            else if (TableDataClass.BetsByPosition[i] == maxRaise && maxRaise != 1)
            {
                numberOfRaiser++;
            }
        }

        if (numberOfRaiser == 1)
            return getHURaise(hand);
        else
            return getMultiWayRaise(hand, numberOfRaiser);
    }

    // Decision when a raise from vilain is called from another vilain
    string getMultiWayRaise(string hand, int numberOfRaiser)
    {
        return "getMultiWayRaise";
    }

    // Decision when all players have limped before hero
    string getLimpDecision(string hand, int numberOfLimp)
    {
        bool isoRaise = false;
        string heroPos = TableDataClass.NameByPosition[0];
        if ("BB".Equals(heroPos) && numberOfLimp == 1)
        {
            if (TableDataClass.BetsByPosition[TableDataClass.NumberOfPlayer - 1] == 1) // SB has limped
                isoRaise = compareHandWithRange(hand, IsoRangeClass.IsoBBVSSB40);
            else
                isoRaise = compareHandWithRange(hand, IsoRangeClass.IsoBBVS1Limp40);
        }
        else if("BB".Equals(heroPos) && numberOfLimp == 2 || numberOfLimp == 1)
        {
            isoRaise = compareHandWithRange(hand, IsoRangeClass.IsoOOPVS1Limp);
        }


        if (isoRaise)
            return "Iso";
        else
            return "You can either call or fold";
    }

    // Decision when 1 vilain opened before hero
    string getHURaise(string hand)
    {
        return "HURaise";
    }

    // Check if a hand belongs to a range
    bool compareHandWithRange(string hand, string range)
    {
        bool result = false;

        ArrayList rangeAdviced = getRangeFromString(range);
        for (int i = 0; i < rangeAdviced.Count; i++)
            if (hand.Equals(rangeAdviced[i]))
                return true;

        return result;
    }

    // "A2s+, K2s+, 88+" => un tableau de mains 
    ArrayList getRangeFromString(string range)
    {
        // Split the range into an arrayList
        ArrayList tokenRange = new ArrayList();
        string hand = "";
        for (int i = 0; i < range.Length; i++)
        {
            if (!','.Equals(range[i]))
            {
                if (!' '.Equals(range[i]))
                {
                    hand += range[i];
                }
            }
            else
            {
                tokenRange.Add(hand);
                hand = "";
            }  
        }
        tokenRange.Add(hand);

        // Make an array which contains all hands playable
        ArrayList result = new ArrayList();
        for (int i = 0; i < tokenRange.Count; i++)
        {
            // Make an array from the first range ("A2s+" => [A2s, A3s...] in this example)
            ArrayList temp = rangeFromShortCuts(tokenRange[i].ToString());
            for (int j = 0; j < temp.Count; j++)
            {
                result.Add(temp[j]);
            }
        }
        return result;
    }

    // J8o+ => [J8o, J9o, JTo]
    ArrayList rangeFromShortCuts(string range)
    {
        // init
        ArrayList result = new ArrayList();
        char[] splittedHand = range.ToCharArray();
        int bestCard = Mathf.Max(getCardNumber(splittedHand[0].ToString()), getCardNumber(splittedHand[1].ToString()));
        int minCard = Mathf.Min(getCardNumber(splittedHand[0].ToString()), getCardNumber(splittedHand[1].ToString()));

        // return an array regarding the kind of hand
        if(splittedHand.Length == 2)// deal with single pocket pairs like AA
        {
            result.Add(getCardName(splittedHand[0]) + getCardName(splittedHand[1]));
        }
        else if (splittedHand[0].Equals(splittedHand[1]) && '+'.Equals(splittedHand[2]))// deal with pocket pairs +
        {
            for (int i = getCardNumber(splittedHand[0].ToString()); i <= 14; i++)
            {
                result.Add(getCardName(i) + getCardName(i));
            }
        }
        else if (splittedHand[0].Equals(splittedHand[1]) && splittedHand[3].Equals(splittedHand[4]))// deal with pocket pairs like QQ-22
        {
            for (int i = getCardNumber(splittedHand[0].ToString()); i >= getCardNumber(splittedHand[3].ToString()); i--)
            {
                result.Add(getCardName(i) + getCardName(i));
            }
        }
        else if ((bestCard - 1) == minCard)// Deal with connectors 
        {
            for (int i = minCard; i <= 13; i++)
            {
                result.Add(getCardName(i + 1) + getCardName(i) + splittedHand[2]);
            }
        }
        else if(splittedHand.Length > 6)// deal with hand like 97o-96o
        {
            bestCard = getCardNumber(splittedHand[0].ToString());
            minCard = getCardNumber(splittedHand[5].ToString());
            for (int i = minCard; i <= getCardNumber(splittedHand[1].ToString()); i++)
            {
                result.Add(getCardName(bestCard) + getCardName(i) + splittedHand[2]);
            }
        }
        else if (splittedHand.Length > 3)// Deal with suited and ofSuited cards
        {
            if ('+'.Equals(splittedHand[3]))
            {
                for (int i = minCard; i < bestCard; i++)
                {
                    result.Add(getCardName(bestCard) + getCardName(i) + splittedHand[2]);
                }
            }
        }
        else // Only one hand like "AJs"
        {
            result.Add(range);
        }
        return result;
    }

    // Transform a card value into a card name
    string getCardName(int card)
    {
        string result = card.ToString();

        switch (card)
        {
            case 10:
                result = "T";
                break;

            case 11:
                result = "J";
                break;

            case 12:
                result = "Q";
                break;

            case 13:
                result = "K";
                break;

            case 14:
                result = "A";
                break;

            case 1:
                result = "A";
                break;
        }

        return result;
    }

    // Transform card name into an integer
    int getCardNumber(string card)
    {
        int result = 0;
        int.TryParse(card, out result);

        switch (card)
        {
            case "A":
                result = 14;
                break;

            case "K":
                result = 13;
                break;

            case "Q":
                result = 12;
                break;

            case "J":
                result = 11;
                break;

            case "T":
                result = 10;
                break;
        }

        return result;
    }
}
