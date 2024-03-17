using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticData;
using UnityEngine;

namespace Infrastructure.Services
{
    public class ShaderPropsService : IShaderPropsService
    {
        private readonly IStaticDataService staticDataService;
        private Dictionary<string, ShaderPropParams> names = new();
        private bool propNames;

        public ShaderPropsService(IStaticDataService staticDataService)
        {
            names = staticDataService
                .GetShaderPropNames()
                .Names
                .ToDictionary(k => k.Key.name, v => v.Value);
        }
        
        public string GetColorPropName(Shader shader, ColorType colorType)
        {
            if (!IsValidShader(shader)) 
                return "_BaseColor";

            this.propNames = names.TryGetValue(shader.name, out var propNames);
            
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

            return names[shader.name].metallicPropName;
        }

        public string GetSmoothnessPropName(Shader shader)
        {
            if (!IsValidShader(shader))
                return "_Smoothness";
            
            return names[shader.name].smoothnessPropName;
        }

        public string GetSecondColorIntensityPropName(Shader shader)
        {
            if (!IsValidShader(shader))
                return "_SecondColorIntensity";
            
            return names[shader.name].secondColorIntensityPropName;
        }

        public bool IsChameleon(Shader shader)
        {
            if (!IsValidShader(shader))
                return false;
            
            return names[shader.name].isChameleon;
        }

        private string DefaultColorPropName(Shader shader)
        {
            Debug.LogError("Unknown color picker type");
            return names[shader.name].mainColorPropName;
        }

        private bool IsValidShader(Shader shader)
        {
            if (!names.ContainsKey(shader.name))
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