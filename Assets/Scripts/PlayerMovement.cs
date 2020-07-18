using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum RelationshipToGround { grounded, coyote, rising, falling }
public enum SpeedState { accelerating, decelerating, idle, knockback }
public enum InputState { allowed, notAllowed }


//use a scriptable object for states. That way I don't need to getComponent every frame

public class PlayerMovement : MonoBehaviour
{
    public SpeedState speedState;
    public RelationshipToGround groundState;
    public InputState inputState;
    private Rigidbody rb;
    public float acceleration = 77, minVelocity = 5, maxVelocity = 77, groundFriction = 0.01f, decellerationSpeed = 20, decellerationCutoff = 5;
    KeyCode down, up, left, right, jump;
    private Vector3 movementInput;
    private Vector3 facingDirection;
    private Vector3 rbVelocity;
    private RaycastHit mousePointRay;
    public GameObject lookTarget;
    public float currentSpeed;
    //[SerializeField] Player_Exhaust gas;
    public bool strafing;

    // Jump jumpScript;
    //private GunControl _gc;
    private Camera gameCam;

    [Header("Knockback variables")]
    public float knockBackForce =10;
    public float knockBackTimer = 0.3f;
    public float knockBackGravity = 3.5f;
    public float knockback;
    public Vector3 knockBackObject;

    public bool canMove;


    void Start()
    {
        //lookTarget = new GameObject();
        //_gc = GetComponent<GunControl>();
        //gas = GetComponent<Player_Exhaust>();
        rb = GetComponent<Rigidbody>();
        //jumpScript = GetComponent<Jump>();
        down = KeyCode.S;
        up = KeyCode.W;
        left = KeyCode.A;
        right = KeyCode.D;
        jump = KeyCode.Space;
        knockback = knockBackForce;
        canMove = true;
    }

    public void TurnOffMove()
    {
        canMove = false;
    }

    public void TurnOnMove()
    {
        canMove = true;
    }




    private void InputCalculation()
    {
        var vert = 0;
        var horiz = 0;

        if (Input.GetKey(up))
        {
            vert += 1;
        }
        if (Input.GetKey(down))
        {
            vert -= 1;
        }
        if (Input.GetKey(left))
        {
            horiz -= 1;
        }
        if (Input.GetKey(right))
        {
            horiz += 1;
        }

        movementInput = new Vector3(horiz, 0, vert).normalized;

        if (movementInput.magnitude > 0)
        {             
            //gas.GasParticlesStart();
            facingDirection = movementInput.normalized;
        }

        if (movementInput.magnitude == 0)
        {

        }


        if (facingDirection != Vector3.zero)
        {
            transform.localRotation = Quaternion.LookRotation(facingDirection, Vector3.up);
        }
    } 

    void Movement(Vector3 inputDirection)
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        float oppositionPercent = Vector3.Angle(velocity.normalized, inputDirection.normalized) / 180f;

        Vector3 baseAcceleration = inputDirection * acceleration;

        Vector3 oppositionAcceleration = baseAcceleration * 0.5f * oppositionPercent;

        Vector3 groundedAcceleration = velocity + baseAcceleration + oppositionAcceleration;//-(velocity/8)

        Vector3 airAcceleration = velocity + baseAcceleration + oppositionAcceleration;

        if (speedState != SpeedState.knockback)
        {
            if (inputDirection.magnitude > 0)
            {
                speedState = SpeedState.accelerating;
            }
            else if (inputDirection.magnitude == 0 && rb.velocity.magnitude > decellerationCutoff)
            {
                speedState = SpeedState.decelerating;
            }
            else
            {
                speedState = SpeedState.idle;
            }

            if (speedState == SpeedState.accelerating)
            {
                if (velocity.magnitude < maxVelocity)
                {
                    Vector3 vel = new Vector3(Mathf.Clamp(groundedAcceleration.x, -maxVelocity, maxVelocity), rb.velocity.y, Mathf.Clamp(groundedAcceleration.z, -maxVelocity, maxVelocity));

                    if (vel.magnitude > maxVelocity)
                    {
                        vel = vel.normalized * maxVelocity;
                    }
                    currentSpeed = vel.magnitude;
                    rb.velocity = vel;

                    //if (jumpScript.grounded)
                    //{


                    //}
                    //else
                    //{
                    //    Vector3 vel = new Vector3(Mathf.Clamp(airAcceleration.x, -maxVelocity, maxVelocity), rb.velocity.y, Mathf.Clamp(airAcceleration.z, -maxVelocity, maxVelocity));
                    //    if (vel.magnitude > maxVelocity)
                    //    {
                    //        vel = vel.normalized * maxVelocity ;
                    //    }
                    //    rb.velocity = vel ;
                    //}
                }
            }
            else if (speedState == SpeedState.decelerating)
            {
                velocity = velocity * decellerationSpeed ;
                velocity.y = rb.velocity.y;
                rb.velocity = velocity;
            }
            else if (speedState == SpeedState.idle)
            {
                velocity = Vector3.zero;
                velocity.y = rb.velocity.y;
                rb.velocity = velocity;
            }
        }

        else if (speedState == SpeedState.knockback)
        {
            Vector3 knockBackVector = knockBackObject.normalized * knockback;
            if(knockback > 0)
            {
                rb.velocity = knockBackVector;
            }            
            knockback = knockback - knockBackGravity * Time.deltaTime;
           
            if (knockback < 0)
            {
                rb.velocity = Vector3.zero;
                speedState = SpeedState.idle;
            }
        }
    }
    
    public void KnockBack(Vector3 ob)
    {
        if(speedState != SpeedState.knockback)
        {
            knockBackObject = ob;
            knockback = knockBackForce;
            speedState = SpeedState.knockback;
            Debug.Log("got knocked");
        }
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (strafing)
            {
                strafing = false;
            }
            else
            {
                strafing = true;
            }
        }


    //}

    //private void FixedUpdate()
    //{
        if (canMove)
        {
            InputCalculation();
            //lookTarget.transform.position;

            
        }        

        rbVelocity = rb.velocity;
        currentSpeed = rb.velocity.magnitude;

        if (canMove)
        {            
            Movement(movementInput);

            lookTarget.transform.SetPositionAndRotation(MouseInput.instance.mousePointTransform.position, MouseInput.instance.mousePointTransform.rotation);
            transform.LookAt(lookTarget.transform);
        }
        
        switch (groundState)
        {
            case RelationshipToGround.grounded:
                if (canMove)
                {
                    Vector3 frictionVector = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    rb.AddForce(-((rb.velocity + frictionVector) * groundFriction), ForceMode.VelocityChange);
                }
                break;
        }
    }

    
}
