using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLoot : MonoBehaviour
{
    public int healthAmount = 10;
    public AudioClip lootSFX;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);

        if (transform.position.y < Random.Range(1.0f, 3.0f))
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(lootSFX, transform.position);

            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(-healthAmount);

            Destroy(gameObject, 0.5f);
        }


    }
}
