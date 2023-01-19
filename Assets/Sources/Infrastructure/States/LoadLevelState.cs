using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic;
using Sources.UI.Factory;
using UnityEngine;

namespace Sources.Infrastructure.States
{
    public class LoadLevelState : IPayLoadState<string>
    {
        private const string InitialPoint = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly LoadingCurtain _loaderCurtain;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loaderCurtain,IUIFactory uiFactory ,IGameFactory gameFactory)
        {
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loaderCurtain = loaderCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string payLoad)
        {
            _loaderCurtain.Show();
            _sceneLoader.Load(payLoad, OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            _loaderCurtain.Hide();
            InitGameWorld();
            InitUI();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitUI()
        {
            _uiFactory.CreateHud();
        }
        
        private void InitGameWorld()
        {
            _gameFactory.CreateSpawners();
            _gameFactory.CreateBoard();
            _gameFactory.CreatePlayer();
        }
    }
}