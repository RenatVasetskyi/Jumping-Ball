using Architecture.Services;
using Architecture.Services.Factories;
using Architecture.Services.Factories.Interfaces;
using Architecture.Services.Interfaces;
using Architecture.States.Services;
using Architecture.States.Services.Interfaces;
using Data;
using UnityEngine;
using Zenject;

namespace Architecture.Installers
{
    public class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private GameSettings _gameSettings;
        
        public override void InstallBindings()
        {
            BindGameSettings();
            BindCoroutineRunner();
            BindAssetProvider();
            BindSceneLoader();
            BindFactories();
            BindTimeServices();
            BindSaveService();
            BindCurrencyService();
            BindAudioService();
            BindStateMachine();
        }

        private void BindFactories()
        {
            Container.Bind<IBaseFactory>().To<BaseFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<ICreateStatesFacade>().To<CreateStatesFacade>().AsSingle();
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
        }
        
        private void BindCurrencyService()
        {
            Container.Bind<ICurrencyService>().To<CurrencyService>().AsSingle();
        }

        private void BindGameSettings()
        {
            Container.Bind<GameSettings>().FromScriptableObject(_gameSettings).AsSingle();
        }

        private void BindSaveService()
        {
            Container.Bind<ISaveService>().To<SaveService>().AsSingle();
        }
        
        private void BindAudioService()
        {
            Container.Bind<IAudioService>().To<AudioService>().AsSingle().NonLazy();
        }
        
        private void BindStateMachine()
        {
            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
        }
        
        private void BindCoroutineRunner()
        {
            Container.BindInterfacesTo<ServiceInstaller>().FromInstance(this).AsSingle().NonLazy();
        }
        
        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().NonLazy();
        }

		private void BindAssetProvider()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindTimeServices()
        {
            Container.Bind<ICountDownService>().To<CountDownService>().AsSingle();
            Container.Bind<IGamePauser>().To<GamePauser>().AsSingle();
        }
    }
}