using System;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "CarColorParams", menuName = "StaticData/Car color params")]
    [Serializable]
    public class CarColorParams : ScriptableObject
    {
        public float metallicValue;
        public float smoothnessValue;
        
        public float matteMetallicValue;
        public float matteSmoothnessValue;
    }
}