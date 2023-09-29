namespace CodeBase.Gameplay.Services.Random
{
    public class RandomService : IRandomService
    {
        public bool DoFiftyFifty() => UnityEngine.Random.value > 0.5f;
    }
}