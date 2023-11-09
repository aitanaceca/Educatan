using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Scripts.TouchDice
{
    public class TouchDice : DefaultObserverEventHandler
    {
        public GameObject diceNumber;
        public GameObject character;
        public GameObject dice;

        public Animator diceAnimator;
        public Animator boardAnimator;
        public Animator characterAnimator;

        public Transform mainCharacterTransform;

        private TMP_Text diceNumberText;

        private List<string> diceFaces = new() { "Cara1", "Cara2", "Cara3", "Cara4", "Cara5", "Cara6" };
        private List<string> diceAnimations = new() { "dice1", "dice2", "dice3", "dice4", "dice5", "dice6" };

        private int finalRandomNumber;

        private int currentPosition = 0;
        private List<string> roadElements = new()
        {
            "Elemento19",
            "Elemento18",
            "Elemento17",
            "Elemento13",
            "Elemento14",
            "Elemento15",
            "Elemento16",
            "Elemento12",
            "Elemento11",
            "Elemento10",
            "Elemento9",
            "Elemento8",
            "Elemento4",
            "Elemento5",
            "Elemento6",
            "Elemento7",
            "Elemento3",
            "Elemento2",
            "Elemento1"
        };

        private List<string> upElementsAnimationNames = new()
        {
            "upElement19Trigger",
            "upElement18Trigger",
            "upElement17Trigger",
            "upElement13Trigger",
            "upElement14Trigger",
            "upElement15Trigger",
            "upElement16Trigger",
            "upElement12Trigger",
            "upElement11Trigger",
            "upElement10Trigger",
            "upElement9Trigger",
            "upElement8Trigger",
            "upElement4Trigger",
            "upElement5Trigger",
            "upElement6Trigger",
            "upElement7Trigger",
            "upElement3Trigger",
            "upElement2Trigger",
            "upElement1Trigger"
        };

        private List<string> possiblePositions = new() { };

        private List<string> ShowPossibleElements(List<string> possiblePositions, int diceNumberTextToInt)
        {
            if (currentPosition - diceNumberTextToInt >= 0)
            {
                boardAnimator.SetTrigger(upElementsAnimationNames[currentPosition - diceNumberTextToInt]);
                possiblePositions.Add(roadElements[currentPosition - diceNumberTextToInt]);
            }

            if (currentPosition + diceNumberTextToInt < roadElements.Count)
            {
                boardAnimator.SetTrigger(upElementsAnimationNames[currentPosition + diceNumberTextToInt]);
                possiblePositions.Add(roadElements[currentPosition + diceNumberTextToInt]);
            }

            return possiblePositions;
        }

        private void MoveCharacter(Transform elementTransform, Transform characterTransform)
        {
            Vector3 elementPosition = elementTransform.GetComponent<Renderer>().bounds.center;
            characterTransform.position = elementPosition;
        }

        private void BlockDice()
        {
            Transform[] diceFacesGameObjects = dice.GetComponentsInChildren<Transform>();

            foreach (Transform diceFace in diceFacesGameObjects)
            {
                Collider collider = diceFace.GetComponent<Collider>();
                collider.enabled = false;
            }
        }


        private void EnableDice()
        {
            Transform[] diceFacesGameObjects = dice.GetComponentsInChildren<Transform>();

            foreach (Transform diceFace in diceFacesGameObjects)
            {
                Collider collider = diceFace.GetComponent<Collider>();
                collider.enabled = true;
            }
        }

        void Update()
        {
            diceNumberText = diceNumber.GetComponent<TMP_Text>();
            diceNumberText.enabled = false;
            int diceNumberTextToInt = 0;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit))
                {
                    if (diceFaces.Contains(hit.transform.name))
                    {
                        System.Random random = new();

                        // Genera un numero aleatorio entre 0 y 5
                        int randomNumber = random.Next(0, diceAnimations.Count);

                        diceAnimator.Play(diceAnimations[randomNumber]);

                        finalRandomNumber = randomNumber + 1;
                        diceNumberText.text = finalRandomNumber.ToString();

                        BlockDice();

                        diceNumberTextToInt = int.Parse(diceNumberText.text);
                        possiblePositions = ShowPossibleElements(possiblePositions, diceNumberTextToInt);
                    }

                    if (possiblePositions.Contains(hit.transform.name))
                    {
                        // Parar animaciones de elementos que se levantan
                        foreach (var position in possiblePositions)
                        {
                            int index = roadElements.IndexOf(position);
                            string triggerName = upElementsAnimationNames[index];
                            string stopTrigger = triggerName + "Stop";
                            boardAnimator.SetTrigger(stopTrigger);
                        }

                        // TODO: Arreglar que el muñeco se mueva cuando se baje la pieza.
                        
                        // Movimiento del personaje.
                        Transform characterTransform = mainCharacterTransform;

                        character.SetActive(false);
                        MoveCharacter(hit.transform, characterTransform);
                        character.SetActive(true);

                        // Actualizar currentPossition.
                        currentPosition = roadElements.IndexOf(hit.transform.name);
                        possiblePositions = new() { };
                        EnableDice();
                    }
                }
            }
        }
    }
}