using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldLoot : MonoBehaviour
{
    public AudioClip lootSFX;
    public static bool shieldActive = false;
    public Image shieldIconUI;

    private Color iconOgColor;
    private bool hasBeenActive = false;

    private void Start()
    {
        shieldIconUI = GameObject.FindGameObjectWithTag("ShieldIcon").GetComponent<Image>();
        iconOgColor = shieldIconUI.color;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);

        if (transform.position.y < Random.Range(1.0f, 3.0f))
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }

        if (!shieldActive && hasBeenActive)
        {
            shieldIconUI.color = iconOgColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(lootSFX, transform.position);

            Debug.Log("Shielded!");
            shieldActive = true;
            hasBeenActive = true;
            shieldIconUI.color = new Color(shieldIconUI.color.r, shieldIconUI.color.g, shieldIconUI.color.b, 1f);

            Destroy(gameObject, 0.5f);
        }
    }
}
