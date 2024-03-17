using Infrastructure.Profiler;
using Infrastructure.Services;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Infrastructure.State
{
    public class StartMenuState : IState
    {
        private const int LowQualityLevelNumber = 1;
        private const int HighQualityLevelNumber = 0;
        
        private readonly UIStateMachine stateMachine;
        private readonly ILevelFactory levelFactory;
        private readonly IUIFactory uiFactory;
        
        private Button enterButton;
        private Button lowQualityButton;
        private Image lowQualityImage;
        private Button highQualityButton;
        private Image highQualityImage;

        private HUDContainer hudContainer;

        public StartMenuState(UIStateMachine stateMachine, ILevelFactory levelFactory, IUIFactory uiFactory)
        {
            this.stateMachine = stateMachine;
            this.levelFactory = levelFactory;
            this.uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            CreateHUD();

            InitEnterButton();
            InitProfiler();
            InitQualityButtons();
        }

        public void Exit()
        {
            enterButton.onClick.RemoveAllListeners();
            enterButton.gameObject.SetActive(false);
        }

        private void InitProfiler()
        {
            var profiler = new GameObject("Profiler").AddComponent<FpsProfiler>();
            profiler.DrawInfo = hudContainer.FpsText;
        }

        private void InitQualityButtons()
        {
            lowQualityButton = hudContainer.LowQualityButton;
            lowQualityImage = hudContainer.LowQualityImage;
            highQualityButton = hudContainer.HighQualityButton;
            highQualityImage = hudContainer.HighQualityImage;
            
            lowQualityButton.onClick.AddListener(delegate { InitQualityButton(lowQualityButton); });
            highQualityButton.onClick.AddListener(delegate { InitQualityButton(highQualityButton); });
        }

        private void InitQualityButton(Button button)
        {
            if (button == lowQualityButton)
            {
                SetButtonColor(highQualityImage, Colors.GreenColor);
                SetButtonColor(lowQualityImage, Colors.RedColor);
                highQualityButton.enabled = true;
                lowQualityButton.enabled = false;
                QualitySettings.SetQualityLevel(LowQualityLevelNumber);
            }
            else
            {
                SetButtonColor(lowQualityImage, Colors.GreenColor);
                SetButtonColor(highQualityImage, Colors.RedColor);
                lowQualityButton.enabled = true;
                highQualityButton.enabled = false;
                QualitySettings.SetQualityLevel(HighQualityLevelNumber);
            }
        }

        private void SetButtonColor(Image buttonImage, Color32 color) => 
            buttonImage.color = color;

        private void InitEnterButton()
        {
            enterButton = hudContainer.EnterButton;
            enterButton.onClick.AddListener(() => stateMachine.Enter<CarPartsState>());
        }

        private void CreateHUD()
        {
            var carPartsData = levelFactory.GetCarPartsData();
            uiFactory.CreateHUD(carPartsData.Parts);
            hudContainer = uiFactory.GetHUD();
        }
    }
}