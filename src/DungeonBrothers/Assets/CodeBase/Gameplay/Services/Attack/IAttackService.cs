using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.PathFinder;

namespace CodeBase.Gameplay.Services.Attack
{
    public interface IAttackService
    {
        public void Attack(ICharacter character);
        public PathFindingResults GetPathFindingResults(ICharacter activeCharacter);
    }
}