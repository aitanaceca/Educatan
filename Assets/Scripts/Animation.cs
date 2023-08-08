using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using System;
using Scripts.LevelElements;
using Scripts.Levels;
using System.Reflection;
using Vuforia;

public class Animation : DefaultObserverEventHandler
{
    public Animator animator;

    public GameObject dice;

    protected override void OnTrackingFound()
    {
        animator.Play("board");
    }

    void Update()
    {
       if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animator.GetCurrentAnimatorStateInfo(0).length >= 9.5)
       {
            dice.SetActive(true);
       }
    }
}