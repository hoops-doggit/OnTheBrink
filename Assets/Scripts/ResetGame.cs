using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{

    public void ResetWholeGame()
    {
        SceneManager.LoadScene(0);
    }

}
