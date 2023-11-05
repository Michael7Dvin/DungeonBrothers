namespace Project.CodeBase.Gameplay.Services.Random
{
    public interface IRandomService
    {
        public float DoRandomInRange(float min, float max);
        public bool DoFiftyFifty();
    }
}