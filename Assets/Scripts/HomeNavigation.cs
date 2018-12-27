using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeNavigation : MonoBehaviour
{
    // Change scene with the name given on the inspector
    public void NavigateTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

//if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
