using Scripts.Levels;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Scripts.LevelElements
{
    public class LevelElements
    {
        public int WaterMaterial { get; set; }
        public int SandMaterial { get; set; }
        public int FireMaterial { get; set; }
        public int GrassMaterial { get; set; }
        public int WoodMaterial { get; set; }

        private void SetMaterials(int waterMaterial, int sandMaterial, int fireMaterial, int grassMaterial, int woodMaterial)
        {
            WaterMaterial = waterMaterial;
            SandMaterial = sandMaterial;
            FireMaterial = fireMaterial;
            GrassMaterial = grassMaterial;
            WoodMaterial = woodMaterial;
        }

        public LevelElements(int level)
        {
            switch (level)
            {
                case (int)Level.ONE:
                    SetMaterials(4, 4, 3, 4, 4);
                    break;
                case (int)Level.TWO:
                    SetMaterials(3, 6, 2, 4, 4);
                    break;
                case (int)Level.THREE:
                    SetMaterials(3, 8, 2, 3, 3);
                    break;
                case (int)Level.FOUR:
                    SetMaterials(2, 10, 1, 3, 3);
                    break;
                default:
                    SetMaterials(4, 4, 3, 4, 4);
                    break;
            }
        }

        public string GetRandomElement()
        {
            Type classType = this.GetType();
            PropertyInfo[] properties = classType.GetProperties();

            List<string> propertiesNames = new() { };

            foreach (PropertyInfo propertyInfo in properties)
            {
                var property = classType.GetProperty(propertyInfo.Name);

                if ((int)property.GetValue(this) > 0)
                {
                    propertiesNames.Add(property.Name);
                }
            }

            System.Random random = new();

            // Genera un número aleatorio entre 0 y 4
            int randomNumber = random.Next(0, propertiesNames.Count);

            return propertiesNames[randomNumber];
        }
    }
}