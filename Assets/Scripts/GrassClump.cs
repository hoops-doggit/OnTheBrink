using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassClump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        foreach(HingeJoint h in GetComponentsInChildren<HingeJoint>())
        {
            Vector3 axis = h.axis;
            axis.z = Random.Range(0, 360);
            h.axis = axis;
        }
    }


}
