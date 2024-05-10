using Game.UI;
using Game.UI.CountDown;
using UI.Base;
using UnityEngine;

namespace Architecture.Services.Factories.Interfaces
{
    public interface IUIFactory
    {
        LoadingCurtain LoadingCurtain { get; }
        GameView GameView { get; }
        LoadingCurtain CreateLoadingCurtain();
        CountDownBeforeStartGame CreateCountDownBeforeStartGame();
        GameView CreateGameView(Transform parent);
    }
}