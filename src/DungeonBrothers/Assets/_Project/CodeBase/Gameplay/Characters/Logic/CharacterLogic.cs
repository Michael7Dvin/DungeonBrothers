namespace _Project.CodeBase.Gameplay.Characters.Logic
{
    public class CharacterLogic : ICharacterLogic
    {
        public CharacterLogic(Health health,
            ICharacterAttack attack)
        {
            Health = health;
            Attack = attack;
        }
        
        public ICharacterAttack Attack { get; private set; }
        public Health Health { get; }
    }
}