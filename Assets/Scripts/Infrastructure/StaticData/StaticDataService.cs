using Infrastructure.AssetManagement;
using Infrastructure.Services;
using UnityEngine.AddressableAssets;

namespace Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        public CarColorParams GetCarColorParams()
        {
            var colorParams = Addressables.LoadAssetAsync<CarColorParams>(AddressableNames.CarColorParams);
            colorParams.WaitForCompletion();
            return colorParams.Result;
        }
        
        public ShadersPropNames GetShaderPropNames()
        {
            var shaderPropNames = Addressables.LoadAssetAsync<ShadersPropNames>(AddressableNames.ShaderPropParams);
            shaderPropNames.WaitForCompletion();
            return shaderPropNames.Result;
        }
    }
}