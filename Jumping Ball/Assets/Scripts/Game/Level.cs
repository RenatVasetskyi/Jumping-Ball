using System.Collections.Generic;
using Architecture.Services.Factories.Interfaces;
using Architecture.Services.Interfaces;
using Data;
using Game.Beam;
using Game.Camera;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Level : MonoBehaviour
    {
        private const float CameraRotationDuration = 0.5f;
        
        [SerializeField] private List<BeamLine> _beamLines;
        
        [SerializeField] private Transform _ballStartPoint;
        [SerializeField] private Transform _finishParticleSpawnPoint;
        
        [SerializeField] private Vector3 _cameraRotationFinish;
        
        private IBaseFactory _baseFactory;
        private IGamePauser _gamePauser;
        
        private CameraFollowTarget _camera;
        private GameView _gameView;

        public List<BeamLine> BeamLines => _beamLines;
        public Transform BallStartPoint => _ballStartPoint;

        [Inject]
        public void Construct(IBaseFactory baseFactory, IGamePauser gamePauser)
        {
            _gamePauser = gamePauser;
            _baseFactory = baseFactory;
        }

        public void Construct(CameraFollowTarget camera, GameView gameView)
        {
            _camera = camera;
            _gameView = gameView;
        }

        public void SendLose()
        {
            _gamePauser.SetPause(true);
            _gameView.ShowLoseWindow();
        }

        public void SendVictory()
        {
            _gamePauser.SetPause(true);
            _camera.Rotate(_cameraRotationFinish, CameraRotationDuration, CreateFinishParticle);
            _gameView.ShowVictoryWindow();
        }
        
        private void CreateFinishParticle()
        {
            _baseFactory.CreateBaseWithObject<ParticleSystem>(AssetPath.FinishParticle,
                _finishParticleSpawnPoint.position, Quaternion.identity, null);
        }
    }
}