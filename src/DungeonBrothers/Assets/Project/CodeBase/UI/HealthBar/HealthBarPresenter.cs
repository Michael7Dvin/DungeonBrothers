using Project.CodeBase.Gameplay.Characters.Logic.Deaths;
using Project.CodeBase.Gameplay.Characters.Logic.Healths;
using UniRx;

namespace Project.CodeBase.UI.HealthBar
{
    public class HealthBarPresenter
    {
        private HealthBarView _healthBarView;
        
        private IHealth _health;
        private IDeath _death;
        private readonly CompositeDisposable _disposable = new();

        public void Construct(IHealth health, IDeath death, HealthBarView healthBarView)
        {
            _health = health;
            _death = death;
            _healthBarView = healthBarView;
        }

        public void Initialize()
        {
            _health.HealthPoints
                .Skip(1)
                .Subscribe(healthPoints => _healthBarView.UpdateHealthBar((float)healthPoints / _health.MaxHealthPoints))
                .AddTo(_disposable);
            
            _death.Died
                .Subscribe(_ => _healthBarView.Destroy())
                .AddTo(_disposable);
            
            _death.Died
                .Subscribe(_ => OnDestroy())
                .AddTo(_disposable);
        }
        
        private void OnDestroy() =>
            _disposable.Clear();
    }
}