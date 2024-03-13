using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReaperAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 50f;
    public AudioClip swipeSFX;
    public AudioClip shootSFX;

    public GameObject swingRadius;

    public Image reticleImage;
    public Color angelColor;
    public Color spiritColor;

    Color originalReticleColor;
    bool angelAttack = false;
    Animator anim;


    void Start()
    {
        originalReticleColor = reticleImage.color;
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (angelAttack)
            {
                GameObject projectile = Instantiate(projectilePrefab,
                    transform.position + transform.forward, transform.rotation) as GameObject;

                Rigidbody rb = projectile.GetComponent<Rigidbody>();

                rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

                // projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

                // AudioSource.PlayClipAtPoint(shootSFX, transform.position);
            } else
            {
                anim.SetTrigger("Swing");

                // swingRadius.SetActive(true);
                Invoke("ActivateSwingRadius", .2f);

                AudioSource.PlayClipAtPoint(swipeSFX, transform.position);

                Invoke("DeactivateSwingRadius", 1);
            }
        }
    }

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    void ReticleEffect()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                angelAttack = true;

                reticleImage.color = Color.Lerp
                    (reticleImage.color, angelColor, Time.deltaTime * 2);

                reticleImage.transform.localScale = Vector3.Lerp
                    (reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
            }
            else if (hit.collider.CompareTag("Spirit"))
            {
                angelAttack = false;

                reticleImage.color = Color.Lerp
                    (reticleImage.color, spiritColor, Time.deltaTime * 2);

                reticleImage.transform.localScale = Vector3.Lerp
                    (reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
            }
            else
            {
                angelAttack = false;

                reticleImage.color = Color.Lerp
                   (reticleImage.color, originalReticleColor, Time.deltaTime * 2);

                reticleImage.transform.localScale = Vector3.Lerp
                    (reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
            }
        }
        else
        {
            reticleImage.color = Color.Lerp
                (reticleImage.color, originalReticleColor, Time.deltaTime * 2);

            reticleImage.transform.localScale = Vector3.Lerp
                (reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
        }
    }
    void ActivateSwingRadius()
    {
        swingRadius.SetActive(true);
    }
    void DeactivateSwingRadius()
    {
        swingRadius.SetActive(false);
    }
}
