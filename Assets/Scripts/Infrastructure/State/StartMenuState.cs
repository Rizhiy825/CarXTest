using Infrastructure.Factory;
using UnityEngine.UI;

namespace Infrastructure.State
{
    public class StartMenuState : IState
    {
        private readonly UIStateMachine stateMachine;
        private readonly ILevelFactory levelFactory;
        private readonly IUIFactory uiFactory;
        
        private Button enterButton;

        public StartMenuState(UIStateMachine stateMachine, ILevelFactory levelFactory, IUIFactory uiFactory)
        {
            this.stateMachine = stateMachine;
            this.levelFactory = levelFactory;
            this.uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            CreateHUD();
        }

        public void Exit()
        {
            enterButton.onClick.RemoveAllListeners();
            enterButton.gameObject.SetActive(false);
        }

        private void CreateHUD()
        {
            var carPartsData = levelFactory.GetCarPartsData();
            uiFactory.CreateHUD(carPartsData.Parts);
            enterButton = uiFactory.GetHUD().EnterButton;
            enterButton.onClick.AddListener(() => stateMachine.Enter<CarPartsState>());
        }
    }
}