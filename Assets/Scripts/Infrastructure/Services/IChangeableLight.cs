using UnityEngine;

namespace Infrastructure.Services
{
    public interface IChangeableLight
    {
        public void SetLightToChange(Light light);
        public Light GetLightToChange();
    }
}