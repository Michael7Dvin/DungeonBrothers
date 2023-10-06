using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.Logic;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Attack
{
    public class AttackService : IAttackService
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IPathFinder _pathFinder;
        private readonly ICustomLogger _customLogger;

        private AllGameBalanceConfig _gameBalanceConfig;
        
        private readonly int _meleeRange;
        private readonly int _rangedRange;
        public AttackService(ITurnQueue turnQueue,
            IPathFinder pathFinder,
            ICustomLogger customLogger,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _pathFinder = pathFinder;
            _customLogger = customLogger;

            _meleeRange = staticDataProvider.GameBalanceConfig.AttackRangeConfig.MeleeRange;
            _rangedRange = staticDataProvider.GameBalanceConfig.AttackRangeConfig.RangedRange;
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
            if (IsSelf(character, activeCharacter))
                return false;

            if (IsAlly(character, activeCharacter))
                return false;
            
            int pathCost = GetPathCost(character, activeCharacter);

            if (pathCost <= 0)
                return false;
            
            switch (activeCharacter.CharacterDamage.CharacterAttackType)
            {
                case CharacterAttackType.Melee:
                    return pathCost <= _meleeRange;
                case CharacterAttackType.Ranged:
                    return pathCost <= _rangedRange;
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
                    return _pathFinder.CalculatePaths(activeCharacter.Coordinate, _meleeRange, 
                        true);
                
                case CharacterAttackType.Ranged:
                    return _pathFinder.CalculatePaths(activeCharacter.Coordinate, _rangedRange, 
                        true);
                default:
                    _customLogger.LogError(
                        new Exception($"{activeCharacter.CharacterDamage.CharacterAttackType}, doesn't exist"));
                    return new PathFindingResults();
            }
        }

        private bool IsSelf(ICharacter character, ICharacter activeCharacter) =>
            character == activeCharacter;

        private bool IsAlly(ICharacter character, ICharacter activeCharacter) =>
            character.CharacterTeam == activeCharacter.CharacterTeam;
    }
}