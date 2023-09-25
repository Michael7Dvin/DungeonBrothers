using CodeBase.Common.Observables;
using UnityEngine;

namespace CodeBase.Gameplay.Characters
{
    public class Character : MonoBehaviour, ICharacter
    {
        private readonly Observable<Vector2Int> _coordinate = new();
        public IReadOnlyObservable<Vector2Int> Coordinate => _coordinate;
        
        public void Construct(CharacterID characterID,
            CharacterTeam characterTeam,
            CharacterStats characterStats,
            ICharacterLogic characterLogic)
        {
            CharacterID = characterID;
            CharacterTeam = characterTeam;
            CharacterStats = characterStats;
            CharacterLogic = characterLogic;
        }
        public CharacterTeam CharacterTeam { get; private set; }
        public CharacterID CharacterID { get; private set; }
        public CharacterStats CharacterStats { get; private set; }
        public ICharacterLogic CharacterLogic { get; private set; }
        
        public void UpdateCoordinate(Vector2Int coordinate) => _coordinate.Value = coordinate;
    }
}