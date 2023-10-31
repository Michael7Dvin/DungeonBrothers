using Cysharp.Threading.Tasks;

namespace Project.CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface IAttackBehaviour
    {
        public UniTask DoTurn();
    }
}