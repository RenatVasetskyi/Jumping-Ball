using Architecture.Services.Factories.Interfaces;
using Architecture.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Architecture.Services.Factories
{
    public class BaseFactory : IBaseFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public BaseFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public T CreateBaseWithContainer<T>(string path) where T : Component
        {
            return _instantiator.InstantiatePrefabForComponent<T>(_assetProvider.LoadAsset<T>(path));
        }

        public T CreateBaseWithContainer<T>(string path, Transform parent) where T : Component
        {
            return _instantiator.InstantiatePrefabForComponent<T>(_assetProvider.LoadAsset<T>(path), parent);
        }

        public T CreateBaseWithContainer<T>(string path, Vector3 at, Quaternion rotation, Transform parent) where T : Component
        {
            return _instantiator.InstantiatePrefabForComponent<T>(_assetProvider
                    .LoadAsset<T>(path), at, rotation, parent);
        }

        public T CreateBaseWithContainer<T>(T prefab, Vector3 at, Quaternion rotation, Transform parent) where T : Component
        {
            return _instantiator.InstantiatePrefabForComponent<T>(prefab, at, rotation, parent);
        }

        public GameObject CreateBaseWithContainer(GameObject prefab, Vector3 at, Quaternion rotation, Transform parent)
        {
            return _instantiator.InstantiatePrefab(prefab, at, rotation, parent);
        }
        
        public GameObject CreateBaseWithContainer(string path, Transform parent)
        {
            return _instantiator.InstantiatePrefab(_assetProvider.LoadAsset<GameObject>(path), parent);
        }

        public T CreateBaseWithObject<T>(string path, Vector3 at, Quaternion rotation, Transform parent) where T : Component
        {
            return Object.Instantiate(_assetProvider.LoadAsset<T>(path), at, rotation, parent);
        }

        public T CreateBaseWithObject<T>(string path) where T : Component
        {
            return Object.Instantiate(_assetProvider.LoadAsset<T>(path));
        }
    }
}