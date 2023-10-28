namespace _Project.CodeBase.Gameplay.Characters.Logic
{
    public class CharacterLogic : ICharacterLogic
    {
        public CharacterLogic(Health.Health health)
        {
            Health = health;
        }
        
        public Health.Health Health { get; }
    }
}