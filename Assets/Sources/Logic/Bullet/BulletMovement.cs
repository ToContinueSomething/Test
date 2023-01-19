using System;
using System.Collections;
using Sources.Infrastructure;
using UnityEngine;

namespace Sources.Logic.Bullet
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Transform _target;

        public Transform Target => _target;

        private void OnDisable()
        {
            _target = null;
        }

        public void Move(Transform target)
        {
            _target = target;
            StartCoroutine(MoveTo(target.position));
        }

        private IEnumerator MoveTo(Vector3 point)
        {
            bool isReached = false;
            
            while (isReached == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, point,
                    _speed * Time.deltaTime);

                if (Vector3.Distance(transform.position,point) <= Mathf.Epsilon) 
                    isReached = true;

                yield return null;
            }
        }
    }
}