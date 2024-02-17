using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    CharacterScript CharacterScript;

    // Start is called before the first frame update
    void Start()
    {
        CharacterScript = GetComponentInParent<CharacterScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        EnemyHitBox hitBox = collision.GetComponent<EnemyHitBox>();

        BossHitBox bossHitBox = collision.GetComponent<BossHitBox>();

        Bullet bullet = collision.GetComponent<Bullet>();

        if (bossHitBox != null && hitBox == null && bullet == null)
        {
            CharacterScript.ChangeHealth(bossHitBox.damage * -1, 1);
        }

        else if (hitBox != null && bullet == null && bossHitBox == null)
        {
            CharacterScript.ChangeHealth(hitBox.damage * -1, 1);
        }

        else if(bullet != null && hitBox == null && bossHitBox == null)
        {
            CharacterScript.ChangeHealth(bullet.damage * -1, 1);
        }

    }
}
