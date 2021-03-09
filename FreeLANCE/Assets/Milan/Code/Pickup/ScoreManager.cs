using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private void Awake()
    {
        instance = this;
    }
    //--------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------

    [Header("Score Settings")]
    public int score;
    public int highScore;

    public static bool pickedUp = false;
    [SerializeField] TMPro.TextMeshProUGUI scoreText; // text that is updated with the new score
    [SerializeField] TMPro.TextMeshProUGUI highScoreText; // text that is updated with the new score
    [SerializeField] TMPro.TextMeshProUGUI timerText; // text that is updated with the new score

    [Header("Timer")]
    public float timer = 180;
    public bool timeIsOver = false;
    public bool timeisRunning = true;

    private void Update()
    {
        if (timeisRunning)
        {
            timer -= Time.deltaTime;
        }
        Countdown();
    }

    public void RefreshUI()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "Highscore: " + highScore;
    }

    public void SaveAndLoadPlayer()
    {
        // Save the player to the file
        PlayerData _data = SaveSystem.loadPlayer();

        Debug.Log(_data.HighScore + "-HighScore, " + ScoreManager.instance.score + "- Score");

        if (_data.HighScore < score)
        {
            SaveSystem.SavePlayer();
            highScore = score;
        }
        else if (_data.HighScore >= score)
        {
            highScore = _data.HighScore;
        }
        RefreshUI();
    }

    public void Countdown()
    {
        if (timer >0)
        {
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("Timer: " + "{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = string.Format("Timer is over.");
            timeisRunning = false;
        }
    }
}
