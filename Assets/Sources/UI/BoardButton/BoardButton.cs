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
        
        private BoardBase _board;
        private IPersistentProgressService _progressService;
        
        protected BoardButton Button;

        public void Construct(BoardButton button,BoardBase board,IPersistentProgressService progressService)
        {
            Button = button;
            _progressService = progressService;
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
            _progressService.Progress.Reset();
        }
    }
}