using Architecture.Services.Interfaces;
using Architecture.States.Interfaces;
using Audio;
using Data;
using Game;
using Game.Camera;
using Game.Player;
using Game.UI;
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

        public LoadGameState(ISceneLoader sceneLoader, IGamePauser gamePauser,
            IAudioService audioService, IBaseFactory baseFactory, IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _gamePauser = gamePauser;
            _audioService = audioService;
            _baseFactory = baseFactory;
            _assetProvider = assetProvider;
        }
        
        public void Exit()
        {
            _audioService.StopMusic();
            _assetProvider.Cleanup();
        }

        public void Enter()
        {
            _sceneLoader.Load(GameScene, Initialize);
        }

        private void Initialize()
        {
            _gamePauser.Clear();
            _gamePauser.SetPause(false);
            
            Transform parent = _baseFactory.CreateBaseWithObject<Transform>(AssetPath.BaseParent);

            CameraFollowTarget cameraFollowTarget = _baseFactory.CreateBaseWithContainer
                <CameraFollowTarget>(AssetPath.CameraFollowTarget, parent);
            
            Camera uiCamera = _baseFactory.CreateBaseWithContainer
                <Camera>(AssetPath.GameUICamera, parent);

            GameView gameView = _baseFactory.CreateGameView(parent);
            gameView.GetComponent<Canvas>().worldCamera = uiCamera;
            gameView.SwipeDetector.SetCamera(uiCamera);
            
            Level level = _baseFactory.CreateBaseWithContainer<Level>(AssetPath.Level, parent);
            
            Ball ball = _baseFactory.CreateBaseWithContainer<Ball>(AssetPath.Ball, 
                level.BallStartPoint.position, Quaternion.identity, parent);
            ball.Initialize(level);
            
            cameraFollowTarget.SetTarget(ball.transform);
            
            _audioService.PlayMusic(MusicType.Game);
        }
    }
}