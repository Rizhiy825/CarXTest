using System.Collections.Generic;
using Infrastructure.Factory;
using UI;

namespace Infrastructure.State
{
    public class CarPartsState : IState
    {
        private readonly UIStateMachine stateMachine;
        private readonly IUIFactory uiFactory;
        private List<CarPartElement> carParts;
        private CarPartsContainer carPartsContainer;

        public CarPartsState(UIStateMachine stateMachine, IUIFactory uiFactory)
        {
            this.stateMachine = stateMachine;
            this.uiFactory = uiFactory;
        }
        public void Enter()
        {
            var hudContainer = uiFactory.GetHUD();
            carPartsContainer = hudContainer.CarPartsContainer;
            carPartsContainer.gameObject.SetActive(true);
            
            carParts = carPartsContainer.Elements;
            
            SubscribeCarParts();
        }

        public void Exit()
        {
            foreach (var carPart in carParts)
            {
                carPart.button.onClick.RemoveAllListeners();
            }
            
            carPartsContainer.gameObject.SetActive(false);
        }

        private void SubscribeCarParts()
        {
            foreach (var carPart in carParts)
            {
                carPart.button.onClick.AddListener(delegate { EnterPickColorState(carPart); });
            }
        }

        private void EnterPickColorState(CarPartElement carPartPressed)
        {
            var carPartData = uiFactory.GetCarPartData(carPartPressed);
            stateMachine.Enter<PickColorState, CarPartData>(carPartData);
        }
    }
}