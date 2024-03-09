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
using TMPro;

namespace Scripts.MainActivity
{
    public class MainActivity : MonoBehaviour
    {
        [SerializeField] private GameObject _dice;
        [SerializeField] private GameObject _diceNumber;
        [SerializeField] private GameObject _card;
        [SerializeField] private GameObject _mainCharacter;
        [SerializeField] private GameObject _bagCanvas;
        [SerializeField] private GameObject _counterCanvas;
        [SerializeField] private GameObject _checkCardCanvas;
        [SerializeField] private GameObject _bagOpen;

        [SerializeField] private GameObject _element1;
        [SerializeField] private GameObject _element2;
        [SerializeField] private GameObject _element3;
        [SerializeField] private GameObject _element4;
        [SerializeField] private GameObject _element5;
        [SerializeField] private GameObject _element6;
        [SerializeField] private GameObject _element7;
        [SerializeField] private GameObject _element8;
        [SerializeField] private GameObject _element9;
        [SerializeField] private GameObject _element10;
        [SerializeField] private GameObject _element11;
        [SerializeField] private GameObject _element12;
        [SerializeField] private GameObject _element13;
        [SerializeField] private GameObject _element14;
        [SerializeField] private GameObject _element15;
        [SerializeField] private GameObject _element16;
        [SerializeField] private GameObject _element17;
        [SerializeField] private GameObject _element18;
        [SerializeField] private GameObject _element19;

        [SerializeField] private TMP_Text _fireCounter;
        [SerializeField] private TMP_Text _waterCounter;
        [SerializeField] private TMP_Text _grassCounter;
        [SerializeField] private TMP_Text _sandCounter;
        [SerializeField] private TMP_Text _woodCounter;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _levelInfo;

        [SerializeField] private Material _waterMaterial;
        [SerializeField] private Material _sandMaterial;
        [SerializeField] private Material _fireMaterial;
        [SerializeField] private Material _grassMaterial;
        [SerializeField] private Material _woodMaterial;

        public GameObject Dice
        {
            get { return _dice; }
            set { _dice = value; }
        }

        public GameObject DiceNumber
        {
            get { return _diceNumber; }
            set { _diceNumber = value; }
        }

