using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        public Transform carStartPosition;
        public Transform cameraStartPosition;

        private IAssetProvider assetProvider;
        private ILevelFactory levelFactory;
        private ICameraPathFollower cameraPathFollower;


        public void Construct(IAssetProvider assetProvider, ILevelFactory levelFactory, ICameraPathFollower cameraPathFollower)
        {
            this.cameraPathFollower = cameraPathFollower;
            //TODO
            this.assetProvider = assetProvider;
            this.levelFactory = levelFactory;
        }
        private void Start()
        {
            assetProvider = new AddressableAssetProvider();
            cameraPathFollower = CreateCameraPathFollower(Camera.main.transform);
            levelFactory = new LevelFactory(assetProvider, cameraPathFollower);
            
            var partsData = levelFactory.CreateCar(carStartPosition);
            levelFactory.CreateHUD(partsData.Parts);
            
        }
        
        //TODO убрать в регистрацию сервисов
        public CameraPathFollower CreateCameraPathFollower(Transform camera)
        {
            var cameraPathFollower = assetProvider.Instantiate<GameObject>(AddressableNames.CameraPathFollower, camera); 
            var follower = cameraPathFollower.GetComponent<CameraPathFollower>();
            follower.Construct(cameraStartPosition, Camera.main);
            return follower;
        }
    }
}