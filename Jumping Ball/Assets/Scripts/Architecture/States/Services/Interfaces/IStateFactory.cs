using System;
using System.Collections.Generic;
using Architecture.States.Interfaces;

namespace Architecture.States.Services.Interfaces
{
    public interface IStateFactory
    {
        void CreateStates(Dictionary<Type, IExitableState> statesList);
    }
}