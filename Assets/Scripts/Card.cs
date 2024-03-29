using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Database;

// prueba
namespace Scripts.Card
{
    public class Card
    {
        public string Element { get; set; }
        public int Num { get; set; }
        public int ElementCount { get; set; }
        public int MaxElementCount { get; set; }
        public string ChangeElement { get; set; }

        public List<string> Elements { get; set; }
        public List<string> Cards { get; set; }

        private void LoadCardsText()
        {
            Database.Database database = Database.Database.InitializeDatabase("Database.db");
            Cards = database.GetTextTableData(Num, Element, ChangeElement);
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