using System;
using Configs;
using DefaultNamespace;
using DefaultNamespace.Model;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Managers
{
    public class DropMaqnager : MonoBehaviour
    {
        [SerializeField] private EffectConfig[] _variants;
        [SerializeField] private PickUpComponent _containerPrefab;
        
        public static DropMaqnager Instance { get; private set; }

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
            
        }

        public void SpawnEnemyDrop(Vector3 position)
        {
            var config = _variants[Random.Range(0, _variants.Length - 1)];
            var container = Instantiate(_containerPrefab, position, Quaternion.identity);
            container.Init(config);
        }
    }
}