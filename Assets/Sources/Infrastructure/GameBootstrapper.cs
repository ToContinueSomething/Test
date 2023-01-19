using Sources.Infrastructure.States;
using Sources.Logic;
using Sources.WebInit;
using UnityEngine;

namespace Sources.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    [SerializeField] private LoadingCurtain _curtainPrefab;
    [SerializeField] private WebPresenter _webPresenter;
    
    private Game _game;

    private void Awake()
    {
      _game = new Game(this, Instantiate(_curtainPrefab),_webPresenter);
      _game.StateMachine.Enter<LoadWebState>();

      DontDestroyOnLoad(this);
    }
  }
}