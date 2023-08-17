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
        "UpElemento19",
        "UpElemento18",
        "UpElemento17",
        "UpElemento13",
        "UpElemento14",
        "UpElemento15",
        "UpElemento16",
        "UpElemento12",
        "UpElemento11",
        "UpElemento10",
        "UpElemento9",
        "UpElemento8",
        "UpElemento4",
        "UpElemento5",
        "UpElemento6",
        "UpElemento7",
        "UpElemento3",
        "UpElemento2",
        "UpElemento1"
    };

    private void ShowPossibleElements()
    {
        int diceNumberTextToInt = int.Parse(diceNumberText.text);

        if (currentPossition - diceNumberTextToInt >= 0)
        {
            // Play animacion de upElementsAnimationNames[currentPossition - diceNumberTextToInt]
        }

        if (currentPossition + diceNumberTextToInt < roadElements.Count)
        {
            // Play animacion de upElementsAnimationNames[currentPossition + diceNumberTextToInt]
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit))
            {
                if (roadElements.Contains(hit.transform.name))
                {
                    // Mover personaje.
                    // Actualizar currentPossition.
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

                    // Genera un número aleatorio entre 0 y 5
                    int randomNumber = random.Next(0, diceAnimations.Count);

                    diceAnimator.Play(diceAnimations[randomNumber]);

                    finalRandomNumber = randomNumber + 1;
                    diceNumberText.text = finalRandomNumber.ToString();

                    // Bloquear dado
                    // ShowPossibleElements()
                }
            }
        }

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

                    // Genera un número aleatorio entre 0 y 5
                    int randomNumber = random.Next(0, diceAnimations.Count);

                    diceAnimator.Play(diceAnimations[randomNumber]);

                    finalRandomNumber = randomNumber + 1;
                    diceNumberText.text = finalRandomNumber.ToString();

                    // Bloquear dado
                    // ShowPossibleElements()
                }
            }
        }

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

                    // Genera un número aleatorio entre 0 y 5
                    int randomNumber = random.Next(0, diceAnimations.Count);

                    diceAnimator.Play(diceAnimations[randomNumber]);

                    finalRandomNumber = randomNumber + 1;
                    diceNumberText.text = finalRandomNumber.ToString();

                    // Bloquear dado
                    // ShowPossibleElements()
                }
            }
        }

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

                    // Genera un número aleatorio entre 0 y 5
                    int randomNumber = random.Next(0, diceAnimations.Count);

                    diceAnimator.Play(diceAnimations[randomNumber]);

                    finalRandomNumber = randomNumber + 1;
                    diceNumberText.text = finalRandomNumber.ToString();

                    // Bloquear dado
                    // ShowPossibleElements()
                }
            }
        }
    }
}