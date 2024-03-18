using UnityEngine;

namespace Infrastructure.Services
{
    public interface IShaderPropsService
    {
        string GetColorPropName(Shader shader, ColorType colorType);
        string GetMetallicPropName(Shader shader);
        string GetSmoothnessPropName(Shader shader);
        string GetSecondColorIntensityPropName(Shader shader);
        bool IsChameleon(Shader shader);
    }
}