using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MyTools.Cameras
{
    [CustomEditor(typeof(TopDownCamera))]
    public class TopDownCameraEditor : Editor
    {
        private TopDownCamera targetCam;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        void OnEnable()
        {
            targetCam = (TopDownCamera)target;
        }

        private void OnSceneGUI()
        {
            if (!targetCam)
            {
                return;
            }

            Transform camTarget = targetCam.target;

            Handles.DrawWireDisc(camTarget.position, Vector3.up, targetCam.distance);

            Handles.color = new Color(0, 1, 0, 0.15f);
            Handles.DrawSolidDisc(camTarget.position, Vector3.up, targetCam.distance);

            Handles.color = new Color(0, 1, 1, 0.3f);
            targetCam.distance = Handles.ScaleSlider(targetCam.distance, camTarget.position, -Vector3.forward, Quaternion.identity, targetCam.distance, 1f);
            targetCam.distance = Mathf.Clamp(targetCam.distance, 10f, float.MaxValue);

            Handles.color = new Color(0, 0, 1, 0.3f);
            targetCam.height = Handles.ScaleSlider(targetCam.height, camTarget.position, camTarget.up, Quaternion.identity, targetCam.height, 1f);
            targetCam.height = Mathf.Clamp(targetCam.height, 10f, float.MaxValue);


            GUIStyle labelStyle = new GUIStyle();
            labelStyle.fontSize = 15;
            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.UpperCenter;

            Handles.Label(camTarget.position + (-Vector3.forward * targetCam.distance), "Distance", labelStyle);


            labelStyle.alignment = TextAnchor.MiddleRight;
            Handles.Label(camTarget.position + (camTarget.up * targetCam.height), "Height", labelStyle);

            targetCam.HandleCamera();

        }

    }
}
