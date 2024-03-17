using Infrastructure.Services;
using UnityEngine;

namespace Utils
{
    public interface IShaderPropsService : IService
    {
        string GetColorPropName(Shader shader, ColorType colorType);
        string GetMetallicPropName(Shader shader);
        string GetSmoothnessPropName(Shader shader);
        string GetSecondColorIntensityPropName(Shader shader);
        bool IsChameleon(Shader shader);
    }
}