using UnityEngine;

namespace CodeBase.Gameplay.Services.Randomise
{
    public class RandomService : IRandomService
    {
        public bool DoFiftyFifty() => Random.value > 0.5f;
    }
}