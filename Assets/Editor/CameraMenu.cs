using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace MyTools.Cameras
{
    public class CameraMenu : MonoBehaviour
    {
        [MenuItem("MyTools/Cameras/Top Down Camera")]
        public static void CreateTopDownCamera()
        {
            GameObject[] selectedGO = Selection.gameObjects;

            if (selectedGO.Length > 0 && selectedGO[0].GetComponent<Camera>() && selectedGO.Length < 3)
            {

                if (selectedGO.Length < 2)
                {
                    AttachTopDownScript(selectedGO[0], null);
                }
                else if(selectedGO.Length == 2)
                {
                    AttachTopDownScript(selectedGO[0], selectedGO[1].transform);
                }
                else if(selectedGO.Length >= 3)
                {
                    EditorUtility.DisplayDialog("Camera Tools", "You can only select two GameObjects and the first selection needs to be a camera", "ok");
                }
            }

            else
            {
                EditorUtility.DisplayDialog("Camera Tools", "You need to select a GameObject in the scene that has a Camera component assigned to it", "ok");
            }

        }

        static void AttachTopDownScript(GameObject _camera, Transform _target)
        {
            //Assign top down camera script to camera
            TopDownCamera cameraScript = null;
            if (_camera)
            {
                cameraScript = _camera.AddComponent<TopDownCamera>();

                if (_camera && _target)
                {
                    cameraScript.target = _target;
                }

                Selection.activeGameObject = _camera;
            }


        }
    }
}