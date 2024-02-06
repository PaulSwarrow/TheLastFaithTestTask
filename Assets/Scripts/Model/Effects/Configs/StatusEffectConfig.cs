using Components;
using Model.Statuses;
using UnityEngine;

namespace Model.Effects.Configs
{
    [CreateAssetMenu(menuName = "Game/Status Effect")]
    public class StatusEffectConfig : EffectConfig
    {
        [SerializeField]
        private StatId _id;
        [SerializeField]
        private int _lifespan;
        [SerializeField, Range(0, 100)]
        private int _modifier;

        [SerializeField] private string _label;

        public override void Apply(CharacterStats stats, StatusHandlerComponent statusHandler)
        {
            var status = new StatModifierStatus(_id, _lifespan, _modifier, _label, IconColor);
            statusHandler.AddStatus(status);
        }
    }
}