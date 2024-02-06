using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Model.Impl
{
    [Serializable]
    public class FunctionStatModel : DynamicStatModel
    {
        [SerializeField] private int _baseValue;
        [SerializeField] private StatId _improvedBy;
        [SerializeField] private int _improvementRate;

        private IEntityStat _improveStat;
        
        public override void Init(Dictionary<StatId, IEntityStat> stats)
        {
            _improveStat = stats[_improvedBy];
            UpdateValue();
            _improveStat.ChangeEvent += OnDependencyUpdate;
        }

        public override void Destroy()
        {
            _improveStat.ChangeEvent -= OnDependencyUpdate;
            base.Destroy();
        }

        public override int MaxValue => int.MaxValue;

        private void OnDependencyUpdate(StatId id, int oldvalue, int newvalue)
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            Value = _baseValue + (_improveStat.Value * _improvementRate); 
        }
    }
}