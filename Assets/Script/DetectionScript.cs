using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{

    [HideInInspector]
    public Transform m_Target;

    public GameObject m_TargetGameObject;
    bool turnedOn = false;

    public bool isBoss = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBoss == true)
        {
            m_Target = collision.GetComponent<Transform>();
            Debug.Log(collision);

            if (turnedOn == false)
            {
                turnedOn = true;
                m_TargetGameObject.SetActive(true);
            }

        }

        else
        {
            m_Target = collision.GetComponent<Transform>();
        }

    }

}
