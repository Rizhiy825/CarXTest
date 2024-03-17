using AYellowpaper.SerializedCollections;
using Infrastructure.Services;
using Infrastructure.StaticData;
using UnityEngine;

namespace Utils
{
    public class ShaderPropsService : IShaderPropsService
    {
        private readonly IStaticDataService staticDataService;
        private SerializedDictionary<Shader, ShaderPropParams> names = new();
        private bool propNames;

        public ShaderPropsService(IStaticDataService staticDataService)
        {
            names = staticDataService.GetShaderPropNames().Names;
        }
        
        public string GetColorPropName(Shader shader, ColorType colorType)
        {
            if (!IsValidShader(shader)) 
                return "_BaseColor";

            this.propNames = names.TryGetValue(shader, out var propNames);
            
            return colorType switch
            {
                ColorType.Main => this.propNames ? propNames.mainColorPropName : "",
                ColorType.Second => this.propNames ? propNames.secondColorPropName : "",
                _ => DefaultColorPropName(shader)
            };
        }

        public string GetMetallicPropName(Shader shader)
        {
            if (!IsValidShader(shader))
                return  "_Metallic";

            return names[shader].metallicPropName;
        }

        public string GetSmoothnessPropName(Shader shader)
        {
            if (!IsValidShader(shader))
                return "_Smoothness";
            
            return names[shader].smoothnessPropName;
        }

        public string GetSecondColorIntensityPropName(Shader shader)
        {
            if (!IsValidShader(shader))
                return "_SecondColorIntensity";
            
            return names[shader].secondColorIntensityPropName;
        }

        public bool IsChameleon(Shader shader)
        {
            if (!IsValidShader(shader))
                return false;
            
            return names[shader].isChameleon;
        }

        private string DefaultColorPropName(Shader shader)
        {
            Debug.LogError("Unknown color picker type");
            return names[shader].mainColorPropName;
        }

        private bool IsValidShader(Shader shader)
        {
            if (!names.ContainsKey(shader))
            {
                Debug.LogError($"Unknown shader {shader.name}. Please add it to shaderPropNamesMap.");
                {
                    return false;
                }
            }

            return true;
        }
    }
    
    
    public enum ColorType
    {
        Main,
        Second
    }
}