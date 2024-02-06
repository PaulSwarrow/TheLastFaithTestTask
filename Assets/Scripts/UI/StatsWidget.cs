using System;
using DefaultNamespace.Model;
using UnityEngine;

namespace UI
{
    public class StatsWidget : MonoBehaviour
    {
        private CharacterEntity _target;

        [Serializable]
        private struct Entry
        {
            public StatId id;
            public StatValueWidget Widget;
        }
        
        [SerializeField] private Entry[] _content;
        private CharacterHud _owner;

        private void Awake()
        {
            _owner = GetComponentInParent<CharacterHud>();
            _target = _owner.Target.GetComponent<CharacterEntity>();
        }
        
        private void Start()
        {
            foreach (var entry in _content)
            {
                entry.Widget.SetStat(_target.GetStat(entry.id));
            }
        }
    }
}