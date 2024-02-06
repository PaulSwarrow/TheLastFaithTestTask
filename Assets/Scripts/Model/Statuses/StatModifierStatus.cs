using Components;
using Interfaces;
using UnityEngine;

namespace Model.Statuses
{
    public class StatModifierStatus : IEntityStatus, IStatModifier
    {
        private readonly StatId _id;
        private readonly int _lifespan;
        private readonly int _modifier;

        private float _lifeTime;
        private CharacterStats _target;

        public StatModifierStatus(StatId id, int lifespan, int modifier, string label, Color color)
        {
            _id = id;
            _lifespan = lifespan;
            _modifier = modifier;
            Label = label;
            Color = color;
        }

        public void Init(CharacterStats stats)
        {
            _target = stats;
            _target.AddModifier(_id, this);
        }

        public void Update(float deltaTime)
        {
            _lifeTime += deltaTime;
        }

        public void Dispose()
        {
            _target.RemoveModifier(_id, this);
        }

        public bool IsFinished => TimeLeft <= 0;
        public bool HasConflict(IEntityStatus other)
        {
            return other is StatModifierStatus statModifier && statModifier._id == _id;
        }

        public int Value => _modifier;
        public int GetModificator(int baseValue)
        {
            return Mathf.RoundToInt(baseValue * _modifier / 100f);
        }

        public Color Color { get; }

        public string Label { get; }
        public float TimeLeft => _lifespan - _lifeTime;
    }
}