using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchDice : DefaultObserverEventHandler
{
    public int DiceNumber;

    public Animator animator;

    private List<string> diceFaces = new() { "Cara1", "Cara2", "Cara3", "Cara4", "Cara5", "Cara6" };
    private List<string> diceAnimations = new() { "dice1", "dice2", "dice3", "dice4", "dice5", "dice6" };

    void Update()
    {
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

                    animator.Play(diceAnimations[randomNumber]);

                    DiceNumber = randomNumber + 1;
                }
            }
        }
    }   
}