        public GameObject Card
        {
            get { return _card; }
            set { _card = value; }
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

        public GameObject Element1
        {
            get { return _element1; }
            set { _element1 = value; }
        }

        public GameObject Element2
        {
            get { return _element2; }
            set { _element2 = value; }
        }

        public GameObject Element3
        {
            get { return _element3; }
            set { _element3 = value; }
        }

        public GameObject Element4
        {
            get { return _element4; }
            set { _element4 = value; }
        }

        public GameObject Element5
        {
            get { return _element5; }
            set { _element5 = value; }
        }

        public GameObject Element6
        {
            get { return _element6; }
            set { _element6 = value; }
        }

        public GameObject Element7
        {
            get { return _element7; }
            set { _element7 = value; }
        }

        public GameObject Element8
        {
            get { return _element8; }
            set { _element8 = value; }
        }

        public GameObject Element9
        {
            get { return _element9; }
            set { _element9 = value; }
        }

        public GameObject Element10
        {
            get { return _element10; }
            set { _element10 = value; }
        }

        public GameObject Element11
        {
            get { return _element11; }
            set { _element11 = value; }
        }

        public GameObject Element12
        {
            get { return _element12; }
            set { _element12 = value; }
        }

        public GameObject Element13
        {
            get { return _element13; }
            set { _element13 = value; }
        }

        public GameObject Element14
        {
            get { return _element14; }
            set { _element14 = value; }
        }

        public GameObject Element15
        {
            get { return _element15; }
            set { _element15 = value; }
        }

        public GameObject Element16
        {
            get { return _element16; }
            set { _element16 = value; }
        }

        public GameObject Element17
        {
            get { return _element17; }
            set { _element17 = value; }
        }

        public GameObject Element18
        {
            get { return _element18; }
            set { _element18 = value; }
        }

        public GameObject Element19
        {
            get { return _element19; }
            set { _element19 = value; }
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

        public TMP_Text Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public TMP_Text LevelInfo
        {
            get { return _levelInfo; }
            set { _levelInfo = value; }
        }

        public Material WaterMaterial
        {
            get { return _waterMaterial; }
            set { _waterMaterial = value; }
        }

        public Material SandMaterial
        {
            get { return _sandMaterial; }
            set { _sandMaterial = value; }
        }

        public Material FireMaterial
        {
            get { return _fireMaterial; }
            set { _fireMaterial = value; }
        }

        public Material GrassMaterial
        {
            get { return _grassMaterial; }
            set { _grassMaterial = value; }
        }

        public Material WoodMaterial
        {
            get { return _woodMaterial; }
            set { _woodMaterial = value; }
        }

        private TMP_Text diceNumberText;

        private void InitializeElements()
        {
            _dice = Dice;
            _diceNumber = DiceNumber;
            _card = Card;
            _mainCharacter = MainCharacter;
            _bagCanvas = BagCanvas;
            _counterCanvas = CounterCanvas;
            _checkCardCanvas = CheckCardCanvas;
            _bagOpen = BagOpen;
            _element1 = Element1;
            _element2 = Element2;
            _element3 = Element3;
            _element4 = Element4;
            _element5 = Element5;
            _element6 = Element6;
            _element7 = Element7;
            _element8 = Element8;
            _element9 = Element9;
            _element10 = Element10;
            _element11 = Element11;
            _element12 = Element12;
            _element13 = Element13;
            _element14 = Element14;
            _element15 = Element15;
            _element16 = Element16;
            _element17 = Element17;
            _element18 = Element18;
            _element19 = Element19;
            _fireCounter = FireCounter;
            _waterCounter = WaterCounter;
            _grassCounter = GrassCounter;
            _sandCounter = SandCounter;
            _woodCounter = WoodCounter;
            _level = Level;
            _levelInfo = LevelInfo;
            _waterMaterial = WaterMaterial;
            _sandMaterial = SandMaterial;
            _fireMaterial = FireMaterial;
            _grassMaterial = GrassMaterial;
            _woodMaterial = WoodMaterial;
        }

        private int GetRandomCounterNumber(int level, int minNum, int maxNum)
        {
            System.Random random = new();
            int randomNumber = random.Next(minNum, maxNum);
            return randomNumber * level;
        }

        private void SetInitialCounters(int level)
        {
            _fireCounter.text = $"0 / {(GetRandomCounterNumber(level, level, level + 4)).ToString()}";
            _waterCounter.text = $"0 / {(GetRandomCounterNumber(level, level, level + 4)).ToString()}";
            _grassCounter.text = $"0 / {(GetRandomCounterNumber(level, level, level + 4)).ToString()}";
            _sandCounter.text = $"0 / {(GetRandomCounterNumber(level, level, level + 4)).ToString()}";
            _woodCounter.text = $"0 / {(GetRandomCounterNumber(level, level, level + 4)).ToString()}";
        }

        private static void HideBridges(List<GameObject> elements)
        {
            foreach(var elem in elements)
            {
                Transform elemTransform = elem.transform;

                foreach (Transform bridge in elemTransform)
                {
                    bridge.gameObject.SetActive(false);
                }
            }
        }

        private void Start()
        {
            InitializeElements();
            _dice.SetActive(false);
            _mainCharacter.SetActive(false);
            _card.SetActive(false);
            diceNumberText = _diceNumber.GetComponent<TMP_Text>();
            diceNumberText.enabled = false;
            _bagCanvas.SetActive(false);
            _checkCardCanvas.SetActive(false);
            _counterCanvas.SetActive(false);
            _bagOpen.SetActive(false);

            int currentLevel;

            if (PlayerPrefs.HasKey("Level"))
            {
                currentLevel = int.Parse(PlayerPrefs.GetString("Level"));
                _levelInfo.text = $"NIVEL {PlayerPrefs.GetString("Level")}";
            }
            else
            {
                currentLevel = int.Parse(_level.text);
                _levelInfo.text = $"NIVEL {_level.text}";
            }

            List<GameObject> elements = new()
            {
                _element1,
                _element2,
                _element3,
                _element4,
                _element5,
                _element6,
                _element7,
                _element8,
                _element9,
                _element10,
                _element11,
                _element12,
                _element13,
                _element14,
                _element15,
                _element16,
                _element17,
                _element18,
                _element19
            };

            LevelElements.LevelElements levelElements = new(currentLevel);

            SetInitialCounters(currentLevel);

            HideBridges(elements);

            foreach (var elem in elements)
            {
                string randomElement = levelElements.GetRandomElement();

                switch (randomElement)
                {
                    case "WaterMaterial":
                        elem.GetComponent<MeshRenderer>().material = _waterMaterial;
                        levelElements.WaterMaterial -= 1;
                        break;

                    case "SandMaterial":
                        elem.GetComponent<MeshRenderer>().material = _sandMaterial;
                        levelElements.SandMaterial -= 1;
                        break;

                    case "FireMaterial":
                        elem.GetComponent<MeshRenderer>().material = _fireMaterial;
                        levelElements.FireMaterial -= 1;
                        break;

                    case "GrassMaterial":
                        elem.GetComponent<MeshRenderer>().material = _grassMaterial;
                        levelElements.GrassMaterial -= 1;
                        break;

                    case "WoodMaterial":
                        elem.GetComponent<MeshRenderer>().material = _woodMaterial;
                        levelElements.WoodMaterial -= 1;
                        break;
                }
            }
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}