using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public int score;

    [SerializeField] TMPro.TextMeshProUGUI scoreText; // text that is updated with the new score

    private void Awake()
    {
        ScoreManager.instance.SaveAndLoadPlayer();
    }
    private void Start()
    {
        scoreText.text = "Score: " + score;
    }
}
