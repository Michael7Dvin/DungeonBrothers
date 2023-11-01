using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Services.Attack;
using Project.CodeBase.Gameplay.Services.TurnQueue;

namespace Project.CodeBase.Gameplay.Services.AI.Behaviours
{
    public class AttackBehaviour : IAttackBehaviour
    {
        private readonly ITurnQueue _turnQueue;
        private readonly ISelectTargetBehaviour _selectTargetBehaviour;
        private readonly IMoveBehaviour _moveBehaviour;
        private readonly IAttackService _attackService;
        
        public AttackBehaviour(ITurnQueue turnQueue, 
            ISelectTargetBehaviour selectTargetBehaviour,
            IMoveBehaviour moveBehaviour,
            IAttackService attackService)
        {
            _turnQueue = turnQueue;
            _selectTargetBehaviour = selectTargetBehaviour;
            _moveBehaviour = moveBehaviour;
            _attackService = attackService;
        }

        public async UniTask DoTurn()
        {
            ICharacter target = _selectTargetBehaviour.GetTarget();

            if (target == null)
                return;

            if (await TryAttack(target))
                return;
            
            await _moveBehaviour.Move(_turnQueue.ActiveCharacter.Value, target);
            
            if (await TryAttack(target) == false)
                _turnQueue.SetNextTurn();
        }

        private async UniTask<bool> TryAttack(ICharacter target)
        {
            if (_attackService.CanAttackEnemy(target, _turnQueue.ActiveCharacter.Value))
            {
                await _attackService.Attack(target);
                return true;
            }

            return false;
        }
    }
}