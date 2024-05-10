using System;
using System.Collections;
using Architecture.Services.Interfaces;
using UnityEngine;

namespace Architecture.Services
{
    public class CountDownService : ICountDownService
    {
        private const int TimeIntervalInSeconds = 1;
        
        private readonly ICoroutineRunner _coroutineRunner;
        
        public event Action OnCountDownFinished;
        public event Action<int> OnTick;
        public int TimeLeftInSeconds { get; private set; }

        private Coroutine _countDownCoroutine;

        public CountDownService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void StartCountDown(int timeInSeconds)
        {
            if (_countDownCoroutine != null)
                _coroutineRunner.StopCoroutine(_countDownCoroutine);
            
            _countDownCoroutine = _coroutineRunner.StartCoroutine(CountDown(timeInSeconds));
        }

        private IEnumerator CountDown(int timeInSeconds)
        {
            if (timeInSeconds <= 0)
            {
                OnCountDownFinished?.Invoke();
                yield break;
            }
            
            TimeLeftInSeconds = timeInSeconds;
            OnTick?.Invoke(TimeLeftInSeconds);
            
            while (TimeLeftInSeconds > 0)
            {
                yield return new WaitForSeconds(TimeIntervalInSeconds);

                TimeLeftInSeconds -= TimeIntervalInSeconds;
                OnTick?.Invoke(TimeLeftInSeconds);
            }
            
            OnCountDownFinished?.Invoke();
        }
    }
}