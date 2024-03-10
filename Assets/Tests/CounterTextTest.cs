using NUnit.Framework;
using Scripts.CounterText;

namespace Tests
{
    public class CounterTextTest
    {
        [Test]
        public void CounterText_Constructor_Should_Return_New_Card_With_Properties_Set_Correctly()
        {
            // Arrange
            string waterCounter = "3";
            string sandCounter = "5";
            string fireCounter = "7";
            string grassCounter = "2";
            string woodCounter = "6";

            // Act
            CounterText counterText = new(waterCounter, sandCounter, fireCounter, grassCounter, woodCounter);

            // Assert
            Assert.AreEqual(waterCounter, counterText.WaterCounter);
            Assert.AreEqual(sandCounter, counterText.SandCounter);
            Assert.AreEqual(fireCounter, counterText.FireCounter);
            Assert.AreEqual(grassCounter, counterText.GrassCounter);
            Assert.AreEqual(woodCounter, counterText.WoodCounter);
        }
    }
}
