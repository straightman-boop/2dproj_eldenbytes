using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical = false;
    public float changeTime = 3.0f;

    Rigidbody2D rb;
    float timer;
    int direction = 1;

    public Animator animator;

    public int maxhealth = 10;
    public int Health { get { return currentHealth; } }
    int currentHealth;

    public GameObject target;
    //bool faceRight = true;

    public Slider slider;

    public GameObject healthBar;

    bool death = false;

    public bool boar = false;
    public bool soldier = false;
    public bool noble = false;
    public bool chariot = false;

    float wait;

    int boarDmg = -1;
    int soldierDmg = -3;
    int nobleDmg = -2;
    int chariotDmg = -100;

    int damage;

    FlickerScript flicker;

    private void Awake()
    {
        currentHealth = maxhealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        flicker = GetComponent<FlickerScript>();

        if (boar == true)
        {
            wait = 1.7f;
            damage = boarDmg;

        }

        if (soldier == true)
        {
            wait = 1.7f;
            damage = soldierDmg;

        }

        if (noble == true)
        {
            wait = 1.8f;
            damage = nobleDmg;

        }

        if (chariot == true)
        {
            wait = 1f;
            damage = chariotDmg;
        }

        slider.maxValue = maxhealth;
        slider.value = maxhealth;


        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;

    }

    // Update is called once per frame
    void Update()
    {

        if (death == false)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                //Debug.Log("Changed Time!");
                direction = -direction;
                flip();
                timer = changeTime;
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

    void FixedUpdate()
    {
        if (death == false)
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
                //animator.SetFloat("Speed", speed);
            }


            rb.MovePosition(position);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterScript player = collision.gameObject.GetComponent<CharacterScript>();
        if (player != null)
        {
            player.ChangeHealth(damage, 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterScript player = target.GetComponent<CharacterScript>();
        flicker.gotHit();
        healthBar.SetActive(true);
        ChangeHealth((player.currentDmg)*-1);
        //Debug.Log("Attacked!" + player.currentDmg);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxhealth);
        slider.value = currentHealth;


        //Debug.Log("Enemy: " + currentHealth + "/" + maxhealth);
    }

    void flip()
    {
        //faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0);
    }

    IEnumerator DeathWait()
    {
        yield return new WaitForSecondsRealtime(wait);    
        gameObject.SetActive(false);
    }
}
