
using Components;
using Model;
using UnityEngine;

namespace Interfaces
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