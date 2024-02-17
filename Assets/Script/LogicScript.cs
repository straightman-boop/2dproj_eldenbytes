using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject youDied;
    public CharacterScript player;

    public string sceneName;
    public float loadTime;

    bool status = false;

    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    public void changeto30FPS()
    {
        Application.targetFrameRate = 30;
    }

    public void changeto60FPS()
    {
        Application.targetFrameRate = 60;
    }

    public void changeto120FPS()
    {
        Application.targetFrameRate = 120;
    }

    public void defaultFPS()
    {
        Application.targetFrameRate = -1;
    }

    public void Update()
    {

        if (player.Health == 0)
        {
            StartCoroutine(DiedWait());
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            status = !status;

            menuScreen.SetActive(status);

        }

    }

    IEnumerator DiedWait()
    {
        yield return new WaitForSeconds(1.5f);
        youDied.SetActive(true);

        yield return new WaitForSeconds(2f);
        GetIndexScript.sceneIndex = SceneManager.GetActiveScene().buildIndex; //changes the static value of GetIndexScript
        SceneManager.LoadScene(3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        if (index == 1)
        {
            GetIndexScript.sceneIndex = SceneManager.GetActiveScene().buildIndex + 1; //changes the static value of GetIndexScript
            SceneManager.LoadScene(3);
        }

        else if (index == 2)
        {
            Debug.Log("End Game!");
        }

    }

    public void quit()
    {
        Application.Quit();
    }

    public void turnOff()
    {
        object3.SetActive(false);
        object1.SetActive(true);
        object2.SetActive(true);
    }

}
