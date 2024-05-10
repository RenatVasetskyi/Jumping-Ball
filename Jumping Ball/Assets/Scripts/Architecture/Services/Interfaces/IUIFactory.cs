using UI.Base;

namespace Architecture.Services.Interfaces
{
    public interface IUIFactory
    {
        LoadingCurtain LoadingCurtain { get; }
        void CreateLoadingCurtain();
    }
}