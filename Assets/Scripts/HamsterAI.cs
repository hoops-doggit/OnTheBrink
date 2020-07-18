using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAI : MonoBehaviour
{
    private Rigidbody rb;  
     public float restartDelay = 5f; 
    public float HamSpeed = 3f;
    public float HamBounce = 1f;
    public Vector3 HamRotate;
    private int randSeed = 0;
    public bool isLured = false;
    float restartTimer;   
    void Awake()
    {        
        rb = GetComponent<Rigidbody>();
        HamRotate = new Vector3(0, 100, 0);
    }
    
    void Update()
    {
// If transform.position.y < 1000, destroy object
            rb.MovePosition(transform.position + ((transform.forward * HamSpeed) + (transform.up * HamBounce)) * Time.deltaTime);
            if(isLured == false)
            {
            // .. increment a timer to count up to restarting.
                restartTimer += Time.deltaTime;
                rb.MovePosition(transform.position + ((transform.forward * HamSpeed) + (transform.up * HamBounce)) * Time.deltaTime);
                // .. if it reaches the restart delay...
                if(restartTimer >= restartDelay)
                {
                    // .. then reload the currently loaded level.
                    //Do something
                    isLured = true;
                }
            }
    
    
    }
    
}
