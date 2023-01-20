using Sources.Data;
using UnityEngine;

namespace Sources.Logic.Board
{
    public class CellBoard : MonoBehaviour
    {
        private const float Delay = 0.5f;
        
        private Ball.Ball _ball;
        private ScoreData _scoreData;
        public bool IsBusy { get; private set; }

        public void Init(Ball.Ball ball, ScoreData scoreData)
        {
            IsBusy = true;
            _scoreData = scoreData;
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
            _scoreData.Add(_ball.Reward);
            UnsubscribeBall();
            Invoke(nameof(Reset), Delay);
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