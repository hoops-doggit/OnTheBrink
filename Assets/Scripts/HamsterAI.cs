using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAI : MonoBehaviour
{
    enum State{gototarget, lookfortarget};
    private Rigidbody rb;  
    public float restartDelay = 5f; 
    public float HamSpeed = 3f;
    public float HamBounce = 1f;
    public Vector3 HamRotate;
    private int randSeed = 0;
    public bool isLured = false;
    public float lookForTargetTimer = 5f; 
    public float goToTargetTimer = 5f;  
    public Transform diamante;
    private State myState;
    public Transform[] lurePoints;
    
    void Awake()
    {        
        myState = State.gototarget;
        rb = GetComponent<Rigidbody>();
        HamRotate = new Vector3(0, 100, 0);
    }
    
    void Update()
    {
        if(lookForTargetTimer == 0f && myState == State.gototarget)
        {
            myState = State.lookfortarget;
            goToTargetTimer = 5f;
        }
        if(goToTargetTimer == 0f && myState == State.lookfortarget)
        {
            myState = State.gototarget;
            int randSeed = Random.Range(1,4);
            lookForTargetTimer = 5f;
        }
        goToTargetTimer = Mathf.Max(0, goToTargetTimer - Time.deltaTime);
        lookForTargetTimer = Mathf.Max(0, lookForTargetTimer - Time.deltaTime);
        rb.MovePosition(transform.position + ((transform.forward * HamSpeed) + (transform.up * HamBounce)) * Time.deltaTime);
        switch (myState)
        {
            case State.gototarget:
                // pick target
                
                transform.LookAt(lurePoints[randSeed]);
                rb.AddRelativeForce(Vector3.forward * HamSpeed, ForceMode.Force);
                // run to target
                //rb.MovePosition(transform.position + ((transform.forward * HamSpeed) + (transform.up * HamBounce)) * Time.deltaTime);
                break;
            case State.lookfortarget:
                // rotate around
                break;
            default:
                break;
        }
                
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PoopDiamond();
        }

        if(transform.position.y < -100)
        {
            DestroyHamster();
        }
        
    }
    
    void PoopDiamond()
    {
        Instantiate(diamante, new Vector3(transform.position.x,transform.position.y+1,transform.position.z), Quaternion.identity);
    }
    
    void DestroyHamster()
    {
        Destroy(gameObject);
    }
}
