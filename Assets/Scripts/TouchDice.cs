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
        [SerializeField] private GameObject _diceNumber;
        [SerializeField] private GameObject _character;
        [SerializeField] private GameObject _dice;
        [SerializeField] private GameObject _card;
        [SerializeField] private GameObject _counterCanvas;
        [SerializeField] private GameObject _checkCardCanvas;
        [SerializeField] private GameObject _bagOpen;
        [SerializeField] private GameObject _bagClosed;

        [SerializeField] private Button _cardButton;
        [SerializeField] private Button _bagButton;
        [SerializeField] private Button _checkCardButton;

        [SerializeField] private Animator _diceAnimator;
        [SerializeField] private Animator _boardAnimator;

        [SerializeField] private Transform _mainCharacterTransform;
        [SerializeField] private Transform _firsElementTransform;

        [SerializeField] private TMP_Text _fireCounter;
        [SerializeField] private TMP_Text _waterCounter;
        [SerializeField] private TMP_Text _grassCounter;
        [SerializeField] private TMP_Text _sandCounter;
        [SerializeField] private TMP_Text _woodCounter;
        [SerializeField] private TMP_Text _currentLevel;
        [SerializeField] private TMP_Text _bagButtonText;
        [SerializeField] private TMP_Text _cardText;


        public GameObject DiceNumber
        {
            get { return _diceNumber; }
            set { _diceNumber = value; }
        }

        public GameObject Character
        {
            get { return _character; }
            set { _character = value; }
        }

        public GameObject Dice
        {
            get { return _dice; }
            set { _dice = value; }
        }

        public GameObject Card
        {
            get { return _card; }
            set { _card = value; }
        }

        public GameObject CounterCanvas
        {
            get { return _counterCanvas; }
            set { _counterCanvas = value; }
        }

        public GameObject CheckCardCanvas
        {
            get { return _checkCardCanvas; }
            set { _checkCardCanvas = value; }
        }

        public GameObject BagOpen
        {
            get { return _bagOpen; }
            set { _bagOpen = value; }
        }

        public GameObject BagClosed
        {
            get { return _bagClosed; }
            set { _bagClosed = value; }
        }

        public Button CardButton
        {
            get { return _cardButton; }
            set { _cardButton = value; }
        }

        public Button BagButton
        {
            get { return _bagButton; }
            set { _bagButton = value; }
        }

        public Button CheckCardButton
        {
            get { return _checkCardButton; }
            set { _checkCardButton = value; }
        }

        public Animator DiceAnimator
        {
            get { return _diceAnimator; }
            set { _diceAnimator = value; }
        }

        public Animator BoardAnimator
        {
            get { return _boardAnimator; }
            set { _boardAnimator = value; }
        }

        public Transform MainCharacterTransform
        {
            get { return _mainCharacterTransform; }
            set { _mainCharacterTransform = value; }
        }

        public Transform FirsElementTransform
        {
            get { return _firsElementTransform; }
            set { _firsElementTransform = value; }
        }

        public TMP_Text FireCounter
        {
            get { return _fireCounter; }
            set { _fireCounter = value; }
        }

        public TMP_Text WaterCounter
        {
            get { return _waterCounter; }
            set { _waterCounter = value; }
        }

        public TMP_Text GrassCounter
        {
            get { return _grassCounter; }
            set { _grassCounter = value; }
        }

        public TMP_Text SandCounter
        {
            get { return _sandCounter; }
            set { _sandCounter = value; }
        }

        public TMP_Text WoodCounter
        {
            get { return _woodCounter; }
            set { _woodCounter = value; }
        }

        public TMP_Text CurrentLevel
        {
            get { return _currentLevel; }
            set { _currentLevel = value; }
        }

        public TMP_Text BagButtonText
        {
            get { return _bagButtonText; }
            set { _bagButtonText = value; }
        }

        public TMP_Text CardText
        {
            get { return _cardText; }
            set { _cardText = value; }
        }

        private readonly List<string> diceFaces = new() { "Cara1", "Cara2", "Cara3", "Cara4", "Cara5", "Cara6" };
        private readonly List<string> diceAnimations = new() { "dice1", "dice2", "dice3", "dice4", "dice5", "dice6" };

        private int currentPosition = 0;
        private readonly List<string> roadElements = new()
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

        private readonly List<string> upElementsAnimationNames = new()
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
                _boardAnimator.SetTrigger(upElementsAnimationNames[currentPosition - diceNumberTextToInt]);
                possiblePositions.Add(roadElements[currentPosition - diceNumberTextToInt]);
            }

            if (currentPosition + diceNumberTextToInt < roadElements.Count)
            {
                _boardAnimator.SetTrigger(upElementsAnimationNames[currentPosition + diceNumberTextToInt]);
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
            Transform[] diceFacesGameObjects = _dice.GetComponentsInChildren<Transform>();

            foreach (Transform diceFace in diceFacesGameObjects)
            {
                Collider collider = diceFace.GetComponent<Collider>();
                collider.enabled = false;
            }
        }

        private void EnableDice()
        {
            Transform[] diceFacesGameObjects = _dice.GetComponentsInChildren<Transform>();

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
            _cardText.text = randomCard.Cards[randomCardIndex];
            _card.SetActive(true);
            cardIsApplied = false;
        }

        private void ShowEndCard()
        {
            _cardText.text = "ENHORABUENA, NIVEL COMPLETADO.";
            _card.SetActive(true);
        }

        private bool CheckStopRestrictionCondition(int diceNumber)
        {
            return diceNumber == currentCardRestriction.Item2.Num && !cardIsApplied;
        }

        private bool CheckStopRestrictionConditionByTimes(int diceNumber, int nextCard)
        {
            return diceNumber == currentCardRestriction.Item2.Num && nextCard <= 2;
        }
        
        private bool CheckIfMaterialSatisfyRestriction(int nextCard, string materialName)
        {
            return nextCard <= 3 && materialName.Contains(currentCardRestriction.Item2.Element);
        }

        private (string, int) ApplyCardRestriction(int diceNumber, int nextCard, string elementName, Transform characterTransform)
        {
            if (currentCardRestriction.Item2 == null)
            {
                return (elementName, 1);
            }

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
                    if (CheckStopRestrictionCondition(diceNumber))
                    {
                        cardIsApplied = true;
                        return (currentCardRestriction.Item2.Element, 1);
                    }
                    break;
                case 3:
                    if (CheckStopRestrictionCondition(diceNumber))
                    {
                        cardIsApplied = true;
                        return (currentCardRestriction.Item2.Element, -1);
                    }
                    break;
                case 4:
                    if (CheckStopRestrictionConditionByTimes(diceNumber, nextCard))
                    {
                        MoveCharacter(_firsElementTransform, characterTransform);
                        characterMovedToFirstElement = true;
                        cardIsApplied = (nextCard == 2);
                        return (currentCardRestriction.Item2.Element, 1);
                    }
                    break;
                case 5:
                    GameObject element = GameObject.Find(elementName);
                    string materialName = element.GetComponent<MeshRenderer>().material.name;
                    if (CheckIfMaterialSatisfyRestriction(nextCard, materialName))
                    {
                        cardIsApplied = (nextCard == 3);
                        return (currentCardRestriction.Item2.ChangeElement, 1);
                    }
                    break;
            }

            return (elementName, 1);
        }

        private void HideCard()
        {
            _card.SetActive(false);
        }

        private void ChangeLevel()
        {
            _card.SetActive(false);
            int level;

            if (PlayerPrefs.HasKey("Level"))
            {
                level = int.Parse(PlayerPrefs.GetString("Level"));
            }
            else
            {
                level = int.Parse(_currentLevel.text);
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
            ChangeCounterColor(_waterCounter, counterText.WaterCounter);
            ChangeCounterColor(_woodCounter, counterText.WoodCounter);
            ChangeCounterColor(_fireCounter, counterText.FireCounter);
            ChangeCounterColor(_grassCounter, counterText.GrassCounter);
            ChangeCounterColor(_sandCounter, counterText.SandCounter);
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
                string newText = GetNewCounterValue(_fireCounter.text, addElement);
                return new(_waterCounter.text, _sandCounter.text, newText, _grassCounter.text, _woodCounter.text);
            }
            if (materialName.Contains("agua") || elementName.Equals("agua"))
            {
                string newText = GetNewCounterValue(_waterCounter.text, addElement);
                return new(newText, _sandCounter.text, _fireCounter.text, _grassCounter.text, _woodCounter.text);
            }
            if (materialName.Contains("hierba") || elementName.Equals("hierba"))
            {
                string newText = GetNewCounterValue(_grassCounter.text, addElement);
                return new(_waterCounter.text, _sandCounter.text, _fireCounter.text, newText, _woodCounter.text);
            }
            if (materialName.Contains("arena") || elementName.Equals("arena"))
            {
                string newText = GetNewCounterValue(_sandCounter.text, addElement);
                return new(_waterCounter.text, newText, _fireCounter.text, _grassCounter.text, _woodCounter.text);
            }
            if (materialName.Contains("madera") || elementName.Equals("madera"))
            {
                string newText = GetNewCounterValue(_woodCounter.text, addElement);
                return new(_waterCounter.text, _sandCounter.text, _fireCounter.text, _grassCounter.text, newText);
            }
            return new(_waterCounter.text, _sandCounter.text, _fireCounter.text, _grassCounter.text, _woodCounter.text);
        }

        private void OpenOrCloseBag()
        {
            if (bagIsClosed)
            {
                _bagClosed.SetActive(false);
                _bagOpen.SetActive(true);
                _counterCanvas.SetActive(true);
                _bagButtonText.text = "PULSA PARA CERRAR";
                bagIsClosed = false;
            }
            else
            {
                _bagOpen.SetActive(false);
                _bagClosed.SetActive(true);
                _counterCanvas.SetActive(false);
                _bagButtonText.text = "PULSA PARA ABRIR";
                bagIsClosed = true;
            }
        }

        private void ShowCheckCardCanvas()
        {
            _checkCardCanvas.SetActive(true);
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

        private void InitializeElements()
        {
            _diceNumber = DiceNumber;
            _character = Character;
            _dice = Dice;
            _card = Card;
            _counterCanvas = CounterCanvas;
            _checkCardCanvas = CheckCardCanvas;
            _bagOpen = BagOpen;
            _bagClosed = BagClosed;
            _cardButton = CardButton;
            _bagButton = BagButton;
            _checkCardButton = CheckCardButton;
            _diceAnimator = DiceAnimator;
            _boardAnimator = BoardAnimator;
            _mainCharacterTransform = MainCharacterTransform;
            _firsElementTransform = FirsElementTransform;
            _fireCounter = FireCounter;
            _waterCounter = WaterCounter;
            _grassCounter = GrassCounter;
            _sandCounter = SandCounter;
            _woodCounter = WoodCounter;
            _currentLevel = CurrentLevel;
            _bagButtonText = BagButtonText;
            _cardText = CardText;
        }

        private List<string> GetPossiblePositions(TMP_Text diceNumberText)
        {
            System.Random random = new();

            // Genera un numero aleatorio entre 0 y 5
            int randomNumber = random.Next(0, diceAnimations.Count);

            _diceAnimator.Play(diceAnimations[randomNumber]);

            int finalRandomNumber = randomNumber + 1;
            diceNumberText.text = finalRandomNumber.ToString();

            BlockDice();

            int diceNumberTextToInt = int.Parse(diceNumberText.text);
            return ShowPossibleElements(possiblePositions, diceNumberTextToInt);
        }

        private void StopPositionsAnimation(List<string> possiblePositions)
        {
            foreach (var position in possiblePositions)
            {
                int index = roadElements.IndexOf(position);
                string triggerName = upElementsAnimationNames[index];
                string stopTrigger = triggerName + "Stop";
                _boardAnimator.SetTrigger(stopTrigger);
            }
        }

        private void MoveCharacterToNewPosition(Transform currentTransform, Transform characterTransform)
        {
            _character.SetActive(false);
            MoveCharacter(currentTransform, characterTransform);
            _character.SetActive(true);
        }

        private void ShowCardIfUserHasReachedFourTimes()
        {
            int cardRandomIndex = GetRandomIndex(6);
            Card.Card randomCard = GetRandomCard();
            ShowCard(cardRandomIndex, randomCard);
            _cardButton.onClick.AddListener(HideCard);
            currentCardRestriction = (cardRandomIndex, randomCard);

            nextCard = 0;
            ShowCheckCardCanvas();
        }

        private CounterText.CounterText ShowUpdatedCounters(TMP_Text diceNumberText, string elementName, Transform characterTransform)
        {
            (string, int) elementsForCounter = ApplyCardRestriction(int.Parse(diceNumberText.text), nextCard, elementName, characterTransform);
            CounterText.CounterText updateCounter = UpdateElementCounter(elementsForCounter.Item1, elementsForCounter.Item2);

            _fireCounter.text = updateCounter.FireCounter;
            _waterCounter.text = updateCounter.WaterCounter;
            _grassCounter.text = updateCounter.GrassCounter;
            _sandCounter.text = updateCounter.SandCounter;
            _woodCounter.text = updateCounter.WoodCounter;

            ChangeCountersColor(updateCounter);
            return updateCounter;
        }

        private void UpdateCurrentPosition(string elementName)
        {
            if (characterMovedToFirstElement)
            {
                currentPosition = roadElements.IndexOf("Elemento19");
            }
            else
            {
                currentPosition = roadElements.IndexOf(elementName);
            }
            characterMovedToFirstElement = false;
            possiblePositions = new() { };
        }

        private void CheckIfHaveToShowCard()
        {
            nextCard += 1;
            if (nextCard == 4)
            {
                // Mostrar tarjeta.
                ShowCardIfUserHasReachedFourTimes();
            }
        }

        private void CheckIfHaveToShowEndCard(CounterText.CounterText updateCounter)
        {
            if (CheckIfUserHasReachedGoal(updateCounter))
            {
                ShowEndCard();
                _cardButton.onClick.AddListener(ChangeLevel);
            }
        }

        void Update()
        {
            InitializeElements();
            TMP_Text diceNumberText;
            _bagButton.onClick.AddListener(OpenOrCloseBag);
            _checkCardButton.onClick.AddListener(ShowCardOnClickCardButton);
            diceNumberText = _diceNumber.GetComponent<TMP_Text>();
            diceNumberText.enabled = false;
            Transform characterTransform = _mainCharacterTransform;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit))
                {
                    if (diceFaces.Contains(hit.transform.name))
                    {
                        possiblePositions = GetPossiblePositions(diceNumberText);
                    }

                    if (possiblePositions.Contains(hit.transform.name))
                    {
                        // Parar animaciones de elementos que se levantan.
                        StopPositionsAnimation(possiblePositions);

                        // Movimiento del personaje.
                        MoveCharacterToNewPosition(hit.transform, characterTransform);

                        // Comprobar si el usuario ha tirado 4 veces.
                        CheckIfHaveToShowCard();

                        // Actualizar contadores.
                        CounterText.CounterText updateCounter = ShowUpdatedCounters(diceNumberText, hit.transform.name, characterTransform);

                        // Actualizar currentPossition.
                        UpdateCurrentPosition(hit.transform.name);
                        EnableDice();

                        // Comprueba si se ha terminado el nivel.
                        CheckIfHaveToShowEndCard(updateCounter);
                    }
                }
            }
        }
    }
}