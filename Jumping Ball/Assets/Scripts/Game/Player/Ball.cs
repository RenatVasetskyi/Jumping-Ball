using Architecture.Services.Interfaces;
using Data;
using DG.Tweening;
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
            
            transform.DOJump(_currentBeamLine.Up.transform.position + new Vector3
                    (0, _sphereCollider.bounds.extents.y, 0), _config.JumpForce,
                    _config.NumberOfJumps, _config.JumpDuration).SetEase(Ease.Linear).onComplete += JumpToNextBeamLine;
            
            _currentBeamLineNumber++;
        }
        
        private void MoveRight()
        {
            if (_isMovingHorizontal)
                return;
            
            if (_currentBeamPlatformNumber < _currentBeamLine.Platforms.Count)
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
            
            LeanTween.moveX(gameObject, _currentBeamLine.Platforms
                [_currentBeamPlatformNumber].transform.position.x, _config.ChangeLineDuration)
                .setOnComplete(() => _isMovingHorizontal = false);
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