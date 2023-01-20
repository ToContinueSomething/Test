using System;
using UnityEngine;

namespace Sources.Logic.Ball
{
    [RequireComponent(typeof(BallDestroyer))]
    public class Ball : MonoBehaviour
    {
        private BallDestroyer _ballDestroyer;

        private const int MinReward = 7;

        public int Reward => MinReward;

        public event Action Destroyed;

        private void Awake() => 
            _ballDestroyer = GetComponent<BallDestroyer>();

        private void OnEnable() =>
            _ballDestroyer.Destroyed += OnDestroyed;

        private void OnDestroyed()
        {
            Destroyed?.Invoke();
            _ballDestroyer.Destroyed -= OnDestroyed;
        }
    }
}