using System;
using Sources.Logic.Board;
using TMPro;
using UnityEngine;

namespace Sources.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private TimerBoard _timerBoard;

        public void Construct(TimerBoard timerBoard)
        {
            _timerBoard = timerBoard;
            _timerBoard.TimeLeftChanged += OnTimeLeftChanged;
            
          SetTime(timerBoard.FinishTime);
        }

        private void OnDisable()
        {
            if (_timerBoard != null)
                _timerBoard.TimeLeftChanged -= OnTimeLeftChanged;
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