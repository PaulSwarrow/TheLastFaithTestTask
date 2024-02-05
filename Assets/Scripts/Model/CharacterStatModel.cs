using System;
using UnityEngine;

namespace DefaultNamespace.Model
{
    [Serializable]
    public class CharacterStatModel : ICharacterStat
    {
        public event CharacterStats.StatChangeDelegate ChangeEvent;
        [SerializeField]
        private StatId _id;
        [SerializeField]
        private int _value;
        [SerializeField]
        private int _maxValue;

        public string Label => _id.ToString();

        public int Value
        {
            get => _value;
            set
            {
                if(_value == value) return;
                var oldValue = _value;
                _value = value;
                ChangeEvent?.Invoke(_id, oldValue, value);
            }
        }

        public int MaxValue => _maxValue;
        public StatId Id => _id;
    }
}