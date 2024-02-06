using System.Collections.Generic;
using Components;
using Interfaces;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatusTrackerWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _output;
        private StatusHandlerComponent _target;
        private IEnumerable<IEntityStatusInfo> _content;
        private CharacterHud _owner;


        private void Awake()
        {
            _owner = GetComponentInParent<CharacterHud>();
            //TODO use more secure interface
            _target = _owner.Target.GetComponent<StatusHandlerComponent>();
        }

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