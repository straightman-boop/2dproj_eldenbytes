using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadnessCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterScript controller = collision.GetComponent<CharacterScript>();

        if (controller != null)
        {

            if (controller.Health > 0)
            {
                controller.madness = true;
                controller.ChangeHealth(-7, 1);
                Destroy(gameObject);
            }

        }

    }

}
