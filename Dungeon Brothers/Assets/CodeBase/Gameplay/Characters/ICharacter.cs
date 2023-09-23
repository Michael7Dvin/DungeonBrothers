namespace CodeBase.Gameplay.Characters
{
    public interface ICharacter
    {
        public CharacterID CharacterID { get; }
        public CharacterTeam CharacterTeam { get; }
        
        public CharacterStats CharacterStats { get; }
        
        public ICharacterLogic CharacterLogic { get; }
    }
}