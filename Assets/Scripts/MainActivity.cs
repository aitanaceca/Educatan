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
        public GameObject dice;
        public GameObject diceNumber;
        public GameObject card;
        public GameObject mainCharacter;
        public GameObject bagCanvas;
        public GameObject counterCanvas;
        public GameObject checkCardCanvas;
        public GameObject bagOpen;

        public TMP_Text fireCounter;
        public TMP_Text waterCounter;
        public TMP_Text grassCounter;
        public TMP_Text sandCounter;
        public TMP_Text woodCounter;
        public TMP_Text level;
        public TMP_Text levelInfo;

        public Material waterMaterial;
        public Material sandMaterial;
        public Material fireMaterial;
        public Material grassMaterial;
        public Material woodMaterial;

        public GameObject element1;
        public GameObject element2;
        public GameObject element3;
        public GameObject element4;
        public GameObject element5;
        public GameObject element6;
        public GameObject element7;
        public GameObject element8;
        public GameObject element9;
        public GameObject element10;
        public GameObject element11;
        public GameObject element12;
        public GameObject element13;
        public GameObject element14;
        public GameObject element15;
        public GameObject element16;
        public GameObject element17;
        public GameObject element18;
        public GameObject element19;

        private TMP_Text diceNumberText;

        private int GetRandomCounterNumber(int level, int minNum, int maxNum)
        {
            System.Random random = new();
            int randomNumber = random.Next(minNum, maxNum);
            return randomNumber * level;
        }

        private void SetInitialCounters(int level)
        {
            fireCounter.text = $"0 / {(GetRandomCounterNumber(level, level, level + 4)).ToString()}";
            waterCounter.text = $"0 / {(GetRandomCounterNumber(level, level, level + 4)).ToString()}";
            grassCounter.text = $"0 / {(GetRandomCounterNumber(level, level, level + 4)).ToString()}";
            sandCounter.text = $"0 / {(GetRandomCounterNumber(level, level, level + 4)).ToString()}";
            woodCounter.text = $"0 / {(GetRandomCounterNumber(level, level, level + 4)).ToString()}";
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
            dice.SetActive(false);
            mainCharacter.SetActive(false);
            card.SetActive(false);
            diceNumberText = diceNumber.GetComponent<TMP_Text>();
            diceNumberText.enabled = false;
            bagCanvas.SetActive(false);
            checkCardCanvas.SetActive(false);
            counterCanvas.SetActive(false);
            bagOpen.SetActive(false);

            int currentLevel;

            if (PlayerPrefs.HasKey("Level"))
            {
                currentLevel = int.Parse(PlayerPrefs.GetString("Level"));
                levelInfo.text = $"NIVEL {PlayerPrefs.GetString("Level")}";
            }
            else
            {
                currentLevel = int.Parse(level.text);
                levelInfo.text = $"NIVEL {level.text}";
            }

            List<GameObject> elements = new()
            {
                element1,
                element2,
                element3,
                element4,
                element5,
                element6,
                element7,
                element8,
                element9,
                element10,
                element11,
                element12,
                element13,
                element14,
                element15,
                element16,
                element17,
                element18,
                element19
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
                        elem.GetComponent<MeshRenderer>().material = waterMaterial;
                        levelElements.WaterMaterial -= 1;
                        break;

                    case "SandMaterial":
                        elem.GetComponent<MeshRenderer>().material = sandMaterial;
                        levelElements.SandMaterial -= 1;
                        break;

                    case "FireMaterial":
                        elem.GetComponent<MeshRenderer>().material = fireMaterial;
                        levelElements.FireMaterial -= 1;
                        break;

                    case "GrassMaterial":
                        elem.GetComponent<MeshRenderer>().material = grassMaterial;
                        levelElements.GrassMaterial -= 1;
                        break;

                    case "WoodMaterial":
                        elem.GetComponent<MeshRenderer>().material = woodMaterial;
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