using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAttack : MonoBehaviour
{
    public GameObject attackPrefab;

    public AudioClip swipeSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(attackPrefab,
                transform.position + transform.forward, transform.rotation) as GameObject;

            AudioSource.PlayClipAtPoint(swipeSFX, transform.position);
        }
    }
}
