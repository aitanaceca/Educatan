using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Scripts.Database
{
    public class Database : MonoBehaviour
    {
        public static Database instance;
        private static string databaseFilePath = "URI=file:Assets/Database/Database.db";

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            CreateTable();
            InsertDataToCardsTable();
            InsertDataToImagesTable();
        }

        private void CreateTable()
        {
            using (var connection = new SqliteConnection(databaseFilePath))
            {
                connection.Open();

                using (var dbCommand = connection.CreateCommand())
                {
                    string createCardsTable =
                      "CREATE TABLE IF NOT EXISTS cards (id INTEGER PRIMARY KEY AUTOINCREMENT, cardText VARCHAR(255))";

                    string createImagesTable =
                      "CREATE TABLE IF NOT EXISTS images (id INTEGER PRIMARY KEY AUTOINCREMENT, image VARCHAR(255))";

                    dbCommand.CommandText = createCardsTable;
                    dbCommand.ExecuteNonQuery();
                    dbCommand.CommandText = createImagesTable;
                    dbCommand.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void InsertDataToCardsTable()
        {
            using (var connection = new SqliteConnection(databaseFilePath))
            {
                connection.Open();

                using (var dbCommand = connection.CreateCommand())
                {
                    //Check if table has data.
                    dbCommand.CommandText = "SELECT * FROM cards LIMIT 1";
                    IDataReader dbReader = dbCommand.ExecuteReader();

                    if (!dbReader.Read())
                    {
                        dbCommand.CommandText = "INSERT INTO cards (id, cardText) VALUES (1, 'Por cada n�mero {Num} que aparezca en el dado se eliminar� un elemento {Element} de la mochila.')";
                        dbCommand.ExecuteNonQuery();
                        dbCommand.CommandText = "INSERT INTO cards (id, cardText) VALUES (2, 'Por cada n�mero {Num} que aparezca en el dado se a�adir� un elemento {Element} de la mochila.')";
                        dbCommand.ExecuteNonQuery();
                        dbCommand.CommandText = "INSERT INTO cards (id, cardText) VALUES (3, 'Si aparece un n�mero {Num} en el dado se a�adir� un elemento {Element} a la mochila.')";
                        dbCommand.ExecuteNonQuery();
                        dbCommand.CommandText = "INSERT INTO cards (id, cardText) VALUES (4, 'Si aparece un n�mero {Num} en el dado se eliminar� un elemento {Element} a la mochila.')";
                        dbCommand.ExecuteNonQuery();
                        dbCommand.CommandText = "INSERT INTO cards (id, cardText) VALUES (5, 'Durante las pr�ximas 2 tiradas si sacas un {Num} volver�s a la casilla de inicio.')";
                        dbCommand.ExecuteNonQuery();
                        dbCommand.CommandText = "INSERT INTO cards (id, cardText) VALUES (6, 'Durante las pr�ximas 3 tiradas si obtienes un elemento {Element} se convertir� en elemento {ChangeElement}.')";
                        dbCommand.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }

        private void InsertDataToImagesTable()
        {
            using (var connection = new SqliteConnection(databaseFilePath))
            {
                connection.Open();

                using (var dbCommand = connection.CreateCommand())
                {
                    //Check if table has data.
                    dbCommand.CommandText = "SELECT * FROM images LIMIT 1";
                    IDataReader dbReader = dbCommand.ExecuteReader();

                    if (!dbReader.Read())
                    {
                        // TODO: A�adir im�genes cuando la pantalla de c�mo jugar est� terminada.
                    }
                }

                connection.Close();
            }
        }

        private static string ReplaceVariablesInCardText(string cardText, int num, string element, string changeElement)
        {
            string result = cardText.Replace("{Num}", num.ToString());
            result = result.Replace("{Element}", element);
            result = result.Replace("{ChangeElement}", changeElement);
            return result;
        }

        public static List<string> GetCardsTableData(int num, string element, string changeElement)
        {
            List<string> result = new List<string>() { };

            using (var connection = new SqliteConnection(databaseFilePath))
            {
                connection.Open();

                using (var dbCommand = connection.CreateCommand())
                {
                    dbCommand.CommandText = "SELECT cardText FROM cards";
                    IDataReader dbReader = dbCommand.ExecuteReader();
                    while (dbReader.Read())
                    {
                        string dbCardText = dbReader[0].ToString();
                        string cardText = ReplaceVariablesInCardText(dbCardText, num, element, changeElement);
                        result.Add(cardText);
                        print(cardText);
                    }
                }

                connection.Close();
                return result;
            }
        }
    }
}
