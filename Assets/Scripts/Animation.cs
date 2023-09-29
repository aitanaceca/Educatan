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

namespace Scripts.Animation
{
    public class Animation : DefaultObserverEventHandler
    {
        public Animator animator;
        public Animator characterAnimator;

        public GameObject dice;
        public GameObject mainCharacter;
        public Transform mainCharacterTransform;

        private bool characterMoved = false;

        private void MoveCharacter()
        {
            mainCharacter.SetActive(true);
            characterAnimator.SetTrigger("Walk");
            Transform myTransform = mainCharacterTransform;
            print(myTransform.position.x);
            print(myTransform.position.y);
            print(myTransform.position.z);
            while (myTransform.position.x < (myTransform.position.x + 0.6f))
            {
                float newX = myTransform.position.x + 0.02f;
                Vector3 moveRightPosition = new Vector3(newX, myTransform.position.y, myTransform.position.z);
                myTransform.position = moveRightPosition;
            }
            float newY = myTransform.position.y + 0.05f;
            Vector3 moveUpPosition = new Vector3(myTransform.position.x, newY, myTransform.position.z);
            myTransform.position = moveUpPosition;
            print(myTransform.position.x);
            print(myTransform.position.y);
            print(myTransform.position.z);
            characterAnimator.SetTrigger("StopWalk");
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
                    MoveCharacter();
                    characterMoved = true;
                }
                ShowDice();
            }
        }
    }
}