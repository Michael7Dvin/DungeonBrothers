using System.Collections.Generic;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Tiles;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Rooms
{
    public class Room : SerializedMonoBehaviour
    {
        [SerializeField] private Tile[] _upDoors;
        [SerializeField] private Tile[] _leftDoors;
        [SerializeField] private Tile[] _rightDoors;
        [SerializeField] private Tile[] _downDoors;

        private readonly ReactiveCollection<ICharacter> _enemies = new();

        public void EnableRoom()
        {
            
        }

        public void DisableRoom()
        {
            
        }
        
        public void AddEnemy(ICharacter character)
        {
            _enemies.Add(character);
            CompositeDisposable disposable = new();

            character.Logic.Death.Died
                .Subscribe(_ => RemoveEnemy(character))
                .AddTo(disposable);

            void RemoveEnemy(ICharacter character)
            {
                _enemies.Remove(character);
                disposable.Clear();;
            }
        }
    }
}