using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.UI.BoardButton;
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

            restartButton.Construct(playButton,_gameFactory.Board, _progressService.Progress.ScoreData);
            playButton.Construct(restartButton,_gameFactory.Board, _progressService.Progress.ScoreData);

            var score = hud.GetComponentInChildren<Score>();
            score.Construct(_progressService.Progress.ScoreData);

            var timer = hud.GetComponentInChildren<TimerView>();
            timer.Construct(_gameFactory.Timer);
        }
    }
}