using Architecture.Services.Interfaces;
using Data;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Architecture.Services
{
    public class BaseFactory : IBaseFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;

        public GameView GameView { get; private set; }

        public BaseFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        public T CreateBaseWithContainer<T>(string path) where T : Component
        {
            return _container.InstantiatePrefabForComponent<T>(_assetProvider.LoadAsset<T>(path));
        }

        public T CreateBaseWithContainer<T>(string path, Transform parent) where T : Component
        {
            return _container.InstantiatePrefabForComponent<T>(_assetProvider.LoadAsset<T>(path), parent);
        }

        public T CreateBaseWithContainer<T>(string path, Vector3 at, Quaternion rotation, Transform parent) where T : Component
        {
            return _container.InstantiatePrefabForComponent<T>(_assetProvider
                    .LoadAsset<T>(path), at, rotation, parent);
        }

        public T CreateBaseWithContainer<T>(T prefab, Vector3 at, Quaternion rotation, Transform parent) where T : Component
        {
            return _container.InstantiatePrefabForComponent<T>(prefab, at, rotation, parent);
        }

        public GameObject CreateBaseWithContainer(GameObject prefab, Vector3 at, Quaternion rotation, Transform parent)
        {
            return _container.InstantiatePrefab(prefab, at, rotation, parent);
        }

        public T CreateBaseWithObject<T>(string path) where T : Component
        {
            return Object.Instantiate(_assetProvider.LoadAsset<T>(path));
        }

        public GameObject CreateBaseWithContainer(string path, Transform parent)
        {
            return _container.InstantiatePrefab(_assetProvider.LoadAsset<GameObject>(path), parent);
        }

        public GameView CreateGameView(Transform parent)
        {
            GameView = _container.InstantiatePrefabForComponent<GameView>(
                _assetProvider.LoadAsset<GameView>(AssetPath.GameView), parent);

            return GameView;
        }
    }
}