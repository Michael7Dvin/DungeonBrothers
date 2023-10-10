using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface IEnemyBehaviour
    {
        public UniTask DoTurn();
    }
}