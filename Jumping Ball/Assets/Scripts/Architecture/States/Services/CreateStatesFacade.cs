using Architecture.States.Services.Interfaces;

namespace Architecture.States.Services
{
    public class CreateStatesFacade : ICreateStatesFacade
    {
        private readonly IStateFactory _stateFactory;
        private readonly IStateMachine _stateMachine;

        public CreateStatesFacade(IStateFactory stateFactory, IStateMachine stateMachine)
        {
            _stateFactory = stateFactory;
            _stateMachine = stateMachine;
        }
        
        public void CreateStates()
        {
            _stateFactory.CreateStates(_stateMachine.States);
        }
    }
}