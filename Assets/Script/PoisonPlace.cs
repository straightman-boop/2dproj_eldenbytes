using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPlace : MonoBehaviour
{
    public GameObject buff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterScript controller = collision.GetComponent<CharacterScript>();

        buff.SetActive(true);

        //Debug.Log("Entered");

        if (controller != null)
        {
            controller.ChangeHealth(-5, 2);
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        CharacterScript controller = collision.GetComponent<CharacterScript>();

        //Debug.Log("Stayed!");

        if (controller != null)
        {
            controller.ChangeHealth(-5, 2);       
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        buff.SetActive(false);
    }
}
