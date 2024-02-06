using System;
using UnityEngine;

namespace DefaultNamespace.Model
{
    [Serializable]
    public class SimpleEntityStatModel : IWritableEntityStat
    {
        public event CharacterStats.StatChangeDelegate ChangeEvent;
        [SerializeField]
        private StatId _id;
        [SerializeField] protected int _value;

        public StatId Id => _id;
        public string Label => _id.ToString();

        public virtual int Value
        {
            get => _value;
            set
            {
                if(_value == value) return;
                _value = Mathf.Clamp(value, 0, MaxValue);
                var oldValue = _value;
                _value = value;
                ChangeEvent?.Invoke(_id, oldValue, value);
            }
        }
        public virtual int MaxValue => int.MaxValue;
    }
}