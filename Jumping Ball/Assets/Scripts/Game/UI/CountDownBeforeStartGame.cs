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
        [SerializeField] private TextMeshProUGUI _text;
        
        private ICountDownService _countDownService;
        private GameSettings _gameSettings;

        [Inject]
        public void Construct(ICountDownService countDownService, GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _countDownService = countDownService;
        }

        private void OnEnable()
        {
            _countDownService.OnTick += UpdateText;
            
            UpdateText(_countDownService.TimeLeftInSeconds);
        }

        private void OnDisable()
        {
            _countDownService.OnTick -= UpdateText;
        }

        private void UpdateText(int timeLeftInSeconds)
        {
            _text.text = timeLeftInSeconds.ToString();

            GameCountDownConfig config = _gameSettings.GameCountDownConfig;
            
            LeanTween.scale(_text.gameObject, config.MaxScale, config.ScaleDuration)
                .setEase(config.Easing);
        }
    }
}