using System;
using System.Collections.Generic;
using Architecture.States.Interfaces;
using Architecture.States.Services.Interfaces;

namespace Architecture.States.Services
{
    public class StateMachine : IStateMachine
    {
        private IExitableState _activeState;
        
        public Dictionary<Type, IExitableState> States { get; set; } = new();

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return States[typeof(TState)] as TState;
        }
    }
}