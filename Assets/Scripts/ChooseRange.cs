using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseRange : MonoBehaviour
{
    // Write the chosenrange in the PlayersPref
	public void SelectChosenRange (string chosenRange)
    {
        PlayerPrefs.SetString("chosenRange", chosenRange);
    }
	
}
