using System;
using System.Collections.Generic;
using Architecture.States.Interfaces;
using Architecture.States.Services.Interfaces;
using Zenject;

namespace Architecture.States.Services
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }
        
        public void CreateStates(Dictionary<Type, IExitableState> statesList)
        {
            statesList.Add(typeof(BootstrapState), CreateState<BootstrapState>(statesList));
            statesList.Add(typeof(LoadMainMenuState), CreateState<LoadMainMenuState>(statesList));
            statesList.Add(typeof(LoadGameState), CreateState<LoadGameState>(statesList));
        }
        
        private IState CreateState<TState>(Dictionary<Type, IExitableState> statesList) where TState : class, IState
        {
            if (statesList.ContainsKey(typeof(TState)))
                return statesList[typeof(TState)] as IState;
            
            _container.Bind<TState>().AsSingle();
            
            return _container.Resolve<TState>();
        }
    }
}