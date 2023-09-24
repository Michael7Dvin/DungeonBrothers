using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Providers.LevelSpawner;
using CodeBase.Infrastructure.StateMachines.Gameplay.FSM;

namespace CodeBase.Infrastructure.StateMachines.Gameplay.States
{
    public class LevelLoadingState : IState
    {
        private readonly ILevelSpawner _levelSpawner;
        private readonly ITurnQueue _turnQueue;
        private readonly IGameplayStateMachine _gameplayStateMachine;

        public LevelLoadingState(ILevelSpawner levelSpawner, ITurnQueue turnQueue, IGameplayStateMachine gameplayStateMachine)
        {
            _levelSpawner = levelSpawner;
            _turnQueue = turnQueue;
            _gameplayStateMachine = gameplayStateMachine;
        }

        public async void Enter()
        {
            _turnQueue.Initialize();
            await _levelSpawner.Spawn();
            _gameplayStateMachine.Enter<BattleState>();
        }

        public void Exit()
        {
        }
    }
}