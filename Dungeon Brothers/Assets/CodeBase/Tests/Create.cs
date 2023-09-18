using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Randomise;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.UnitsProvider;

namespace CodeBase.Tests
{
    public class Create
    {
        public static ICharacter Character(int level,
            int intelligence, 
            int strength, 
            int dexterity,
            int initiative)
        {
            ICharacter character1 = new Character(new CharacterStats(level, intelligence, strength, dexterity, initiative),
                new CharacterLogic());
            return character1;
        }

        public static CharactersProvider CharactersProvider()
        {
            CharactersProvider charactersProvider = new CharactersProvider();
            return charactersProvider;
        }

        public static TurnQueue TurnQueue(CharactersProvider charactersProvider)
        {
            TurnQueue turnQueue = new TurnQueue(new RandomService(), charactersProvider,
                new CustomLogger(new LogWriter()));
            return turnQueue;
        }
    }
}