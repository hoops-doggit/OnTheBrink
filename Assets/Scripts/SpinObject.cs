using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public int xVal=0;
    public int yVal=50;
    public int zVal=0;

    void Update ()
    {
        transform.Rotate(xVal * Time.deltaTime, yVal * Time.deltaTime, zVal * Time.deltaTime); 
    }
}
