using System.Collections.Generic;
using System.Linq;
using Components;
using Interfaces;
using UnityEngine;

namespace Model.Stats
{
    public class EntityStatWrapper : IEntityStat, IDestroyable
    {
        public event CharacterStats.StatChangeDelegate ChangeEvent;
        
        private readonly IEntityStat _origin;
        private readonly HashSet<IStatModifier> _modifiers = new ();

        public EntityStatWrapper(IEntityStat origin)
        {
            _origin = origin;
            _origin.ChangeEvent += OnOriginUpdate;
        }

        public void Destroy()
        {
            _origin.ChangeEvent -= OnOriginUpdate;
            
        }

        public void AddModifier(IStatModifier modifier)
        {
            var value = Value;
            _modifiers.Add(modifier);
            if (value != Value) ChangeEvent?.Invoke(Id, value, Value);
        }

        public void RemoveModifier(IStatModifier modifier)
        {
            var value = Value;
            _modifiers.Remove(modifier);
            if (value != Value) ChangeEvent?.Invoke(Id, value, Value);
            
        }

        public string Label => _origin.Label;

        public int Value => _origin.Value + _modifiers.Sum(m => m.GetModificator(_origin.Value));

        public StatId Id => _origin.Id;
        public int MaxValue => _origin.MaxValue;
        public Color BoostInfo => _modifiers.Count == 0 ? _origin.BoostInfo : _modifiers.First().Color;

        private void OnOriginUpdate(StatId id, int oldvalue, int newvalue)
        {
            //TODO: possible extra calculations. better to remove eventually
            oldvalue = _origin.Value + _modifiers.Sum(m => m.GetModificator(_origin.Value));
            ChangeEvent?.Invoke(Id, oldvalue, Value);
        }
    }
}