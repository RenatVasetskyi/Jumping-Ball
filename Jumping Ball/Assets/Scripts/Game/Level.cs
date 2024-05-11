using System;
using System.Collections.Generic;
using Architecture.Services.Factories.Interfaces;
using Architecture.Services.Interfaces;
using Data;
using Game.Beam;
using Game.Camera;
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
        [SerializeField] private Vector3 _cameraRotationonFinish;
        
        private CameraFollowTarget _camera;
        private IBaseFactory _baseFactory;
        private IGamePauser _gamePauser;

        public List<BeamLine> BeamLines => _beamLines;
        public Transform BallStartPoint => _ballStartPoint;

        [Inject]
        public void Construct(IBaseFactory baseFactory, IGamePauser gamePauser)
        {
            _gamePauser = gamePauser;
            _baseFactory = baseFactory;
        }

        public void Construct(CameraFollowTarget camera)
        {
            _camera = camera;
        }

        public void SendLose()
        {
            _gamePauser.SetPause(true);
        }

        public void SendVictory()
        {
            _gamePauser.SetPause(true);
            _camera.Rotate(_cameraRotationonFinish, CameraRotationDuration, CreateFinishParticle);
        }
        
        private void CreateFinishParticle()
        {
            _baseFactory.CreateBaseWithObject<ParticleSystem>(AssetPath.FinishParticle,
                _finishParticleSpawnPoint.position, Quaternion.identity, null);
        }
    }
}