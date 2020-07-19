using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void TurnOnPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void TurnOffPauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(1);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == true)
        {
            TurnOffPauseMenu();
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
        {
            TurnOnPauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }
}
