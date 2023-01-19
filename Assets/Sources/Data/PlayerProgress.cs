using System;
using UnityEngine;

namespace Sources.Data
{
    [Serializable]
    public class PlayerProgress
    {
        private const string BestScorePrefs = "Score";
        public string InitialLevel { get; set; }
        public int Score { get; set; }
        public int BestScore { get; set; }

        public event Action<int> ScoreChanged;
        public event Action<int> BestScoreChanged;

        public PlayerProgress(string initialLevel)
        {
            BestScore = PlayerPrefs.GetInt(BestScorePrefs, 0);
            Score = 0;
            InitialLevel = initialLevel;
        }

        public void AddScore(int score)
        {
            if (score < 0)
                throw new ArgumentException(nameof(score));

            Score += score;
            ScoreChanged?.Invoke(Score);
        }

        public void Save()
        {
            if (Score > BestScore)
            {
                PlayerPrefs.SetInt(BestScorePrefs, Score);
                BestScore = Score;
                BestScoreChanged?.Invoke(BestScore);
            }
        }

        public void Reset()
        {
            Score = 0;
            ScoreChanged?.Invoke(Score);
        }
    }
}