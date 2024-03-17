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
        [SerializeField] private FlexibleColorPicker mainColorPicker;
        [SerializeField] private FlexibleColorPicker secondColorPicker;
        [SerializeField] private Slider secondColorIntensity;
        [SerializeField] private Button backButton;
        [SerializeField] private Toggle chameleonToggle;
        [SerializeField] private Toggle metallicToggle;
        [SerializeField] private Button enterButton;
        
        public CarPartsContainer CarPartsContainer => carPartsContainer;
        public FlexibleColorPicker MainColorPicker => mainColorPicker;
        public FlexibleColorPicker SecondColorPicker => secondColorPicker;
        public Slider SecondColorIntensity => secondColorIntensity;
        public Button BackButton => backButton;
        public Toggle ChameleonToggle => chameleonToggle;
        public Toggle MetallicToggle => metallicToggle;
        public Button EnterButton => enterButton;
    }
}