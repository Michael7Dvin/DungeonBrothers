using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Gameplay.Characters.View.Hit
{
    public interface IHitView
    {
        public UniTask TakeHit();
    }
}