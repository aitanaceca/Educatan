using NUnit.Framework;
using Scripts.RandomCardText;

namespace Tests
{
    public class TextTest
    {
        [Test]
        public void RandomCardText_Constructor_Should_Return_New_Text_With_Properties_Set_Correctly()
        {
            // Arrange
            int id = 4;
            string cardText = "Se va a eliminar un elemento fuego de la mochila.";

            // Act
            Text text = new(id, cardText);

            // Assert
            Assert.AreEqual(id, text.Id);
            Assert.AreEqual(cardText, text.CardText);
        }
    }
}
