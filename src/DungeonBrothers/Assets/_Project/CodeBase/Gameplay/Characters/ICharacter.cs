using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using _Project.CodeBase.Gameplay.Characters.Logic;
using _Project.CodeBase.Gameplay.Characters.View;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Characters
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