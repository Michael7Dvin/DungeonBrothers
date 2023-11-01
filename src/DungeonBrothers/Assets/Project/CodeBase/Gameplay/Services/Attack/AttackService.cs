using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Gameplay.Services.PathFinder;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Infrastructure.Services.Logger;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;

namespace Project.CodeBase.Gameplay.Services.Attack
{
    public class AttackService : IAttackService
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IPathFinder _pathFinder;
        private readonly ICustomLogger _customLogger;

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

        public async UniTask Attack(ICharacter character)
        {
            ICharacter activeCharacter = _turnQueue.ActiveCharacter.Value;

            if (CanAttackEnemy(character, activeCharacter) == false)
                return;

            await character.View.HitView.TakeHit();
            character.Logic.Health.TakeDamage(activeCharacter.Damage.GetCharacterDamage());
            
            _turnQueue.SetNextTurn();
        }

        public bool CanAttackEnemy(ICharacter character, ICharacter activeCharacter)
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

            return pathFindingResults.ObstaclesCoordinates.Contains(character.Logic.Movement.Coordinates);
        }

        public PathFindingResults GetPathFindingResults(ICharacter activeCharacter)
        {
            switch (activeCharacter.Damage.CharacterAttackType)
            {
                case CharacterAttackType.Melee:
                    return _pathFinder.CalculatePaths(activeCharacter.Logic.Movement.Coordinates, _meleeRange, 
                        false);
                
                case CharacterAttackType.Ranged:
                    return _pathFinder.CalculatePaths(activeCharacter.Logic.Movement.Coordinates, _rangedRange, 
                        true);
                default:
                    _customLogger.LogError(
                        new Exception($"{activeCharacter.Damage.CharacterAttackType}, doesn't exist"));
                    return new PathFindingResults();
            }
        }

        private bool IsSelf(ICharacter character, ICharacter activeCharacter) =>
            character == activeCharacter;

        private bool IsAlly(ICharacter character, ICharacter activeCharacter) =>
            character.Team == activeCharacter.Team;
    }
}