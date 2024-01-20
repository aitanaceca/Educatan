using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Scripts.CounterText;
using Scripts.Card;
using UnityEngine.SceneManagement;
using Scripts.Scenes;

namespace Scripts.TouchDice
{
    public class TouchDice : DefaultObserverEventHandler
    {
        public GameObject diceNumber;
        public GameObject character;
        public GameObject dice;
        public GameObject card;
        public GameObject counterCanvas;
        public GameObject checkCardCanvas;
        public GameObject bagOpen;
        public GameObject bagClosed;

        public Button cardButton;
        public Button bagButton;
        public Button checkCardButton;

        public Animator diceAnimator;
        public Animator boardAnimator;
        public Animator characterAnimator;

        public Transform mainCharacterTransform;
        public Transform firsElementTransform;

        public TMP_Text fireCounter;
        public TMP_Text waterCounter;
        public TMP_Text grassCounter;
        public TMP_Text sandCounter;
        public TMP_Text woodCounter;
        public TMP_Text currentLevel;

        public TMP_Text bagButtonText;

        private TMP_Text diceNumberText;

        public TMP_Text cardText;

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

        private (int, Card.Card) currentCardRestriction;
        private bool cardIsApplied = false;
        private bool characterMovedToFirstElement = false;

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

        private int GetRandomIndex(int maxNumber)
        {
            System.Random random = new();
            return random.Next(maxNumber);
        }

        private Card.Card GetRandomCard()
        {
            List<string> elementNames = new() { "agua", "fuego", "hierba", "madera", "arena" };

            int firstRandomIndex = GetRandomIndex(elementNames.Count);

            string element = elementNames[firstRandomIndex];

            elementNames.RemoveAt(firstRandomIndex);

            int secondRandomIndex = GetRandomIndex(elementNames.Count);

            string changeElement = elementNames[secondRandomIndex];

            int diceNumberRandom = GetRandomIndex(diceFaces.Count) + 1;

            return new(element, diceNumberRandom, changeElement);
        }

        private void ShowCard(int randomCardIndex, Card.Card randomCard)
        {
            cardText.text = randomCard.Cards[randomCardIndex];
            card.SetActive(true);
            cardIsApplied = false;
        }

        private void ShowEndCard()
        {
            cardText.text = "ENHORABUENA, HAS COMPLETADO EL NIVEL.";
            card.SetActive(true);
        }

        private (string, int) ApplyCardRestriction(int diceNumber, int nextCard, string elementName, Transform characterTransform)
        {
            if (currentCardRestriction.Item2 != null)
            {
                switch (currentCardRestriction.Item1)
                {
                    case 0:
                        if (diceNumber == currentCardRestriction.Item2.Num)
                        {
                            return (currentCardRestriction.Item2.Element, -1);
                        }
                        break;
                    case 1:
                        if (diceNumber == currentCardRestriction.Item2.Num)
                        {

                            return (currentCardRestriction.Item2.Element, 1);
                        }
                        break;
                    case 2:
                        if (diceNumber == currentCardRestriction.Item2.Num && !cardIsApplied)
                        {
                            cardIsApplied = true;
                            return (currentCardRestriction.Item2.Element, 1);
                        }
                        break;
                    case 3:
                        if (diceNumber == currentCardRestriction.Item2.Num && !cardIsApplied)
                        {
                            cardIsApplied = true;
                            return (currentCardRestriction.Item2.Element, -1);
                        }
                        break;
                    case 4:
                        if (diceNumber == currentCardRestriction.Item2.Num && nextCard <= 2)
                        {            
                            MoveCharacter(firsElementTransform, characterTransform);
                            characterMovedToFirstElement = true;
                            if (nextCard == 2)
                            {
                                cardIsApplied = true;
                            }
                            return (currentCardRestriction.Item2.Element, 1);
                        }
                        break;
                    case 5:
                        GameObject element = GameObject.Find(elementName);
                        string materialName = element.GetComponent<MeshRenderer>().material.name;
                        if (nextCard <= 3 && materialName.Contains(currentCardRestriction.Item2.Element))
                        {
                            if (nextCard == 3)
                            {
                                cardIsApplied = true;
                            }
                            return (currentCardRestriction.Item2.ChangeElement, 1);
                        }
                        break;
                }
            }

            return (elementName, 1);
        }

        private void HideCard()
        {
            card.SetActive(false);
        }

        private void ChangeLevel()
        {
            card.SetActive(false);
            int level;

            if (PlayerPrefs.HasKey("Level"))
            {
                level = int.Parse(PlayerPrefs.GetString("Level"));
            }
            else
            {
                level = int.Parse(currentLevel.text);
            }

            level += 1;
            PlayerPrefs.SetString("Level", level.ToString());
            SceneManager.LoadScene((int)GameScene.MAIN);
        }

        private bool CheckIfCounterHasReachedGoal(string counter)
        {
            string[] counterElements = counter.Split(" ");
            int count = int.Parse(counterElements[0]);
            int goal = int.Parse(counterElements[2]);
            if (count >= goal)
            {
                return true;
            }
            return false;
        }

        private void ChangeCounterColor(TMP_Text counterText, string elementCounter)
        {
            if (CheckIfCounterHasReachedGoal(elementCounter))
            {
                counterText.color = Color.green;
            }
            else
            {
                counterText.color = Color.white;
            }
        }

