using Architecture.Services.Factories.Interfaces;
using Architecture.Services.Interfaces;
using Architecture.States.Interfaces;
using Audio;
using Data;
using Game;
using Game.Camera;
using Game.Player;
using Game.UI;
using Game.UI.CountDown;
using UnityEngine;

namespace Architecture.States
{
    public class LoadGameState : IState
    {
        private const string GameScene = "Game";
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IGamePauser _gamePauser;
        private readonly IAudioService _audioService;
        private readonly IBaseFactory _baseFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IUIFactory _uiFactory;
        private readonly ICountDownService _countDownService;
        private readonly GameSettings _gameSettings;
        
        private Ball _ball;

        public LoadGameState(ISceneLoader sceneLoader, IGamePauser gamePauser,
            IAudioService audioService, IBaseFactory baseFactory,
            IAssetProvider assetProvider, IUIFactory uiFactory, 
            ICountDownService countDownService, GameSettings gameSettings)
        {
            _sceneLoader = sceneLoader;
            _gamePauser = gamePauser;
            _audioService = audioService;
            _baseFactory = baseFactory;
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;
            _countDownService = countDownService;
            _gameSettings = gameSettings;
        }
        
        public void Exit()
        {
            _audioService.StopMusic();
            _assetProvider.Cleanup();
            
            _countDownService.OnCountDownFinished -= Unpause;
        }

        public void Enter()
        {
            if (_uiFactory.LoadingCurtain == null)
                _uiFactory.CreateLoadingCurtain();
            
            _countDownService.OnCountDownFinished += Unpause;
            
            _sceneLoader.Load(GameScene, Initialize);
        }

        private void Initialize()
        {
            _gamePauser.Clear();
            _gamePauser.SetPause(true);
            
            Transform parent = _baseFactory.CreateBaseWithObject<Transform>(AssetPath.BaseParent);

            CameraFollowTarget cameraFollowTarget = _baseFactory.CreateBaseWithContainer
                <CameraFollowTarget>(AssetPath.CameraFollowTarget, parent);
            
            Camera uiCamera = _baseFactory.CreateBaseWithContainer
                <Camera>(AssetPath.GameUICamera, parent);

            GameView gameView = _uiFactory.CreateGameView(parent);
            gameView.GetComponent<Canvas>().worldCamera = uiCamera;
            gameView.SwipeDetector.SetCamera(uiCamera);
            
            Level level = _baseFactory.CreateBaseWithContainer<Level>(AssetPath.Level, parent);
            level.Construct(cameraFollowTarget, gameView);
            
            _ball = _baseFactory.CreateBaseWithContainer<Ball>(AssetPath.Ball, 
                level.BallStartPoint.position, Quaternion.identity, parent);
            _ball.Construct(level);
            
            cameraFollowTarget.SetTarget(_ball.transform);
            
            _audioService.PlayMusic(MusicType.Game);
            
            if (_uiFactory.LoadingCurtain != null)
                _uiFactory.LoadingCurtain.Hide();

            StartCountDown(uiCamera);
        }

        private void StartCountDown(Camera camera)
        {
            CountDownBeforeStartGame countDown = _uiFactory.CreateCountDownBeforeStartGame();
            countDown.Canvas.worldCamera = camera;
            
            _countDownService.StartCountDown(_gameSettings.GameCountDownConfig.TimeInSecondsBeforeGameStart);
        }
        
        private void Unpause()
        {
            _gamePauser.SetPause(false);
        }
    }
}