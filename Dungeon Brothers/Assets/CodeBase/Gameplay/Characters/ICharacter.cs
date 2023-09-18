namespace CodeBase.Gameplay.Characters
{
    public interface ICharacter
    {
        public CharacterStats CharacterStats { get; }
        
        public CharacterLogic CharacterLogic { get; }
    }
}