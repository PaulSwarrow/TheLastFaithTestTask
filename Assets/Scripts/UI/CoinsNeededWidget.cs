using Components;
using Interfaces;
using Managers;
using Model;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CoinsNeededWidget : MonoBehaviour
    {
        private CharacterStats _target;
        [SerializeField] private TMP_Text _textField;
        private CharacterHud _owner;
        private IEntityStat _coins;
        private IEntityStat _level;


        private void Awake()
        {
            _owner = GetComponentInParent<CharacterHud>();
            //TODO use more secure interface
            _target = _owner.Target.GetComponent<CharacterStats>();
            _coins = _target.Get(StatId.Coins);
            _level = _target.Get(StatId.Level);
        }

        private void OnEnable()
        {
            _coins.ChangeEvent += OnStatUpdate;
            _level.ChangeEvent += OnStatUpdate;
            UpdateView();
        }
        private void OnDisable()
        {
            _coins.ChangeEvent -= OnStatUpdate;
            _level.ChangeEvent -= OnStatUpdate;
        }

        private void OnStatUpdate(StatId id, int oldvalue, int newvalue)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            var cost = GameManager.Instance.GetUpgradeCost(_level.Value);
            var left = cost - _coins.Value;
            if (left > 0)
                _textField.text = $"Till next level: {left} coins.";
            else
                _textField.text = $"New Level!";
        }
    }
}