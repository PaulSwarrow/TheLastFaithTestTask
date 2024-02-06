using Components;
using Interfaces;
using Model;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatValueWidget : MonoBehaviour
    {
        [SerializeField] private StatId _statId;
        [SerializeField] private TMP_Text _textField;
        private IEntityStat _stat;
        private CharacterHud _owner;

        private void Awake()
        {
            _owner = GetComponentInParent<CharacterHud>();
            _stat = _owner.Target.GetComponent<CharacterStats>().Get(_statId);
        }

        private void OnEnable()
        {
            OnChange(_stat.Id, _stat.Value, _stat.Value);
            _stat.ChangeEvent += OnChange;
        }

        public void OnDisable()
        {
            _stat.ChangeEvent -= OnChange;
            _textField.text = "";
        }

        private void OnChange(StatId id, int oldvalue, int newvalue)
        {
            _textField.color = _stat.BoostInfo;
            if (_stat.MaxValue != int.MaxValue)
                _textField.text = $"{_stat.Label}: {newvalue}/{_stat.MaxValue}";
            else
                _textField.text = $"{_stat.Label}: {newvalue}";
        }
    }
}