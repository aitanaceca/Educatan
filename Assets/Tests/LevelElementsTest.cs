using NUnit.Framework;
using System.Collections.Generic;
using Scripts.LevelElements;
using Scripts.Levels;

namespace Tests
{
    public class LevelElementsTest
    {
        List<int> numberOfElements = new() { 1, 2, 3, 4, 6, 8, 10 };

        [Test]
        public void LevelElements_Constructor_Should_Return_First_Level_With_Properties_Set_Correctly()
        {
            // Arrange
            int firstLevel = (int)Level.ONE;

            // Act
            LevelElements level = new(firstLevel);

            // Assert
            Assert.AreEqual(numberOfElements[3], level.WaterMaterial);
            Assert.AreEqual(numberOfElements[3], level.SandMaterial);
            Assert.AreEqual(numberOfElements[2], level.FireMaterial);
            Assert.AreEqual(numberOfElements[3], level.GrassMaterial);
            Assert.AreEqual(numberOfElements[3], level.WoodMaterial);
        }

        [Test]
        public void LevelElements_Constructor_Should_Return_Second_Level_With_Properties_Set_Correctly()
        {
            // Arrange
            int firstLevel = (int)Level.TWO;

            // Act
            LevelElements level = new(firstLevel);

            // Assert
            Assert.AreEqual(numberOfElements[2], level.WaterMaterial);
            Assert.AreEqual(numberOfElements[4], level.SandMaterial);
            Assert.AreEqual(numberOfElements[1], level.FireMaterial);
            Assert.AreEqual(numberOfElements[3], level.GrassMaterial);
            Assert.AreEqual(numberOfElements[3], level.WoodMaterial);
        }

        [Test]
        public void LevelElements_Constructor_Should_Return_Third_Level_With_Properties_Set_Correctly()
        {
            // Arrange
            int firstLevel = (int)Level.THREE;

            // Act
            LevelElements level = new(firstLevel);

            // Assert
            Assert.AreEqual(numberOfElements[2], level.WaterMaterial);
            Assert.AreEqual(numberOfElements[5], level.SandMaterial);
            Assert.AreEqual(numberOfElements[1], level.FireMaterial);
            Assert.AreEqual(numberOfElements[2], level.GrassMaterial);
            Assert.AreEqual(numberOfElements[2], level.WoodMaterial);
        }

        [Test]
        public void LevelElements_Constructor_Should_Return_Fourth_Level_With_Properties_Set_Correctly()
        {
            // Arrange
            int firstLevel = (int)Level.FOUR;

            // Act
            LevelElements level = new(firstLevel);

            // Assert
            Assert.AreEqual(numberOfElements[1], level.WaterMaterial);
            Assert.AreEqual(numberOfElements[6], level.SandMaterial);
            Assert.AreEqual(numberOfElements[0], level.FireMaterial);
            Assert.AreEqual(numberOfElements[2], level.GrassMaterial);
            Assert.AreEqual(numberOfElements[2], level.WoodMaterial);
        }

        [Test]
        public void LevelElements_Constructor_Should_Return_Default_Level_With_Properties_Set_Correctly()
        {
            // Arrange
            int firstLevel = 0;

            // Act
            LevelElements level = new(firstLevel);

            // Assert
            Assert.AreEqual(numberOfElements[3], level.WaterMaterial);
            Assert.AreEqual(numberOfElements[3], level.SandMaterial);
            Assert.AreEqual(numberOfElements[2], level.FireMaterial);
            Assert.AreEqual(numberOfElements[3], level.GrassMaterial);
            Assert.AreEqual(numberOfElements[3], level.WoodMaterial);
        }

        [Test]
        public void GetRandomElement_Should_Return_Property_From_LevelElements_Class()
        {
            // Arrange
            List<string> levelElementsProperties = new() { "WaterMaterial", "SandMaterial", "FireMaterial", "GrassMaterial", "WoodMaterial" };
            LevelElements levelElements = new((int)Level.ONE);

            // Act
            string randomElement = levelElements.GetRandomElement();

            // Assert
            Assert.Contains(randomElement, levelElementsProperties);
        }
    }
}
