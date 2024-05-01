using Data;
using Game.Camera.Data;
using UnityEngine;
using Zenject;

namespace Game.Camera
{
    public class CameraFollowTarget : MonoBehaviour
    {
        private Transform _target;

        private GameCameraConfig _config;

        [Inject]
        public void Construct(GameSettings gameSettings)
        {
            _config = gameSettings.GameCameraConfig;
        }
        
        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Awake()
        {
            transform.rotation = Quaternion.Euler(_config.StartRotation);
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            FollowTarget();
        }

        private void FollowTarget()
        {
            transform.position = _target.position + _config.FollowTargetOffset;
        }
    }
}