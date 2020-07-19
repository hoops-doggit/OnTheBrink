using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public int hampsters;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        hampsters = RemainingHamsters.instance.remainingHamsters;
        ContainerOfEverything.instance.scoreText.text = score + " gems,  " + hampsters.ToString()+" hamsters";

    }

    public void PlusOneToScore()
    {
        score++;
        ContainerOfEverything.instance.scoreText.text = score.ToString();
    }

    

}
