using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Services.Input;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic;
using Sources.Logic.Board;
using Sources.Logic.Handlers;
using Sources.Logic.Player;
using Sources.Spawner;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;
        private readonly IPersistentProgressService _progressService;

        private BallSpawner _ballSpawner;
        private BulletSpawner _bulletSpawner;
        private Camera _camera;
        
        public Timer Timer { get; private set; }
        public Board Board { get; private set; }

        public GameFactory(IAssetProvider assets, IInputService inputService,
            IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _assets = assets;
            _inputService = inputService;
        }

        public GameObject CreatePlayer()
        {
            var player = Instantiate(AssetPath.PlayerPath).GetComponent<PlayerShooting>();
            player.Construct(_inputService, _camera, _bulletSpawner);

            return player.gameObject;
        }

        public void AddHandlers()
        {
            var saveScoreHandler = new SaveScoreHandler(Board,_progressService.Progress.ScoreData);
            var spawnerResetHandler = new SpawnerResetHandler(Board,_bulletSpawner,_ballSpawner);
        }
        
        public void CreateSpawners()
        {
            _ballSpawner = Instantiate(AssetPath.BallSpawnerPath).GetComponent<BallSpawner>();
            _bulletSpawner = Instantiate(AssetPath.BulletSpawnerPath).GetComponent<BulletSpawner>();
        }

        public void CreateTimer()
        {
            Timer = Instantiate(AssetPath.TimerPath).GetComponent<Timer>();
        }
        
        public void CreateBoard()
        {
             Board = Instantiate(AssetPath.BoardPath).GetComponent<Board>();
             _camera = Board.gameObject.GetComponentInChildren<Camera>();
             Board.Construct(_ballSpawner, _progressService.Progress.ScoreData,Timer);
        }

        private GameObject Instantiate(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
            return gameObject;
        }

        private GameObject Instantiate(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath);
            return gameObject;
        }
    }
}