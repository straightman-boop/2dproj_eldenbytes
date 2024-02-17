using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossShoot : MonoBehaviour
{
    public Bullet bulletPrefab;
    public int rateOfFire = 5;
    private ObjectPool bulletPool;

    bool playerSpotted = false;
    bool haveFired = false;

    public GameObject healthBar;
    public Slider slider;

    public int maxhealth = 30;
    public int Health { get { return currentHealth; } }
    int currentHealth;

    bool death = false;

    public GameObject fogwall;
    public GameObject wall;

    DetectionScript detection;

    FlickerScript flicker;

    private void Awake()
    {
        bulletPool = ObjectPool.CreateInstance(bulletPrefab, 100);

        detection = gameObject.GetComponentInChildren<DetectionScript>();

        currentHealth = maxhealth;

        flicker = GetComponent<FlickerScript>();
    }

    private void Start()
    {
        slider.maxValue = maxhealth;
        slider.value = maxhealth;
    }

    private void Update()
    {

        if (detection.m_Target != null)
        {
            wall.SetActive(false);
            fogwall.SetActive(true);
            playerSpotted = true;
        }

        if (playerSpotted == true && haveFired == false)
        {
            haveFired = true;
            StartCoroutine(fire());
        }

        if (currentHealth == 0 && death == false)
        {
            death = true;

            if (death == true)
            {
                //speed = -5;

                //gameObject.SetActive(false);
                //animator.SetFloat("Speed", speed);
                //animator.SetTrigger("Death");
                StartCoroutine(DeathWait());
            }
        }
    }

    private IEnumerator fire()
    {
        WaitForSeconds wait = new WaitForSeconds(1f / rateOfFire);

        while (true)
        {
            PoolableObject instance = bulletPool.GetObject();

            if (instance != null)
            {
                instance.transform.SetParent(transform, false);
                instance.transform.localPosition = Vector2.zero;
            }

            yield return wait;
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
        flicker.gotHit();
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
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        fogwall.SetActive(false);
    }
}
