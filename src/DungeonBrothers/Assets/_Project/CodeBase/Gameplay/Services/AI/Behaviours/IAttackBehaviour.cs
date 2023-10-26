using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface IAttackBehaviour
    {
        public UniTask DoTurn();
    }
}