using UniRx;

namespace Project.CodeBase.Gameplay.Characters.Logic.Healths
{
    public interface IHealth : IDamageable, IHealable
    {
        int MaxHealthPoints { get; }
        IReadOnlyReactiveProperty<int> HealthPoints { get; }
    }
}