using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic.Effect;
using UnityEngine;

namespace Sources.Logic.Board
{
    public class CellBoard : MonoBehaviour
    {
        private const float Offset = 2f;
        private Ball.Ball _ball;
        private IPersistentProgressService _progressService;
        public bool IsBusy { get; private set; }

        public void Init(Ball.Ball ball, IPersistentProgressService persistentProgressService)
        {
            IsBusy = true;
            _progressService = persistentProgressService;
            _ball = ball;

            SetBall();
            SubscribeBall();
        }

        private void SetBall()
        {
            _ball.transform.position = transform.position;
            _ball.gameObject.SetActive(true);
        }

        private void SubscribeBall()
        {
            _ball.Destroyed += OnBallDestroyed;
        }

        public void Clear()
        {
            Reset();
            
            if(_ball != null)
                UnsubscribeBall();
        }

        private void OnBallDestroyed()
        {
            _progressService.Progress.AddScore(_ball.Reward);
            UnsubscribeBall();
            Invoke(nameof(Reset), 0.5f);
        }

        private void UnsubscribeBall()
        {
            _ball.Destroyed -= OnBallDestroyed;
            _ball = null;
        }


        private void Reset() =>
            IsBusy = false;
    }
}