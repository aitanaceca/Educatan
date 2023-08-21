using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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

    private void ShowPossibleElements()
    {
        int diceNumberTextToInt = int.Parse(diceNumberText.text);
        List<string> possiblePositions = new() { };

        if (currentPossition - diceNumberTextToInt >= 0)
        {
            boardAnimator.SetTrigger(upElementsAnimationNames[currentPossition - diceNumberTextToInt]);
            possiblePositions.Add(upElementsAnimationNames[currentPossition - diceNumberTextToInt]);
        }

        if (currentPossition + diceNumberTextToInt < roadElements.Count)
        {
            boardAnimator.SetTrigger(upElementsAnimationNames[currentPossition + diceNumberTextToInt]);
            possiblePositions.Add(upElementsAnimationNames[currentPossition + diceNumberTextToInt]);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit))
            {
                if (possiblePositions.Contains(hit.transform.name))
                {
                    // Mover personaje.
                    // Actualizar currentPossition.
                    // Parar animaciones de elementos que se levantan
                    print("Esta posicion si se puede");
                }
            }
        }
    }

    void Update()
    {
        diceNumberText = diceNumber.GetComponent<TMP_Text>();
        diceNumberText.enabled = false;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit))
            {
                if (diceFaces.Contains(hit.transform.name))
                {
                    System.Random random = new();

                    // Genera un n�mero aleatorio entre 0 y 5
                    int randomNumber = random.Next(0, diceAnimations.Count);

                    diceAnimator.Play(diceAnimations[randomNumber]);

                    finalRandomNumber = randomNumber + 1;
                    diceNumberText.text = finalRandomNumber.ToString();

                    // Bloquear dado cuando se termine la animaci�n del movimiento del propio dado.
                    ShowPossibleElements();
                }
            }
        }
    }
}