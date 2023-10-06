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

        private void MoveCharacter()
        {
            Transform myTransform = mainCharacterTransform;

            float newX = myTransform.position.x + 0.8f;
            Vector3 moveRightPosition = new Vector3(newX, myTransform.position.y, myTransform.position.z);
            myTransform.position = moveRightPosition;

            float newY = myTransform.position.y + 0.07f;
            Vector3 moveUpPosition = new Vector3(myTransform.position.x, newY, myTransform.position.z);
            myTransform.position = moveUpPosition;

            Vector3 currentRotation = myTransform.eulerAngles;
            currentRotation.y += 90;
            myTransform.eulerAngles = currentRotation;
        }

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
                    MoveCharacter();
                    characterMoved = true;
                }
                ShowDice();
            }
        }
    }
}