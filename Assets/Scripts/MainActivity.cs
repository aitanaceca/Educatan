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

public class MainActivity : MonoBehaviour
{
    public GameObject dice;
    public GameObject diceNumber;

    private TMP_Text diceNumberText;

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

    private void Start()
    {
        dice.SetActive(false);
        diceNumberText = diceNumber.GetComponent<TMP_Text>();
        diceNumberText.enabled = false;

        int currentLevel = 2;
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

        LevelElements levelElements = new(currentLevel);

        foreach (var elem in elements)
        {
            string randomElement = levelElements.GetRandomElement();

            switch (randomElement)
            {
                case "WaterMaterial":
                    elem.GetComponent<MeshRenderer>().sharedMaterial = waterMaterial;
                    levelElements.WaterMaterial -= 1;
                    break;

                case "SandMaterial":
                    elem.GetComponent<MeshRenderer>().sharedMaterial = sandMaterial;
                    levelElements.SandMaterial -= 1;
                    break;

                case "FireMaterial":
                    elem.GetComponent<MeshRenderer>().sharedMaterial = fireMaterial;
                    levelElements.FireMaterial -= 1;
                    break;

                case "GrassMaterial":
                    elem.GetComponent<MeshRenderer>().sharedMaterial = grassMaterial;
                    levelElements.GrassMaterial -= 1;
                    break;

                case "WoodMaterial":
                    elem.GetComponent<MeshRenderer>().sharedMaterial = woodMaterial;
                    levelElements.WoodMaterial -= 1;
                    break;
            }
        }
    }
}