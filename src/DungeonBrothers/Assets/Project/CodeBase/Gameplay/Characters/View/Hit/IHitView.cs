using Cysharp.Threading.Tasks;

namespace Project.CodeBase.Gameplay.Characters.View.Hit
{
    public interface IHitView
    {
        UniTask TakeHit();
    }
}