using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHover : MonoBehaviour
{

    Vector3 tempPos = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        tempPos.x = transform.position.x;
        tempPos.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos.y +=  Mathf.Sin(Time.fixedTime) * 0.01f;
        transform.position = tempPos + Vector3.up;
    }
}
