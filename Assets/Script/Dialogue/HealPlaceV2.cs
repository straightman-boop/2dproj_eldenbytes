using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealPlaceV2 : MonoBehaviour
{
    public GameObject buff;
    //public GameObject interact;
    NPC npc;

    bool validArea = false;
    bool didOnce;

    public GameObject melina;
    //public GameObject DialogueObject;
    //public GameObject UI;

    Animator animator;

    public float coolTime { get { return cooldownTime; } }
    float cooldownTime = 0.8f;
    float cooldownTimer;

    bool onCooldown = false;
    bool canSummon = true;

    private void Start()
    {
        animator = melina.gameObject.GetComponent<Animator>();
        //npc = melina.gameObject.GetComponent<NPC>();
        //dialogueManager = DialogueObject.GetComponent<DialogueManager>();

    }

    private void Update()
    {
        //var E = Input.GetKey(KeyCode.E);

        //if (E == true && validArea == true && didOnce == false)
        //{       
        //    didOnce = true;

        //}

        if (onCooldown == true)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                onCooldown = false;
                canSummon = true;
            }
        }

        //Debug.Log("cooldown: " + onCooldown + " " + "can summon: " + canSummon);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //interact.SetActive(true);
        validArea = true;

        CharacterScript controller = collision.GetComponent<CharacterScript>();
        controller.frozen = false;
        controller.freezeBuff.SetActive(false);
        controller.speed = 5;

        if (didOnce == false)
        {
            didOnce = true;
            //UI.SetActive(true);
        }

        buff.SetActive(true);

        //Debug.Log("Entered");

        if (controller != null)
        {
            if (controller.Health < controller.maxhealth)//Can only be collected if healt != max
            {
                controller.ChangeHealth(1, 2);

            }
        }

        if (onCooldown == false && canSummon == true)
        {
            melina.SetActive(true);

        }


    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        CharacterScript controller = collision.GetComponent<CharacterScript>();

        //Debug.Log("Stayed!");

        var e = Input.GetKey(KeyCode.E);

        if (e)
        {
            //dialogueManager.DisplayNextSentence();
            e = false;
        }

        if (controller != null)
        {
            if (controller.Health < controller.maxhealth)//Can only be collected if healt != max
            {
                controller.ChangeHealth(1, 2);

            }
        }



    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //interact.SetActive(false);
        validArea = false;

        buff.SetActive(false);

        //StartCoroutine(melinaWait());
    }

    //IEnumerator melinaWait()
    //{
    //    animator.SetBool("Exited", true);
    //    yield return new WaitForSeconds(1f);
    //    animator.SetBool("Exited", false);
    //    yield return new WaitForSeconds(0.5f);
    //    melina.SetActive(false);

    //    onCooldown = true;
    //    cooldownTimer = cooldownTime;
    //}


}
