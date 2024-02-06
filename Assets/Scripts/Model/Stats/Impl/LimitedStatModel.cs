using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Model.Stats.Impl
{
    /// <summary>
    /// Provides secondary stat behavior: limit by stat * multiplier
    /// </summary>
    [Serializable]
    public class LimitedStatModel : DynamicStatModel, IWritableEntityStat
    {
        [SerializeField] private int _baseLimit = 100;
        [SerializeField] private int _amplifyStep = 5;
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
            CallUpdate(); //notify limit update. TODO: improve stat interface
        }
        
        public override int MaxValue => _baseLimit + _limit.Value * _amplifyStep;
    }
}