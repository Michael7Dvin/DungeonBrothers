using CodeBase.Gameplay.UI.TurnQueue;

namespace CodeBase.Gameplay.Characters
{
    public interface ICharacter
    {
        public CharacterID CharacterID { get; }
        
        public CharacterStats CharacterStats { get; }
        
        public CharacterLogic CharacterLogic { get; }
    }
}