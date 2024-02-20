using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelHit : MonoBehaviour
{
    public GameObject angelDeath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SwipeAttack"))
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
        Destroy(gameObject);
    }
}
