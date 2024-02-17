using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossAttackRadius : MonoBehaviour
{
    public int bossDmg = 10;
    int damage;

    bool onCooldown = false;
    public float coolTime { get { return cooldownTime; } }
    float cooldownTime;
    float cooldownTimer;

    KnightBossScript KnightBossScript;
    public GameObject hitbox;
    BossHitBox bossHitBox;
    public Animator animator;

    BoxCollider2D boxCollider;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        damage = bossDmg;

        bossHitBox = hitbox.GetComponent<BossHitBox>();
        KnightBossScript = GetComponentInParent<KnightBossScript>();

        boxCollider = GetComponent<BoxCollider2D>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onCooldown == true)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                //Debug.Log("Cooldown end");
                bossHitBox.boxToggle();
                Invoke("toggleBox", 0f);
                KnightBossScript.attacking = false;
                onCooldown = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float random = Random.Range(1, 3);

        if (onCooldown == false && random == 1)
        {
            StartCoroutine(DelayAnim1());
        }

        else if (onCooldown == false && random == 2)
        {
            StartCoroutine(DelayAnim2());
        }

    }

    public void toggleBox()
    {
        boxCollider.enabled = !boxCollider.enabled;
    }

    IEnumerator DelayAnim1()
    {
        Invoke("toggleBox", 0f);
        KnightBossScript.attacking = true;
        animator.SetBool("Attack1", true);
        yield return new WaitForSeconds(0.7f);
        audioSource.Play();
        animator.SetBool("Attack1", false);
        yield return new WaitForSeconds(0.1f);
        bossHitBox.boxToggle();
        yield return new WaitForSeconds(0.8f);

        //Debug.Log("Enemy can ATTACK");
        onCooldown = true;
        cooldownTimer = cooldownTime;
    } 
    
    IEnumerator DelayAnim2()
    {
        Invoke("toggleBox", 0f);
        KnightBossScript.attacking = true;
        animator.SetBool("Attack2", true);
        audioSource.Play();
        yield return new WaitForSeconds(0.3f);
        bossHitBox.boxToggle();
        yield return new WaitForSeconds(0.7f);
        animator.SetBool("Attack2", false);   
        yield return new WaitForSeconds(0.8f);

        //Debug.Log("Enemy can ATTACK");
        onCooldown = true;
        cooldownTimer = cooldownTime;
    }
}
