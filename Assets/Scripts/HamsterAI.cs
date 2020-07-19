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
    [SerializeField]private State myState;
    public Vector3 lurePoint;
    private Vector3 smoothDampVelocityREF;
    private Transform lookTransform;
    public float turnSpeed;    
    public float timeStamp;
    public Animator anim;
    
    public float hungerTimer = 0f; 
    public float luredTimer = 0f; 
    public float wrangledTimer = 10f;
    public float boredTimer = 8f;
    public float eatingTimer = 4f;

    public GameObject lookTargetSphere;
    public GameObject lureTargetSphere;
    
    void Start()
    {
        lookTransform = new GameObject().transform;
        lookTransform.SetParent(null);
        myState = State.lured;
        rb = GetComponent<Rigidbody>();
        lurePoint = LurePoint();
        luredTimer = Random.Range(5f,10f);
        //anim = GetComponent<Animator> ();
    }

    public Vector3 RandomPoint()
    {
        Vector3 lureP = new Vector3
        {
            x = Random.Range(-17, 17),
            y = 0,
            z = Random.Range(-17, 17)
        };
        return lureP;
    }

    public Vector3 LurePoint()
    {
        Vector3 lureP = Vector3.one;
        if(LureManager.instance.numberOfActiveLures > 0)
        {
            var lures = LureManager.instance.currentlyActiveLures;

            lureP = LureManager.instance.currentlyActiveLures[Random.Range(0, lures.Count)].transform.position;
            lureP.y = 0;
        }
        Debug.Log(lureP);

        lureTargetSphere.transform.position = lureP;

        return lureP;
    }

    void Update()
    {


        if(myState == State.lured && (lurePoint - transform.position).magnitude < 1)
        {
            myState = State.hungry;
            luredTimer = 5f;
            Debug.Log("I gone and done");
        }

// Manual poop or chill
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PoopDiamond();
            //ChillPosition();
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
            PoopDiamond();
            anim.enabled = true;
            lurePoint = LurePoint();
            myState = State.lured;
            randSeed = Random.Range(1,4);
            hungerTimer = 25f;
        }
// State bored, but not eating        
        if (boredTimer == 0f && myState == State.bored)
        {
            myState = State.lured;
            anim.enabled = false;
            luredTimer = 10f;
        }
// State wrangled, but not hungry        
        if (wrangledTimer == 0f && myState == State.wrangled)
        {
            myState = State.eating;
            ChillPosition();
            BurstOfSpeed();
            eatingTimer = 5f;
        }
// State eating, but not bored        
        if (eatingTimer == 0f && myState == State.eating)
        {
            
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
                lookTransform.position = Vector3.SmoothDamp(lookTransform.position, lurePoint, ref smoothDampVelocityREF, turnSpeed);
                transform.LookAt(lookTransform);
                lookTargetSphere.transform.position = lookTransform.position;

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
                NormalSpeed();
                break;
            case State.bored:
                // stop moving
                break;
            default:
                break;
        }
    }
    
    void PoopDiamond()
    {
        Instantiate(diamante, new Vector3(transform.position.x+1.25f,transform.position.y+1,transform.position.z), Quaternion.identity);
    }
    
    void DestroyHamster()
    {
        Destroy(gameObject);
    }
    
    public void Wrangled()
    {
        myState = State.wrangled;
        // Will wait five seconds and then remove parent and escape?
        wrangledTimer = 5f;
    }

    public void Eating()
    {
        myState = State.eating;
        eatingTimer = 5f;

    }

    private void BurstOfSpeed()
    {

            this.HamSpeed *= 2f;

    }
    
    private void NormalSpeed()
    {
        this.HamSpeed = 3f;
    }
    
    public void ChillPosition()
    {
        rb.velocity = new Vector3(0f,0f,0f); 
        rb.angularVelocity = new Vector3(0f,0f,0f);
        transform.rotation = Quaternion.Euler(new Vector3(0f,Random.Range(0f,360f),0f));
         
    }
}
