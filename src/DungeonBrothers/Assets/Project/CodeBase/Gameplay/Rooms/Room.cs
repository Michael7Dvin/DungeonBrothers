using System.Collections.Generic;
using Project.CodeBase.Gameplay.Characters;
using Sirenix.OdinInspector;
using UniRx;

namespace Project.CodeBase.Gameplay.Rooms
{
    public class Room : SerializedMonoBehaviour
    {
        public RoomInfo RoomInfo;

        public Dictionary<Direction, Door> Doors;

        private readonly ReactiveCollection<ICharacter> _enemies = new();
        
        public void EnableRoom() => 
            gameObject.SetActive(true);

        public void DisableRoom() => 
            gameObject.SetActive(false);

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