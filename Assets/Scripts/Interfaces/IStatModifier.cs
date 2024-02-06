using UnityEngine;

namespace DefaultNamespace.Model
{
    public interface IStatModifier
    {
        int GetModificator(int baseValue);

        Color Color { get; }
    }
}