using DefaultNamespace;
using DefaultNamespace.Model;
using DefaultNamespace.Model.Statuses;
using UnityEngine;

namespace Configs
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

        public override void Apply(CharacterStats stats, StatusHandlerComponent statusHandler)
        {
            var status = new StatModifierStatus(_id, _lifespan, _modifier);
            statusHandler.AddStatus(status);
        }
    }
}