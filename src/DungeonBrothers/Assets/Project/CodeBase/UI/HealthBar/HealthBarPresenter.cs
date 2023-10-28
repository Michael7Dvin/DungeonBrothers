﻿using _Project.CodeBase.Gameplay.Characters.Logic;
using _Project.CodeBase.Gameplay.Characters.Logic.Health;
using UniRx;

namespace _Project.CodeBase.UI.HealthBar
{
    public class HealthBarPresenter
    {
        private HealthBarView _healthBarView;
        
        private Health _health;
        private readonly CompositeDisposable _disposable = new();

        public void Construct(Health health, HealthBarView healthBarView)
        {
            _health = health;
            _healthBarView = healthBarView;
        }

        public void Initialize()
        {
            _health.HealthPoints
                .Skip(1)
                .Subscribe(healthPoints => _healthBarView.UpdateHealthBar((float)healthPoints / _health.MaxHealthPoints))
                .AddTo(_disposable);

            _health.Died
                .Subscribe(_ => _healthBarView.Destroy())
                .AddTo(_disposable);
            
            _health.Died
                .Subscribe(_ => OnDestroy())
                .AddTo(_disposable);
        }
        
        private void OnDestroy() =>
            _disposable.Clear();
    }
}