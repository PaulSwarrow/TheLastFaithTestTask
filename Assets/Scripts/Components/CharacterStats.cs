using System;
using System.Collections.Generic;
using Interfaces;
using Model;
using Model.Stats;
using Model.Stats.Impl;
using UnityEngine;

namespace Components
{
    /// <summary>
    /// Stores character's stats
    /// Manages stats modifications
    /// </summary>
    public class CharacterStats : MonoBehaviour
    {
        public delegate void StatChangeDelegate(StatId id, int oldValue, int newValue);

        [SerializeField] private SimpleEntityStatModel[] _stats;

        //TODO: more flexible configuration(as a list of different stats, custom inspector needed)
        [SerializeField] private LimitedStatModel _health;
        [SerializeField] private FunctionStatModel _speed;
        [SerializeField] private FunctionStatModel _damage;


        private Dictionary<StatId, IEntityStat> _map;
        private Dictionary<StatId, EntityStatWrapper> _wrappers;
        private List<IDestroyable> _destroyables;

        private void LazyInit()
        {
            if (_map != null) return;
            _map = new();
            _wrappers = new();
            _destroyables = new();
            foreach (var model in _stats)
            {
                _map.Add(model.Id, model);
            }

            //TODO: generate this list dynamically
            var dynamic = new DynamicStatModel[] { _health, _speed, _damage };
            foreach (var model in dynamic)
            {
                //assumption: dynamic stats can not depend on each other!
                model.Init(_map);
                _map.Add(model.Id, model);
                _destroyables.Add(model);
            }

            foreach (var model in _map.Values)
            {
                var wrapper = new EntityStatWrapper(model);
                _wrappers.Add(model.Id, wrapper);
                _destroyables.Add(wrapper);
                
            }
        }

        private void OnDestroy()
        {
            if (_map != null)
            {
                foreach (var model in _destroyables)
                {
                    model.Destroy();
                }
            }
        }


        public void Subscribe(StatId id, StatChangeDelegate handler)
        {
            if (TryGetValue<IEntityStat>(id, false, out var stat))
            {
                stat.ChangeEvent += handler;
            }
        }

        public void Unsubscribe(StatId id, StatChangeDelegate handler)
        {
            if (TryGetValue<IEntityStat>(id,false, out var stat))
            {
                stat.ChangeEvent -= handler;
            }
        }

        public IEntityStat Get(StatId id)
        {
            if (TryGetValue<IEntityStat>(id, true, out var stat))
            {
                return stat;
            }

            //TODO returning dummy stat with 0/0 values may be better solution in some cases
            throw new Exception($"Stat not found: {id}");
        }

        public void AddModifier(StatId id, IStatModifier modifier)
        {
            if(TryGetValue<EntityStatWrapper>(id, true, out var wrapper))
            {
                wrapper.AddModifier(modifier);
            }
        }

        public void RemoveModifier(StatId id, IStatModifier modifier)
        {
            if(TryGetValue<EntityStatWrapper>(id, true, out var wrapper))
            {
                wrapper.RemoveModifier(modifier);
            }
        }

        public IEntityStat GetPureStat(StatId id)
        {
            if (TryGetValue<IEntityStat>(id, true, out var stat))
            {
                return stat;
            }

            //TODO returning dummy stat with 0/0 values may be better solution in some cases
            throw new Exception($"Stat not found: {id}");
            
        }

        public void ChangeValue(StatId statId, int delta)
        {
            //Note: only base stats change is supported. Stat Modifiers can not be affected externally
            //To implement "extended health" effect there should be complex effect: amplify Vitality + heal up to new MaxValue
            if (TryGetValue<IWritableEntityStat>(statId, false, out var stat))
            {
                stat.Value += delta;
            }
        }

        private bool TryGetValue<T>(StatId id, bool wrappers, out T stat) where T: IEntityStat
        {
            LazyInit();
            if (wrappers && _wrappers.TryGetValue(id, out var wrapper) && wrapper is T wrapperT)
            {
                stat = wrapperT;
                return true;
            }

            if (_map.TryGetValue(id, out var model) && model is T modelT)
            {
                stat = modelT;
                return true;
            }

            stat = default;
            return false;
        }
    }
}