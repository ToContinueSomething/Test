using System;

namespace Sources.UI.BoardButton
{
    public class PlayBoardButton : BoardButton
    {
        private void Start()
        {
            Button.DisableRaycastTarget();
        }

        protected override void OnUIButtonClick()
        {
            Button.EnableRaycastTarget();
            base.OnUIButtonClick();
            gameObject.SetActive(false);
        }
    }
}