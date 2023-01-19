using Sources.Logic.Ball;
using Sources.Logic.Bullet;
using UnityEngine;

namespace Sources.Spawner
{
    public class BulletSpawner : Spawner<Bullet>
    {
        public Bullet GetBullet() => GetObject();

        public bool CheckBulletOnTarget(Transform target)
        {
            foreach (var bullet in Pool)
            {
                if (bullet.Target == target)
                    return true;
            }

            return false;
        }

        public void DisableBullets()
        {
            DisableAll();
        }
    }
}