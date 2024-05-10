using Architecture.Services.Factories.Interfaces;
using Architecture.Services.Interfaces;
using Data;
using DG.Tweening;
using Game.Beam;
using Game.Beam.Data;
using Game.Beam.Enums;
using Game.Player.Data;
using Game.UI.Swipes.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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

        private bool _isMovementStarted;
        private bool _canMove;
        private bool _isMovingHorizontal;

        private ColorType _colorType;

        [Inject]
        public void Construct(GameSettings gameSettings, IUIFactory uiFactory, 
            IGamePauser gamePauser)
        {
            _gamePauser = gamePauser;
            _config = gameSettings.BallConfig;
            _colorConfigs = gameSettings.ColorConfigs;
            _swipeReporter = uiFactory.GameView.SwipeDetector;
        }

        public void Initialize(Level level)
        {
            _level = level;
        }

        public void SetPause(bool isPaused)
        {
            if (isPaused)
            { 
                Pause();
                return;
            }

            if (_isMovementStarted)
                UnPause();
            else
                StartMovement();
        }

        private void Awake()
        {
            _canMove = false;
            
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
        
        private void StartMovement()
        {
            _isMovementStarted = true;
            _canMove = true;
            
            JumpToNextBeamLine();
            MoveHorizontalToCurrentPlatform(_config.ChangeLineDuration);
        }
        
        private void Pause()
        {
            _canMove = false;
            
            DOTween.Pause(transform);
        }
        
        private void UnPause()
        {
            _canMove = true;
            
            DOTween.Play(transform);
        }

        private void JumpToNextBeamLine()
        {
            //Check Lose
            if (IsLose())
            {
                Lose();
                return;
            }
            
            //Check Victory
            if (IsTheEndOfPath())
            {
                Victory();
                return;
            }
            
            ChangeColorType(GetRandomColorConfig());

            _currentBeamLine = _level.BeamLines[_currentBeamLineNumber];

            StartCoroutine(transform.DoJumpWithoutX(_currentBeamLine.Up.transform.position + new Vector3
                (0, _sphereCollider.bounds.extents.y, 0), _config.JumpForce, _config.JumpDuration, JumpToNextBeamLine));
            
            _currentBeamLineNumber++;
        }

        private void MoveHorizontalToCurrentPlatform(float duration)
        {
            _isMovingHorizontal = true;

            transform.DOMoveX(_currentBeamLine.Platforms
                [_currentBeamPlatformNumber].transform.position.x, duration)
                .onComplete += () => { _isMovingHorizontal = false; };
        }
        
        private void MoveRight()
        {
            if (_isMovingHorizontal | !_canMove)
                return;
            
            if (_currentBeamPlatformNumber < _currentBeamLine.Platforms.Count - 1)
            {
                ++_currentBeamPlatformNumber;
                MoveHorizontalToCurrentPlatform(_config.ChangeLineDuration);   
            }
        }

        private void MoveLeft()
        {
            if (_isMovingHorizontal | !_canMove)
                return;
            
            if (_currentBeamPlatformNumber > 0)
            {
                --_currentBeamPlatformNumber;
                MoveHorizontalToCurrentPlatform(_config.ChangeLineDuration);
            }
        }
        
        private void Victory()
        {
            _gamePauser.SetPause(true);
            _level.SendVictory();
                
            _canMove = false;
        }

        private void Lose()
        {
            _gamePauser.SetPause(true);
            _level.SendLose();
                
            _canMove = false;
        }

        private bool IsLose()
        {
            return _currentBeamLine != null && _colorType !=
                _currentBeamLine.Platforms[_currentBeamPlatformNumber].ColorType;
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