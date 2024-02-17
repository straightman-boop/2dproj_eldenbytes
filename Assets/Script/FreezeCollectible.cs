using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterScript controller = collision.GetComponent<CharacterScript>();

        float amount = 1f;
        float time = 7;

        if (controller != null)
        {

            if (controller.Health > 0)
            {
                controller.frozen = true;
                controller.ChangeSpeed(amount, time);
                Destroy(gameObject);
            }          

        }

    }

}
