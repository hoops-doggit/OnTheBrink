using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<HamsterAI>() != null)
        {
            HamsterAI ham = other.GetComponent<HamsterAI>();
            ham.Eating();
        }
    }
}
