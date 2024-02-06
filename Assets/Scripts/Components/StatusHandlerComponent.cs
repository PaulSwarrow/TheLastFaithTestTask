using System;
using System.Collections.Generic;
using DefaultNamespace.Model;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterStats))]
    public class StatusHandlerComponent : MonoBehaviour
    {
        private List<IEntityStatus> _statuses = new ();
        private CharacterStats _stats;

        private void Awake()
        {
            _stats = GetComponent<CharacterStats>();
        }

        public void AddStatus(IEntityStatus status)
        {
            status.Init(_stats);
            _statuses.Add(status);
        }

        private void Update()
        {
            for (int i = _statuses.Count - 1; i >= 0; i--)
            {
                var status = _statuses[i];
                status.Update(Time.deltaTime);
                if (status.IsFinished)
                {
                    status.Dispose();
                    _statuses.RemoveAt(i);
                }
            }
            
        }
    }
}