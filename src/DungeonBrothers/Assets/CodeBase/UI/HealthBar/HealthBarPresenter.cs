using System;
using CodeBase.Gameplay.Characters.Logic;
using UniRx;
using UnityEngine;

namespace CodeBase.UI.HealthBar
{
    public class HealthBarPresenter : MonoBehaviour
    {
        private Health _health;
        private HealthBarView _healthBarView;
        
        private readonly CompositeDisposable _disposable = new();

        public void Construct(Health health, 
            HealthBarView healthBarView)
        {
            _health = health;
            _healthBarView = healthBarView;
        }

        public void Initialize()
        {
            SpriteRenderer spriteRenderer;
            
            _health.HealthPoints
                .Skip(1)
                .Subscribe(_healthPoints => _healthBarView.UpdateHealthBar(_healthPoints / _health.MaxHealthPoints))
                .AddTo(_disposable);

            _health.Died
                .Subscribe(unit => Destroy(gameObject))
                .AddTo(_disposable);
        }

        private void OnDestroy() =>
            _disposable.Clear();
    }
}