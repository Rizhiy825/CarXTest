using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class HUDContainer : MonoBehaviour
    {
        [Header("Auto Parts")] 
        [SerializeField] private CarPartsContainer carPartsContainer;
        [SerializeField] private FlexibleColorPicker colorPicker;
        [SerializeField] private Button backButton;
        
        public CarPartsContainer CarPartsContainer => carPartsContainer;
        public FlexibleColorPicker ColorPicker => colorPicker;
        public Button BackButton => backButton;
    }
}