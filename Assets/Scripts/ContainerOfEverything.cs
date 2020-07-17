using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContainerOfEverything : MonoBehaviour
{
    public static ContainerOfEverything instance;
    public GameObject player;
    [SerializeField] GameObject gameCam;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GameCam()
    {
        if (gameCam != null)
        {
            return gameCam;
        }

        else return null;        
    }

}
