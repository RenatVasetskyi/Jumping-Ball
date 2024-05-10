using Architecture.Services.Interfaces;
using Data;
using Game.UI.CountDown.Data;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class CountDownBeforeStartGame : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TextMeshProUGUI _text;
        
        private ICountDownService _countDownService;
        private GameSettings _gameSettings;
        
        public Canvas Canvas => _canvas;

        [Inject]
        public void Construct(ICountDownService countDownService, GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _countDownService = countDownService;
        }

        private void OnEnable()
        {
            _text.text = string.Empty;
            
            _countDownService.OnTick += UpdateText;
            _countDownService.OnCountDownFinished += DestroyMyself;
            
            UpdateText(_countDownService.TimeLeftInSeconds);
        }

        private void OnDisable()
        {
            _countDownService.OnTick -= UpdateText;
            _countDownService.OnCountDownFinished -= DestroyMyself;
        }

        private void UpdateText(int timeLeftInSeconds)
        {
            _text.text = timeLeftInSeconds.ToString();
            
            DoScaleAnimation();
        }

        private void DoScaleAnimation()
        {
            GameCountDownConfig config = _gameSettings.GameCountDownConfig;
            
            LeanTween.scale(_text.gameObject, config.MinScale, config.UnscaleDuration)
                .setEase(config.UnScaleEasing).setOnComplete(() =>
                {
                    LeanTween.scale(_text.gameObject, config.MaxScale, config.ScaleDuration)
                        .setEase(config.ScaleEasing);        
                });
        }
        
        private void DestroyMyself()
        {
            Destroy(gameObject);
        }
    }
}