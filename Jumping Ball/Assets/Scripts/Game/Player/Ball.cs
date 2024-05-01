using Data;
using DG.Tweening;
using Game.Player.Data;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private SphereCollider _sphereCollider;
        
        private BallConfig _config;
        private Level _level;
        private int _currentBeamLine;

        [Inject]
        public void Construct(GameSettings gameSettings)
        {
            _config = gameSettings.BallConfig;
        }

        public void Initialize(Level level)
        {
            _level = level;
            
            JumpToNextBeamLine();
        }

        private void JumpToNextBeamLine()
        {
            if (IsTheEndOfPath())
            {
                _level.SendVictory();
                
                return;
            }
            
            transform.DOJump(_level.BeamLines[_currentBeamLine].Up.transform.position + new 
                        Vector3(0, _sphereCollider.bounds.extents.y, 0), _config.JumpForce,
                    _config.NumberOfJumps, _config.JumpDuration).SetEase(Ease.Linear).onComplete += JumpToNextBeamLine;
            
            _currentBeamLine++;
        }

        private bool IsTheEndOfPath()
        {
            return _currentBeamLine >= _level.BeamLines.Count;
        }
    }
}