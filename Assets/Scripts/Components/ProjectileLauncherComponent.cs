using DefaultNamespace.Model;
using Managers;
using UnityEngine;

namespace DefaultNamespace
{
    public class ProjectileLauncherComponent : MonoBehaviour, IAttackBehavior
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private ProjectileSpec _baseProjectile;
        [SerializeField] private float _cooldown = 0.1f;

        private float _lastAttack;
        public void Attack()
        {
            if (Time.time - _lastAttack < _cooldown)
            {
                return;
            }

            _lastAttack = Time.time;
            
            var spec = _baseProjectile;
            //TODO modify projectile
            ProjectilesManager.Instance.SpawnProjectile(spec, _spawnPoint.position, _spawnPoint.forward);
        }
    }
}