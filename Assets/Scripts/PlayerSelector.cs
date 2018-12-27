using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    private void Start()
    {
        // Change the text
        GameObject.Find("TxtPlayerNumber").GetComponent<Text>().text = "Number of player : " + TableDataClass.NumberOfPlayer.ToString();
    }

    // Change the number of player
    public void IncreaseNumberOfPlayer(bool increase)
    {
        // Change the number
        if (increase)
            TableDataClass.NumberOfPlayer++;
        else
            TableDataClass.NumberOfPlayer--;

        // Make a loop, the number of player goes from 3 to 9
        if (TableDataClass.NumberOfPlayer == 2)
            TableDataClass.NumberOfPlayer = 9;
        else if (TableDataClass.NumberOfPlayer == 10)
            TableDataClass.NumberOfPlayer = 3;

        // Change the text
        GameObject.Find("TxtPlayerNumber").GetComponent<Text>().text = "Number of player : " + TableDataClass.NumberOfPlayer.ToString();
    }

}
