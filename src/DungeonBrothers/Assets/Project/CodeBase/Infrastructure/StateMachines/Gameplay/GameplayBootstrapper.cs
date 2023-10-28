﻿using _Project.CodeBase.Infrastructure.StateMachines.Gameplay.FSM;
using _Project.CodeBase.Infrastructure.StateMachines.Gameplay.States;
using VContainer.Unity;

namespace _Project.CodeBase.Infrastructure.StateMachines.Gameplay
{
    public class GameplayBootstrapper : IInitializable
    {
        private readonly IGameplayStateMachine _gameplayStateMachine;

        public GameplayBootstrapper(IGameplayStateMachine gameplayStateMachine,
            LevelLoadingState levelLoadingState,
            BattleState battleState)
        {
            _gameplayStateMachine = gameplayStateMachine;
            
            _gameplayStateMachine.Add(levelLoadingState);
            _gameplayStateMachine.Add(battleState);
        }

        public void Initialize() => 
            _gameplayStateMachine.Enter<LevelLoadingState>();
    }
}