using CodeBase.Gameplay.UI.TurnQueue;

namespace CodeBase.Gameplay.Characters
{
    public class Character : ICharacter
    {
        public Character(CharacterID characterID, 
            CharacterStats characterStats,
            CharacterLogic characterLogic)
        {
            CharacterID = characterID;
            CharacterStats = characterStats;
            CharacterLogic = characterLogic;
        }

        public CharacterID CharacterID { get; }
        public CharacterStats CharacterStats { get; }
        public CharacterLogic CharacterLogic { get; }
    }
}