using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerScript : MonoBehaviour
{
    SpriteRenderer sprite;


    public void gotHit()
    {
        StartCoroutine(blink());
    }

    public IEnumerator blink()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(.5f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(.5f);

    }
}
