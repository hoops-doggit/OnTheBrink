using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureManager : MonoBehaviour
{
    public static LureManager instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Transform> currentlyActiveLures;

    public void AddSelfToLureList(Transform _t)
    {
        currentlyActiveLures.Add(_t);
    }

    public void RemoveFromLureList(Transform _t)
    {
        currentlyActiveLures.Remove(_t);
    }
}
