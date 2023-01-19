using System;
using System.Collections;
using UnityEngine;

namespace Sources.Logic.Board
{
    public class TimerBoard : MonoBehaviour
    {
        [SerializeField] private float _finishTime = 120f;

        private float _timeLeft = 0f;
        private Coroutine _coroutine;
        
        private float OneTenth => _finishTime * 10f / 100;
        public bool IsActive { get; private set; }

        public float FinishTime => _finishTime;
        public bool IsOneTenthPassed => _timeLeft < _finishTime - OneTenth;

        public event Action<float> TimeLeftChanged;


        public void Enable()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _timeLeft = _finishTime;
            IsActive = true;
            _coroutine = StartCoroutine(Tick());
        }

        private IEnumerator Tick()
        {
            while (IsActive)
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.deltaTime;
                    TimeLeftChanged?.Invoke(_timeLeft);
                }

                if (_timeLeft <= 0)
                    IsActive = false;

                yield return null;
            }
        }
    }
}