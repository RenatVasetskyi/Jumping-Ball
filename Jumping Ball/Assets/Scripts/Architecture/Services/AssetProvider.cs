using System.Collections.Generic;
using Architecture.Services.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Architecture.Services
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, Object> _loadedAssets = new();

        public T LoadAsset<T>(string path) where T : Object
        {
            if (_loadedAssets.TryGetValue(path, out Object asset))
                return asset as T;
            
            T loadedResource = Resources.Load<T>(path);
            _loadedAssets.Add(path, loadedResource);
            
            return loadedResource;
        }

        public void Cleanup()
        {
            _loadedAssets.Clear();
        }
    }
}