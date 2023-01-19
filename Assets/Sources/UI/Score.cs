using Sources.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace Sources.UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;    
        [SerializeField] private TMP_Text _bestScore;
        
        private IPersistentProgressService _progressService;

        public void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;
            
            _progressService.Progress.ScoreChanged += OnScoreChanged;
            _progressService.Progress.BestScoreChanged += OnBestScoreChanged;
            
            _score.text = "Current: " + progressService.Progress.Score.ToString();
            _bestScore.text = "Best: " + _progressService.Progress.BestScore.ToString();
        }

        private void OnDisable()
        {
            if (_progressService != null)
            {
                _progressService.Progress.ScoreChanged -= OnScoreChanged;
                _progressService.Progress.BestScoreChanged -= OnBestScoreChanged;
            }
        }

        private void OnBestScoreChanged(int score)
        {
            _bestScore.text = "Best: " + score;
        }

        private void OnScoreChanged(int score)
        {
            _score.text = "Current: " + score.ToString();
        }
    }
}