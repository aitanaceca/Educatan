using Scripts.Levels;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Scripts.CounterText
{
    public class CounterText
    {
        public string WaterCounter { get; set; }
        public string SandCounter { get; set; }
        public string FireCounter { get; set; }
        public string GrassCounter { get; set; }
        public string WoodCounter { get; set; }

        public CounterText(string waterCounter, string sandCounter, string fireCounter, string grassCounter, string woodCounter)
        {
            WaterCounter = waterCounter;
            SandCounter = sandCounter;
            FireCounter = fireCounter;
            GrassCounter = grassCounter;
            WoodCounter = woodCounter;
        }
    }
}