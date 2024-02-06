using Managers;
using UnityEngine;

namespace Components
{
    /// <summary>
    /// Add drop behavior: dead character will spawn some pick-ups
    /// </summary>
    [RequireComponent(typeof(CharacterEntity))]
    public class DropComponent : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _dropChance;
        private CharacterEntity _character;

        private void Awake()
        {
            _character = GetComponent<CharacterEntity>();
            
        }

        private void OnEnable()
        {
            _character.DeathEvent += OnDeath;
        }

        private void OnDisable()
        {
            _character.DeathEvent -= OnDeath;
        }

        private void OnDeath(CharacterEntity obj)
        {
            if (Random.value < _dropChance)
            {
                //TODO provide drop spec
                DropMaqnager.Instance.SpawnEnemyDrop(transform.position);
            }
        }
    }
}