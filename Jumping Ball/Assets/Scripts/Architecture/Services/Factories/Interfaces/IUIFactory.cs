using Game.UI;
using UI.Base;

namespace Architecture.Services.Interfaces
{
    public interface IUIFactory
    {
        LoadingCurtain LoadingCurtain { get; }
        LoadingCurtain CreateLoadingCurtain();
        CountDownBeforeStartGame CreateCountDownBeforeStartGame();
    }
}