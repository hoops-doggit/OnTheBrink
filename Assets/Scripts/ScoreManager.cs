using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;

    private void Awake()
    {
        instance = this;
    }

    public void PlusOneToScore()
    {
        score++;
        ContainerOfEverything.instance.scoreText.text = score.ToString();
    }

}
