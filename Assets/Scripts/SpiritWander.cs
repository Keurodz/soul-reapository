using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritWander : MonoBehaviour
{
    public float posxLimit;
    public float negxLimit;
    public float poszLimit;
    public float negzLimit;
    public float moveSpeed;

    Vector3 nextDestination;

    // Start is called before the first frame update
    void Start()
    {
        nextDestination = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else
        {
            transform.LookAt(nextDestination);

            transform.position = Vector3.MoveTowards
                (transform.position, nextDestination, moveSpeed * Time.deltaTime);
        }
    }

    void FindNextPoint()
    {
        float randomX = Random.Range(negxLimit, posxLimit);
        float randomZ = Random.Range(negzLimit, poszLimit);
        nextDestination = new Vector3(randomX, transform.position.y, randomZ);
    }

}
