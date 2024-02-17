using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    public GameObject hitBox;
    EnemyAttackRadius attackRadius;
    BoxCollider2D boxCollider;
    EnemyAttacker EnemyAttacker;

    float time;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        attackRadius = hitBox.GetComponent<EnemyAttackRadius>();
        EnemyAttacker = GetComponentInParent<EnemyAttacker>();
        time = attackRadius.coolTime;

        
    }

    // Update is called once per frame
    void Update()
    {
        damage = attackRadius.damage;

        if (EnemyAttacker.isDead == true)
        {
            if(boxCollider.enabled == true)
            {
                boxToggle();
            }
        }
    }

    public void boxToggle()
    {
        boxCollider.enabled = !boxCollider.enabled;
    }
}
