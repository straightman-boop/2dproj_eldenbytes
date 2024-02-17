using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : AutoDestroyPoolableObject
{
    [HideInInspector]
    public Rigidbody2D rb;
    //public Vector2 speed = new Vector2(200, 0);

    private GameObject player;
    public float force;

    public int damage = 5;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector2 dir = player.transform.position - transform.position;
            rb.velocity = new Vector2(dir.x, dir.y).normalized * force;
        }

        audioSource.Play();

    }

    //public override void OnEnable()
    //{
    //    base.OnEnable();
    //    rb.velocity = speed;
    //}

    //public override void OnDisable()
    //{
    //    base.OnDisable();
    //    rb.velocity = Vector2.zero;
    //}


}
