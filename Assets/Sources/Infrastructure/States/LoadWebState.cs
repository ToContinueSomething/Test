using Sources.Infrastructure.Services.Input;
using Sources.WebInit;
using UnityEngine;

namespace Sources.Infrastructure.States
{
    public class LoadWebState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly WebPresenter _webPresenter;

        public LoadWebState(GameStateMachine gameStateMachine, WebPresenter webPresenter)
        {
            _gameStateMachine = gameStateMachine;
            _webPresenter = webPresenter;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
           _webPresenter.StartWeb(EnterRegisterState);
        }

        private void EnterRegisterState()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}