using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public Text gameText;
    public Text scoreText;

    public AudioClip gameOverClipSFX;

    public AudioClip gameWonSFX;

    public static bool isGameOver = false;

    public string nextLevel;

    public float targetScore;
    public static float totalScore = 0;

    bool endSFXPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;

        totalScore = 0;

        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (totalScore >= targetScore)
            {
                // time delay so final score can be updated
                Invoke("LevelBeat", 0.5f);
            }
            else if (PlayerHealth.currentHealth <= 0)
            {
                //LevelLost();
            }

            SetScoreText();
        }

    }





    void SetScoreText()
    {
        scoreText.text = totalScore.ToString("Score: " + totalScore);
    }

    /*
    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "GAME OVER!";
        gameText.gameObject.SetActive(true);

        if (endSFXPlayed == false)
        {
            endSFXPlayed = true;
            AudioSource.PlayClipAtPoint(gameOverClipSFX, Camera.main.transform.position);
        }
        

        Invoke("LoadCurrentLevel", 2);
    }


    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "YOU WIN!";
        gameText.gameObject.SetActive(true);

        if (endSFXPlayed == false)
        {
            endSFXPlayed = true;
            AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);
        }
        

        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2);
        }

    }
    */

    /*
    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    */
}
