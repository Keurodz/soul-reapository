using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAttack : MonoBehaviour
{
    // public GameObject attackPrefab;

    public AudioClip swipeSFX;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            /*GameObject projectile = Instantiate(attackPrefab,
                transform.position + transform.forward, transform.rotation) as GameObject;*/
            anim.SetTrigger("Swing");
            GameObject swingRadius = transform.Find("SwingRadius").gameObject;
            swingRadius.SetActive(true);
            AudioSource.PlayClipAtPoint(swipeSFX, transform.position);
            Invoke("StopSwing", 1f);
        }
    }

    void StopSwing()
    {
        GameObject swingRadius = transform.Find("SwingRadius").gameObject;
        swingRadius.SetActive(false);
    }
}