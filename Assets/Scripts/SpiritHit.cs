using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHit : MonoBehaviour
{
    public GameObject spiritDeath;

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
            DestroySpirit();
        }
    }

    void DestroySpirit()
    {
        //Increment points / do something with level manager

        // spawn death particles
        //Instantiate(spiritDeath, transform.position, transform.rotation);

        // destroy angel
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
