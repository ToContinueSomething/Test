using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Infrastructure.States;
using Sources.Logic.Board;
using Sources.UI.BoardButton;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace Sources.UI.Factory
{
    class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public UIFactory(IAssetProvider assetProvider,IPersistentProgressService progressService,IGameFactory gameFactory)
        {
            _assetProvider = assetProvider;
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void CreateHud()
        {
            Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
            Screen.autorotateToPortrait = false;
            
            var hud =   _assetProvider.Instantiate(AssetPath.HudPath);
            
            var  playButton = hud.GetComponentInChildren<PlayBoardButton>();
            var  restartButton = hud.GetComponentInChildren<RestartBoardButton>();

            restartButton.Construct(playButton,_gameFactory.TimerBoard.transform.GetComponent<BoardBase>(), _progressService);
            playButton.Construct(restartButton,_gameFactory.TimerBoard.transform.GetComponent<BoardBase>(), _progressService);

            var score = hud.GetComponentInChildren<Score>();
            score.Construct(_progressService);

            var timer = hud.GetComponentInChildren<Timer>();
            timer.Construct(_gameFactory.TimerBoard);
        }
    }
}