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

    protected override void OnTrackingFound()
    {
        print("HOLA");
        animator.Play("board");
    }
}