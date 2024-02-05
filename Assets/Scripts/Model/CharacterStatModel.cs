using System;
using UnityEngine;

namespace DefaultNamespace.Model
{
    [Serializable]
    public class CharacterStatModel : ICharacterStat
    {
        [SerializeField]
        private StatId _id;
        [SerializeField]
        private int _value;
        [SerializeField]
        private int _maxValue;

        public string Label => _id.ToString();

        public int CurrentValue => _value;
        public int MaxValue => _maxValue;
        public StatId Id => _id;
    }
}