using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogwallScript : MonoBehaviour
{
    public GameObject interact;
    bool validArea = false;
    bool didOnce;

    Animator animator;
    //BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //boxCollider = GetComponent<BoxCollider2D>();
        didOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        var E = Input.GetKey(KeyCode.E);

        if (E == true && validArea == true && didOnce == false)
        {
            StartCoroutine(FogDead());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interact.SetActive(true);
        validArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interact.SetActive(false);
        validArea = false;
    }

    IEnumerator FogDead()
    {
        didOnce = true;
        animator.SetBool("Opened", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Opened", false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        //boxCollider.enabled = !boxCollider.enabled;
    }
}
