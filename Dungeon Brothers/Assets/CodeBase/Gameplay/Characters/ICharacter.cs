using CodeBase.Common.Observables;
using UnityEngine;

namespace CodeBase.Gameplay.Characters
{
    public interface ICharacter
    {
        public CharacterID CharacterID { get; }
        public CharacterTeam CharacterTeam { get; }
        
        public CharacterStats CharacterStats { get; }
        
        public ICharacterLogic CharacterLogic { get; }
        
        public IReadOnlyObservable<Vector2Int> Coordinate { get; }
    }
}