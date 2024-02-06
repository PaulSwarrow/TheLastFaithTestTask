using DefaultNamespace.Model;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatValueWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;
        private IEntityStat _stat;

        public void SetStat(IEntityStat stat)
        {
            _stat = stat;
            OnChange(_stat.Id, stat.Value, stat.Value);
            _stat.ChangeEvent += OnChange;

        }

        public void ClearStat()
        {
            
        }

        private void OnChange(StatId id, int oldvalue, int newvalue)
        {
            _textField.text = $"{_stat.Label}: {newvalue}/{_stat.MaxValue}";
        }
        
        
    }
}