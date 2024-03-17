using System;
using TMPro;
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
        [SerializeField] private Slider lightIntensity;
        [SerializeField] private Button backButton;
        [SerializeField] private Toggle chameleonToggle;
        [SerializeField] private Toggle metallicToggle;
        [SerializeField] private Button enterButton;
        [SerializeField] private Button lowQualityButton;
        [SerializeField] private Image lowQualityImage;
        [SerializeField] private Button highQualityButton;
        [SerializeField] private Image highQualityImage;
        [SerializeField] private Button changeLightButton;
        [SerializeField] private Image changeLightImage;
        
        [SerializeField] private TMP_Text fpsText;
        
        public CarPartsContainer CarPartsContainer => carPartsContainer;
        public FlexibleColorPicker MainColorPicker => mainColorPicker;
        public FlexibleColorPicker SecondColorPicker => secondColorPicker;
        public Slider SecondColorIntensity => secondColorIntensity;
        public Slider LightIntensity => lightIntensity;
        public Button BackButton => backButton;
        public Toggle ChameleonToggle => chameleonToggle;
        public Toggle MetallicToggle => metallicToggle;
        public Button EnterButton => enterButton;
        public Button LowQualityButton => lowQualityButton;
        public Image LowQualityImage => lowQualityImage;
        public Button HighQualityButton => highQualityButton;
        public Image HighQualityImage => highQualityImage;
        public TMP_Text FpsText => fpsText;
        public Button ChangeLightButton => changeLightButton;
        public Image ChangeLightImage => changeLightImage;
    }
}