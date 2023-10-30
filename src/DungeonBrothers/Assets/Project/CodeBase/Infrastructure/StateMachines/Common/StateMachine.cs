using System;
using System.Collections.Generic;
using Project.CodeBase.Infrastructure.StateMachines.Common.States;

namespace Project.CodeBase.Infrastructure.StateMachines.Common
{
    public class StateMachine 
    {
        private readonly Dictionary<Type, IExitableState> _states = new();

        private IExitableState _activeState;

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