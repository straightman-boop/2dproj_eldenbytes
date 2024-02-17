using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterScript controller = collision.GetComponent<CharacterScript>();

        if(controller != null)
        {       
            if(controller.Health < controller.maxhealth)//Can only be collected if healt != max
            {
                controller.ChangeHealth(5, 1);
                Destroy(gameObject);
            }
        }

    }
}
