using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface IAttackBehaviour
    {
        public UniTask DoTurn();
    }
}