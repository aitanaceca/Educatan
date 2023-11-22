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

        public Transform mainCharacterTransform;

        public TMP_Text fireCounter;
        public TMP_Text waterCounter;
        public TMP_Text grassCounter;
        public TMP_Text sandCounter;
        public TMP_Text woodCounter;
        public TMP_Text currentLevel;

        private bool characterMoved = false;

        private void SetInitialCounters()
        {
            LevelElements.LevelElements levelElements = new(int.Parse(currentLevel.text));
            fireCounter.text = $"0 / {(levelElements.FireMaterial).ToString()}";
            waterCounter.text = $"0 / {(levelElements.WaterMaterial).ToString()}";
            grassCounter.text = $"0 / {(levelElements.GrassMaterial).ToString()}";
            sandCounter.text = $"0 / {(levelElements.SandMaterial).ToString()}";
            woodCounter.text = $"0 / {(levelElements.WoodMaterial).ToString()}";
        }

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

        protected override void OnTrackingFound()
        {
            animator.Play("board");
        }

        void Update()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animator.GetCurrentAnimatorStateInfo(0).length >= 9.5)
            {
                SetInitialCounters();
                ShowBagCanvas();
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