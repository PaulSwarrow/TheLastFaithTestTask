using Components;
using Interfaces;
using UnityEngine;

namespace Model.Effects.Configs
{
    public abstract class EffectConfig : ScriptableObject, IEntityEffect
    {
        [SerializeField] public Color IconColor = Color.white;

        //TODO some effect object creation might be required instead
        public abstract void Apply(CharacterStats stats, StatusHandlerComponent statusHandler);
    }
}