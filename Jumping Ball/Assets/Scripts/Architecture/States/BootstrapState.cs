using Architecture.Services.Factories.Interfaces;
using Architecture.Services.Interfaces;
using Architecture.States.Services.Interfaces;
using Application = UnityEngine.Device.Application;
using IState = Architecture.States.Interfaces.IState;

namespace Architecture.States
{
    public class BootstrapState : IState
    {
        private const int TargetFrameRate = 120;
        
        private const string BootSceneName = "Boot";
        
        private readonly IStateMachine _stateMachine;
        private readonly IAudioService _audioService;
        private readonly ICurrencyService _currencyService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;

        public BootstrapState(IStateMachine stateMachine, IAudioService audioService,
            ICurrencyService currencyService, ISceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _audioService = audioService;
            _currencyService = currencyService;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _uiFactory.CreateLoadingCurtain();
            _sceneLoader.Load(BootSceneName, Initialize);
        }

        private void Initialize()
        {
            Application.targetFrameRate = TargetFrameRate;
            
            _audioService.Initialize();
            _currencyService.Load();
            
            _stateMachine.Enter<LoadMainMenuState>();
        }
    }
}