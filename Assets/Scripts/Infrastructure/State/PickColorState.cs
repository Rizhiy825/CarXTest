using System;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using Infrastructure.StaticData;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.State
{
    public class PickColorState : IPayloadedState<CarPartData>
    {
        private readonly UIStateMachine stateMachine;
        private readonly IUIFactory uiFactory;
        private readonly ICameraPathFollower cameraPathFollower;
        private readonly IStaticDataService staticDataService;
        private readonly IShaderPropsService shaderPropsService;
        private HUDContainer hudContainer;
        
        private FlexibleColorPicker mainColorPicker;
        private FlexibleColorPicker secondColorPicker;
        private Slider secondColorIntensity;
        private Button backButton;
        private Toggle chameleonToggle;
        private Toggle metallicToggle;
        private CarPartData carPartData;
        private Shader materialShader;
        private string secondColorIntensityPropName;
        private string mainColorPropName;
        private string secondColorPropName;
        private string metallicPropName;

        private CarColorParams carColorParams;

        private ShaderPropParams shaderPropParams;

        private bool isChameleon;

        public PickColorState(UIStateMachine stateMachine,
            IUIFactory uiFactory,
            ICameraPathFollower cameraPathFollower,
            IStaticDataService staticDataService,
            IShaderPropsService shaderPropsService)
        {
            this.stateMachine = stateMachine;
            this.uiFactory = uiFactory;
            this.cameraPathFollower = cameraPathFollower;
            this.staticDataService = staticDataService;
            this.shaderPropsService = shaderPropsService;
        }

        public void Enter(CarPartData carPartData)
        {
            this.carPartData = carPartData;
            materialShader = carPartData.material.shader;
            cameraPathFollower.FollowToPoint(this.carPartData.viewPoint);
            carColorParams = staticDataService.GetCarColorParams();
            
            hudContainer = uiFactory.GetHUD();

            backButton = hudContainer.BackButton;
            mainColorPicker = hudContainer.MainColorPicker;
            secondColorPicker = hudContainer.SecondColorPicker;
            secondColorIntensity = hudContainer.SecondColorIntensity;
            chameleonToggle = hudContainer.ChameleonToggle;
            metallicToggle = hudContainer.MetallicToggle;

            InitPropParams();
            ChangeElementsActive(true);
            SubscribeBackButton();
            SubscribeColorPicker(mainColorPicker, mainColorPropName);
            SubscribeColorPicker(secondColorPicker, secondColorPropName);
            SubscribeIntensitySlider();

            if (isChameleon) 
                InitChameleonToggle();
            
            InitMetallicToggle();
        }

        public void Exit()
        {
            RemoveAllListeners();
            ChangeElementsActive(false);
            secondColorPicker.gameObject.SetActive(false);
            secondColorIntensity.gameObject.SetActive(false);
            cameraPathFollower.FollowToStart();
        }

        private void InitPropParams()
        {
            mainColorPropName = shaderPropsService.GetColorPropName(materialShader, ColorType.Main);
            secondColorPropName = shaderPropsService.GetColorPropName(materialShader, ColorType.Second);
            secondColorIntensityPropName = shaderPropsService.GetSecondColorIntensityPropName(materialShader);
            metallicPropName = shaderPropsService.GetMetallicPropName(materialShader);
            isChameleon = shaderPropsService.IsChameleon(materialShader);
        }

        private void ChangeElementsActive(bool isActive)
        {
            backButton.gameObject.SetActive(isActive);
            mainColorPicker.gameObject.SetActive(isActive);

            if (carPartData.canBeMetallic) 
                metallicToggle.gameObject.SetActive(isActive);

            if (isChameleon) 
                chameleonToggle.gameObject.SetActive(isActive);
        }

        private void SubscribeBackButton() => 
            backButton.onClick.AddListener(() => stateMachine.Enter<CarPartsState>());

        private void SubscribeColorPicker(FlexibleColorPicker picker, string colorPropName)
        {
            picker.onColorChange.AddListener(newColor => ChangeColor(colorPropName, newColor));
            
            if (carPartData.material.HasProperty(colorPropName))
            {
                picker.color = carPartData.material.GetColor(colorPropName);
            }
        }

        private void SubscribeIntensitySlider()
        {
            secondColorIntensity.onValueChanged.AddListener(value =>
            {
                if (carPartData.material.HasProperty(secondColorIntensityPropName))
                {
                    carPartData.material.SetFloat(secondColorIntensityPropName, value);
                }
            });
        }

        private void InitMetallicToggle()
        {
            var currentMetallicValue = carPartData.material.GetFloat(metallicPropName);
            var metallicValue = carColorParams.metallicValue;

            metallicToggle.isOn = Math.Abs(currentMetallicValue - metallicValue) < 0.01;
            metallicToggle.onValueChanged.AddListener(ToggleMetallic);
        }

        private void InitChameleonToggle()
        {
            var mainColor = carPartData.material.GetColor(mainColorPropName);
            var secondColor = carPartData.material.GetColor(secondColorPropName);
            
            chameleonToggle.isOn = mainColor != secondColor;
            chameleonToggle.onValueChanged.AddListener(ToggleChameleon);
            chameleonToggle.onValueChanged.Invoke(chameleonToggle.isOn);
        }

        private void ToggleChameleon(bool isOn)
        {
            if (isOn)
            {
                secondColorPicker.gameObject.SetActive(true);
                secondColorIntensity.gameObject.SetActive(true);
                var selectedColor = secondColorPicker.color;
                ChangeColor(secondColorPropName, selectedColor);

                secondColorIntensity.value = carPartData.material.GetFloat(secondColorIntensityPropName);
            }
            else
            {
                var mainColor = carPartData.material.GetColor(mainColorPropName);
                ChangeColor(secondColorPropName, mainColor);
                secondColorPicker.gameObject.SetActive(false);
                secondColorIntensity.gameObject.SetActive(false);
            }
        }

        private void ToggleMetallic(bool isOn)
        {
            var carColorParams = this.carColorParams;
            
            float metallicValue;
            float smoothnessValue;
            
            if (isOn)
            {
                metallicValue = carColorParams.metallicValue;
                smoothnessValue = carColorParams.smoothnessValue;
            }
            else
            {
                metallicValue = carColorParams.matteMetallicValue;
                smoothnessValue = carColorParams.matteSmoothnessValue;
            }
            
            var metallicPropName = this.metallicPropName;
            var smoothnessPropName = shaderPropsService.GetSmoothnessPropName(materialShader);
            
            carPartData.material.SetFloat(metallicPropName, metallicValue);
            carPartData.material.SetFloat(smoothnessPropName, smoothnessValue);
        }

        private void RemoveAllListeners()
        {
            backButton.onClick.RemoveAllListeners();
            mainColorPicker.onColorChange.RemoveAllListeners();
            chameleonToggle.onValueChanged.RemoveAllListeners();
            metallicToggle.onValueChanged.RemoveAllListeners();
        }

        private void ChangeColor(string colorPropName, Color newColor)
        {
            carPartData.material.SetColor(colorPropName, newColor);
        }
    }
}