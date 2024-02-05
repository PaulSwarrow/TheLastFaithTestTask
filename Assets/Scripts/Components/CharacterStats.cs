using System;
using System.Collections.Generic;
using DefaultNamespace.Model;
using UnityEngine;

namespace DefaultNamespace
{
    public class CharacterStats : MonoBehaviour
    {
        public delegate void StatChangeDelegate(StatId id, int oldValue, int newValue);
        [SerializeField] private CharacterStatModel[] _stats;

        private Dictionary<StatId, CharacterStatModel> _map;

        private void LazyInit()
        {
            if(_map!=null) return;
            _map = new();
            foreach (var model in _stats)
            {
                _map.Add(model.Id, model);
            }
        }
        

        public void Subscribe(StatId id, StatChangeDelegate handler)
        {
            if (TryGetValue(id, out var stat))
            {
                stat.ChangeEvent += handler;
            }

        }

        public void Unsubscribe(StatId id, StatChangeDelegate handler)
        {
            if (TryGetValue(id, out var stat))
            {
                stat.ChangeEvent -= handler;
            }
            
        }

        public ICharacterStat Get(StatId id)
        {
            if (TryGetValue(id, out var stat))
            {
                return stat;
            }

            //TODO returning dummy stat with 0/0 values may be better solution in some cases
            throw new Exception($"Stat not found: {id}");
        }

        public void ChangeValue(StatId stat, int delta)
        {
            if (TryGetValue(stat, out var statInfo))
            {
                statInfo.Value += delta;
            }
        }

        private bool TryGetValue(StatId id, out CharacterStatModel stat)
        {
            LazyInit();
            return _map.TryGetValue(id, out stat);
        }
    }
}