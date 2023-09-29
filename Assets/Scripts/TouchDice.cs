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

        public Animator diceAnimator;
        public Animator boardAnimator;

        private TMP_Text diceNumberText;

        private List<string> diceFaces = new() { "Cara1", "Cara2", "Cara3", "Cara4", "Cara5", "Cara6" };
        private List<string> diceAnimations = new() { "dice1", "dice2", "dice3", "dice4", "dice5", "dice6" };

        private int finalRandomNumber;

        private int currentPossition = 0;
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

        List<string> possiblePositions = new() { };

        private List<string> ShowPossibleElements(List<string> possiblePositions, int diceNumberTextToInt)
        {
            if (currentPossition - diceNumberTextToInt >= 0)
            {
                boardAnimator.SetTrigger(upElementsAnimationNames[currentPossition - diceNumberTextToInt]);
                possiblePositions.Add(roadElements[currentPossition - diceNumberTextToInt]);
            }

            if (currentPossition + diceNumberTextToInt < roadElements.Count)
            {
                boardAnimator.SetTrigger(upElementsAnimationNames[currentPossition + diceNumberTextToInt]);
                possiblePositions.Add(roadElements[currentPossition + diceNumberTextToInt]);
            }

            return possiblePositions;
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

                        // TODO: Bloquear dado cuando se termine la animacion del movimiento del propio dado.

                        diceNumberTextToInt = int.Parse(diceNumberText.text);
                        possiblePositions = ShowPossibleElements(possiblePositions, diceNumberTextToInt);
                    }

                    if (possiblePositions.Contains(hit.transform.name))
                    {
                        // TODO: Mover personaje.
                        //float moveAmount = 2.0f * Time.deltaTime;
                        //Transform myTransform = mainCharacterTransform;

                        //// Mueve el GameObject en el eje Z
                        //myTransform.Translate(Vector3.forward * moveAmount);
                        //myTransform.Translate(Vector3.back * moveAmount);
                        //// Mueve el GameObject en el eje X 
                        //myTransform.Translate(Vector3.right * moveAmount);
                        //myTransform.Translate(Vector3.left * moveAmount);
                        //// Mueve el GameObject en el eje Y
                        //myTransform.Translate(Vector3.up * moveAmount);
                        //myTransform.Translate(Vector3.down * moveAmount);

                        // Parar animaciones de elementos que se levantan
                        foreach (var position in possiblePositions)
                        {
                            int index = roadElements.IndexOf(position);
                            string triggerName = upElementsAnimationNames[index];
                            string stopTrigger = triggerName + "Stop";
                            boardAnimator.SetTrigger(stopTrigger);
                        }

                        // Actualizar currentPossition.
                        currentPossition = roadElements.IndexOf(hit.transform.name);
                        possiblePositions = new() { };
                    }
                }
            }
        }
    }
}