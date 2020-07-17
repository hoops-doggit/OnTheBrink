using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MyTools.Cameras
{
    public class CameraFollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [Tooltip("The point in the world from ScreenPointToWorld()")]
        [SerializeField] private Transform cursorWorldRayPoint;
        [SerializeField] private float lerpPercentage = 0.3f;

        [ExecuteInEditMode]
        public void NaturalSlerp()
        {
            if (player == null)
            {
                return;
            }
            if (cursorWorldRayPoint == null)
            {
                return;
            }

            Vector3 position = Vector3.Lerp(player.position, cursorWorldRayPoint.position, lerpPercentage);
            position.y = 0;
            transform.position = position;
        }

        void Update()
        {
            Vector3 position = Vector3.Lerp(player.position, cursorWorldRayPoint.position, lerpPercentage);
            position.y = 0;
            transform.position = position;
        }
    }
}