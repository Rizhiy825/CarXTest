using System;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using UI;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly ICameraPathFollower cameraPathFollower;
        
        private HUDContainer hudContainer;
        private Dictionary<CarPartElement, CarPartData> partDataToElement = new();

        public UIFactory(IAssetProvider assetProvider, ICameraPathFollower cameraPathFollower)
        {
            this.assetProvider = assetProvider;
            this.cameraPathFollower = cameraPathFollower;
        }
        
        public void CreateHUD(List<CarPartData> carPartsData)
        {
            var hud = assetProvider.Instantiate<GameObject>(AddressableNames.Hud);
            hudContainer = hud.GetComponent<HUDContainer>();
            var carPartElements = CreateCarPartElements(carPartsData);
            hudContainer.CarPartsContainer.Init(carPartElements);
        }

        public HUDContainer GetHUD()
        {
            if (hudContainer == null)
                throw new NullReferenceException("HUDContainer is null. You should call CreateHUD before getting HUD.");

            return hudContainer;
        }

        public CarPartData GetCarPartData(CarPartElement carPartElement)
        {
            if (partDataToElement.TryGetValue(carPartElement, out var partData))
                return partData;
            
            throw new KeyNotFoundException($"CarPartElement {carPartElement} is not found in partDataToElement dictionary.");
        }

        private List<CarPartElement> CreateCarPartElements(List<CarPartData> carPartsData)
        {
            var elements = new List<CarPartElement>();
            foreach (var partData in carPartsData)
            {
                var carPartElement = CreateElement(partData);
                partDataToElement.Add(carPartElement, partData);
                elements.Add(carPartElement);
            }

            return elements;
        }

        private CarPartElement CreateElement(CarPartData partData)
        {
            var element = assetProvider.Instantiate<GameObject>(AddressableNames.CarPartElement);
            var carPartElement = element.GetComponent<CarPartElement>();
            carPartElement.Init(partData);
            return carPartElement;
        }
    }
}