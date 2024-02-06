
using Components;
using Model;
using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Stat info
    /// </summary>
    public interface IEntityStat
    {
        /// <summary>
        /// Dispatched when value or maxValue changes
        /// </summary>
        public event CharacterStats.StatChangeDelegate ChangeEvent;
        /// <summary>
        /// Name for UI purposes
        /// </summary>
        string Label { get; }
        int Value { get; }
        
        StatId Id { get; }
        
        int MaxValue { get; }
        
        /// <summary>
        /// Additional information about current stat state.
        /// TODO: make a struct with different fields when required.
        /// </summary>
        Color BoostInfo { get; }
    }
}