using System;

namespace Project.CodeBase.Gameplay.Services.Random
{
    public class RandomService : IRandomService
    {
        public int DoRandomInRange(int min, int max) =>
            UnityEngine.Random.Range(min, max);
        
        public bool DoFiftyFifty() => UnityEngine.Random.value > 0.5f;
    }
}