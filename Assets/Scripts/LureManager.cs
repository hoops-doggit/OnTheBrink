using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureManager : MonoBehaviour
{
    public static LureManager instance;

    public List<Transform> currentlyActiveLures;

    public int numberOfActiveLures;

    private void Awake()
    {
        instance = this;
    }



    public void AddSelfToLureList(Transform _t)
    {
        currentlyActiveLures.Add(_t);
    }

    public void RemoveFromLureList(Transform _t)
    {
        currentlyActiveLures.Remove(_t);
    }

    private void Update()
    {
        numberOfActiveLures = currentlyActiveLures.Count;
    }
}
