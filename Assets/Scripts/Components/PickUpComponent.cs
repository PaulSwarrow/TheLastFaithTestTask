using Model.Effects.Configs;
using UnityEngine;

namespace Components
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
            //TODO pools!
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}