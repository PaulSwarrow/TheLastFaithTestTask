using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Managers;
using UnityEngine;

namespace Components
{
    /// <summary>
    /// Manages statuses on the character
    /// </summary>
    [RequireComponent(typeof(CharacterStats))]
    public class StatusHandlerComponent : MonoBehaviour
    {
        public event Action ListUpdateEvent;
        
        private List<IEntityStatus> _statuses = new ();
        private CharacterStats _stats;

        private void Awake()
        {
            _stats = GetComponent<CharacterStats>();
        }

        public void AddStatus(IEntityStatus status)
        {
            status.Init(_stats);
            var conflicts = _statuses.Where(s => s.HasConflict(status)).ToArray();
            foreach (var conflict in conflicts)
            {
                conflict.Dispose();
                _statuses.Remove(conflict);
            }
            _statuses.Add(status);
            ListUpdateEvent?.Invoke();
        }

        private void Update()
        {
            var changed = false;
            for (int i = _statuses.Count - 1; i >= 0; i--)
            {
                var status = _statuses[i];
                status.Update(GameManager.Instance.GameDeltaTime);
                if (status.IsFinished)
                {
                    status.Dispose();
                    _statuses.RemoveAt(i);
                    changed = true;
                }
            }
            if(changed) ListUpdateEvent?.Invoke();
            
        }

        public IEnumerable<IEntityStatusInfo> GetCurrentStatuses() => _statuses;
    }
}