using UnityEngine;

namespace Infrastructure.Services
{
    public interface ILevelFactory
    {
        void CreateCar(Transform carStartPosition);
        CarPartsData GetCarPartsData();
    }
}