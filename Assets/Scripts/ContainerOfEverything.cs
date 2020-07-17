using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContainerOfEverything : MonoBehaviour
{
    public static ContainerOfEverything instance;
    public GameObject player;
    [SerializeField] GameObject gameCam;
    private Camera gameCamCamera;

    private Vector3 mousePointInWorld;

    public Vector3 MousePointToWorldVector3
    {
        get
        {
            return mousePointInWorld;
        }
    }

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

        gameCamCamera = gameCam.GetComponent<Camera>();
    }

    public GameObject GameCam()
    {
        if (gameCam != null)
        {
            return gameCam;
        }

        else return null;        
    }


    // Update is called once per frame
    void Update()
    {
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = gameCamCamera.pixelHeight - currentEvent.mousePosition.y;

        mousePointInWorld = gameCamCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, gameCamCamera.nearClipPlane));

    }

}
