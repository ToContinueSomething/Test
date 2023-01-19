using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Spawner;
using UnityEngine;

namespace Sources.Logic.Board
{
    [RequireComponent(typeof(TimerBoard))]
    public class BoardBase : MonoBehaviour
    {
        private List<CellBoard> _cells;
        private BallSpawner _ballSpawner;
    
        private float _currentTime;
        private TimerBoard _timer;
        private IPersistentProgressService _progressService;
        private BulletSpawner _bulletSpawner;
        private Coroutine _сoroutine;

        public void Construct(BallSpawner ballSpawner,BulletSpawner bulletSpawner,IPersistentProgressService progressService)
        {
            _bulletSpawner = bulletSpawner;
            _progressService = progressService;
            _ballSpawner = ballSpawner;
            
            _timer = GetComponent<TimerBoard>();
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
                    int randomIndex = UnityEngine.Random.Range(0, _cells.Count);
                    
                    if (IsHalfBusy() == false)
                        yield return TryPutBall(randomIndex);
                    
                    if(_timer.IsActive == false)
                        break;
                }

                yield return null;
            }

            _progressService.Progress.Save();
            Reset();
        }

        private void Reset()
        {
            if (_сoroutine != null)
            {
                StopCoroutine(_сoroutine);
                _сoroutine = null;
            }
            
            Clear();
            _bulletSpawner.DisableBullets();
            _ballSpawner.DisableBalls();
        }

        private bool IsHalfBusy()
        {
            var halfCells = (_cells.Count / 2) + 1;

            List<CellBoard> cellBusy = _cells.Where(cell => cell.IsBusy).ToList();

            return cellBusy.Count >= halfCells;
        }

        private IEnumerator TryPutBall(int randomIndex)
        {
            if (_cells[randomIndex].IsBusy == false)
            {
                _cells[randomIndex].Init(_ballSpawner.GetBall(),_progressService);

                yield return GetDelay();
            }
        }

        private object GetDelay() => 
            _timer.IsOneTenthPassed ? new WaitForSeconds(0.7f) : new WaitForSeconds(1f);

        private void Clear()
        {
            foreach (var cell in _cells) 
                cell.Clear();
        }
    }
}