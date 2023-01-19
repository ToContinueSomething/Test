using System;
using Sources.Logic.Ball;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.Logic.Bullet
{
    public class BulletTriggerHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            transform.gameObject.SetActive(false);
        }
    }
}