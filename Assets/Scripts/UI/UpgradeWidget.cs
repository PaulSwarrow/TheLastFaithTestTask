using System;
using DefaultNamespace;
using DefaultNamespace.Model;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeWidget : MonoBehaviour
    {

        [SerializeField] private StatId _statId;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Slider _progress;
        [SerializeField] private Button _btn;
        [SerializeField] private TMP_Text _priceField;
        
        private IEntityStat _stat;
        private IEntityStat _level;
        private IEntityStat _coins;
        private CharacterStats _target;
        private int _cost;

        private void Awake()
        {
            var owner = GetComponentInParent<CharacterHud>();
            _target = owner.Target.GetComponent<CharacterStats>();
            _stat = _target.Get(_statId);
            _level = _target.Get(StatId.Level);
            _coins = _target.Get(StatId.Coins);
            _btn.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            _label.text = $"{_stat.Label}: {_stat.Value}/{_stat.MaxValue}";
            _progress.value = _stat.Value;
            _progress.maxValue = _stat.MaxValue;
            _cost = GameManager.Instance.GetUpgradeCost(_level.Value);
            _priceField.text = _cost.ToString();
        }

        private void OnClick()
        {
            if (_stat.Value < _stat.MaxValue && _cost <= _coins.Value)
            {
                _target.ChangeValue(_statId, 1);
                _target.ChangeValue(StatId.Coins, -_cost);
                _target.ChangeValue(StatId.Level, 1);
                UpdateState();
            }
        }
    }
}