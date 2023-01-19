using System.Collections.Generic;
using Sources.Logic.Ball;
using UnityEngine;

namespace Sources.Spawner
{
    public abstract class Spawner<T> : PoolObject<T> where T : MonoBehaviour
    {
        [SerializeField] private List<T> _prefabs;
        [SerializeField] private int _count;

        private void Awake()
        {
            Init(_prefabs, _count);
        }
    }
}