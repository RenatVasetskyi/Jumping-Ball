using UnityEngine;

namespace Architecture.Services.Interfaces
{
    public interface IAssetProvider
    {
        T LoadAsset<T>(string path) where T : Object;
        void Cleanup();
    }
}
