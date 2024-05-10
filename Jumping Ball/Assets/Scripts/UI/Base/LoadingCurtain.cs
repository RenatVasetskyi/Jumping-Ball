using UnityEngine;

namespace UI.Base
{
    public class LoadingCurtain : MonoBehaviour
    {
        private const int DoFadeDuration = 1;
        
        private const int HidedTransparency = 0;
        private const int OpenedTransparency = 1;
        
        [SerializeField] private CanvasGroup _canvasGroup;

        private LTDescr _hideTween;

        public void Show()
        {
            _canvasGroup.alpha = OpenedTransparency;
        }

        public void Hide()
        {
            _hideTween = LeanTween.value(OpenedTransparency, HidedTransparency, DoFadeDuration)
                .setOnUpdate((value) => _canvasGroup.alpha = value)
                .setOnComplete(() => Destroy(gameObject));
        }

        private void OnDestroy()
        {
            if (_hideTween != null)
                LeanTween.cancel(_hideTween.id);
        }
    }
}