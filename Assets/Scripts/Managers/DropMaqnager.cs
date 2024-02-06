using Components;
using Model.Effects.Configs;
using UnityEngine;
using UnityEngine.Assertions;

namespace Managers
{
    /// <summary>
    /// Manages drops from killed enenies
    /// TODO: pooling
    /// </summary>
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
            var config = _variants[Random.Range(0, _variants.Length)];
            var container = Instantiate(_containerPrefab, position, Quaternion.identity);
            container.Init(config);
        }
    }
}