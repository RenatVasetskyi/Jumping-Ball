using UnityEngine;

namespace UI.Base
{
    public class ScaledWindow : AnimatedWindow
    {
        private const int StartTransparency = 0;
        private const int EndTransparency = 1;

        [SerializeField] private GameObject _mainWindow;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [SerializeField] private LeanTweenType _easing = LeanTweenType.easeOutQuart;
        [SerializeField] private float _duration = 0.3f;
        
        [SerializeField] private Vector3 _minScale = new(0.85f, 0.85f, 0.85f);
        [SerializeField] private Vector3 _maxScale = new(2f, 2f, 2f);
        
        public override void Open()
        {
            _canvasGroup.alpha = StartTransparency;
            
            transform.localScale = _maxScale;
            
            _mainWindow.SetActive(true);

            LeanTween.scale(gameObject, _minScale, _duration)
                .setEase(_easing);

            LeanTween.value(StartTransparency, EndTransparency, _duration)
                .setOnUpdate((value) => _canvasGroup.alpha = value)
                .setEase(LeanTweenType.linear);
        }

        public override void Hide()
        {
            _canvasGroup.alpha = EndTransparency;
            
            transform.localScale = _minScale;

            LeanTween.scale(gameObject, _maxScale, _duration)
                .setEase(_easing);

            LeanTween.value(EndTransparency, StartTransparency, _duration)
                .setOnUpdate((value) => _canvasGroup.alpha = value)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() => _mainWindow.SetActive(false));
        }
    }
}