using UnityEngine;

namespace Infrastructure.Services
{
    class ChangeableLight : IChangeableLight
    {
        private Light lightToChange;
        
        public void SetLightToChange(Light light)
        {
            if (light != null) 
                lightToChange = light;
        }

        public Light GetLightToChange() => lightToChange;
    }
}