using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class KnightBossScript : MonoBehaviour
{
    Rigidbody2D rb;
    DetectionScript detection;

    public float bossSpeed = 0.07f;
    private float speed;

    bool playerDetected = false;
    Transform target;

    bool isRight = false;

    bool death = false;

    Animator animator;

    bool isDone = false;
    bool isDone2 = false;

    public bool attacking = false;

    public GameObject attackradius;
    BoxCollider2D bossAttackRadius;

    public GameObject fogWall;

    public GameObject healthBar;
    public Slider slider;

    public int maxhealth = 30;
    public int Health { get { return currentHealth; } }
    int currentHealth;

    FlickerScript flicker;

    private void Awake()
    {
        currentHealth = maxhealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = bossSpeed;

        rb = GetComponent<Rigidbody2D>();
        detection = GetComponentInChildren<DetectionScript>();

        bossAttackRadius = attackradius.gameObject.GetComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();

        flicker = GetComponent<FlickerScript>();

        slider.maxValue = maxhealth;
        slider.value = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(currentHealth);

        if (detection.m_Target != null)
        {
            target = detection.m_Target;

            healthBar.SetActive(true);

            if (isDone == false)
            {
                StartCoroutine(bossTransition());
            }

        }

        if (attacking == true)
        {
            speed = 0;
            animator.SetFloat("Speed", speed);
        }

        else
        {
            speed = bossSpeed;
        }

        if (currentHealth == 0 && death == false)
        {
            death = true;

            if (death == true)
            {
                speed = -5;

                //gameObject.SetActive(false);
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
                if (playerDetected == true)
                {
                    Vector2 vector2 = target.position - transform.position;
                    vector2.Normalize();
                    rb.position += vector2 * speed;
                    animator.SetFloat("Speed", speed);

                    if (vector2.x > 0 && isRight == false)
                    {
                        flip();
                        isRight = true;
                    }

                    if (vector2.x < 0 && isRight == true)
                    {
                        flip();
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

    IEnumerator bossTransition()
    {
        animator.SetBool("playerFound", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("playerFound", false);
        yield return new WaitForSeconds(0f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.8f);
        playerDetected = true;
        isDone = true;

        if (isDone2 == false)
        {
            fogWall.SetActive(true);
            bossAttackRadius.enabled = !bossAttackRadius.enabled;
            isDone2 = true;
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
        healthBar.SetActive(true);
        ChangeHealth((player.currentDmg) * -1);

        flicker.gotHit();
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
        yield return new WaitForSeconds(1f);
        fogWall.SetActive(false);
        gameObject.SetActive(false);
    }


}
