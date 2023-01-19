using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Spawner
{
    public class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        private List<T> _pool;
        public IReadOnlyCollection<T> Pool => _pool;

        protected void Init(T template, int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++)
            {
                var spawned = Object.Instantiate<T>(template);
                spawned.gameObject.SetActive(false);
                _pool.Add(spawned);
            }
        }

        protected void Init(List<T> templates, int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < templates.Count; i++)
            {
                CreateObject(templates, i);
            }

            for (int i = 0; i < count; i++)
            {
                var randomIndex = Random.Range(0, templates.Count);
                CreateObject(templates, randomIndex);
            }
        }

        private void CreateObject(List<T> templates, int i)
        {
            var spawned = Object.Instantiate<T>(templates[i]);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned);
        }

        protected T GetObject() =>
            _pool.FirstOrDefault(o => o.gameObject.activeSelf == false);

        protected void DisableAll()
        {
            foreach (var obj in _pool)
            {
                if(obj.gameObject.activeSelf)
                    obj.gameObject.SetActive(false);
            }
        }
    }
}