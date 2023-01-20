using System;
using System.Collections.Generic;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic;
using Sources.UI.Factory;
using Sources.WebInit;

namespace Sources.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        public GameStateMachine(WebPresenter webPresenter, SceneLoader sceneLoader, LoadingCurtain loaderCurtain,
            AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadWebState)] = new LoadWebState(this, webPresenter),

                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loaderCurtain,services.Single<IUIFactory>(),
                    services.Single<IGameFactory>()),

                [typeof(LoadProgressState)] = new LoadProgressState(this,
                    services.Single<IPersistentProgressService>()),

                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }


        public void Enter<TState>() where TState : class, IState
        {
            var state = GetState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayLoadState<TPayload>
        {
            var state = GetState<TState>();
            state.Enter(payload);
        }
        
        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}