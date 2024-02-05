using System;
using System.Collections.Generic;
using DefaultNamespace.Model;
using UnityEngine;

namespace DefaultNamespace
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] private CharacterStatModel[] _stats;

        private readonly Dictionary<StatId, CharacterStatModel> _map = new();

        private void Awake()
        {
            foreach (var model in _stats)
            {
                _map.Add(model.Id, model);
            }
        }

        public ICharacterStat Get(StatId id)
        {
            if (_map.TryGetValue(id, out var stat))
            {
                return stat;
            }

            //TODO returning dummy stat with 0/0 values may be better solution in some cases
            throw new Exception($"Stat not found: {id}");
        }
    }
}