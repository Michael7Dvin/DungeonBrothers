using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.PathFinder;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.Services.Attack
{
    public interface IAttackService
    {
        public UniTask Attack(ICharacter character);
        public PathFindingResults GetPathFindingResults(ICharacter activeCharacter);
        public bool TryAttackEnemy(ICharacter character, ICharacter activeCharacter);
    }
}