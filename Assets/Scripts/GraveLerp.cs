using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveLerp : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    public Light SpotLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SpotLight.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1));
    }
}
