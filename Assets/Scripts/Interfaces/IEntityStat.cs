
using UnityEngine;

namespace DefaultNamespace.Model
{
    public interface IEntityStat
    {
        public event CharacterStats.StatChangeDelegate ChangeEvent;
        string Label { get; }
        int Value { get; }
        StatId Id { get; }
        int MaxValue { get; }
        Color BoostInfo { get; }
    }
}