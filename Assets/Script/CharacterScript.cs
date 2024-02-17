using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class CharacterScript : MonoBehaviour
{
    private float moveSpd;
    private float defaultSpeed = 5;
    public float speed { set { moveSpd = value; } }

    public Animator animator;

    public Rigidbody2D rb;

    AudioSource deathSFX;

    Vector2 movement;

    public int maxhealth = 10;

    public int Health { get { return currentHealth; } }

    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public bool madness;
    //float madDuration = 3.0f;
    //float madnessTimer;

    public bool frozen;

    bool faceRight = true;

    public Slider slide;

    public GameObject madBuff;
    public GameObject freezeBuff;
    //public Slider freezeSlide;

    bool death = false;

    public GameObject blink;
    public GameObject blink2;

    bool firstWeapon = true;
    bool secondWeapon = false;

    public GameObject estoc;
    public GameObject staff;

    int estocDmg = 3;
    int staffDmg = 2;

    public int currentDmg;

    public BoxCollider2D attackCollider;

    FlickerScript flicker;

    [HideInInspector]
    public bool onDialogue;

    private void Awake()
    {
        currentHealth = maxhealth;
    }

    private void Start()
    {
        moveSpd = defaultSpeed;

        slide.maxValue = maxhealth;
        slide.value = maxhealth;

        currentDmg = estocDmg; //declaring default damage; game starts with estoc;

        flicker = GetComponent<FlickerScript>();

        deathSFX = GetComponent<AudioSource>();

        onDialogue = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(onDialogue);

        if (onDialogue == false)
        {
            if (!death)
            {
                //movement.x = Input.GetAxis("Horizontal");
                //movement.y = Input.GetAxis("Vertical");

                var A = Input.GetKey(KeyCode.A);
                var D = Input.GetKey(KeyCode.D);
                var S = Input.GetKey(KeyCode.S);
                var W = Input.GetKey(KeyCode.W);

                var one = Input.GetKey(KeyCode.Alpha1);
                var two = Input.GetKey(KeyCode.Alpha2);

                if (A || D)
                {
                    if (D)
                    {
                        movement.x = 1;
                        animator.SetInteger("X", 1);
                        animator.SetInteger("Speed2", 5);
                        //Debug.Log(animator.speed);

                        if (faceRight == false)
                        {
                            flip();
                            animator.SetBool("Right", true);
                        }

                    }

                    else
                    {
                        movement.x = -1;
                        animator.SetInteger("X", -1);
                        animator.SetInteger("Speed2", 5);
                        //Debug.Log(animator.speed);

                        if (faceRight == true)
                        {
                            flip();
                            animator.SetBool("Right", false);
                        }
                    }

                }

                else
                {
                    movement.x = 0;

                    animator.SetInteger("X", 0);
                    animator.SetInteger("Speed2", 0);

                }

                if (W || S)
                {
                    if (W)
                    {
                        movement.y = 1;

                        animator.SetInteger("Y", 1);
                        animator.SetInteger("Speed", 5);

                    }

                    else
                    {
                        movement.y = -1;

                        animator.SetInteger("Y", -1);
                        animator.SetInteger("Speed", 5);

                    }
                }

                else
                {
                    movement.y = 0;

                    animator.SetInteger("Y", 0);
                    animator.SetInteger("Speed", 0);
                }

                //Debug.Log(movement.x + "" + movement.y);

                if (one)
                {
                    if (firstWeapon == false && secondWeapon == true)
                    {
                        Invoke("StaffOff", 0);
                        Invoke("Pocket1On", 0);
                        Invoke("EstocOn", 0);
                        Invoke("Pocket1Off", 0.2f);
                        currentDmg = estocDmg;
                        firstWeapon = true;
                        secondWeapon = false;
                    }
                }
                if (two)
                {
                    if (firstWeapon == true && secondWeapon == false)
                    {
                        Invoke("EstocOff", 0);
                        Invoke("Pocket1On", 0);
                        Invoke("StaffOn", 0);
                        Invoke("Pocket1Off", 0.2f);
                        currentDmg = staffDmg;
                        secondWeapon = true;
                        firstWeapon = false;
                    }
                }











                if (isInvincible)
                {
                    //Invoke("BlinkOn", 0f);
                    //Invoke("BlinkOff", 0.1f);

                    invincibleTimer -= Time.deltaTime;
                    if (invincibleTimer < 0)
                    {
                        //Debug.Log("NOT");
                        isInvincible = false;
                    }
                }

                //Debug.Log("health: " + currentHealth);

                if (frozen)
                {
                    freezeBuff.SetActive(true);
                }
                else
                {
                    freezeBuff.SetActive(false);
                }
            }


        }




        if (currentHealth == 0 && death == false)
        {
            animator.SetTrigger("Death");
            death = true;
            StartCoroutine(DeathWait());
        }

        if (onDialogue == true)
        {
            rb.velocity = Vector2.zero;
        }

    }

    void flip()
    {
        if (death == false)
        {
            faceRight = !faceRight;
            transform.Rotate(0f, 180f, 0);
        }

    }


    private void FixedUpdate()
    {
        if(onDialogue == false)
        {
            if (death == false)
            {
                rb.MovePosition(rb.position + movement * moveSpd * Time.fixedDeltaTime);
            }
        }

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Invoke("BlinkOn", 0f);
    //    Invoke("BlinkOff", 0.1f);
    //}

    public void ChangeHealth(int amount, int type)
    {      
        if (type == 2) //type 2 damage is equivalent to environmental hazards || mobs (for now)
        {         
            if (amount < 0)
            {
                flicker.gotHit();

                if (isInvincible)
                {
                    Debug.Log("Currently Invincible");
                    return;
                }

                isInvincible = true;
                invincibleTimer = timeInvincible;
            }

            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxhealth);
            slide.value = currentHealth;
        }

        else if (type == 1) //type 1 damage is equivalent to collectibles
        {
            if(amount < 0)
            {
                flicker.gotHit();
            }
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxhealth);
            slide.value = currentHealth;
        }

        //Debug.Log(currentHealth + "/" + maxhealth);
    }

    public void ChangeSpeed(float amount, float speedDur)
    {
        moveSpd = amount;

        while (frozen)
        {
            StartCoroutine(SpeedWait(speedDur));
            return;
        }

    }

    IEnumerator SpeedWait(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        //Debug.Log("Finished!");
        moveSpd = 5;
        frozen = false;
    }

    IEnumerator DeathWait()
    {
        deathSFX.Play();
        yield return new WaitForSecondsRealtime(1.5f);    
        animator.ResetTrigger("Death");
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //EnemyHitBox hitbox = collision.gameObject.GetComponentInParent<EnemyHitBox>();
        //ChangeHealth(hitbox.damage * -1, 2);
        //Debug.Log("CharacterScript: Got hit! CAN Be DAMAGED");
    }




    private void BlinkOn()
    {
        blink.SetActive(true);
    }

    private void BlinkOff()
    {
        blink.SetActive(false);
    }

    private void Pocket1On()
    {
        blink2.SetActive(true);
    }

    private void Pocket1Off()
    {
        blink2.SetActive(false);
    }

    private void EstocOn()
    {
        estoc.SetActive(true);
    }

    private void EstocOff()
    {
        estoc.SetActive(false);
    }

    private void StaffOn()
    {
        staff.SetActive(true);
    }

    private void StaffOff()
    {
        staff.SetActive(false);
    }

    private void toggleCollider()
    {
        attackCollider.enabled = !attackCollider.enabled;
    }

}
