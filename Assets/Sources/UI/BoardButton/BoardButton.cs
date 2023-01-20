using Sources.Data;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic.Board;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.BoardButton
{
    public abstract class BoardButton : MonoBehaviour
    {
        [SerializeField] private UIButton _uiButton;
        [SerializeField] private Image _image;
        
        private Board _board;
        private ScoreData _scoreData;
        
        protected BoardButton Button;

        public void Construct(BoardButton button,Board board,ScoreData scoreData)
        {
            Button = button;
            _scoreData = scoreData;
            _board = board;
        }

        private void OnEnable() =>
            _uiButton.Clicked += OnUIButtonClick;

        private void OnDisable() => 
            _uiButton.Clicked -= OnUIButtonClick;

        public void DisableRaycastTarget() => 
             _image.raycastTarget = false;

        public void EnableRaycastTarget() => 
            _image.raycastTarget = true;

        protected virtual void OnUIButtonClick()
        {
            _board.StartFill();
            _scoreData.Reset();
        }
    }
}