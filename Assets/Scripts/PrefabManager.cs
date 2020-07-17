using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;

    [Header("prefabs")]
    public GameObject[] bulletCase;

    void Awake()
    {
        instance = this;
    }

    public void SpawnPrefab(GameObject _o, Vector3 _p, Quaternion _r, float _scl)
    {
        if (_o != null)
        {
            GameObject o = Instantiate(_o, _p, _r) as GameObject;
            Transform tr = o.transform;
            tr.localScale = Vector3.one * _scl;
        }
    }

    public GameObject SpawnPrefabAsGameObject(GameObject _o, Vector3 _p, Quaternion _r, float _scl)
    {
        GameObject ret = null;

        if (_o != null)
        {
            GameObject o = Instantiate(_o, _p, _r) as GameObject;
            Transform tr = o.transform;
            tr.localScale = Vector3.one * _scl;

            ret = o;
        }

        return ret;
    }
}
