using System;
using Sources.Infrastructure;
using UnityEngine;

namespace Sources.Logic.Bullet
{
    [RequireComponent(typeof(BulletMovement))]
    public class Bullet : MonoBehaviour,ICoroutineRunner
    {
        private BulletMovement _movement;
        
        public Transform Target => _movement.Target;  

        private void Awake()
        {
            _movement = GetComponent<BulletMovement>();
        }

        public void Move(Transform target)
        {
            _movement.Move(target);
        }
    }
}