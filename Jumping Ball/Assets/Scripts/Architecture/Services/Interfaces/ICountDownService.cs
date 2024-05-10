using System;

namespace Architecture.Services.Interfaces
{
    public interface ICountDownService
    {
        event Action OnCountDownFinished;
        event Action<int> OnTick;
        int TimeLeftInSeconds { get; }
        void StartCountDown(int timeInSeconds);
    }
}