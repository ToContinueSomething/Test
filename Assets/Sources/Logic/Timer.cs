using System;
using System.Collections;
using Sources.Logic.Extensions;
using UnityEngine;

namespace Sources.Logic
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _finishTime = 120f;

        private float _timeLeft;
        private Coroutine _coroutine;
        
        private float OneTenth => _finishTime * 10f / 100;
        public bool IsActive { get; private set; }

        public float FinishTime => _finishTime;
        public bool IsOneTenthPassed => _timeLeft < _finishTime - OneTenth;

        public event Action<float> TimeLeftChanged;


        public void Enable()
        {
            _timeLeft = _finishTime;
            IsActive = true;
            
            _coroutine.TryClear(this);
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
                else
                {
                    IsActive = false;
                }

                yield return null;
            }
        }
    }
}