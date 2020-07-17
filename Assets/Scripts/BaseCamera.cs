using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.Cameras
{
    public class BaseCamera : MonoBehaviour
    {
        public Transform target;

        void Start()
        {
            HandleCamera();
        }

        void Update()
        {
            HandleCamera();
        }

        public virtual void HandleCamera()
        {
            if (!target)
            {
                return;
            }
        }

    }
}