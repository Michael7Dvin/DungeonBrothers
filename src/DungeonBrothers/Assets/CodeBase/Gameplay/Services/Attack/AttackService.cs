using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.Logic;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Attack
{
    public class AttackService : IAttackService
    {
        private const int MeleeRange = 1;
        private const int RangedRange = 3;
        
        private readonly ITurnQueue _turnQueue;
        private readonly IPathFinder _pathFinder;
        private readonly ICustomLogger _customLogger;
        
        public AttackService(ITurnQueue turnQueue,
            IPathFinder pathFinder,
            ICustomLogger customLogger)
        {
            _turnQueue = turnQueue;
            _pathFinder = pathFinder;
            _customLogger = customLogger;
        }

        public void Attack(ICharacter character)
        {
            ICharacter activeCharacter = _turnQueue.ActiveCharacter.Value;
            
            if (TryAttackEnemy(character, activeCharacter) == false)
                return;

            _turnQueue.SetNextTurn();
            character.CharacterLogic.Health.TakeDamage(activeCharacter.CharacterDamage.GetCharacterDamage());
        }

        private bool TryAttackEnemy(ICharacter character, ICharacter activeCharacter)
        {
            if (TryAttackYourSelf(character, activeCharacter))
                return false;

            if (TryAttackTeammate(character, activeCharacter))
                return false;
            
            int pathCost = GetPathCost(character, activeCharacter);

            switch (activeCharacter.CharacterDamage.CharacterAttackType)
            {
                case CharacterAttackType.Melee:
                    return pathCost == MeleeRange;
                case CharacterAttackType.Ranged:
                    return pathCost <= RangedRange && pathCost > 0;
                default:
                    _customLogger.LogError(
                        new Exception($"{activeCharacter.CharacterDamage.CharacterAttackType}, doesn't exist"));
                    return false;
            }
        }

        private int GetPathCost(ICharacter character, 
            ICharacter activeCharacter)
        {
            PathFindingResults pathFindingResults = GetPathFindingResults(activeCharacter);
            
            List<Vector2Int> path = pathFindingResults.GetPathTo(character.Coordinate, true);

            if (path == null)
                return 0;

            return path.Count;
        }

        private PathFindingResults GetPathFindingResults(ICharacter activeCharacter)
        {
            switch (activeCharacter.CharacterDamage.CharacterAttackType)
            {
                case CharacterAttackType.Melee:
                    return _pathFinder.CalculatePaths(activeCharacter.Coordinate, MeleeRange, 
                        true);
                
                case CharacterAttackType.Ranged:
                    return _pathFinder.CalculatePaths(activeCharacter.Coordinate, RangedRange, 
                        true);
                default:
                    _customLogger.LogError(
                        new Exception($"{activeCharacter.CharacterDamage.CharacterAttackType}, doesn't exist"));
                    return new PathFindingResults();
            }
        }

        private bool TryAttackYourSelf(ICharacter character, ICharacter activeCharacter) =>
            character == activeCharacter;

        private bool TryAttackTeammate(ICharacter character, ICharacter activeCharacter) =>
            character.CharacterTeam == activeCharacter.CharacterTeam;
    }
}