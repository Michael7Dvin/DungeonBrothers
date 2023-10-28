using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using _Project.CodeBase.Gameplay.Characters.Logic;
using _Project.CodeBase.Gameplay.Characters.View;
using UniRx;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Characters
{
    public class Character : MonoBehaviour, ICharacter
    {
        public void Construct(CharacterID characterID,
            CharacterTeam characterTeam,
            CharacterStats characterStats,
            CharacterDamage characterDamage,
            ICharacterLogic characterLogic,
            ICharacterView characterView)
        {
            ID = characterID;
            Team = characterTeam;
            Stats = characterStats;
            Damage = characterDamage;
            Logic = characterLogic;
            View = characterView;
        }
        
        public Vector2Int Coordinate { get; private set; }
        public CharacterTeam Team { get; private set; }
        public CharacterID ID { get; private set; }
        public CharacterStats Stats { get; private set; }
        public CharacterDamage Damage { get; private set; }
        public ICharacterLogic Logic { get; private set; }
        public ICharacterView View { get; private set; }

        public Transform Transform =>
            transform;

        public void UpdateCoordinate(Vector2Int coordinate) => 
            Coordinate = coordinate;
    }
}