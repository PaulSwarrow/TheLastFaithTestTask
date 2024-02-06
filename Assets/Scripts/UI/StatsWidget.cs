using System;
using DefaultNamespace.Model;
using UnityEngine;

namespace UI
{
    public class StatsWidget : MonoBehaviour
    {
        //TODO target provider
        [SerializeField] private CharacterEntity _target;

        [Serializable]
        private struct Entry
        {
            public StatId id;
            public StatValueWidget Widget;
        }
        
        [SerializeField] private Entry[] _content;
        private void Start()
        {
            foreach (var entry in _content)
            {
                entry.Widget.SetStat(_target.GetStat(entry.id));
            }
        }
    }
}