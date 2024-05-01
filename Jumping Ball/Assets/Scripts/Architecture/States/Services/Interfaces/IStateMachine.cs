using System;
using System.Collections.Generic;
using Architecture.States.Interfaces;

namespace Architecture.States.Services.Interfaces
{
    public interface IStateMachine
    {
        Dictionary<Type, IExitableState> States { get; set; }
        void Enter<TState>() where TState : class, IState;
    }
}