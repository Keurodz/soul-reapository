using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame() {
        SaveTime();
        Application.Quit();
    }

     public void SaveTime() {
        PlayerPrefs.SetFloat("Playtime", (Mathf.Round((PlayerPrefs.GetFloat("Playtime", 0) + Time.time) * 100)) / 100.0f);
    }
}
