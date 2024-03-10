using NUnit.Framework;
using Scripts.CardImage;

namespace Tests
{
    public class ImageTest
    {
        [Test]
        public void CardImage_Constructor_Should_Return_New_Image_With_Properties_Set_Correctly()
        {
            // Arrange
            int id = 4;
            string imageBase64 = "data:image/png;base64,iVBORw0KGgo";

            // Act
            Image image = new(id, imageBase64);

            // Assert
            Assert.AreEqual(id, image.Id);
            Assert.AreEqual(imageBase64, image.ImageBase64);
        }
    }
}
