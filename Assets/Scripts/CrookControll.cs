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



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        notUsingPose.gameObject.SetActive(false);
        readyPose.gameObject.SetActive(false);
        wrangledPose.gameObject.SetActive(false);
        wrangling = false;
        SetCrookIdlePose();
        //crookOffset = crook.transform.localPosition;
    }

    private void SetCrookIdlePose()
    {
        crook.transform.localPosition = notUsingPose.localPosition + crookOffset;
        crook.transform.localRotation = notUsingPose.localRotation;
    }

    private void SetCrookSlash()
    {
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
        Debug.Log(other.gameObject.name);


        if(other.tag == "hampster" && !wrangling)
        {
            if (!wrangling)
            {
                CrookHampster(other.gameObject);
            }

        }
    }

    public void CrookHampster(GameObject _hamp)
    {
        currentlyWrangledHamp = _hamp;
        //currentlyWrangledHamp.transform.position = crookedHingePosition.transform.localPosition;
        currentlyWrangledHamp.AddComponent<HingeJoint>();
        HingeJoint hj = currentlyWrangledHamp.GetComponent<HingeJoint>();
        hj.connectedBody = rb;
        hj.anchor = Vector3.zero;

        currentlyWrangledHamp.layer = 10; //crookedHampster physics layer

        wrangling = true;
    }

    public void UnCrookHampster()
    {
        HingeJoint hj = currentlyWrangledHamp.GetComponent<HingeJoint>();
        Destroy(hj);
        currentlyWrangledHamp.layer = 9; //hampster layer
        currentlyWrangledHamp = null;
        StartCoroutine(UnWranglingCooldown());
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
