using Architecture.Services.Interfaces;
using Data;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game.Beam;
using Game.Beam.Data;
using Game.Beam.Enums;
using Game.Player.Data;
using Game.UI.Swipes.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Ball : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private BallConfig _config;
        private Level _level;
        private ColorConfig[] _colorConfigs;
        private ISwipeReporter _swipeReporter;
        private IGamePauser _gamePauser;
        
        private BeamLine _currentBeamLine;
        
        private int _currentBeamLineNumber;
        private int _currentBeamPlatformNumber = 1;

        private bool _isMovingHorizontal;
        private bool _canMove;

        private ColorType _colorType;

        [Inject]
        public void Construct(GameSettings gameSettings, IBaseFactory baseFactory, 
            IGamePauser gamePauser)
        {
            _gamePauser = gamePauser;
            _config = gameSettings.BallConfig;
            _colorConfigs = gameSettings.ColorConfigs;
            _swipeReporter = baseFactory.GameView.SwipeDetector;
        }

        public void Initialize(Level level)
        {
            _level = level;
            _isMovingHorizontal = true;
        }
        
        public void SetPause(bool isPaused)
        {
            // if (isPaused)
                // Stop();
            // else
                // StartMove();
        }

        private void Awake()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
        
        private void StartMove()
        {
            _isMovingHorizontal = false;
            
            JumpToNextBeamLine();
            MoveHorizontalToCurrentPlatform();
        }

        private void JumpToNextBeamLine()
        {
            //Check Lose
            if (_currentBeamLine != null && _colorType != _currentBeamLine.Platforms[_currentBeamPlatformNumber].ColorType)
            {
                _level.SendLose();
                
                return;
            }
            
            //Check Victory
            if (IsTheEndOfPath())
            {
                _level.SendVictory();
                
                return;
            }
            
            ChangeColorType(GetRandomColorConfig());

            _currentBeamLine = _level.BeamLines[_currentBeamLineNumber];

            StartCoroutine(transform.DoJumpWithoutX(_currentBeamLine.Up.transform.position + new Vector3
                (0, _sphereCollider.bounds.extents.y, 0), _config.JumpForce, _config.JumpDuration, JumpToNextBeamLine));
            
            _currentBeamLineNumber++;
        }
        
        private void MoveHorizontalToCurrentPlatform()
        {
            _isMovingHorizontal = true;
            
            TweenerCore<Vector3, Vector3, VectorOptions> move = transform.DOMoveX(_currentBeamLine
                .Platforms[_currentBeamPlatformNumber].transform.position.x, _config.ChangeLineDuration);
            
            move.onComplete += () => _isMovingHorizontal = false;
        }
        
        private void MoveRight()
        {
            if (_isMovingHorizontal)
                return;
            
            if (_currentBeamPlatformNumber < _currentBeamLine.Platforms.Count - 1)
            {
                ++_currentBeamPlatformNumber;
                MoveHorizontalToCurrentPlatform();   
            }
        }

        private void MoveLeft()
        {
            if (_isMovingHorizontal)
                return;
            
            if (_currentBeamPlatformNumber > 0)
            {
                --_currentBeamPlatformNumber;
                MoveHorizontalToCurrentPlatform();
            }
        }
        
        private void ChangeColorType(ColorConfig colorConfig)
        {
            _meshRenderer.material.color = colorConfig.Color;
            _colorType = colorConfig.Type;
        }
        
        private ColorConfig GetRandomColorConfig()
        {
            return _colorConfigs[Random.Range(0, _colorConfigs.Length)];
        }

        private bool IsTheEndOfPath()
        {
            return _currentBeamLineNumber >= _level.BeamLines.Count;
        }

        private void Subscribe()
        {
            _swipeReporter.OnSwipeLeft += MoveLeft;
            _swipeReporter.OnSwipeRight += MoveRight;
            _gamePauser.Register(this);
        }

        private void Unsubscribe()
        {
            _swipeReporter.OnSwipeLeft -= MoveLeft;
            _swipeReporter.OnSwipeRight -= MoveRight;
            _gamePauser.UnRegister(this);
        }
    }
}