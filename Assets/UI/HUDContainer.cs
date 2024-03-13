using System;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class HUDContainer : MonoBehaviour
    {
        [Header("Auto Parts")] 
        [SerializeField] private CarPartsContainer carPartsContainer;
        [SerializeField] private FlexibleColorPicker colorPicker;
        
        public CarPartsContainer CarPartsContainer => carPartsContainer;
        public FlexibleColorPicker ColorPicker => colorPicker;
    }
}