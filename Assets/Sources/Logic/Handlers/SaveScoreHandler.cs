using Sources.Data;
using Sources.Logic.Board;

namespace Sources.Logic.Handlers
{
    public class SaveScoreHandler
    {
        private readonly Board.Board _board;
        private readonly ScoreData _scoreData;

        public SaveScoreHandler(Board.Board board,ScoreData scoreData)
        {
            _board = board;
            _scoreData = scoreData;
            _board.Completed += OnBoardCompleted;
        }

        public void Disable() => 
            _board.Completed -= OnBoardCompleted;

        private void OnBoardCompleted() => 
            _scoreData.Save();
    }
}