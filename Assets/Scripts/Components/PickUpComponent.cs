using Configs;
using Game.Logic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PickUpComponent : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _icon;
        private EffectConfig _config;

        public void Init(EffectConfig config)
        {
            _config = config;
            UpdateView();
        }

        private void UpdateView()
        {
            _icon.color = _config.IconColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (GameUtils.GetEntity(other, out var entity) && entity.CanPickUp)
            {
                entity.ApplyEffect(_config);
            }
        }
    }
}