using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonedPlace : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterScript controller = collision.GetComponent<CharacterScript>();

        if (controller != null)
        {

            if (controller.Health > 0)
            {
                controller.madness = true;
                controller.ChangeHealth(-1, 2);
                Destroy(gameObject);
            }

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CharacterScript controller = collision.GetComponent<CharacterScript>();

        if (controller != null)
        {

            if (controller.Health > 0)
            {
                controller.madness = true;
                controller.ChangeHealth(-1, 2);
                Destroy(gameObject);
            }

        }

    }
}
