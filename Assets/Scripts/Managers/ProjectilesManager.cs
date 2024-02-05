﻿using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Model;
using UnityEngine;
using UnityEngine.Assertions;

namespace Managers
{
    public class ProjectilesManager : MonoBehaviour
    {
        //TODO remove singleton, use Dependency Injection instead
        public static ProjectilesManager Instance { get; private set; }
        
        [SerializeField] private ProjectileComponent _projectilePrefab;
        
        private readonly Queue<ProjectileComponent> _pool = new();

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
        }


        public void SpawnProjectile(ProjectileSpec spec, Vector3 position, Vector3 direction)
        {
            var projectile = _pool.Count > 0 ? _pool.Dequeue() : Instantiate(_projectilePrefab);
            projectile.transform.position = position;
            projectile.transform.forward = direction;
            projectile.Spec = spec;
            projectile.LifetimeEndEvent += OnProjectileDie;
            projectile.gameObject.SetActive(true);

        }

        private void OnProjectileDie(ProjectileComponent projectile)
        {
            projectile.LifetimeEndEvent -= OnProjectileDie;
            projectile.gameObject.SetActive(false);
            _pool.Enqueue(projectile);

        }
    }
}