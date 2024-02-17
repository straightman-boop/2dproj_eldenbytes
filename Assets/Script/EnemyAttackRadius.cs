using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackRadius : MonoBehaviour
{
    bool onCooldown = false;
    public float coolTime {get {return cooldownTime;} }
    float cooldownTime;
    float cooldownTimer;

    BoxCollider2D boxcollider;

    public Animator animator;
    public EnemyAttacker attacker;
    float defSpeed;

    public GameObject hitbox;
    EnemyHitBox EnemyHitBox;

    int type;

    int boarDmg = 3;
    int soldierDmg = 8;
    int nobleDmg = 5;

    float boarAttk = 0.3f;
    float soldierAttk = 0.3f;
    float nobleAttk = 0.7f;

    float boarDelay = 0f;
    float soldierDelay = 0f;
    float nobleDelay = 0f;

    float attckTime;
    float delayTime;

    public int damage;

    AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
        attacker = GetComponentInParent<EnemyAttacker>();

        defSpeed = attacker.speed;

        EnemyHitBox = hitbox.GetComponent<EnemyHitBox>();

        AudioSource = GetComponent<AudioSource>();

        if (attacker.boar == true)
        {
            type = 1;
            attckTime = boarAttk;
            delayTime = boarDelay;
        }
        else if (attacker.soldier == true)
        {
            type = 2;
            attckTime = soldierAttk;
            delayTime = soldierDelay;
        }
        else if (attacker.noble == true)
        {
            type = 3;
            attckTime = nobleAttk;
            delayTime = nobleDelay;
        }

        if (type == 1)
        {
            cooldownTime = 0.02f;
            damage = boarDmg;
        }

        else if (type == 2)
        {
            cooldownTime = 0.03f;
            damage = soldierDmg;
        }

        else if (type == 3)
        {
            cooldownTime = 1f;
            damage = nobleDmg;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (onCooldown == true)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                EnemyHitBox.boxToggle();
                Invoke("toggleBox", 0f);
                attacker.attacking = false;
                onCooldown = false;
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onCooldown == false)
        {           
            StartCoroutine(DelayAnim());
        }

    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (onCooldown == false)
    //    {
    //        StartCoroutine(DelayAnim());
    //    }
    //}

    void toggleBox()
    {
        boxcollider.enabled = !boxcollider.enabled;
    }

    //IEnumerator HitBoxWait() //coroutine can be used to delay the invoke methods for the animation sprites
    //{
    //    yield return null;
    //    Invoke("toggleBox", 0f);
    //    Invoke("toggleBox", 0.1f);
    //}

    IEnumerator DelayAnim()
    {
        Invoke("toggleBox", 0f);
        //attacker.speed = 0;
        attacker.attacking = true;
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(delayTime);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(attckTime);
        AudioSource.Play();
        EnemyHitBox.boxToggle();     
        yield return new WaitForSeconds(0.8f);





        //Debug.Log("Enemy can ATTACK");
        onCooldown = true;
        cooldownTimer = cooldownTime;
    }


}
