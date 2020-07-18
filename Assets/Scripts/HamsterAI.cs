using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAI : MonoBehaviour
{
    enum State{lured, hungry, wrangled, bored, eating};
    private Rigidbody rb;  
    public float HamSpeed = 3f;
    public float HamBounce = 1f;
    private int randSeed = 0; 
    public Transform diamante;
    private State myState;
    public Vector3 lurePoint;
    private Vector3 smoothDampVelocityREF;
    private Transform lookTransform;
    public float turnSpeed;    
    
    public float hungerTimer = 5f; 
    public float luredTimer = 0f; 
    public float wrangledTimer = 10f;
    public float boredTimer = 8f;
    public float eatingTimer = 4f;
    
    void Awake()
    {
        lookTransform = new GameObject().transform;
        myState = State.lured;
        rb = GetComponent<Rigidbody>();
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
// Manual poop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoopDiamond();
        }
// State lured, but not dead (reset state)
        if (hungerTimer == 0f && myState == State.lured)
        {
            myState = State.hungry;
            luredTimer = 5f;
        }
// State hungry, but not lured        
        if (luredTimer == 0f && myState == State.hungry)
        {
            lurePoint = LurePoint();
            myState = State.lured;
            randSeed = Random.Range(1,4);
            hungerTimer = 5f;
        }
// State bored, but not eating        
        if (boredTimer == 0f && myState == State.bored)
        {
            myState = State.eating;
            // turn off run animation
            eatingTimer = 10f;
        }
// State wrangled, but not hungry        
        if (wrangledTimer == 0f && myState == State.wrangled)
        {
            myState = State.hungry;
            luredTimer = 10f;
        }
// State eating, but not bored        
        if (eatingTimer == 0f && myState == State.eating)
        {
            PoopDiamond();
            myState = State.bored;
            boredTimer = 7f;   
        }
// This block counts down all the timers        
        hungerTimer = Mathf.Max(0, hungerTimer - Time.deltaTime);
        luredTimer = Mathf.Max(0, luredTimer - Time.deltaTime);
        boredTimer = Mathf.Max(0, boredTimer - Time.deltaTime);
        wrangledTimer = Mathf.Max(0, wrangledTimer - Time.deltaTime);
        eatingTimer = Mathf.Max(0, eatingTimer - Time.deltaTime);
        //rb.MovePosition(transform.position + ((transform.forward * HamSpeed) + (transform.up * HamBounce)) * Time.deltaTime);

        if (transform.position.y < -100)
        {
            DestroyHamster();
        }

        switch (myState)
        {
            case State.lured:
                // run at lure
                rb.MovePosition(transform.position + ((transform.forward * HamSpeed) + (transform.up * HamBounce)) * Time.deltaTime);
//                rb.AddRelativeForce(Vector3.forward * HamSpeed, ForceMode.Force);
                //rb.AddRelativeForce(Vector3.forward * HamBounce, ForceMode.Force);
                break;
            case State.hungry:
                // look around for lure
                lookTransform.position = Vector3.SmoothDamp(lookTransform.position, lurePoint, ref smoothDampVelocityREF, turnSpeed);
                transform.LookAt(lookTransform);
                break;
            case State.wrangled:
                // stop moving
                break;
            case State.bored:
                // look for grass
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
    
    public void Wrangled()
    {
        myState = State.wrangled;
        // Will wait ten seconds and then remove parent and escape?
        wrangledTimer = 10f;
    }
}
