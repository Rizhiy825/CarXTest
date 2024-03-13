using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        public Transform CarStartPosition;

        private IAssetProvider assetProvider;
        private ILevelFactory levelFactory;
        
        public void Construct(IAssetProvider assetProvider, ILevelFactory levelFactory)
        {
            //TODO
            this.assetProvider = assetProvider;
            this.levelFactory = levelFactory;
        }
        private void Awake()
        {
            assetProvider = new AddressableAssetProvider();
            levelFactory = new LevelFactory(assetProvider);
            var partsData = levelFactory.CreateCar(CarStartPosition);
            levelFactory.CreateHUD(partsData.Parts);
        }
    }
}