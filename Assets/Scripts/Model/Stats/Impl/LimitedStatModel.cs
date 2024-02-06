using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Model.Impl
{
    [Serializable]
    public class LimitedStatModel : DynamicStatModel, IWritableEntityStat
    {
        [SerializeField] private StatId _limitStat;
        private IEntityStat _limit;
        

        public override void Init(Dictionary<StatId, IEntityStat> stats)
        {
            _limit = stats[_limitStat];
            Value = _limit.Value;
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
        
        public override int MaxValue => _limit.Value;
    }
}