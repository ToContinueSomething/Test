using System;
using UnityEngine;

namespace Sources.Logic.Ball
{
    [RequireComponent(typeof(BallTriggerHandler))]
    public class BallDestroyer : MonoBehaviour
    {
        private BallTriggerHandler _triggerHandler;

        public event Action Destroyed;


        private void Awake()
        {
            _triggerHandler = GetComponent<BallTriggerHandler>();
        }

        private void OnEnable()
        {
            _triggerHandler.Collided += OnCollided;
        }

        private void OnDisable()
        {
            _triggerHandler.Collided -= OnCollided;
        }

        private void OnCollided()
        {
            Destroy();
        }

        private void Destroy()
        {
            Destroyed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}