using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeMouseSensitivity : MonoBehaviour
{
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseSensitivity() {
        MouseLook.mouseSensitivity = Mathf.Clamp(MouseLook.mouseSensitivity + 50, MouseLook.mouseSensitivity, 500);
        UpdateText();
    }

    public void DecreaseSensitivity() {
        MouseLook.mouseSensitivity = Mathf.Clamp(MouseLook.mouseSensitivity - 50, 50, MouseLook.mouseSensitivity);
        UpdateText();
    }

    void UpdateText() {
        text.text = MouseLook.mouseSensitivity.ToString();
    }
}
