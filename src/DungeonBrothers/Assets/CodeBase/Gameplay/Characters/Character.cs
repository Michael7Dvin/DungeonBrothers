using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Characters.Logic;
using Codice.Client.BaseCommands.CheckIn.CodeReview;
using UnityEngine;

namespace CodeBase.Gameplay.Characters
{
    public class Character : MonoBehaviour, ICharacter
    {
        public void Construct(CharacterID characterID,
            CharacterTeam characterTeam,
            MovementStats movementStats,
            CharacterStats characterStats,
            CharacterDamage characterDamage,
            ICharacterLogic characterLogic)
        {
            CharacterID = characterID;
            CharacterTeam = characterTeam;
            MovementStats = movementStats;
            CharacterStats = characterStats;
            CharacterDamage = characterDamage;
            CharacterLogic = characterLogic;
        }
        
        public Vector2Int Coordinate { get; private set; }
        public CharacterTeam CharacterTeam { get; private set; }
        public MovementStats MovementStats { get; private set; }
        public CharacterID CharacterID { get; private set; }
        public CharacterStats CharacterStats { get; private set; }
        public CharacterDamage CharacterDamage { get; private set; }
        public ICharacterLogic CharacterLogic { get; private set; }

        public Transform Transform =>
            transform;

        public void UpdateCoordinate(Vector2Int coordinate) => 
            Coordinate = coordinate;
    }
}