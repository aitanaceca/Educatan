using Scripts.Levels;
using System;

namespace Scripts.LevelElements
{
    public class LevelElements
    {
        public int WaterMaterial { get; set; }
        public int SandMaterial { get; set; }
        public int FireMaterial { get; set; }
        public int WoodMaterial { get; set; }
        public int GrassMaterial { get; set; }

        private void SetMaterials(int waterMaterial, int sandMaterial, int fireMaterial, int woodMaterial, int grassMaterial)
        {
            WaterMaterial = waterMaterial;
            SandMaterial = sandMaterial;
            FireMaterial = fireMaterial;
            WoodMaterial = woodMaterial;
            GrassMaterial = grassMaterial;
        }

        public LevelElements(Level level)
        {
            Console.WriteLine("level", level);
            switch (level)
            {
                case Level.ONE:
                    SetMaterials(4, 4, 3, 4, 4);
                    break;
                case Level.TWO:
                    SetMaterials(3, 6, 2, 4, 4);
                    break;
                case Level.THREE:
                    SetMaterials(3, 8, 2, 3, 3);
                    break;
                case Level.FOUR:
                    SetMaterials(2, 10, 1, 3, 3);
                    break;
                default:
                    SetMaterials(4, 4, 3, 4, 4);
                    break;
            }
        }
    }
}