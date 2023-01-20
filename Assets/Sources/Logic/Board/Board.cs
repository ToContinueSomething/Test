using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Data;
using Sources.Logic.Extensions;
using Sources.Spawner;
using UnityEngine;

namespace Sources.Logic.Board
{
    [RequireComponent(typeof(Timer))]
    public class Board : MonoBehaviour
    {
        private List<CellBoard> _cells;
        private BallSpawner _ballSpawner;

        private Timer _timer;
        private ScoreData _scoreData;
        
        private Coroutine _сoroutine;

        public event Action Updated;
        public event Action Completed;

        public void Construct(BallSpawner ballSpawner, ScoreData scoreData, Timer timer)
        {
            _scoreData = scoreData;
            _ballSpawner = ballSpawner;
            _timer = timer;
        }

        private void Awake()
        {
            _cells = GetComponentsInChildren<CellBoard>().ToList();
            Application.targetFrameRate = 60;
        }

        public void StartFill()
        {
            Reset();

            _timer.Enable();
            _сoroutine = StartCoroutine(Fill());
        }

        private IEnumerator Fill()
        {
            while (_timer.IsActive)
            {
                for (int i = 0; i < _cells.Count; i++)
                {
                    if (IsHalfBusy() == false)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, _cells.Count);
                        yield return TryPutBall(randomIndex);
                    }

                    if (_timer.IsActive == false)
                        break;
                }

                yield return null;
            }

            Completed?.Invoke();
            Reset();
        }

        private IEnumerator TryPutBall(int randomIndex)
        {
            if (_cells[randomIndex].IsBusy == false)
            {
                _cells[randomIndex].Init(_ballSpawner.GetBall(), _scoreData);

                yield return GetDelay();
            }
        }

        private object GetDelay() =>
            _timer.IsOneTenthPassed ? new WaitForSeconds(0.7f) : new WaitForSeconds(1f);

        private void Reset()
        {
            _сoroutine.TryClear(this);
            Clear();
            
            Updated?.Invoke();
        }

        private void Clear()
        {
            foreach (var cell in _cells)
                cell.Clear();
        }

        private bool IsHalfBusy()
        {
            var halfCells = (_cells.Count / 2) + 1;

            int counterBusy = _cells.Count(cell => cell.IsBusy);

            return counterBusy >= halfCells;
        }
    }
}