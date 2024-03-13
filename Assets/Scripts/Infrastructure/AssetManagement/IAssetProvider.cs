using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        public TResourceType Instantiate<TResourceType>(string assetName, Vector3 at = default) where TResourceType : Object;

        TResourceType Instantiate<TResourceType>(string assetName, Transform parent) where TResourceType : Object;
    }
}