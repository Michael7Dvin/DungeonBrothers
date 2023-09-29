﻿using CodeBase.Common.Observables;
using CodeBase.Gameplay.Characters.View;
using UnityEngine;

namespace CodeBase.Gameplay.Characters
{
    public class Character : MonoBehaviour, ICharacter
    {
        public void Construct(CharacterID characterID,
            CharacterTeam characterTeam,
            CharacterStats characterStats,
            ICharacterLogic characterLogic,
            CharacterAnimation characterAnimation)
        {
            CharacterID = characterID;
            CharacterTeam = characterTeam;
            CharacterStats = characterStats;
            CharacterLogic = characterLogic;
            CharacterAnimation = characterAnimation;
        }
        public Vector2Int Coordinate { get; private set; }
        public CharacterTeam CharacterTeam { get; private set; }
        public CharacterID CharacterID { get; private set; }
        public CharacterStats CharacterStats { get; private set; }
        public ICharacterLogic CharacterLogic { get; private set; }

        public CharacterAnimation CharacterAnimation { get; private set; }

        public void UpdateCoordinate(Vector2Int coordinate) => Coordinate = coordinate;
    }
}