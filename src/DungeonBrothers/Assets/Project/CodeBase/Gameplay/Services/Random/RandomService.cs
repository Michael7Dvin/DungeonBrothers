using System;

namespace Project.CodeBase.Gameplay.Services.Random
{
    public class RandomService : IRandomService
    {
        public float DoRandomInRange(float min, float max) =>
            UnityEngine.Random.Range(min, max);
        
        public bool DoFiftyFifty() => UnityEngine.Random.value > 0.5f;
    }
}