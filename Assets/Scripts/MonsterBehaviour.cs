using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MonsterBehaviour : MonoBehaviour
{
    public Transform lure;
    private Animator an;

    private void Start()
    {
        an = GetComponent<Animator>();
    }

    public void AddSelfToLureList()
    {
        LureManager.instance.AddSelfToLureList(lure);
        TurnOnLure();
    }

    public void RemoveSelfFromLureList()
    {
        LureManager.instance.RemoveFromLureList(lure);
    }

    public void StopAnimation()
    {
        Debug.Log("animstop");
        an.enabled = false;
        an.speed = 0;

    }

    private void TurnOnLure()
    {

    }

}
