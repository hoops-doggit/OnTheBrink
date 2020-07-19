using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingObject : MonoBehaviour
{
public Renderer rend;

void Start(){
rend = GetComponent<Renderer>();
}
    void Update () {
        if(Time.fixedTime%.5<.2)
        {
            rend.enabled=false;
        }
        else{
            rend.enabled=true;
        }
    }
}
