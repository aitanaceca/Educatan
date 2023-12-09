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

        public Material waterMaterial;
        public Material sandMaterial;
        public Material fireMaterial;
        public Material grassMaterial;
        public Material woodMaterial;

        public ProBuilderMesh element1;
        public ProBuilderMesh element2;
        public ProBuilderMesh element3;
        public ProBuilderMesh element4;
        public ProBuilderMesh element5;
        public ProBuilderMesh element6;
        public ProBuilderMesh element7;
        public ProBuilderMesh element8;
        public ProBuilderMesh element9;
        public ProBuilderMesh element10;
        public ProBuilderMesh element11;
        public ProBuilderMesh element12;
        public ProBuilderMesh element13;
        public ProBuilderMesh element14;
        public ProBuilderMesh element15;
        public ProBuilderMesh element16;
        public ProBuilderMesh element17;
        public ProBuilderMesh element18;
        public ProBuilderMesh element19;

        private TMP_Text diceNumberText;

        private void SetInitialCounters(int currentLevel)
        {
            LevelElements.LevelElements levelElements = new(currentLevel);
            fireCounter.text = $"0 / {(levelElements.FireMaterial).ToString()}";
            waterCounter.text = $"0 / {(levelElements.WaterMaterial).ToString()}";
            grassCounter.text = $"0 / {(levelElements.GrassMaterial).ToString()}";
            sandCounter.text = $"0 / {(levelElements.SandMaterial).ToString()}";
            woodCounter.text = $"0 / {(levelElements.WoodMaterial).ToString()}";
        }

        private static void HideBridges(List<ProBuilderMesh> elements)
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
            }
            else
            {
                currentLevel = int.Parse(level.text);
            }

            SetInitialCounters(currentLevel);

            List<ProBuilderMesh> elements = new()
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