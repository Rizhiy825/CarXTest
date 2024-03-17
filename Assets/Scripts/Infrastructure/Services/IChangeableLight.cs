using UnityEngine;

namespace Infrastructure.Services
{
    public interface IChangeableLight : IService
    {
        public void SetLightToChange(Light light);
        public Light GetLightToChange();
    }
}