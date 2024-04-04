using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLoot : MonoBehaviour
{
    public float speedMultiplier = 1.5f;
    public AudioClip lootSFX;

    public static bool boostActive = false;
    public static float boostDuration = 5;

    private float originalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = PlayerController.moveSpeed;

        // band-aid fix because the object needs to exist for the boost to take action
        // and go away properly
        Destroy(gameObject, boostDuration + 3f);
    }

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
            AudioSource.PlayClipAtPoint(lootSFX, transform.position);

            // only issue is that picking up a speed boost while already having
            // one doesn't extend duration (?)
            if (!boostActive)
            {
                PlayerController.moveSpeed *= speedMultiplier;

                StartCoroutine(SpeedBoost());
            }

            // Boost needs to exist in game for the Coroutine to take place...
            GetComponent<Collider>().enabled = false;
            foreach (MeshRenderer r in GetComponentsInChildren<MeshRenderer>())
            {
                r.enabled = false;
            }

            Destroy(gameObject, boostDuration + 0.1f);
        }


    }

    public IEnumerator SpeedBoost()
    {
        // Activate speed boost
        SpeedLoot.boostActive = true;


        // Allow speed boost for boostDuration seconds
        yield return new WaitForSeconds(SpeedLoot.boostDuration);

        PlayerController.moveSpeed = originalSpeed;
        // De-activate speed boost
        SpeedLoot.boostActive = false;

    }
}
