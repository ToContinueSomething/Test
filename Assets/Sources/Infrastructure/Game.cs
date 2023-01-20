using Sources.Infrastructure.Services;
using Sources.Infrastructure.States;
using Sources.Logic;
using Sources.WebInit;

namespace Sources.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, WebPresenter webPresenter)
        {
            StateMachine = new GameStateMachine(webPresenter, new SceneLoader(coroutineRunner), curtain,
                AllServices.Container);
        }
    }
}