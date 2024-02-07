using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Database;

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
            Cards = Database.Database.GetTableData("cardText", "cards", Num, Element, ChangeElement);
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