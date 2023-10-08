using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Characters.Logic;
using CodeBase.Gameplay.Characters.View;
using CodeBase.UI.TurnQueue;
using UnityEngine;

namespace CodeBase.Gameplay.Characters
{
    public interface ICharacter
    {
        public CharacterID CharacterID { get; }
        public CharacterTeam CharacterTeam { get; }
        public MovementStats MovementStats { get; }
        public CharacterStats CharacterStats { get; }
        public CharacterDamage CharacterDamage { get; }
        public ICharacterLogic CharacterLogic { get; }
        public ICharacterView CharacterView { get; }
        public Transform Transform { get; }
        public Vector2Int Coordinate { get; }
        void UpdateCoordinate(Vector2Int coordinate);
    }
}