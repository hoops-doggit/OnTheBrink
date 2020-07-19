using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingHamsters : MonoBehaviour
{
    public static RemainingHamsters instance;

    public HamsterAI[] hamsters;

    public int remainingHamsters = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach(HamsterAI h in hamsters)
        {
            remainingHamsters++;
        }
    }

    public void RemoveHamster()
    {
        remainingHamsters--;
    }
}
