using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Model
{
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterEntity : MonoBehaviour, IGameEntity
    {
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

        public void ReceiveDamage(int amount)
        {
            _stats.ChangeValue(StatId.Health, -amount);
        }

        public IEntityStat GetStat(StatId id)
        {
            return _stats.Get(id);
        }

        private void OnHealthChange(StatId id, int oldvalue, int newvalue)
        {
            if (newvalue <= 0)
            {
                //Death
            }
        }

    }
}