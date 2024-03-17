using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IAssetProvider assetProvider;
        
        private CarPartsData carPartsData = new();

        public LevelFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
        
        public void CreateCar(Transform carStartPosition)
        {
            var car = assetProvider.Instantiate<GameObject>(AddressableNames.AudiRs6, carStartPosition);
            carPartsData = car.GetComponent<CarPartsData>();
        }

        public CarPartsData GetCarPartsData() => 
            carPartsData;
    }
}