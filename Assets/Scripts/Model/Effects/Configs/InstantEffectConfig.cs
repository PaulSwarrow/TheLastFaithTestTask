using Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model.Effects.Configs
{
    [CreateAssetMenu(menuName = "Game/Instant Effect")]
    public class InstantEffectConfig : EffectConfig
    {
        //TODO different modifications support (multiply, add, etc)
        [SerializeField] private StatId _stat;
        [FormerlySerializedAs("_increase")] [SerializeField] private bool _positive;
        [SerializeField, Range(0,100)] private int _persent;

        public override void Apply(CharacterStats stats, StatusHandlerComponent statusHandler)
        {
            var stat = stats.Get(_stat);
            var delta = Mathf.RoundToInt(stat.MaxValue * _persent/100f);
            stats.ChangeValue(_stat, _positive? delta : -delta);
        }
    }
}