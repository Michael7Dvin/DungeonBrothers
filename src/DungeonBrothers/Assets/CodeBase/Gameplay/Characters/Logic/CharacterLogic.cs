namespace CodeBase.Gameplay.Characters.Logic
{
    public class CharacterLogic : ICharacterLogic
    {
        public Health Health { get; private set; }
        
        public CharacterLogic(Health health)
        {
            Health = health;
        }
    }
}