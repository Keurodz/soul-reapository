using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateAbout : MonoBehaviour
{
    public TMP_Text playtimeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlaytime() {
        playtimeText.text = PlayerPrefs.GetFloat("Playtime", 0).ToString() + " sec";
    }
}
