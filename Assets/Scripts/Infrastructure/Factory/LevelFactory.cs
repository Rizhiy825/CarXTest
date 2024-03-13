using System.Collections.Generic;
using Infrastructure.AssetManagement;
using UI;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IAssetProvider assetProvider;
        private FlexibleColorPicker colorPicker;

        public LevelFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public GameObject CreateHUD(List<CarPartData> carPartsData)
        {
            var hud = assetProvider.Instantiate<GameObject>(AddressableNames.Hud);
            var container = hud.GetComponent<HUDContainer>();

            colorPicker = container.ColorPicker;
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
            var go = colorPicker.gameObject;
            if (go.activeSelf)
            {
                colorPicker.onColorChange.RemoveAllListeners();
                go.SetActive(false);
            }
            
            go.SetActive(true);
            colorPicker.color = partData.material.color;
            colorPicker.onColorChange.AddListener(newColor => ChangeColor(partData, newColor));
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