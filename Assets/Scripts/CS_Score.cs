using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Score : MonoBehaviour
{
    public static int Score;
    public static int TotalScore;
    public Text ShowScore;

    void Start()
    {
        Score = 0;
    }

    void Update()
    {
        TotalScore = Score;
        ShowScore.text = TotalScore.ToString();
    }

}
