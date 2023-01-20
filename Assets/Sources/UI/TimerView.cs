using Sources.Logic;
using Sources.Logic.Board;
using TMPro;
using UnityEngine;

namespace Sources.UI
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Timer _timer;

        public void Construct(Timer timer)
        {
            _timer = timer;
            _timer.TimeLeftChanged += OnTimeLeftChanged;

            SetTime(timer.FinishTime);
        }

        private void OnDisable()
        {
            if (_timer != null)
                _timer.TimeLeftChanged -= OnTimeLeftChanged;
        }

        private void OnTimeLeftChanged(float time)
        {
            if (time <= 0)
                time = 0;

            SetTime(time);
        }

        private void SetTime(float time)
        {
            var seconds = Mathf.FloorToInt(time % 60);

            _text.text = "Time: " + time.ToString();
        }
    }
}