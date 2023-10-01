using System;
using System.Collections.Generic;
using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.Services.Logger;

namespace CodeBase.Common.FSM
{
    public class StateMachine : IStateMachine
    {
        private readonly ICustomLogger _logger;
        private readonly Dictionary<Type, IExitableState> _states = new();

        private IExitableState _activeState;

        public StateMachine(ICustomLogger logger)
        {
            _logger = logger;
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();

            if (_states[typeof(TState)] is TState state)
            {
                _activeState = state;
                
                state.Enter();
            }
        }

        public void Enter<TState, TArgs>(TArgs args) where TState : IStateWithArgument<TArgs>
        {
            _activeState?.Exit();

            if (_states[typeof(TState)] is TState state) 
                state.Enter(args);
        }

        public void AddState<TState>(TState state) where TState : IExitableState => 
            _states.Add(typeof(TState), state);
    }
}