        private void ChangeCountersColor(CounterText.CounterText counterText)
        {
            ChangeCounterColor(waterCounter, counterText.WaterCounter);
            ChangeCounterColor(woodCounter, counterText.WoodCounter);
            ChangeCounterColor(fireCounter, counterText.FireCounter);
            ChangeCounterColor(grassCounter, counterText.GrassCounter);
            ChangeCounterColor(sandCounter, counterText.SandCounter);
        }

        private string GetNewCounterValue(string elementText, int addElement)
        {
            string[] counterElements = elementText.Split(" ");
            int updatedCounter = int.Parse(counterElements[0]) + addElement;
            return $"{updatedCounter.ToString()} / {counterElements[2]}";
        }

        private CounterText.CounterText UpdateElementCounter(string elementName, int addElement)
        {
            string materialName = "";
            if (elementName.Contains("Elemento"))
            {
                GameObject element = GameObject.Find(elementName);
                materialName = element.GetComponent<MeshRenderer>().material.name;
            }

            if (materialName.Contains("fuego") || elementName.Equals("fuego"))
            {
                string newText = GetNewCounterValue(fireCounter.text, addElement);
                return new(waterCounter.text, sandCounter.text, newText, grassCounter.text, woodCounter.text);
            }
            if (materialName.Contains("agua") || elementName.Equals("agua"))
            {
                string newText = GetNewCounterValue(waterCounter.text, addElement);
                return new(newText, sandCounter.text, fireCounter.text, grassCounter.text, woodCounter.text);
            }
            if (materialName.Contains("hierba") || elementName.Equals("hierba"))
            {
                string newText = GetNewCounterValue(grassCounter.text, addElement);
                return new(waterCounter.text, sandCounter.text, fireCounter.text, newText, woodCounter.text);
            }
            if (materialName.Contains("arena") || elementName.Equals("arena"))
            {
                string newText = GetNewCounterValue(sandCounter.text, addElement);
                return new(waterCounter.text, newText, fireCounter.text, grassCounter.text, woodCounter.text);
            }
            if (materialName.Contains("madera") || elementName.Equals("madera"))
            {
                string newText = GetNewCounterValue(woodCounter.text, addElement);
                return new(waterCounter.text, sandCounter.text, fireCounter.text, grassCounter.text, newText);
            }
            return new(waterCounter.text, sandCounter.text, fireCounter.text, grassCounter.text, woodCounter.text);
        }

        private void OpenOrCloseBag()
        {
            if (bagIsClosed)
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

        private void ShowCheckCardCanvas()
        {
            checkCardCanvas.SetActive(true);
        }

        private void ShowCardOnClickCardButton()
        {
            ShowCard(currentCardRestriction.Item1, currentCardRestriction.Item2);
        }

        private bool CheckIfUserHasReachedGoal(CounterText.CounterText counterText)
        {
            return CheckIfCounterHasReachedGoal(counterText.WaterCounter) && CheckIfCounterHasReachedGoal(counterText.FireCounter) &&
                CheckIfCounterHasReachedGoal(counterText.WoodCounter) && CheckIfCounterHasReachedGoal(counterText.GrassCounter) &&
                CheckIfCounterHasReachedGoal(counterText.SandCounter);
        }

        void Update()
        {
            bagButton.onClick.AddListener(OpenOrCloseBag);
            checkCardButton.onClick.AddListener(ShowCardOnClickCardButton);
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

                        // Actualizar contadores.
                        nextCard += 1;

                        if (nextCard == 4)
                        {
                            // Mostrar tarjeta.
                            int cardRandomIndex = GetRandomIndex(6);
                            Card.Card randomCard = GetRandomCard();
                            ShowCard(cardRandomIndex, randomCard);
                            cardButton.onClick.AddListener(HideCard);
                            currentCardRestriction = (cardRandomIndex, randomCard);

                            // TODO: Bloquear dado hasta que se pulse Aceptar en la carta.

                            nextCard = 0;
                            ShowCheckCardCanvas();
                        }

                        (string, int) elementsForCounter = ApplyCardRestriction(int.Parse(diceNumberText.text), nextCard, hit.transform.name, characterTransform);
                        CounterText.CounterText updateCounter = UpdateElementCounter(elementsForCounter.Item1, elementsForCounter.Item2);
                        
                        fireCounter.text = updateCounter.FireCounter;
                        waterCounter.text = updateCounter.WaterCounter;
                        grassCounter.text = updateCounter.GrassCounter;
                        sandCounter.text = updateCounter.SandCounter;
                        woodCounter.text = updateCounter.WoodCounter;

                        ChangeCountersColor(updateCounter);

                        // Actualizar currentPossition.
                        if(characterMovedToFirstElement)
                        {
                            currentPosition = roadElements.IndexOf("Elemento19");
                        }
                        else
                        {
                            currentPosition = roadElements.IndexOf(hit.transform.name);
                        }
                        characterMovedToFirstElement = false;
                        possiblePositions = new() { };
                        EnableDice();

                        if (CheckIfUserHasReachedGoal(updateCounter))
                        {
                            ShowEndCard();
                            cardButton.onClick.AddListener(ChangeLevel);
                        }
                    }
                }
            }
        }
    }
}