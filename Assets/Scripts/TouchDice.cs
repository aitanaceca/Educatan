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

    //void Start()
    //{
        //string faceName = face.name;
        //switch (faceName)
        //{
        //    case "Cara1":
        //        DiceNumber = 1;
        //        break;

        //    case "Cara2":
        //        DiceNumber = 2;
        //        break;

        //    case "Cara3":
        //        DiceNumber = 3;
        //        break;

        //    case "Cara4":
        //        DiceNumber = 4;
        //        break;

        //    case "Cara5":
        //        DiceNumber = 5;
        //        break;

        //    case "Cara6":
        //        DiceNumber = 6;
        //        break;

        //    default:
        //        print("Dado");
        //        break;
        //}
    //}

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

                    // Genera un número aleatorio entre 0 y 5
                    int randomNumber = random.Next(0, diceAnimations.Count);
                    print(randomNumber);

                    animator.Play(diceAnimations[0]);
                }
            }
        }
    }   
}