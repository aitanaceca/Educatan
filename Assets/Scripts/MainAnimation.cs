using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using System;
using Scripts.Levels;
using System.Reflection;
using Vuforia;
using System.Threading;
using TMPro;

namespace Scripts.Animation
{
    public class MainAnimation : DefaultObserverEventHandler
    {
        public Animator animator;
        public Animator characterAnimator;

        public GameObject dice;
        public GameObject mainCharacter;
        public GameObject bagCanvas;
        
        public GameObject board;

        public Transform mainCharacterTransform;

        private bool characterMoved = false;

        private void ShowBagCanvas()
        {
            bagCanvas.SetActive(true);
        }

        private void ShowCharacter()
        {
            mainCharacter.SetActive(true);
        }

        private void ShowDice()
        {
            dice.SetActive(true);
        }

        private void ShowBridges()
        {
            Transform[] boardTransforms = board.GetComponentsInChildren<Transform>();

            foreach (var elem in boardTransforms)
            {
                Transform elemTransform = elem.transform;

                foreach (Transform bridge in elemTransform)
                {
                    if (bridge.name.Contains("Puente"))
                    {
                        bridge.gameObject.SetActive(true);
                    }                   
                }
            }
        }

        protected override void OnTrackingFound()
        {
            animator.Play("board");
        }

        void Update()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animator.GetCurrentAnimatorStateInfo(0).length >= 9.5)
            {
                ShowBagCanvas();
                ShowBridges();
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