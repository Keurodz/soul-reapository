using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{

    public AudioClip pickupSFX;

    float playerToPickupDist;
    Vector3 playerPosition;
    Vector3 pickupPosition;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{

        //    AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        //    Destroy(gameObject);

        //}
    }
}
