﻿using System;
using Interfaces;
using Managers;
using Model;
using UnityEngine;

namespace Components
{
    /// <summary>
    /// Projectile behavior. Applies damage on hit
    /// </summary>
    public class ProjectileComponent : MonoBehaviour
    {
        public event Action<ProjectileComponent> LifetimeEndEvent;

        [SerializeField] private LayerMask layerMask;
        private ProjectileSpec _spec;

        private Transform _self;


        private float _launchTimestemp;
        private IGameEntity _owner;

        private void Awake()
        {
            _self = transform;
        }

        public void Init(ProjectileSpec spec, IGameEntity owner)
        {
            _spec = spec;
            _owner = owner;
        }

        private void OnEnable()
        {
            _launchTimestemp = GameManager.Instance.GameTime;
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.GameTime - _launchTimestemp > _spec.Lifespan)
            {
                Die();
                return;
            }

            var delta = _spec.Velocity * GameManager.Instance.GameDeltaTime;
            if (Physics.SphereCast(_self.position, _spec.Radius, _self.forward, out var hit, delta, layerMask))
            {
                if (GameUtils.GetEntity(hit.collider, out var effectTarget))
                {
                    //apply projectile effect
                    effectTarget.ReceiveDamage(_spec.Damage, _owner);
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
            Gizmos.DrawWireSphere(transform.position, _spec.Radius);
        }
    }
}