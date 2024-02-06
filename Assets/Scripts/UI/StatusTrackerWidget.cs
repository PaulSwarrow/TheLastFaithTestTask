using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Model;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatusTrackerWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _output;
        [SerializeField] private StatusHandlerComponent _target;

        private IEnumerable<IEntityStatusInfo> _content;

        private void OnEnable()
        {
            OnTargetUpdate();
            _target.ListUpdateEvent += OnTargetUpdate;
        }

        private void OnDisable()
        {
            _target.ListUpdateEvent -= OnTargetUpdate;
        }

        private void OnTargetUpdate()
        {
            _content = _target.GetCurrentStatuses();
        }

        private void Update()
        {
            var text = "";
            foreach (var info in _content)
            {
                text += $"{info.Label}: {info.TimeLeft:F}s";
            }

            _output.text = text;
        }
    }
}