using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool camShakeActive = true;
    [Range(0, 1)] [SerializeField] float trauma;
    [SerializeField] float traumaMult = 5f;
    [SerializeField] float traumaMag = 0.8f;

    float timeCounter;

    public float Trauma
    {
        get
        { 
            return trauma;
        }

        set
        {
            trauma = Mathf.Clamp01(value);
        }
        
    }

    float GetFloat(float seed)
    {
        return (Mathf.PerlinNoise(seed, timeCounter) -0.5f) * 2;
    }

    Vector3 GetVec3()
    {
        return new Vector3(GetFloat(1), GetFloat(10), 0);
    }


    private void Update()
    {
        if (camShakeActive)
        {
            timeCounter += Time.deltaTime * trauma * traumaMult;
            Vector3 newPos = GetVec3() * traumaMag;
            transform.position = newPos;
        }
    }
}
