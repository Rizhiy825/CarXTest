using System.Collections.Generic;
using Infrastructure.AssetManagement;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Factory
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly ICameraPathFollower cameraPathFollower;
        private FlexibleColorPicker colorPicker;
        private CarPartsContainer carPartsContainer;
        private Button backButton;

        public LevelFactory(IAssetProvider assetProvider, ICameraPathFollower cameraPathFollower)
        {
            this.assetProvider = assetProvider;
            this.cameraPathFollower = cameraPathFollower;
        }

        public GameObject CreateHUD(List<CarPartData> carPartsData)
        {
            var hud = assetProvider.Instantiate<GameObject>(AddressableNames.Hud);
            var container = hud.GetComponent<HUDContainer>();

            colorPicker = container.ColorPicker;
            backButton = container.BackButton;
            carPartsContainer = container.CarPartsContainer;
            
            backButton.onClick.AddListener(HideColorPicker);
            var carPartElements = CreateCarPartElements(carPartsData);
            container.CarPartsContainer.Init(carPartElements);
            return hud;
        }

        public CarPartsData CreateCar(Transform carStartPosition)
        {
            var car = assetProvider.Instantiate<GameObject>(AddressableNames.AudiRs6, carStartPosition);
            var partsData = car.GetComponent<CarPartsData>();
            return partsData;
        }

        private List<CarPartElement> CreateCarPartElements(List<CarPartData> carPartsData)
        {
            var elements = new List<CarPartElement>();
            foreach (var partData in carPartsData)
            {
                var carPartElement = CreateElement(partData);
                carPartElement.button.onClick.AddListener(() => ShowColorPicker(partData));
                elements.Add(carPartElement);
            }

            return elements;
        }

        private void ShowColorPicker(CarPartData partData)
        {
            carPartsContainer.gameObject.SetActive(false);
            backButton.gameObject.SetActive(true);
            
            colorPicker.gameObject.SetActive(true);
            colorPicker.color = partData.material.color;
            colorPicker.onColorChange.AddListener(newColor => ChangeColor(partData, newColor));

            cameraPathFollower.FollowToPoint(partData.viewPoint);
        }
        
        private void HideColorPicker()
        {
            colorPicker.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            carPartsContainer.gameObject.SetActive(true);
            
            colorPicker.onColorChange.RemoveAllListeners();
            
            cameraPathFollower.FollowToStart();
        }

        private void ChangeColor(CarPartData partData, Color newColor) =>
            partData.material.color = newColor;

        private CarPartElement CreateElement(CarPartData partData)
        {
            var element = assetProvider.Instantiate<GameObject>(AddressableNames.CarPartElement);
            var carPartElement = element.GetComponent<CarPartElement>();
            carPartElement.Init(partData);
            return carPartElement;
        }
    }
}