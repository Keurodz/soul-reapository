using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public Transform player;

    public float moveSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        transform.position = Vector3.MoveTowards
            (transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
