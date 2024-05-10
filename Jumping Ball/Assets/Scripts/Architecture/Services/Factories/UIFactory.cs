using System;
using Architecture.Services.Interfaces;
using Data;
using UI.Base;
using Zenject;
using Object = UnityEngine.Object;

namespace Architecture.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public LoadingCurtain LoadingCurtain { get; private set; }

        public UIFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }
        
        public void CreateLoadingCurtain()
        {
            if (LoadingCurtain != null)
            {
                Object.Destroy(LoadingCurtain.gameObject);
            }
            
            LoadingCurtain = _instantiator.InstantiatePrefabForComponent<LoadingCurtain>
                (_assetProvider.LoadAsset<LoadingCurtain>(AssetPath.LoadingCurtain));
            
            LoadingCurtain.Show();
        }
    }
}