using Architecture.Services.Interfaces;
using Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Base
{
    public class WindowControlButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        [Space]
        
        [SerializeField] protected GameObject[] _windows;

        [Space] 
        
        [SerializeField] private bool _subscribe = true;
        [SerializeField] private bool _playSfx = true;

        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        public virtual void Control()
        {
            if (_playSfx) 
                _audioService.PlaySfx(SfxType.UIClick);
        }

        private void OnEnable()
        {
            if (_subscribe) 
                _button.onClick.AddListener(Control);
        }

        private void OnDisable()
        {
            if (_subscribe) 
                _button.onClick.RemoveListener(Control);
        }
    }
}