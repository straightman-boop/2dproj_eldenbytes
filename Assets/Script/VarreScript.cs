using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarreScript : MonoBehaviour
{
    public GameObject interact;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interact.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interact.SetActive(false);
    }
}
