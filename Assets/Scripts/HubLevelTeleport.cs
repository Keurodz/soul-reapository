using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubLevelTeleport : MonoBehaviour
{
    public string levelToLoad;
    public float rotationSpeed = 30f;
    public float bobSpeed = 2f;
    public float bobHeight = 0.1f;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = originalPosition + new Vector3(0f, yOffset, 0f);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }

}
