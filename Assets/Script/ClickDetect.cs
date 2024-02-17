using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickDetect : MonoBehaviour
{
    //public Camera Camera;
    public GameObject attackRadius;
    BoxCollider2D attackCollider;
    public Animator animator;
    float time = 1f;
    float timer;
    //float timeCatch;

    bool coolDown = false;

    AudioSource AudioSource;

    private void Start()
    {
        attackCollider = attackRadius.GetComponent<BoxCollider2D>();
        AudioSource = GetComponent<AudioSource>();
        timer = time;
    }
    private void Update()
    {
        if(coolDown == true)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                coolDown = false;
                timer = time;
                //Debug.Log("Done Cooldown!");
            }
        }

    }

    private void OnMouseDown()
    {
        if (coolDown == false)
        {
            coolDown = true;

            AudioSource.Play();
            animator.SetTrigger("Attack");
            StartCoroutine(AttackWait());
        }

        else
        {
            Debug.Log("Still Cooldown!");
        }

        //Debug.Log("Clicked.");
    }

    private void toggleCollider()
    {
        attackCollider.enabled = !attackCollider.enabled;
    }

    IEnumerator AttackWait()
    { 
        yield return new WaitForSeconds(0.2f);
        Invoke("toggleCollider", 0.3f);
        Invoke("toggleCollider", 0f);
    }
}

