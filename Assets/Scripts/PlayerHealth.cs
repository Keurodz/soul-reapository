using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public Text healthText;

    int currentHealth;
    LevelManager levelManager;

    static public bool playerCanTakeDamage = true;
    [SerializeField]
    private float iFramesDuration = 1;

    // I want to make the scythe opaque when invincible!!
    /**
    public GameObject scythe;
    Renderer scytheRenderer;
    Color scytheColor;
    **/

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmt)
    {
        if (playerCanTakeDamage && currentHealth > 0)
        {
            currentHealth -= damageAmt;
            healthSlider.value = currentHealth;
            healthText.text = currentHealth.ToString();

            StartCoroutine(InvincibilityFrames());
        }

        if (currentHealth <= 0)
        {
            PlayerDies();
            levelManager.LevelLost();
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        // Make player invincible
        playerCanTakeDamage = false;

        // scytheColor.a = 0.25f;
        // scytheRenderer.material.color = scytheColor;

        // Allow iFramesDuration seconds of invincibility
        yield return new WaitForSeconds(iFramesDuration);

        // Allow player to be hit again
        playerCanTakeDamage = true;

        // scytheColor.a = 1f;
        // scytheRenderer.material.color = scytheColor;
    }

    void PlayerDies()
    {
        Debug.Log("Player dead");
        transform.Rotate(-90, 0, 0, Space.Self);
    }

    public void Heal(int healthAmt)
    {
        if (currentHealth < startingHealth)
        {
            currentHealth += healthAmt;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, startingHealth);
        }
    }
}
