using Infrastructure.Factory;
using Infrastructure.Services;
using UI;
using UnityEngine.UI;
using Utils;

namespace Infrastructure.State
{
    public class LightSettingsState : IState
    {
        private const float ChangeLimit = 2000;
        
        private readonly UIStateMachine stateMachine;
        private readonly UIFactory uiFactory;
        private readonly IChangeableLight changeableLight;
        private readonly float baseLightIntensity;
        
        private HUDContainer hudContainer;
        private Button lightSettingsButton;
        private Image lightSettingsImage;
        private Slider lightSlider;
        
        private float lastSliderValue = -1;

        public LightSettingsState(UIStateMachine stateMachine, UIFactory uiFactory, IChangeableLight changeableLight)
        {
            this.stateMachine = stateMachine;
            this.uiFactory = uiFactory;
            this.changeableLight = changeableLight;
            
            baseLightIntensity = changeableLight.GetLightToChange().intensity;
        }

        public void Enter()
        {
            hudContainer = uiFactory.GetHUD();

            InitChangeLightButton();
            InitLightSlider();
        }

        private void InitChangeLightButton()
        {
            lightSettingsButton = hudContainer.ChangeLightButton;
            lightSettingsImage = hudContainer.ChangeLightImage;
            lightSettingsImage.color = Colors.RedColor;
            lightSettingsButton.onClick.AddListener(() => stateMachine.Enter<CarPartsState>());
        }

        private void InitLightSlider()
        {
            lightSlider = hudContainer.LightIntensity;
            lightSlider.gameObject.SetActive(true);
            lightSlider.maxValue = ChangeLimit;

            if (lastSliderValue == -1)
                lightSlider.value = ChangeLimit / 2;
            else
                lightSlider.value = lastSliderValue;

            lightSlider.onValueChanged.AddListener(value =>
            {
                lastSliderValue = value;
                changeableLight.GetLightToChange().intensity = baseLightIntensity + value;
            });
        }

        public void Exit()
        {
            lightSettingsImage.color = Colors.GreenColor;
            lightSettingsButton.onClick.RemoveAllListeners();
            
            
            lightSlider.onValueChanged.RemoveAllListeners();
            lightSlider.gameObject.SetActive(false);
        }
    }
}