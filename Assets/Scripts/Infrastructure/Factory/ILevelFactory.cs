using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface ILevelFactory
    {
        GameObject CreateHUD(List<CarPartData> carPartsData);
        CarPartsData CreateCar(Transform carStartPosition);
    }
}