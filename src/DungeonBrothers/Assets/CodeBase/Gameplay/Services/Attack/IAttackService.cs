using CodeBase.Gameplay.Characters;

namespace CodeBase.Gameplay.Services.Attack
{
    public interface IAttackService
    {
        public void Attack(ICharacter character);
    }
}