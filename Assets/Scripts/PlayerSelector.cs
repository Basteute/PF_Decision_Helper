using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    private void Start()
    {
        // Change the text
        GameObject.Find("TxtPlayerNumber").GetComponent<Text>().text = "Nombre de joueurs : " + TableDataClass.NumberOfPlayer.ToString();
    }

    // Change the number of player
    public void IncreaseNumberOfPlayer(bool increase)
    {
        // Change the number
        if (increase)
            TableDataClass.NumberOfPlayer++;
        else
            TableDataClass.NumberOfPlayer--;

        // Make a loop, the number of player goes from 3 to 6
        if (TableDataClass.NumberOfPlayer == 2)
            TableDataClass.NumberOfPlayer = 6;
        else if (TableDataClass.NumberOfPlayer == 7)
            TableDataClass.NumberOfPlayer = 3;

        // Change the text
        GameObject.Find("TxtPlayerNumber").GetComponent<Text>().text = "Nombre de joueurs : " + TableDataClass.NumberOfPlayer.ToString();

        // Change the number of player in the arrays
        TableDataClass.initNamePosition();
        TableDataClass.initBetPosition();
        TableDataClass.initFoldByPosition();
    }

}
