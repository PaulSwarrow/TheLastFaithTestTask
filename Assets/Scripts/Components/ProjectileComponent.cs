using System;
using DefaultNamespace.Model;
using Game.Logic;
using UnityEngine;

namespace DefaultNamespace
{
    public class ProjectileComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        public ProjectileSpec Spec;

        public event Action<ProjectileComponent> LifetimeEndEvent; 

        private Transform _self;


        private float _launchTimestemp;

        private void Awake()
        {
            _self = transform;
        }

        private void OnEnable()
        {
            _launchTimestemp = Time.time;
        }

        private void FixedUpdate()
        {
            if (Time.time - _launchTimestemp > Spec.Lifespan)
            {
                Die();
                return;
            }
            
            //TODO support different movement behaviors here. 
            var delta = Spec.Velocity * Time.fixedDeltaTime;
            if (Physics.SphereCast(_self.position, Spec.Radius, _self.forward, out var hit, delta, layerMask))
            {
                if (GameUtils.GetEntity(hit.collider, out var effectTarget))
                {
                    //apply projectile effect
                    effectTarget.ReceiveDamage(Spec.Damage);
                }

                Die();
            }
            else
            {
                _self.position += _self.forward * delta;
            }
        }

        private void Die()
        {
            LifetimeEndEvent?.Invoke(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Spec.Radius);
        }
        
    }
}