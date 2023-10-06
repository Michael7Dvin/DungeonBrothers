using System;
using System.Collections.Generic;
using System.Linq;
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
            
            character.CharacterLogic.Health.TakeDamage(activeCharacter.CharacterDamage.GetCharacterDamage());
            _turnQueue.SetNextTurn();
        }

        private bool TryAttackEnemy(ICharacter character, ICharacter activeCharacter)
        {
            if (IsSelf(character, activeCharacter))
                return false;

            if (IsAlly(character, activeCharacter))
                return false;

            if (IsInRange(character, activeCharacter) == false)
                return false;
            
            return true;
        }

        private bool IsInRange(ICharacter character, 
            ICharacter activeCharacter)
        {
            PathFindingResults pathFindingResults = GetPathFindingResults(activeCharacter);

            if (pathFindingResults.NotWalkableCoordinates.Contains(character.Coordinate))
                return true;

            return false;
        }

        public PathFindingResults GetPathFindingResults(ICharacter activeCharacter)
        {
            switch (activeCharacter.CharacterDamage.CharacterAttackType)
            {
                case CharacterAttackType.Melee:
                    return _pathFinder.CalculatePaths(activeCharacter.Coordinate, _meleeRange, 
                        false);
                
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