using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters.Logic.Deaths
{
    public class Death : IDeath
    {
        private readonly GameObject _gameObject;
        private readonly ReactiveCommand _died = new();

        public Death(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public IReactiveCommand<Unit> Died => _died;
        
        public void Die()
        {
            Object.Destroy(_gameObject);
            _died?.Execute();
        }
    }
}