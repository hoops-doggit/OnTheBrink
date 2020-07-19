using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookControll : MonoBehaviour
{
    [Header("Crook Attributes")]
    public bool wrangling;
    public float unWranglingCooldown;


    [Header("Crook poses")]
    public Transform notUsingPose;
    public Transform readyPose;
    public Transform wrangledPose;
    public GameObject currentlyWrangledHamp;

    [Header("Other boring stuff")]
    public GameObject crook;
    private Vector3 crookOffset;
    private Rigidbody rb;
    [SerializeField] GameObject crookedHingePosition;
    [SerializeField] private Collider crookTrigger;
    [SerializeField] Transform player;

    private Quaternion hampOrigin;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        notUsingPose.gameObject.SetActive(false);
        readyPose.gameObject.SetActive(false);
        wrangledPose.gameObject.SetActive(false);
        wrangling = false;
        SetCrookIdlePose();
        crookTrigger.enabled = false;
        //crookOffset = crook.transform.localPosition;
    }

    private void SetCrookIdlePose()
    {
        crookTrigger.enabled = false;
        crook.transform.localPosition = notUsingPose.localPosition + crookOffset;
        crook.transform.localRotation = notUsingPose.localRotation;
    }

    private void SetCrookSlash()
    {
        crookTrigger.enabled = true;
        crook.transform.localPosition = readyPose.localPosition + crookOffset;
        crook.transform.localRotation = readyPose.localRotation;
        
    }

    private void SetCrookWrangled()
    {
        crook.transform.localPosition = wrangledPose.localPosition + crookOffset;
        crook.transform.localRotation = wrangledPose.localRotation;
    }


    private void OnTriggerEnter(Collider other)
    {


        if(other.tag == "hampster" && !wrangling)
        {

            other.GetComponent<HamsterAI>().Wrangled();
            CrookHampster(other.gameObject);


        }
    }

    public void CrookHampster(GameObject _hamp)
    {
        if (_hamp != null)
        {
            hampOrigin = _hamp.transform.rotation;
            currentlyWrangledHamp = _hamp;
            currentlyWrangledHamp.layer = 10;
            currentlyWrangledHamp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //currentlyWrangledHamp.transform.position = crookedHingePosition.transform.localPosition;
            currentlyWrangledHamp.AddComponent<HingeJoint>();
            HingeJoint hj = currentlyWrangledHamp.GetComponent<HingeJoint>();
            hj.connectedBody = rb;
            hj.anchor = Vector3.zero;

            //crookedHampster physics layer

            wrangling = true;
        }
        else
        {
            currentlyWrangledHamp = null;
        }
    }

    public void UnCrookHampster()
    {
        HingeJoint hj = currentlyWrangledHamp.GetComponent<HingeJoint>();
        if (hj != null)
        {
            Destroy(hj);
            currentlyWrangledHamp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            currentlyWrangledHamp.layer = 9; //hampster layer
            currentlyWrangledHamp.transform.position = crookedHingePosition.transform.position;
            currentlyWrangledHamp.transform.up = player.forward;
            currentlyWrangledHamp.transform.rotation = hampOrigin;
            currentlyWrangledHamp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentlyWrangledHamp = null;
            StartCoroutine(UnWranglingCooldown());
        }
        else
        {
            currentlyWrangledHamp = null;
        }
    }

    IEnumerator UnWranglingCooldown()
    {
        yield return new WaitForSeconds(unWranglingCooldown);
        wrangling = false;
    }


    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if(wrangling == false)
            {
                SetCrookSlash();
            }
            else if (wrangling == true)
            {
                SetCrookWrangled();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (wrangling)
            {
                UnCrookHampster();
            }
            else
            {
                SetCrookIdlePose();
            }

        }

        else
        {
            SetCrookIdlePose();
        }
    }
}
