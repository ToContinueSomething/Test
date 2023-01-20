using Sources.Logic.Board;
using Sources.Spawner;

namespace Sources.Logic.Handlers
{
    public class SpawnerResetHandler
    {
        private readonly Board.Board _board;
        private readonly BulletSpawner _bulletSpawner;
        private readonly BallSpawner _ballSpawner;

        public SpawnerResetHandler(Board.Board board,BulletSpawner bulletSpawner,BallSpawner ballSpawner)
        {
            _board = board;
            _bulletSpawner = bulletSpawner;
            _ballSpawner = ballSpawner;

            _board.Updated += OnBoardUpdated;
        }

        public void Disable() => 
            _board.Updated -= OnBoardUpdated;

        private void OnBoardUpdated()
        { 
            _ballSpawner.DisableBalls();
            _bulletSpawner.DisableBullets();
        }
    }
}