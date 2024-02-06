﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Model
{
    [Serializable]
    public abstract class DynamicStatModel : IEntityStat
    {
        public abstract void Init(Dictionary<StatId, IEntityStat> stats);

        public virtual void Destroy() {}
        
        
        public event CharacterStats.StatChangeDelegate ChangeEvent;
        [SerializeField]
        private StatId _id;
        private int _value;

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