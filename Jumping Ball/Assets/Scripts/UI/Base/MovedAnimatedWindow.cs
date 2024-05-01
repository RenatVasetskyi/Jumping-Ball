using UnityEngine;

namespace UI.Base
{
    public class MovedAnimatedWindow : AnimatedWindow
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        
        [SerializeField] private LeanTweenType _openEasing = LeanTweenType.easeOutBack;
        [SerializeField] private LeanTweenType _hideEasing = LeanTweenType.easeInBack;
        
        [SerializeField] private float _movementDuration = 0.5f;

        [SerializeField] private GameObject _window;
        
        public float MovementDuration => _movementDuration;
        
        public override void Open()
        {
            DoOpenAnimation();
        }

        public override void Hide()
        {
            DoHideAnimation();
        }
        
        private void DoOpenAnimation()
        {
            _window.SetActive(true);
            
            transform.localPosition = _startPoint.localPosition;
            
            LeanTween.moveLocal(gameObject, _endPoint.localPosition, _movementDuration)
                .setEase(_openEasing);
        }
        
        private void DoHideAnimation()
        {
            transform.localPosition = _endPoint.localPosition;
            
            LeanTween.moveLocal(gameObject, _startPoint.localPosition, _movementDuration)
                .setEase(_hideEasing)
                .setOnComplete(() => _window.SetActive(false));
        }
    }
}