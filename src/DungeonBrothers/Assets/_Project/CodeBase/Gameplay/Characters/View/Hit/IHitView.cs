using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Gameplay.Characters.View
{
    public interface IHitView
    {
        public UniTask TakeHit();
    }
}