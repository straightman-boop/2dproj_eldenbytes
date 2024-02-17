using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    public float timer;
    public GameObject Object;
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Object.SetActive(true);
            StartCoroutine(sceneLoad());
        }

    }

    IEnumerator sceneLoad()
    {
        int indexNum = GetIndexScript.sceneIndex;
        //Debug.Log(indexNum);

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(indexNum);
    }


}
