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
                $"Por cada n�mero {Num} que aparezca en el dado se eliminar� un elemento {Element} de la mochila.",
                $"Por cada n�mero {Num} que aparezca en el dado se a�adir� un elemento {Element} de la mochila.",
                $"Si aparece un n�mero {Num} en el dado se a�adir� un elemento {Element} a la mochila.",
                $"Si aparece un n�mero {Num} en el dado se eliminar� un elemento {Element} a la mochila.",
                $"Durante las pr�ximas 2 tiradas si sacas un {Num} volver�s a la casilla de inicio.",
                $"Durante las pr�ximas 3 tiradas si obtienes un elemento {Element} se convertir� en elemento {ChangeElement}.",
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