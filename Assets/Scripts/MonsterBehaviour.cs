using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    public Transform lure;

    public void AddSelfToLureList()
    {
        LureManager.instance.AddSelfToLureList(lure);
    }

    public void RemoveSelfFromLureList()
    {
        LureManager.instance.RemoveFromLureList(lure);
    }
}
