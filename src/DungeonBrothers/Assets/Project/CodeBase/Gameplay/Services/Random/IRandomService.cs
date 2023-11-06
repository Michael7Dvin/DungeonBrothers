namespace Project.CodeBase.Gameplay.Services.Random
{
    public interface IRandomService
    {
        public int DoRandomInRange(int min, int max);
        public bool DoFiftyFifty();
    }
}