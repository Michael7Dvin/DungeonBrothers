namespace CodeBase.Gameplay.Characters
{
    public class Character : ICharacter
    {
        public void Construct(CharacterID characterID, 
            CharacterStats characterStats,
            ICharacterLogic characterLogic)
        {
            CharacterID = characterID;
            CharacterStats = characterStats;
            CharacterLogic = characterLogic;
        }

        public CharacterID CharacterID { get; private set; }
        public CharacterStats CharacterStats { get; private set; }
        public ICharacterLogic CharacterLogic { get; private set; }
    }
}