using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpStates { first, second, third, fourth, falling, Else, shortJump}

public class Jump : MonoBehaviour
{
    public JumpStates jumpState;

    public float normalGravity;
    public float jumpStrength;
    public float fallMultiplier = 2.5f;
    public float groundedSkin = 0.05f;
    public float heightOffset = 0.8f;
    public float restingHeight = 1.53f;
    public float snapHeight = 0.01f;
    public LayerMask mask;
    public float jumpV;
    


    Rigidbody rb;
    public bool jumpRequest;
    public bool grounded;
    private bool rayHit;
    private Vector2 playerSize, boxSize;
    Vector3 test;
    Collider body;
    [SerializeField] Transform boxCastCentre;
    [SerializeField] Transform[] heightRays = new Transform[3];
    RaycastHit[] heightHits = new RaycastHit[3];
    public Transform gasParticles;





    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<Collider>();

        //boxCastCentre.position = body.center;
        boxSize = new Vector2(playerSize.x, groundedSkin);
        //playerSize = body.bounds;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jumpRequest = true;
            grounded = false;
        }
    }

    IEnumerator LateFixedUpdate()
    {
        yield return new WaitForFixedUpdate();
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, heightHits[1].point.y + heightOffset, transform.position.y +1), transform.position.z);

        if(transform.position.y == heightHits[1].point.y + heightOffset)
        {
            grounded = true;
        }
        else { grounded = false; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(LateFixedUpdate());

        #region gravity
        if (!grounded)
        {
            
            if (rb.velocity.y < 0) //if falling
            {
                if (rb.velocity.y < -2f)
                {
                    rb.AddForce(Vector3.down * normalGravity * 7f);
                }
                else
                {
                    rb.AddForce(Vector3.down * normalGravity *1.5f);
                }

                jumpState = JumpStates.falling;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) //if rising and you let go spacebar
            {
                if(rb.velocity.y > 2)
                {
                    rb.AddForce(Vector3.down * normalGravity * fallMultiplier);
                }
                else
                {
                    rb.AddForce(Vector3.down * normalGravity * fallMultiplier * 0.2f);
                }
                jumpState = JumpStates.shortJump;
            }
            else if (rb.velocity.y > 0 && rb.velocity.y < 0.5f) //if rising and before you reach yspeed of 2
            {
                rb.AddForce(Vector3.down * normalGravity * 0.05f);
                jumpState = JumpStates.second;
            }
            else if (rb.velocity.y > 0 && rb.velocity.y < 1) //if rising and before you reach yspeed of 2
            {
                rb.AddForce(Vector3.down * normalGravity * 0.13f);
                jumpState = JumpStates.third;
            }
            else if (rb.velocity.y > 0 && rb.velocity.y < 2) //if rising and before you reach yspeed of 2
            {
                rb.AddForce(Vector3.down * normalGravity * 0.2f);
                jumpState = JumpStates.second;
            }
            else if(rb.velocity.y > 2) //if rising and after you reach 2
            {
                rb.AddForce(Vector3.down * normalGravity * 0.7f);
                jumpState = JumpStates.first;
            }
            else //every other time
            {
                rb.AddForce(Vector3.down * normalGravity);
                jumpState = JumpStates.Else;
            }

                  
        }
        #endregion

        if (grounded)
        {
            //gasParticles.localRotation = Quaternion.Euler(0, 180, 0);
            jumpState = JumpStates.Else;

        }


        #region y clamp to stop ground penetration
        if (!grounded && rb.velocity.y < 0)
        {
            if (Physics.Raycast(heightRays[0].position, heightRays[0].up * -1, out heightHits[0], restingHeight * 3f, mask))
            {
                Debug.DrawRay(heightRays[0].position, heightRays[0].up * -1 * heightHits[0].distance, Color.yellow);
                rayHit = true;

            }
            else
            {
                Debug.DrawRay(heightRays[0].position, heightRays[0].up * -1 * restingHeight * 3f, Color.red);
                rayHit = false;
            }
        }
        #endregion

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, heightHits[1].point.y + heightOffset, transform.position.y + 1), transform.position.z);

        if (transform.position.y <= heightHits[1].point.y + heightOffset + snapHeight)
        {
            if (jumpState == JumpStates.falling)
            {
                Vector3 position = transform.position;
                grounded = true;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                position.y = heightHits[1].point.y;
                transform.position = position;
                transform.up = heightHits[1].normal;
            }
        }
        else { grounded = false; }


        #region jump request
        if (jumpRequest)
        {
            jumpRequest = false;
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            grounded = false;
            
        }
        else
        {
            //Vector3 boxCenter = transform.position + new Vector3(0,body.size.y/2, 0);
            //Collider[] co = new Collider[2];
            //co = Physics.OverlapBox(boxCenter, body.size * 0.5f, transform.rotation, mask);
            //if (co.Length > 0)
            //{
            //    grounded = true;
            //}
            //else
            //{
            //    grounded = false;
            //}
        }
        #endregion


    }
}
