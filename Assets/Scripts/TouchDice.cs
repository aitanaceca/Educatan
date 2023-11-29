using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Scripts.CounterText;

namespace Scripts.TouchDice
{
    public class TouchDice : DefaultObserverEventHandler
    {
        public GameObject diceNumber;
        public GameObject character;
        public GameObject dice;
        public GameObject card;
        public GameObject counterCanvas;
        public GameObject bagOpen;
        public GameObject bagClosed;

        public Button cardButton;
        public Button bagButton;

        public Animator diceAnimator;
        public Animator boardAnimator;
        public Animator characterAnimator;

        public Transform mainCharacterTransform;

        public TMP_Text fireCounter;
        public TMP_Text waterCounter;
        public TMP_Text grassCounter;
        public TMP_Text sandCounter;
        public TMP_Text woodCounter;

        public TMP_Text bagButtonText;

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

        private int nextCard = 0;
        private bool bagIsClosed = true;

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
            elementPosition.y += 0.002f;
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

        private void ShowCard()
        {
            card.SetActive(true);
        }

        private void HideCard()
        {
            card.SetActive(false);
        }

        private string GetNewCounterValue(string elementText)
        {
            string[] counterElements = elementText.Split(" ");
            int updatedCounter = int.Parse(counterElements[0]) + 1;
            return $"{updatedCounter.ToString()} / {counterElements[2]}";
        }

        private CounterText.CounterText UpdateElementCounter(string elementName)
        {
            GameObject element = GameObject.Find(elementName);
            string materialName = element.GetComponent<MeshRenderer>().material.name;
            
            if (materialName.Contains("fuego"))
            {
                string newText = GetNewCounterValue(fireCounter.text);
                return new(waterCounter.text, sandCounter.text, newText, grassCounter.text, woodCounter.text);
            } 
            if (materialName.Contains("agua"))
            {
                string newText = GetNewCounterValue(waterCounter.text);
                return new(newText, sandCounter.text, fireCounter.text, grassCounter.text, woodCounter.text);
            } 
            if (materialName.Contains("hierba"))
            {
                string newText = GetNewCounterValue(grassCounter.text);
                return new(waterCounter.text, sandCounter.text, fireCounter.text, newText, woodCounter.text);
            }
            if (materialName.Contains("arena"))
            {
                string newText = GetNewCounterValue(sandCounter.text);
                return new(waterCounter.text, newText, fireCounter.text, grassCounter.text, woodCounter.text);
            }
            if (materialName.Contains("madera"))
            {
                string newText = GetNewCounterValue(woodCounter.text);
                return new(waterCounter.text, sandCounter.text, fireCounter.text, grassCounter.text, newText);
            }
            return new(waterCounter.text, sandCounter.text, fireCounter.text, grassCounter.text, woodCounter.text);
        }

        private void OpenOrCloseBag()
        {
            if(bagIsClosed)
            {
                bagClosed.SetActive(false);
                bagOpen.SetActive(true);
                counterCanvas.SetActive(true);
                bagButtonText.text = "PULSA PARA CERRAR";
                bagIsClosed = false;
            } 
            else
            {
                bagOpen.SetActive(false);
                bagClosed.SetActive(true);
                counterCanvas.SetActive(false);
                bagButtonText.text = "PULSA PARA ABRIR";
                bagIsClosed = true;
            }
        }

        void Update()
        {
            bagButton.onClick.AddListener(OpenOrCloseBag);
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

                        CounterText.CounterText updateCounter = UpdateElementCounter(hit.transform.name);
                        fireCounter.text = updateCounter.FireCounter;
                        waterCounter.text = updateCounter.WaterCounter;
                        grassCounter.text = updateCounter.GrassCounter;
                        sandCounter.text = updateCounter.SandCounter;
                        woodCounter.text = updateCounter.WoodCounter;

                        nextCard += 1;
                        if (nextCard == 4)
                        {
                            ShowCard();
                            cardButton.onClick.AddListener(HideCard);

                            // TODO: Bloquear dado hasta que se pulse Aceptar en la carta.

                            nextCard = 0;
                        }

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