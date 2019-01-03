using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardSelector : MonoBehaviour
{
    public GameObject panelCardSelector = null;
    public GameObject panelColorSelector = null;

    string[] cardColor = { "h", "s", "d", "c" };

    private void Start()
    {
        DisplayHeroCards();
    }

    // Card panel selector
    public void CardValueSelector(string side)
    {
        PlayerPrefs.SetString("cardValueSide", side);
        panelCardSelector.SetActive(true);
    }

    // Color panel selector
    public void CardColorSelector(string side)
    {
        PlayerPrefs.SetString("cardColorSide", side);
        panelColorSelector.SetActive(true);
    }

    // Increase the color of a card by 1
    public void UpColorLeft(bool leftCard)
    {
        if (leftCard)
        {
            int cardColorL = PlayerPrefs.GetInt("cardColorL") + 1;
            if (cardColorL == 4) cardColorL = 0;

            PlayerPrefs.SetInt("cardColorL", cardColorL);
        }
        else
        {
            int cardColorR = PlayerPrefs.GetInt("cardColorR") + 1;
            if (cardColorR == 4) cardColorR = 0;

            PlayerPrefs.SetInt("cardColorR", cardColorR);
        }

        ChangeCardColor(leftCard);
        DisplayHeroCards();
    }

    // Decrease the color of a card by 1
    public void DownColorLeft(bool leftCard)
    {
        if (leftCard)
        {
            int cardColorL = PlayerPrefs.GetInt("cardColorL") - 1;
            if (cardColorL == -1) cardColorL = 3;

            PlayerPrefs.SetInt("cardColorL", cardColorL);
        }
        else
        {
            int cardColorR = PlayerPrefs.GetInt("cardColorR") - 1;
            if (cardColorR == -1) cardColorR = 3;

            PlayerPrefs.SetInt("cardColorR", cardColorR);
        }

        ChangeCardColor(leftCard);
        DisplayHeroCards();
    }

    // Increase a card value by 1
    public void UpValueLeft(bool leftCard)
    {
        if (leftCard)
        {
            int cardValueL = PlayerPrefs.GetInt("cardValueL") + 1;
            if (cardValueL == 15) cardValueL = 2;

            PlayerPrefs.SetInt("cardValueL", cardValueL);
        }
        else
        {
            int cardValueR = PlayerPrefs.GetInt("cardValueR") + 1;
            if (cardValueR == 15) cardValueR = 2;

            PlayerPrefs.SetInt("cardValueR", cardValueR);
        }

        ChangeCardValue(leftCard);
        DisplayHeroCards();
    }

    // Decrease a card value by 1
    public void DownValueLeft(bool leftCard)
    {
        if (leftCard)
        {
            int cardValueL = PlayerPrefs.GetInt("cardValueL") - 1;
            if (cardValueL == 1) cardValueL = 14;

            PlayerPrefs.SetInt("cardValueL", cardValueL);
        }
        else
        {
            int cardValueR = PlayerPrefs.GetInt("cardValueR") - 1;
            if (cardValueR == 1) cardValueR = 14;

            PlayerPrefs.SetInt("cardValueR", cardValueR);
        }

        ChangeCardValue(leftCard);
        DisplayHeroCards();
    }

    // Change the value of the card via the arrows
    void ChangeCardValue(bool leftCard)
    {
        Text txtCardValue;
        if (leftCard)
        {
            txtCardValue = GameObject.Find("TxtCardL").GetComponent<Text>();
            txtCardValue.text = getCardName(PlayerPrefs.GetInt("cardValueL"));
        }
        else
        {
            txtCardValue = GameObject.Find("TxtCardR").GetComponent<Text>();
            txtCardValue.text = getCardName(PlayerPrefs.GetInt("cardValueR"));
        }
        DisplayHeroCards();
    }

    // Change the value of the card from the selector
    public void ChangeCardValueFromSelector(int value)
    {
        Text txtCardValue;
        if ("left".Equals(PlayerPrefs.GetString("cardValueSide")))
        {
            txtCardValue = GameObject.Find("TxtCardL").GetComponent<Text>();
            txtCardValue.text = getCardName(value);

            // Set the value in the playersPref so the user still can change the value with the arrows
            PlayerPrefs.SetInt("cardValueL", value);
        }
        else if ("right".Equals(PlayerPrefs.GetString("cardValueSide")))
        {
            txtCardValue = GameObject.Find("TxtCardR").GetComponent<Text>();
            txtCardValue.text = getCardName(value);

            // Set the value in the playersPref so the user still can change the value with the arrows
            PlayerPrefs.SetInt("cardValueR", value);
        }

        panelCardSelector.SetActive(false); // Hide the selector window
        DisplayHeroCards();
    }

    // Change the color of the card via the arrows
    void ChangeCardColor(bool leftCard)
    {
        Text txtCardColor;
        if (leftCard)
        {
            txtCardColor = GameObject.Find("TxtCardColorL").GetComponent<Text>();
            //txtCardColor.text = cardColor[PlayerPrefs.GetInt("cardColorL")];
            GameObject.Find("ImgCardL").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardColor[PlayerPrefs.GetInt("cardColorL")]);
        }
        else
        {
            txtCardColor = GameObject.Find("TxtCardColorR").GetComponent<Text>();
            //txtCardColor.text = cardColor[PlayerPrefs.GetInt("cardColorR")];
            GameObject.Find("ImgCardR").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardColor[PlayerPrefs.GetInt("cardColorR")]);
        }
        DisplayHeroCards();
    }

    // Change the color of the card from the selector
    public void ChangeCardColorFromSelector(int color)
    {
        Text txtCardColor;
        if ("left".Equals(PlayerPrefs.GetString("cardColorSide")))
        {
            txtCardColor = GameObject.Find("TxtCardColorL").GetComponent<Text>();
            txtCardColor.text = cardColor[color];

            // Set the value in the playersPref so the user still can change the color with the arrows
            PlayerPrefs.SetInt("cardColorL", color);
            GameObject.Find("ImgCardL").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardColor[PlayerPrefs.GetInt("cardColorL")]);
        }
        else if ("right".Equals(PlayerPrefs.GetString("cardColorSide")))
        {
            txtCardColor = GameObject.Find("TxtCardColorR").GetComponent<Text>();
            txtCardColor.text = cardColor[color];

            // Set the value in the playersPref so the user still can change the color with the arrows
            PlayerPrefs.SetInt("cardColorR", color);
            GameObject.Find("ImgCardR").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardColor[PlayerPrefs.GetInt("cardColorR")]);
        }

        panelColorSelector.SetActive(false); // Hide the selector window
        DisplayHeroCards();
    }

    // Transform a card value into a card name
    string getCardName(int card)
    {
        string result = card.ToString();

        switch(card)
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
        }

        return result;
    }

    // Transform card name into an integer
    int getCardNumber(string card)
    {
        int result = int.Parse(card);

        switch (card)
        {
            case "A":
                result = 1;
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
        }

        return result;
    }

    // Display the face up cards of hero
    void DisplayHeroCards()
    {       
        string cardNameL = PlayerPrefs.GetInt("cardValueL").ToString() + cardColor[PlayerPrefs.GetInt("cardColorL")];
        string cardNameR = PlayerPrefs.GetInt("cardValueR").ToString() + cardColor[PlayerPrefs.GetInt("cardColorR")];
        GameObject.Find("0CardL").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardNameL);
        GameObject.Find("0CardR").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + cardNameR);
    }
}


/*
// Change the color of the cards
public void ChangeCardColor()
{
    int suitedCards = PlayerPrefs.GetInt("suit");
    Button cardL = GameObject.Find("BtnCardL").GetComponent<Button>();
    Button cardR = GameObject.Find("BtnCardR").GetComponent<Button>();
    Text txtCardsColor = GameObject.Find("TxtColor").GetComponent<Text>();
    if (suitedCards == 0)
    {
        PlayerPrefs.SetInt("suit", 1);
        cardL.image.color = Color.red;
        cardR.image.color = Color.red;
        txtCardsColor.text = "Suit";
    }
    else
    {
        PlayerPrefs.SetInt("suit", 0);
        cardL.image.color = Color.black;
        cardR.image.color = Color.red;
        txtCardsColor.text = "Of suit";
    }
}
*/
