using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.Logic.Ball
{
    public class BallTriggerHandler : MonoBehaviour
    {
        public event Action Collided;
        
        private void OnTriggerEnter(Collider other)
        {
            Collided?.Invoke();
        }
    }
}