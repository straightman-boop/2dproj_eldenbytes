using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttacker : MonoBehaviour
{
    Transform target;
    public float speed = 2;
    float defSpeed;
    float foundSpeed;

    Rigidbody2D rb;

    public bool isDead { get { return death; } }

    bool death = false;

    bool isRight = true;

    bool playerDetected = false;

    public bool vertical = false;
    public float changeTime = 3.0f;

    float timer;
    int direction = 1;

    DetectionScript detection;

    public Animator animator;

    public bool attacking = false;

    public GameObject healthBar;
    public Slider slider;

    public int maxhealth = 10;
    public int Health { get { return currentHealth; } }
    int currentHealth;

    float wait;

    public bool boar = false;
    public bool soldier = false;
    public bool noble = false;

    FlickerScript flicker;

    private void Awake()
    {
        currentHealth = maxhealth;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        detection = GetComponentInChildren<DetectionScript>();

        flicker = GetComponent<FlickerScript>();

        defSpeed = speed;

        foundSpeed = speed * 4;


        if (boar == true)
        {
            wait = 1.7f;
        }

        if (soldier == true)
        {
            wait = 1.7f;
        }

        if (noble == true)
        {
            wait = 1.8f;
        }

        slider.maxValue = maxhealth;
        slider.value = maxhealth;

    }

    private void Update()
    {
        if (death == false)
        {
            if (playerDetected == false)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    //Debug.Log("Changed Time!");
                    direction = -direction;
                    flip();
                    isRight = !isRight;
                    timer = changeTime;
                }

            }


            if (detection.m_Target != null)
            {
                playerDetected = true;

                target = detection.m_Target;
            }

            if (attacking == true)
            {
                speed = 0;
                animator.SetFloat("Speed", speed);
            }

            else
            {
                speed = defSpeed;
                //animator.SetFloat("Speed", speed);
            }

        }

        if (currentHealth == 0 && death == false)
        {
            death = true;

            if (death == true)
            {
                speed = -5;
                animator.SetFloat("Speed", speed);
                animator.SetTrigger("Death");
                StartCoroutine(DeathWait());
            }
        }

    }

    private void FixedUpdate()
    {
        if (death == false)
        {
            if (attacking == false)
            {

                if (playerDetected == false)
                {
                    Vector2 position = rb.position;

                    if (vertical)
                    {
                        position.y += speed * direction;
                    }

                    else
                    {
                        position.x += speed * direction;
                        //animator.SetInteger("Direction", direction);
                        animator.SetFloat("Speed", speed);
                    }


                    rb.MovePosition(position);

                }

                else if (playerDetected == true)
                {
                    Vector2 vector2 = target.position - transform.position;
                    vector2.Normalize();
                    rb.position += vector2 * foundSpeed;
                    animator.SetFloat("Speed", foundSpeed);
                    //animator.SetFloat("Speed", speed);

                    //Debug.Log(vector2.x + " " + isRight);

                    if (vector2.x > 0 && isRight == false)
                    {
                        flip();
                        //Debug.Log("Facing Right");
                        isRight = true;
                    }

                    if (vector2.x < 0 && isRight == true)
                    {
                        flip();
                        //Debug.Log("Facing Left");
                        isRight = false;
                    }


                }



            }



            else if (attacking == true)
            {
                speed = 0;
                rb.velocity = Vector2.zero;
            }

        }

        

    }

    void flip()
    {
        if (death == false)
        {
            //faceRight = !faceRight;
            transform.Rotate(0f, 180f, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterScript player = collision.gameObject.GetComponent<CharacterScript>();
        if (player != null)
        {
            player.ChangeHealth(-1, 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterScript player = collision.gameObject.GetComponentInParent<CharacterScript>();
        flicker.gotHit();
        healthBar.SetActive(true);
        ChangeHealth((player.currentDmg) * -1);
        //Debug.Log("Attacked!" + player.currentDmg);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxhealth);
        slider.value = currentHealth;


        //Debug.Log("Enemy: " + currentHealth + "/" + maxhealth);
    }

    IEnumerator DeathWait()
    {
        yield return new WaitForSecondsRealtime(wait);
        gameObject.SetActive(false);
    }

}
