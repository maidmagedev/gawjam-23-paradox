using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);  
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
