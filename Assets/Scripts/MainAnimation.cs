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
using System.Linq;

namespace Scripts.Animation
{
    public class MainAnimation : DefaultObserverEventHandler
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private GameObject _dice;
        [SerializeField] private GameObject _mainCharacter;
        [SerializeField] private GameObject _bagCanvas;
        [SerializeField] private GameObject _board;
        [SerializeField] private Transform _mainCharacterTransform;

        public Animator Animator
        {
            get { return _animator; }
            set { _animator = value; }
        }

        public Animator CharacterAnimator
        {
            get { return _characterAnimator; }
            set { _characterAnimator = value; }
        }

        public GameObject Dice
        {
            get { return _dice; }
            set { _dice = value; }
        }

        public GameObject MainCharacter
        {
            get { return _mainCharacter; }
            set { _mainCharacter = value; }
        }

        public GameObject BagCanvas
        {
            get { return _bagCanvas; }
            set { _bagCanvas = value; }
        }

        public GameObject Board
        {
            get { return _board; }
            set { _board = value; }
        }

        public Transform MainCharacterTransform
        {
            get { return _mainCharacterTransform; }
            set { _mainCharacterTransform = value; }
        }

        private bool characterMoved = false;

        private void ShowBagCanvas()
        {
            _bagCanvas.SetActive(true);
        }

        private void ShowCharacter()
        {
            _mainCharacter.SetActive(true);
        }

        private void ShowDice()
        {
            _dice.SetActive(true);
        }

        private bool IsBridgeTransform(Transform bridge)
        {
            return bridge.name.Contains("Puente");
        }

        private void ShowBridges()
        {
            Transform[] boardTransforms = _board.GetComponentsInChildren<Transform>();

            foreach (Transform elemTransform in boardTransforms.Select(elem => elem.transform))
            {
                foreach (Transform bridge in elemTransform)
                {
                    if (IsBridgeTransform(bridge))
                    {
                        bridge.gameObject.SetActive(true);
                    }
                }
            }
        }


        protected override void OnTrackingFound()
        {
            _animator.Play("board");
        }

        void Update()
        {
            _animator = Animator;
            _characterAnimator = CharacterAnimator;
            _dice = Dice;
            _mainCharacter = MainCharacter;
            _bagCanvas = BagCanvas;
            _board = Board;
            _mainCharacterTransform = MainCharacterTransform;

            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime * _animator.GetCurrentAnimatorStateInfo(0).length >= 9.5)
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