using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public static MouseInput instance;

    private Camera gameCameraCam;

    private Vector3 mousePointInWorld;
    public Transform mousePointTransform;

    public Vector3 MousePointToWorldVector3
    {
        get
        {
            return mousePointInWorld;
        }
    }


    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        GameObject gameCam = ContainerOfEverything.instance.gameCam;
        gameCameraCam = gameCam.GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;

        RaycastHit hit;

        Ray ray = gameCameraCam.ScreenPointToRay(mouse);

        //LayerMask lm = (1 << LayerMask.NameToLayer("default"));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            mousePointTransform.transform.position = hit.point;
        }
    }
}
