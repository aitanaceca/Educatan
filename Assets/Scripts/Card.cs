using System;
using System.Collections;
using System.Collections.Generic;

namespace Scripts.Card
{
    public class Card
    {
        const string WATER = "agua";
        const string FIRE = "fuego";
        const string WOOD = "madera";
        const string SAND = "arena";
        const string GRASS = "hierba";

        public string Element { get; set; }
        public int Num { get; set; }
        public int ElementCount { get; set; }
        public int MaxElementCount { get; set; }
        public string ChangeElement { get; set; }

        public List<string> Elements { get; set; }
        public List<string> Cards { get; set; }

        private void LoadCardsText()
        {
            Cards = new()
            {
                $"Por cada número {Num} que aparezca en el dado se eliminará un elemento {Element} de la mochila.",
                $"Por cada número {Num} que aparezca en el dado se añadirá un elemento {Element} de la mochila.",
                $"Si aparece un número {Num} en el dado se añadirá un elemento {Element} a la mochila.",
                $"Si aparece un número {Num} en el dado se eliminará un elemento {Element} a la mochila.",
                $"Durante las próximas 2 tiradas si sacas un {Num} volverás a la casilla de inicio.",
                $"Durante las próximas 3 tiradas si obtienes un elemento {Element} se convertirá en elemento {ChangeElement}.",
            };
        }

        public Card(string element, int num, string changeElement)
        {
            Element = element;
            Num = num;
            ChangeElement = changeElement;
            LoadCardsText();
        }
    }
}