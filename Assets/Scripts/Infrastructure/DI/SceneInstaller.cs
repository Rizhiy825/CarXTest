using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.State;
using Infrastructure.StaticData;
using UnityEngine;
using Utils;
using Zenject;

namespace Infrastructure.DI
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField]public CameraPathFollower cameraPathFollower;
        [SerializeField]public Transform cameraStartPosition;
        [SerializeField]public Light lightToChange;
        
        public override void InstallBindings()
        {
            Container.Bind<UIStateMachine>().FromNew().AsSingle();
            
            BindStates();
            BindServices();
            CreateCameraPathFollower();
        }
        
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<AddressableAssetProvider>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShaderPropsService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChangeableLight>().AsSingle().NonLazy();
            var changeableLight = Container.Resolve<ChangeableLight>();
            changeableLight.SetLightToChange(lightToChange);
        }
        
        private void BindStates()
        {
            Container.Bind<StartMenuState>().FromNew().AsSingle();
            Container.Bind<CarPartsState>().FromNew().AsSingle();
            Container.Bind<PickColorState>().FromNew().AsSingle();
            Container.Bind<LightSettingsState>().FromNew().AsSingle();
        }

        private void CreateCameraPathFollower()
        {
            var cameraPathFollower = Container.InstantiatePrefabForComponent<CameraPathFollower>(this.cameraPathFollower);
            cameraPathFollower.Construct(cameraStartPosition, Camera.main);
            Container.Bind<ICameraPathFollower>().FromInstance(cameraPathFollower).AsSingle();
        }
    }
}