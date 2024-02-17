using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public void quitGame()
    {
        Application.Quit();
    }

    public void returnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
