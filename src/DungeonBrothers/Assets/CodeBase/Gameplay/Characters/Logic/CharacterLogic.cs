namespace CodeBase.Gameplay.Characters.Logic
{
    public class CharacterLogic : ICharacterLogic
    {
        public CharacterLogic(Health health)
        {
            Health = health;
        }
        
        public Health Health { get; }
    }
}