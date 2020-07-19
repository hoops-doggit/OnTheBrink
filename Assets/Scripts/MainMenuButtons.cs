using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject instructions;


    public void TurnInstructionsOn()
    {
        instructions.SetActive(true);
    }

    public void TurnInstructionsOff()
    {
        instructions.SetActive(false);
    }
}
