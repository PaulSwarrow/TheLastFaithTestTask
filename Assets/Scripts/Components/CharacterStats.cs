using System;
using System.Collections.Generic;
using DefaultNamespace.Model;
using DefaultNamespace.Model.Impl;
using UnityEngine;

namespace DefaultNamespace
{
    public class CharacterStats : MonoBehaviour
    {
        public delegate void StatChangeDelegate(StatId id, int oldValue, int newValue);

        [SerializeField] private SimpleEntityStatModel[] _stats;

        //TODO: more flexible configuration(as a list of different stats, custom inspector needed)
        [SerializeField] private LimitedStatModel _health;
        [SerializeField] private FunctionStatModel _speed;
        [SerializeField] private FunctionStatModel _damage;


        private Dictionary<StatId, IEntityStat> _map;
        private DynamicStatModel[] _dynamicStats;

        private void LazyInit()
        {
            if (_map != null) return;
            _map = new();
            foreach (var model in _stats)
            {
                _map.Add(model.Id, model);
            }

            _dynamicStats = new DynamicStatModel[] { _health, _speed, _damage };

            foreach (var model in _dynamicStats)
            {
                //assumption: dynamic stats can not depend on each other!
                model.Init(_map);
                _map.Add(model.Id, model);
            }
        }

        private void OnDestroy()
        {
            if (_map != null)
            {
                foreach (var model in _dynamicStats)
                {
                    model.Destroy();
                }
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

        public IEntityStat Get(StatId id)
        {
            if (TryGetValue(id, out var stat))
            {
                return stat;
            }

            //TODO returning dummy stat with 0/0 values may be better solution in some cases
            throw new Exception($"Stat not found: {id}");
        }

        public void ChangeValue(StatId statId, int delta)
        {
            if (TryGetValue(statId, out var stat) && stat is IWritableEntityStat writable)
            {
                writable.Value += delta;
            }
        }

        private bool TryGetValue(StatId id, out IEntityStat stat)
        {
            LazyInit();
            return _map.TryGetValue(id, out stat);
        }
    }
}