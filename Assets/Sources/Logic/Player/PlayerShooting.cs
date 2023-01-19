using System;
using Sources.Infrastructure.Services.Input;
using Sources.Spawner;
using UnityEngine;

namespace Sources.Logic.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform _shotPoint;

        private BulletSpawner _bulletSpawner;
        private IInputService _inputService;
        private Camera _camera;

        public void Construct(IInputService inputService,Camera mainCamera, BulletSpawner bulletSpawner)
        {
            _camera = mainCamera;
            _bulletSpawner = bulletSpawner;
            _inputService = inputService;
            _inputService.Clicked += OnClicked;
        }

        private void OnClicked()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
                TryShoot(hit.transform);
        }

        private bool TryShoot(Transform target)
        {
            if (_bulletSpawner.CheckBulletOnTarget(target) == false)
            {
                var bullet = GetBullet();
                bullet.Move(target);
                return true;
            }

            return false;
        }

        private Bullet.Bullet GetBullet()
        {
            var bullet = _bulletSpawner.GetBullet().GetComponent<Bullet.Bullet>();
            bullet.transform.position = _shotPoint.position;
            bullet.gameObject.SetActive(true);
            return bullet;
        }
    }
}