using Zenject;

namespace Infrastructure.State
{
    public class UIStateMachine
    {
        private readonly DiContainer container;
        private IExitableState activeState;

        public UIStateMachine(DiContainer container)
        {
            this.container = container;
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload param) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(param);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            activeState?.Exit();
            TState state = GetState<TState>();
            activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            var state = container.Resolve<TState>();
            return state;
        }
    }
}