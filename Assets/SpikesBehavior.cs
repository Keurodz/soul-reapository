using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBehavior : MonoBehaviour
{
    public int damageAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // apply damage
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
