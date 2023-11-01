using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Services.PathFinder;

namespace Project.CodeBase.Gameplay.Services.Attack
{
    public interface IAttackService
    {
        public UniTask Attack(ICharacter character);
        public PathFindingResults GetPathFindingResults(ICharacter activeCharacter);
        public bool CanAttackEnemy(ICharacter character, ICharacter activeCharacter);
    }
}