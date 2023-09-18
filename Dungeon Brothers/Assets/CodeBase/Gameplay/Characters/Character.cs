namespace CodeBase.Gameplay.Characters
{
    public class Character : ICharacter
    {
        public Character(CharacterStats characterStats, CharacterLogic characterLogic)
        {
            CharacterStats = characterStats;
            CharacterLogic = characterLogic;
        }

        public CharacterStats CharacterStats { get; private set; }
        
        public CharacterLogic CharacterLogic { get; private set; }
    }
}