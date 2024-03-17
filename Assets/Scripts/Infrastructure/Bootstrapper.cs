﻿using Infrastructure.Factory;
using Infrastructure.State;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        public Transform carStartPosition;

        private ILevelFactory levelFactory;
        private IUIFactory uIFactory;
        private UIStateMachine stateMachine;

        [Inject]
        public void Construct(UIStateMachine stateMachine, ILevelFactory levelFactory)
        {
            this.stateMachine = stateMachine;
            this.levelFactory = levelFactory;
        }
        
        private void Start()
        {
            levelFactory.CreateCar(carStartPosition);
            stateMachine.Enter<StartMenuState>();
        }
    }
}