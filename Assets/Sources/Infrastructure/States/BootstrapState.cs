using System;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Input;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.UI.Factory;
using UnityEngine;

namespace Sources.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();

        }
        
        public void Enter()
        {
            _sceneLoader.Load(InitialScene,EnterProgressState);
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(new InputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(),
                _services.Single<IInputService>(),_services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>()
                ,_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }
        
        private void EnterProgressState()
        {
          _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}