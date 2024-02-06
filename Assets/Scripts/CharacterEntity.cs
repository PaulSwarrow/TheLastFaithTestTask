using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Model
{
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterEntity : MonoBehaviour, IGameEntity
    {
        [SerializeField]//TODO move pick up feature to a dedicated component 
        private bool _canPickUp;
        [SerializeField] //TODO: characters lifecycle system, pools etc
        private bool _destroyOnDeath;
        public event Action<CharacterEntity> DeathEvent; 
        private CharacterStats _stats;
        private void Awake()
        {
            _stats = GetComponent<CharacterStats>();
        }

        private void OnEnable()
        {
            _stats.Subscribe(StatId.Health, OnHealthChange);
        }

        private void OnDisable()
        {
            _stats.Unsubscribe(StatId.Health, OnHealthChange);
        }

        public bool CanPickUp => _canPickUp && _stats.Get(StatId.Health).Value > 0;

        public void ReceiveDamage(int amount)
        {
            _stats.ChangeValue(StatId.Health, -amount);
        }

        public IEntityStat GetStat(StatId id)
        {
            return _stats.Get(id);
        }

        public void ApplyEffect(IEntityEffect effect)
        {
            effect.Apply(_stats);
        }

        private void OnHealthChange(StatId id, int oldvalue, int newvalue)
        {
            if (newvalue <= 0)
            {
                DeathEvent?.Invoke(this);
                if (_destroyOnDeath)
                {
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }
        }

    }
}