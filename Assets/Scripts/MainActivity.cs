using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Scripts.Scenes;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using System;

public class MainActivity : MonoBehaviour
{
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

        List<Material> materials = new() { waterMaterial, sandMaterial, fireMaterial, grassMaterial, woodMaterial };

        System.Random random = new System.Random();

        foreach (var elem in elements)
        {
            // Genera un número aleatorio entre 0 y 4
            int randomNumber = random.Next(0, 5);

            elem.GetComponent<MeshRenderer>().sharedMaterial = materials[randomNumber];
        }
    }
}