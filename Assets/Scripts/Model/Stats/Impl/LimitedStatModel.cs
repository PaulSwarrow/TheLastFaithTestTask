using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Model.Impl
{
    [Serializable]
    public class LimitedStatModel : DynamicStatModel, IWritableEntityStat
    {
        [SerializeField] private int _baseLimit = 100;
        [SerializeField] private StatId _limitAmplifier;
        private IEntityStat _limit;
        

        public override void Init(Dictionary<StatId, IEntityStat> stats)
        {
            _limit = stats[_limitAmplifier];
            Value = MaxValue;
            _limit.ChangeEvent += OnMaxValueChange;
        }
        public override void Destroy()
        {
            _limit.ChangeEvent -= OnMaxValueChange;
            base.Destroy();
        }
        
        private void OnMaxValueChange(StatId id, int oldvalue, int newvalue)
        {
            Value = Mathf.Min(Value, MaxValue);
        }
        
        public override int MaxValue => _baseLimit + _limit.Value;
    }
}