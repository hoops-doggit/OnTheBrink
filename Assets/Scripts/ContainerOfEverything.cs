using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContainerOfEverything : MonoBehaviour
{
    public static ContainerOfEverything instance;
    public GameObject player;
    public GameObject gameCam;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


    // Update is called once per frame
    void Update()
    {
    


    }


}