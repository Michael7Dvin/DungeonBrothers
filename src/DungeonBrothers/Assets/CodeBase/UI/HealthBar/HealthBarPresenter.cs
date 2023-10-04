using System;
using CodeBase.Gameplay.Characters.Logic;
using UniRx;
using UnityEngine;

namespace CodeBase.UI.HealthBar
{
    public class HealthBarPresenter : MonoBehaviour
    {
        [SerializeField] private HealthBarView _healthBarView;
        
        private Health _health;
        
        private readonly CompositeDisposable _disposable = new();

        public void Construct(Health health)
        {
            _health = health;
        }

        public void Initialize()
        {
            _health.HealthPoints
                .Skip(1)
                .Subscribe(_healthPoints => _healthBarView.UpdateHealthBar((float)_healthPoints / _health.MaxHealthPoints))
                .AddTo(_disposable);

            _health.Died
                .Subscribe(unit => Destroy(gameObject))
                .AddTo(_disposable);
        }


        private void OnDestroy() =>
            _disposable.Clear();
    }
}