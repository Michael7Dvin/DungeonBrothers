using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.UnitsProvider;

namespace CodeBase.Tests
{
    public class Setup
    {
        public static ITurnQueue TurnQueue(CharactersProvider charactersProvider)
        {
            ITurnQueue turnQueue = Create.TurnQueue(charactersProvider);
            turnQueue.Initialize();
            return turnQueue;
        }
    }
}