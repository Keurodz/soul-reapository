using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject gameStateVisual;
    public Text gameStateText;
    public Text spiritCountText;

    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;

    public static bool isGameOver = false;

    public string nextLevel;
    public Text levelText;

    float spiritCount;
    static public float spiritsCollected;

    bool gameEndSFXPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.playerCanTakeDamage = true;
        isGameOver = false;
        Invoke("DisableText", 5f);

        // count the spirits in the level for score-keeping 
        GameObject[] spirits = GameObject.FindGameObjectsWithTag("Spirit");
        spiritCount = spirits.Length;
        spiritsCollected = 0;

        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (spiritsCollected >= spiritCount)
            {
                // time delay so final score can be updated
                Invoke("LevelBeat", 0.5f);
            }


            SetScoreText();
        }
    }

    void SetScoreText()
    {
        spiritCountText.text = spiritsCollected + " / " + spiritCount;
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameStateText.text = "Game Over!";
        gameStateVisual.gameObject.SetActive(true);

        if (gameEndSFXPlayed == false)
        {
            gameEndSFXPlayed = true;
            AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);
        }

        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameStateText.text = "You Win!";
        gameStateVisual.gameObject.SetActive(true);

        if (gameEndSFXPlayed == false)
        {
            gameEndSFXPlayed = true;
            AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);
        }

        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void DisableText() {
        levelText.enabled = false;
    }
}
