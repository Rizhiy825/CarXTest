using UnityEngine;

namespace Infrastructure.Factory
{
    public interface ILevelFactory
    {
        void CreateCar(Transform carStartPosition);
        CarPartsData GetCarPartsData();
    }
}