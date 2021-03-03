using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static int score;
    public static int highScore;

    public static bool pickedUp = false;
    [SerializeField] private static TMP_Text PointTxt;

    public static void RefreshUI()
    {
        PointTxt.text = "Score: " + score;
    }
}
