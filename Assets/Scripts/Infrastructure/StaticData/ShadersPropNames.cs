using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "ShaderPropParams", menuName = "StaticData/Shader prop params")]
    [Serializable]
    public class ShadersPropNames : ScriptableObject
    {
        [SerializeField] public SerializedDictionary<Shader, ShaderPropParams> Names;
    }
    
    [Serializable]
    public class ShaderPropParams
    {
        public string mainColorPropName;
        public string secondColorPropName;
        public string metallicPropName;
        public string smoothnessPropName;
        public string secondColorIntensityPropName;
        public bool isChameleon;
    }
}