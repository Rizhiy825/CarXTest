using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.AssetManagement
{
    public class AddressableAssetProvider : IAssetProvider
    {
        public TResourceType Instantiate<TResourceType>(string assetName, Vector3 at) where TResourceType : Object
        {
            var handle = LoadAsset<TResourceType>(assetName);
            return Object.Instantiate(handle.Task.Result, at, Quaternion.identity);
        }
        
        public TResourceType Instantiate<TResourceType>(string assetName, Transform parent) where TResourceType : Object
        {
            var handle = LoadAsset<TResourceType>(assetName);

            var instance = Object.Instantiate(handle.Task.Result);
            if (instance is GameObject gameObject)
            {
                if (parent != null) 
                    gameObject.transform.SetParent(parent, false);
            }

            return instance;
        }

        private static AsyncOperationHandle<TResourceType> LoadAsset<TResourceType>(string addressableName) where TResourceType : Object
        {
            var handle = Addressables.LoadAssetAsync<TResourceType>(addressableName);
            handle.WaitForCompletion();
            return handle;
        }
    }
}