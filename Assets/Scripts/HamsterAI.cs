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
    public Vector3 lurePoint;
    private Vector3 smoothDampVelocityREF;
    private Transform lookTransform;
    public float turnSpeed;
    
    void Awake()
    {
        lookTransform = new GameObject().transform;
        myState = State.gototarget;
        rb = GetComponent<Rigidbody>();
        HamRotate = new Vector3(0, 100, 0);
        lurePoint = LurePoint();
    }

    public Vector3 LurePoint()
    {
        Vector3 lureP = new Vector3
        {
            x = Random.Range(-17, 17),
            y = 0,
            z = Random.Range(-17, 17)
        };
        return lureP;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoopDiamond();
        }

        if (lookForTargetTimer == 0f && myState == State.gototarget)
        {
            myState = State.lookfortarget;
            goToTargetTimer = 5f;
        }
        if(goToTargetTimer == 0f && myState == State.lookfortarget)
        {
            lurePoint = LurePoint();
            myState = State.gototarget;
            randSeed = Random.Range(1,4);
            lookForTargetTimer = 5f;
        }
        goToTargetTimer = Mathf.Max(0, goToTargetTimer - Time.deltaTime);
        lookForTargetTimer = Mathf.Max(0, lookForTargetTimer - Time.deltaTime);
        rb.MovePosition(transform.position + ((transform.forward * HamSpeed) + (transform.up * HamBounce)) * Time.deltaTime);

        if (transform.position.y < -100)
        {
            DestroyHamster();
        }

        switch (myState)
        {
            case State.gototarget:
                // pick target


                rb.AddRelativeForce(Vector3.up * HamSpeed, ForceMode.Force);

                // run to target
                //rb.MovePosition(transform.position + ((transform.forward * HamSpeed) + (transform.up * HamBounce)) * Time.deltaTime);
                break;
            case State.lookfortarget:
                lookTransform.position = Vector3.SmoothDamp(lookTransform.position, lurePoint, ref smoothDampVelocityREF, turnSpeed);
                transform.LookAt(lookTransform);
                // rotate around
                break;
            default:
                break;
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
