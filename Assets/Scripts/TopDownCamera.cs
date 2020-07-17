using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MyTools.Cameras
{
    public class TopDownCamera : BaseCamera
    {

        [Header("Camera position")]
        [SerializeField] public float height = 10f;
        public float distance = 20f;
        public float angle = 45f;
        public float smoothSpeed = 0.5f;
        private Vector3 refVelocity;

        [Header("Camera shake")]
        public bool camShakeActive = true;
        [Range(0, 1)] [SerializeField] float trauma;
        [SerializeField] float traumaMult = 5f;
        [SerializeField] float traumaMag = 0.8f;
        [SerializeField] float traumaRotMag = 17f;
        [SerializeField] float traumaDepthMag = 1.3f;
        [SerializeField] float traumaDecay = 1.3f;

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

        void Start()
        {
            HandleCamera();
        }

        void LateUpdate()
        {
            HandleCamera();
        }

        [ExecuteInEditMode]
        public override void HandleCamera() 
        {
            base.HandleCamera();

            //tracking
            Vector3 worldPos = (Vector3.forward * -distance) + (Vector3.up * height);
            Debug.DrawLine(target.position, worldPos, Color.red);

            Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPos;
            Debug.DrawLine(target.position, rotatedVector, Color.green);

            Vector3 flatTargetPos = target.position;
            flatTargetPos.y = 0;

            Vector3 finalPosition = flatTargetPos + rotatedVector;
            Debug.DrawLine(target.position, rotatedVector, Color.blue);

            Vector3 newPos = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed, Mathf.Infinity, Time.deltaTime);
            transform.position = newPos;
            transform.LookAt(target);
            Quaternion targetRot = transform.rotation;

            //shake
            if (camShakeActive && Trauma > 0)
            {
                timeCounter += Time.deltaTime * Mathf.Pow(trauma, 0.3f) * traumaMult;
                Vector3 shakeNewPos = GetVec3() * traumaMag * Trauma;
                transform.localPosition = newPos + shakeNewPos;
                transform.localRotation = Quaternion.Euler(targetRot.eulerAngles + shakeNewPos * traumaRotMag);
                Trauma -= Time.deltaTime * traumaDecay * Trauma;
            }
            else
            {
                Vector3 returnPos = Vector3.Lerp(transform.localPosition, newPos, Time.deltaTime);
                transform.localPosition = returnPos;
                transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.25f);
            if (target)
            {
                Gizmos.DrawLine(transform.position, target.position);
                Gizmos.DrawSphere(target.position, 1.5f);
            }
            Gizmos.DrawSphere(transform.position, 1.5f);
        }      

        float GetFloat(float seed)
        {
            return (Mathf.PerlinNoise(seed, timeCounter) - 0.5f) * 2;
        }

        Vector3 GetVec3()
        {
            return new Vector3(GetFloat(1), GetFloat(10), GetFloat(2));
        }
    }
}
