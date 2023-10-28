using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Services.PathFinder;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Gameplay.Services.Attack
{
    public interface IAttackService
    {
        public UniTask Attack(ICharacter character);
        public PathFindingResults GetPathFindingResults(ICharacter activeCharacter);
        public bool TryAttackEnemy(ICharacter character, ICharacter activeCharacter);
    }
}