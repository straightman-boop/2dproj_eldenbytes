using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator animator;
    public Animator background;
    bool hover = false;

    public GameObject button;
    public GameObject loading;
    public GameObject frameMenu;
    public GameObject quitButton;
    public GameObject settings;

    public string sceneName;

    public float time = 5;
    public float loadingTime = 3;

    public void IsHovering()
    {
        hover = true;
        animator.SetBool("Hover", hover);
    }

    public void NotHovering()
    {
        hover = false;
        animator.SetBool("Hover", hover);
    }

    public void onClick()
    {
        StartCoroutine(WaitLoad());
    }

    IEnumerator WaitLoad()
    {
        button.SetActive(false);
        frameMenu.SetActive(false);
        quitButton.SetActive(false);
        settings.SetActive(false);
        background.SetBool("isLoading", true);

        yield return new WaitForSeconds(loadingTime);
        loading.SetActive(true);

        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }

    public void onFrame()
    {
        frameMenu.SetActive(true);

    }

    public void closeFrame()
    {
        frameMenu.SetActive(false);

    }

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

    public void quit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
