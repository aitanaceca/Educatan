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
using System.Threading;

namespace Scripts.Animation
{
    public class MainAnimation : DefaultObserverEventHandler
    {
        public Animator animator;
        public Animator characterAnimator;

        public GameObject dice;
        public GameObject mainCharacter;
        public Transform mainCharacterTransform;

        private bool characterMoved = false;

        private void ShowCharacter()
        {
            mainCharacter.SetActive(true);
        }

        private void ShowDice()
        {
            dice.SetActive(true);
        }

        protected override void OnTrackingFound()
        {
            animator.Play("board");
        }

        void Update()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animator.GetCurrentAnimatorStateInfo(0).length >= 9.5)
            {
                if (!characterMoved)
                {
                    ShowCharacter();
                    characterMoved = true;
                }
                ShowDice();
            }
        }
    }
}