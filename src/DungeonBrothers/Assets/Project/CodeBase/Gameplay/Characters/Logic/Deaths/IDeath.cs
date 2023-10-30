using UniRx;

namespace Project.CodeBase.Gameplay.Characters.Logic.Deaths
{
    public interface IDeath
    {
        IReactiveCommand<Unit> Died { get; }
        public void Die();
    }
}