using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondScript : MonoBehaviour
{
    private ScoreManager x;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GotCollected();
        }
    }

    private void GotCollected()
    {
        ScoreManager.instance.PlusOneToScore();
        Destroy(gameObject);
    }

}
