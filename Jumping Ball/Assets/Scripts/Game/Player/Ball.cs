using Architecture.Services.Interfaces;
using Data;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game.Beam;
using Game.Player.Data;
using Game.UI.Swipes.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private SphereCollider _sphereCollider;
        
        private BallConfig _config;
        private Level _level;
        private ISwipeReporter _swipeReporter;
        
        private BeamLine _currentBeamLine;
        
        private int _currentBeamLineNumber;
        private int _currentBeamPlatformNumber = 1;

        private bool _isMovingHorizontal;

        [Inject]
        public void Construct(GameSettings gameSettings, IBaseFactory baseFactory)
        {
            _config = gameSettings.BallConfig;
            _swipeReporter = baseFactory.GameView.SwipeDetector;
        }

        public void Initialize(Level level)
        {
            _level = level;
            
            JumpToNextBeamLine();
            MoveHorizontalToCurrentPlatform();
        }

        private void Awake()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void JumpToNextBeamLine()
        {
            if (IsTheEndOfPath())
            {
                _level.SendVictory();
                
                return;
            }

            _currentBeamLine = _level.BeamLines[_currentBeamLineNumber];
            
            // transform.DOJump(_currentBeamLine.Up.transform.position + new Vector3
                    // (0, _sphereCollider.bounds.extents.y, 0), _config.JumpForce,
                    // _config.NumberOfJumps, _config.JumpDuration).SetEase(Ease.Linear).onComplete += JumpToNextBeamLine;
            
            _currentBeamLineNumber++;
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
        
        private void MoveHorizontalToCurrentPlatform()
        {
            _isMovingHorizontal = true;
            
            TweenerCore<Vector3, Vector3, VectorOptions> move = transform.DOMoveX(_currentBeamLine
                .Platforms[_currentBeamPlatformNumber].transform.position.x, _config.ChangeLineDuration);
            
            move.onComplete += () => _isMovingHorizontal = false;
        }

        private bool IsTheEndOfPath()
        {
            return _currentBeamLineNumber >= _level.BeamLines.Count;
        }

        private void Subscribe()
        {
            _swipeReporter.OnSwipeLeft += MoveLeft;
            _swipeReporter.OnSwipeRight += MoveRight;
        }

        private void Unsubscribe()
        {
            _swipeReporter.OnSwipeLeft -= MoveLeft;
            _swipeReporter.OnSwipeRight -= MoveRight;
        }
    }
}