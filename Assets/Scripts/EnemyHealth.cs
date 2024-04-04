using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deadSFX;
    public Slider healthSlider;

    public int currentHealth;

    private void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmt)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmt;
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            //Enemy dead!
        }
    }

    public void Heal(int healthAmt)
    {
        if (currentHealth < 100)
        {
            currentHealth += healthAmt;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(10);
        }
    }
}
