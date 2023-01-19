using System;
using UnityEngine;

namespace Sources.Logic.Ball
{
    [RequireComponent(typeof(BallDestroyer))]
    public class Ball : MonoBehaviour
    {
        private BallDestroyer _ballDestroyer;
        private MeshRenderer _meshRenderer;

        private const int _reward = 7;

        public int Reward => _reward;
        public Color Color { get; private set; }

        public event Action Destroyed;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            Color = _meshRenderer.material.color;
            _ballDestroyer = GetComponent<BallDestroyer>();
        }

        private void OnEnable() => _ballDestroyer.Destroyed += OnDestroyed;

        private void OnDestroyed()
        {
            Destroyed?.Invoke();
            _ballDestroyer.Destroyed -= OnDestroyed;
        }
    }
}