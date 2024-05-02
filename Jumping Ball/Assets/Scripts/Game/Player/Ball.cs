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
        
        private int _currentBeamLine;

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

            BeamLine nextBeamLine = _level.BeamLines[_currentBeamLine];
            
            transform.DOJump(nextBeamLine.Up.transform.position + new Vector3
                    (0, _sphereCollider.bounds.extents.y, 0), _config.JumpForce,
                    _config.NumberOfJumps, _config.JumpDuration).SetEase(Ease.Linear).onComplete += JumpToNextBeamLine;
            
            _currentBeamLine++;
        }
        
        private void MoveRight()
        {
        }

        private void MoveLeft()
        {
        }
        
        private void MoveToLine(int line)
        {
            
        }

        private bool IsTheEndOfPath()
        {
            return _currentBeamLine >= _level.BeamLines.Count;
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