using Interfaces;
using Managers;
using Model;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(CharacterStats))]
    public class ProjectileLauncherComponent : MonoBehaviour, IAttackBehavior
    {
        [SerializeField] private StatId _damageStat;
        [SerializeField] private float _projectileVelocity = 10;
        [SerializeField] private int _projectileLifespan = 5;
        [SerializeField] private float _projectileRadius = 0.1f;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _cooldown = 0.1f;

        private IGameEntity _owner;
        private CharacterStats _stats;
        private IEntityStat _damage;

        private void Awake()
        {
            _owner = GetComponent<IGameEntity>();
            _stats = GetComponent<CharacterStats>();
            _damage = _stats.Get(_damageStat);
        }

        private float _lastAttack;

        public void Attack()
        {
            if (Time.time - _lastAttack < _cooldown)
            {
                return;
            }

            _lastAttack = Time.time;

            var spec = new ProjectileSpec()
            {
                Damage = _damage.Value,
                Lifespan = _projectileLifespan,
                Radius = _projectileRadius,
                Velocity = _projectileVelocity
            };
            //TODO modify projectile
            ProjectilesManager.Instance.SpawnProjectile(spec, _spawnPoint.position, _spawnPoint.forward, _owner);
        }
    }
}