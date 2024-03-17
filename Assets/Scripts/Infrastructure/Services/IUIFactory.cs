using System.Collections.Generic;
using UI;

namespace Infrastructure.Services
{
    public interface IUIFactory
    {
        void CreateHUD(List<CarPartData> carPartsData);
        HUDContainer GetHUD();
        CarPartData GetCarPartData(CarPartElement carPartElement);
    }
}