using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface IMeleeBehaviour
    {
        public UniTask DoTurn();
    }
}