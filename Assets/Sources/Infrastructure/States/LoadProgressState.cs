using Sources.Data;
using Sources.Infrastructure.Services.PersistentProgress;

namespace Sources.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string GameScene = "Game";
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
        }

        public void Enter()
        {
            _progressService.Progress = GetNewProgress();
            
            _gameStateMachine.Enter<LoadLevelState, string>
                (_progressService.Progress.InitialLevel);
        }

        private PlayerProgress GetNewProgress()
        {
            return new PlayerProgress(GameScene);
        }
    }
}