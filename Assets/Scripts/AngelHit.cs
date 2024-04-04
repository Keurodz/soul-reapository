using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelHit : MonoBehaviour
{
    public GameObject angelDeath;

    public GameObject healthLoot;
    public GameObject speedLoot;
    public GameObject shieldLoot;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ReaperProjectile") || other.CompareTag("SwipeAttack"))
        {
            DestroyAngel();
        }
    }

    void DestroyAngel()
    {
        // spawn death particles
        Instantiate(angelDeath, transform.position, transform.rotation);

        // destroy angel
        gameObject.SetActive(false);

        // 50% chance to spawn a random loot (3 different types, 33% each)
        float coinFlip = Random.value;
        if (coinFlip < 0.5)
        {
            float lootType = Random.value;
            if (lootType <= 0.33)
            {
                // spawn health drop
                Instantiate(healthLoot, transform.position, transform.rotation);
            }
            else if (lootType >= 0.67)
            {
                // spawn speed boost
                Instantiate(speedLoot, transform.position, transform.rotation);
            }
            else
            {
                // spawn 1-hit shield
                Instantiate(shieldLoot, transform.position, transform.rotation);
            }
        }

        Destroy(gameObject);
    }
}
