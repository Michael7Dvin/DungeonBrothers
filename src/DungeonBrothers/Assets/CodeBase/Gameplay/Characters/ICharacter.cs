using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Characters.Logic;
using CodeBase.Gameplay.Characters.View;
using UnityEngine;

namespace CodeBase.Gameplay.Characters
{
    public interface ICharacter
    {
        public CharacterID ID { get; }
        public CharacterTeam Team { get; }
        public CharacterStats Stats { get; }
        public CharacterDamage Damage { get; }
        public ICharacterLogic Logic { get; }
        public ICharacterView View { get; }
        public Transform Transform { get; }
        public Vector2Int Coordinate { get; }
        void UpdateCoordinate(Vector2Int coordinate);
    }
}