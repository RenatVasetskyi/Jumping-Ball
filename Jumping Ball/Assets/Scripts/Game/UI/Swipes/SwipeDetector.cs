using System;
using Game.UI.Swipes.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI.Swipes
{
    public class SwipeDetector : MonoBehaviour, IPointerDownHandler,
        IPointerMoveHandler, ISwipeReporter
    {
        private const float MinDistanceToDetectSwipe = 0.8f;
        
        private const int MaxAngle = 180;

        private const int RightSwipeMinAngle = -45;
        private const int RightSwipeMaxAngle = 45;

        private const int LeftSwipeMinAngle = -135;
        private const int LeftSwipeMaxAngle = 135;

        public event Action OnSwipeLeft;
        public event Action OnSwipeRight;

        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition;

        private UnityEngine.Camera _camera;

        private bool _canSwipe;
        
        public void SetCamera(UnityEngine.Camera camera)
        {
            _camera = camera;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_camera == null)
                return;
            
            _startTouchPosition = _camera.ScreenToWorldPoint(eventData.position);

            _canSwipe = true;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!_canSwipe | _camera == null)
                return;
            
            _endTouchPosition = _camera.ScreenToWorldPoint(eventData.position);
            
            if (Vector2.Distance(_startTouchPosition, _endTouchPosition) >= MinDistanceToDetectSwipe)
            {
                CalculateAngle();

                _canSwipe = false;
            }
        }

        private void CalculateAngle()
        {
            float swipeAngle = Mathf.Atan2(_endTouchPosition.y - _startTouchPosition.y, 
                _endTouchPosition.x - _startTouchPosition.x) * MaxAngle / Mathf.PI;
            
            GetSwipeDirection(swipeAngle);
        }

        private void GetSwipeDirection(float swipeAngle)
        {
            if (swipeAngle > RightSwipeMinAngle && swipeAngle <= RightSwipeMaxAngle)
                OnSwipeRight?.Invoke();
            else if (swipeAngle > LeftSwipeMaxAngle || swipeAngle <= LeftSwipeMinAngle)
                OnSwipeLeft?.Invoke();
        }
    }
}