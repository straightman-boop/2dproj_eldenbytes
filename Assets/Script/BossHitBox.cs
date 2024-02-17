using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    BoxCollider2D boxCollider;

    public int damage;

    public GameObject attack;
    BossAttackRadius attackRadius;

    // Start is called before the first frame update
    void Start()
    {
        attackRadius = attack.gameObject.GetComponent<BossAttackRadius>();
        boxCollider = GetComponent<BoxCollider2D>();

        damage = attackRadius.bossDmg;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void boxToggle()
    {
        boxCollider.enabled = !boxCollider.enabled;
    }
}
