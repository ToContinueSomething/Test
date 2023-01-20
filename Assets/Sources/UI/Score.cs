using Sources.Data;
using TMPro;
using UnityEngine;

namespace Sources.UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _bestScore;

        private ScoreData _scoreData;

        public void Construct(ScoreData scoreData)
        {
            _scoreData = scoreData;

            _scoreData.ValueChanged += OnScoreChanged;
            _scoreData.BestValueChanged += OnBestScoreChanged;

            SetScore(_scoreData.Value);
            SetBestScore(_scoreData.BestValue);
        }

        private void OnDisable()
        {
            if (_scoreData != null)
            {
                _scoreData.ValueChanged -= OnScoreChanged;
                _scoreData.BestValueChanged -= OnBestScoreChanged;
            }
        }

        private void OnScoreChanged(int score)
        {
            SetScore(score);
        }

        private void OnBestScoreChanged(int bestScore)
        {
            SetBestScore(bestScore);
        }

        private void SetBestScore(int bestScore) =>
            _bestScore.text = "Best: " + bestScore;

        private void SetScore(int score) =>
            _score.text = "Current: " + score;
    }
}